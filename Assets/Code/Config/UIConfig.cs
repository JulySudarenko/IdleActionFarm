using UnityEngine;

namespace Code.Config
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "Configs/UIConfig", order = 0)]
    public sealed class UIConfig : ScriptableObject
    {
        [Header("Control settings")] 
        public Transform Joystick;
        public Transform TouchCircle;
        [SerializeField] private int _touchCircleMaxPosition = 100;
        
        [Header("Game sprites")] 
        public Transform _coins;
        public Transform _bag;
        
        private Canvas _canvas;

        public Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                {
                    _canvas = FindObjectOfType<Canvas>();
                }
                return _canvas;
            }
        }

        public int TouchCircleMaxPosition => _touchCircleMaxPosition;
    }
}
