using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player { 
    public abstract class Tank : Base
    {
        protected bool shield;
        protected int shieldLife;
        protected int shieldRecovery;

        public Tank(int hp, int speed, int jump, bool shield, int shieldLife, int shieldRecovery) : base (hp, speed, jump) //heritage
        {
            this.shield = shield;
            this.shieldLife = shieldLife;
            this.shieldRecovery = shieldRecovery;
        }
        #region get
        public bool getShield
        {
            get { return shield; }
        }
        public int getShieldLife
        {
            get { return shieldLife; }
        }
        public int getShieldRecovery
        {
            get { return shieldRecovery; }
        }
        #endregion get
    }
}