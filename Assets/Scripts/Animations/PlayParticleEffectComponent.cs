using System;
using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(ParticleSystem))]
    public class PlayParticleEffectComponent : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void PlayParticleEffect()
        {
            _particleSystem.Play();
        }
    }
}