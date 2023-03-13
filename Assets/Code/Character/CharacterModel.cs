﻿using Code.Assistance;
using Code.Config;
using Code.Hit;
using UnityEngine;

namespace Code.Character
{
    public class CharacterModel
    {
        public GameObject Character { get; }
        public Transform Transform { get; }
        public Rigidbody Rigidbody { get; }
        public HitHandler HitHandler { get; }
        public Animator Animator { get; }
        public Renderer Renderer { get; }
        public AudioSource AudioSource { get; }
        public int CharacterID { get; }
        public int ColliderID { get; }

        public CharacterModel(CharacterConfig config)
        {
            var character = Object.Instantiate(config.FemalePrefab).gameObject;
            Character = character;
            Transform = character.transform;
            Rigidbody = character.GetOrAddComponent<Rigidbody>();
            Animator = character.GetOrAddComponent<Animator>();
            Renderer = character.GetComponent<Renderer>();
            HitHandler = character.GetOrAddComponent<HitHandler>();
            var collider = character.GetOrAddComponent<Collider>();
            ColliderID = collider.GetInstanceID();
            CharacterID = character.GetInstanceID();
            AudioSource = character.GetOrAddComponent<AudioSource>();
            AudioSource.volume = 0.2f;
        }
    }
}
