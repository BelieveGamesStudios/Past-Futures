using System;
using UnityEngine;

namespace Believe.Games.Studios
{
    public class ParticleManager : MonoBehaviour
    {
        [Header("Particle Effect Setup")]
        public SurfaceTypes[] surfaceTypes;
        public ParticleSystem zombieBlood;
        
    }
    [Serializable]
    public class SurfaceTypes
    {

        public string SurfaceType;
        public GameObject surfaceEffect;
    }
}
