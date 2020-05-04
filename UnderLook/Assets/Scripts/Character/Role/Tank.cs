using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player { 
    public abstract class Tank : Base
    {
        [Header("Tank")]
        public bool shield;
        public float shieldLifeMax;
        public float shieldLife;
        public float shieldRecovery;


    }
}