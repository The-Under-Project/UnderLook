using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public abstract class Base : MonoBehaviour
    {
        protected int hp;
        protected int speed;
        protected int jump;

        public Base(int hp, int speed, int jump)
        {
            this.hp = hp;
            this.speed = speed;
            this.jump = jump;
        }
        #region get
        public int Hp
        {
            get { return hp; }
        }
        public int Speed
        {
            get { return speed; }
        }
        public int Jump
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
