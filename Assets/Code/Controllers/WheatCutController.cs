using System;
using System.Collections.Generic;
using Code.Config;
using Code.Farm;
using Code.Instruments;
using Code.Interfaces;
using Code.Timer;
using UnityEngine;

namespace Code.Controllers
{
    internal class WheatCutController : IInitialization, IDisposable
    {
        private static readonly int BladeHeight = Shader.PropertyToID("_BladeHeight");
        private readonly Renderer[] _whealRenderers;
        private readonly Collider[] _whealColliders;
        private readonly int[] _wheatIds;
        private readonly float[] _grownList;
        private readonly Queue<int> _queueIsReady = new Queue<int>();
        private readonly ScytheController _scytheController;
        private readonly FarmConfig _config;
        private readonly float _deltaGrow;
        private ITimeRemaining _isReadyTimer;
        private ITimeRemaining _isGrownTimer;

        public WheatCutController(FarmModel farmModel, ScytheController scytheController, FarmConfig config)
        {
            _wheatIds = farmModel.WheatCollidersIDs;
            _scytheController = scytheController;
            _config = config;
            _whealRenderers = farmModel.WheatRenderers;
            _whealColliders = farmModel.WheatTcColliders;
            _grownList = new float[_whealRenderers.Length];
            _deltaGrow = (_config.WheatMaxHeight - _config.WheatMinHeight) / _config.WheatGrowthStages;
        }

        public void Initialize()
        {
            _scytheController.CutWheat += OnScytheHit;
            var time = _config.WheatGrowthTime / _config.WheatGrowthStages;
            Debug.Log(time);
            _isGrownTimer = new TimeRemaining(GrownWheat, time, true);
            _isGrownTimer.AddTimeRemaining();
            for (int i = 0; i < _whealRenderers.Length; i++)
            {
                _grownList[i] = _config.WheatMaxHeight;
            }
        }

        private void OnScytheHit(int id)
        {
            for (int i = 0; i < _wheatIds.Length; i++)
            {
                if (_wheatIds[i] == id)
                {
                    _queueIsReady.Enqueue(i);
                    _isReadyTimer = new TimeRemaining(WheatIsReady, _config.WheatGrowthTime);
                    _isReadyTimer.AddTimeRemaining();
                    _whealRenderers[i].material.SetFloat(BladeHeight, _config.WheatMinHeight);
                    _whealColliders[i].enabled = false;
                    _grownList[i] = _config.WheatMinHeight;
                }
            }
        }

        private void GrownWheat()
        {
            for (int i = 0; i < _grownList.Length; i++)
            {
                if (_grownList[i] < _config.WheatMaxHeight)
                {
                    _grownList[i] += _deltaGrow;
                    _whealRenderers[i].material.SetFloat(BladeHeight, _grownList[i]);
                }
            }
        }

        private void WheatIsReady()
        {
            var index = _queueIsReady.Dequeue();

            _isReadyTimer.RemoveTimeRemaining();
            _whealColliders[index].enabled = true;
        }

        public void Dispose()
        {
            _scytheController.CutWheat -= OnScytheHit;
            _isGrownTimer.RemoveTimeRemaining();
        }
    }
}
