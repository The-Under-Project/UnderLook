using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Human
    {
        protected int hp;
        protected int speed;
        protected int jump;

        public Human(int hp, int speed, int jump)
        {
            this.hp = hp;
            this.speed = speed;
            this.jump = jump;
        }
        #region get
        public int getHp
        {
            get { return hp; }
        }
        public int getSpeed
        {
            get { return speed; }
        }
        public int getJump
        {
            get { return jump; }
        }
        #endregion get
        #region set
        public int setHp
        {
            set { hp = value; }
        }
        #endregion set
        #region DEBUG
        public virtual void Life()
        {
            Debug.Log(("current life {0}", hp));
        }
        #endregion DEBUG

    }
}
