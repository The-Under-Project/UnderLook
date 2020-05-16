using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSetWeapon : MonoBehaviour
{
    public GameObject Weapon;
    public bool changeweapondUp;
    public bool changeweapondDown;
    public PhotonView pv;

    public GameObject normal;
    public Vector3 up;
    public Vector3 down;
    public Vector3 idle;

    public Animator anim;

    public float offset = 0.5f;

    private void Start()
    {

        anim = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
        if (!pv.isMine)
        {
            normal.transform.position = new Vector3(normal.transform.position.x, normal.transform.position.y - 1, normal.transform.position.z);
        }
        Weapon.transform.position = normal.transform.position;
    }
    // x = 0 y = -1 -> monte
    // x = -1 y = 0 -> monte
    // x = 1  y = 0 -> monte



    void Update()
    {
        if (!pv.isMine)
        {
            idle = normal.transform.position;
            idle.y += 0.35f;


            up = normal.transform.position;
            up.y += 0.5f;

            down = normal.transform.position;
            down.y += 0.15f;


            float x = anim.GetFloat("VelX");
            float y = anim.GetFloat("VelY");
            if ((x <= -0.5 && y == 0) || (x >= 0.5 && y == 0) || (x == 0 && y <= -0.5))
            {
                Weapon.transform.position = up;
            }
            else if (x == 0 && y >= 0.5)
            {
                Weapon.transform.position = down;
            }
            else
            {
                Weapon.transform.position = idle;
            }
        }
    }
}
