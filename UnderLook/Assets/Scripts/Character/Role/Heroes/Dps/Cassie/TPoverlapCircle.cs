using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPoverlapCircle : MonoBehaviour
{
    public int nbCol;
    public Collider[][] colliders = new Collider[5][];
    public bool[] whoTrue = new bool[5];  //avoid glitch when tp and fall

    public Collider[][] top = new Collider[10][];

    public bool colAbove;
    void FixedUpdate()
    {
        CountCol();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position + new Vector3(1, 0), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(-1, 0), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0, 1), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0, -1), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0), 0.1f);

        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position + new Vector3(1, 1.5f), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(-1, 1.5f), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1.5f, 1), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1.5f, -1), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1.5f), 0.1f);

        Gizmos.DrawWireSphere(transform.position + new Vector3(1, 0.4f), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(-1, 0.4f), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0.4f, 1), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0.4f, -1), 0.1f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0.4f), 0.1f);


    }
    void CountCol()
    {

        colliders[0] = Physics.OverlapSphere(this.transform.position + new Vector3(1, 0), 0.1f);
        colliders[1] = Physics.OverlapSphere(this.transform.position + new Vector3(-1, 0), 0.1f);
        colliders[2] = Physics.OverlapSphere(this.transform.position + new Vector3(0, 0), 0.1f);
        colliders[3] = Physics.OverlapSphere(this.transform.position + new Vector3(0, 0, 1), 0.1f);
        colliders[4] = Physics.OverlapSphere(this.transform.position + new Vector3(0, 0, -1), 0.1f);

        top[0] = Physics.OverlapSphere(this.transform.position + new Vector3(1, 1.5f, 0), 0.1f);
        top[1] = Physics.OverlapSphere(this.transform.position + new Vector3(-1, 1.5f, 0), 0.1f);
        top[2] = Physics.OverlapSphere(this.transform.position + new Vector3(0, 1.5f, 0), 0.1f);
        top[3] = Physics.OverlapSphere(this.transform.position + new Vector3(0, 1.5f, 1), 0.1f);
        top[4] = Physics.OverlapSphere(this.transform.position + new Vector3(0, 1.5f, -1), 0.1f);

        top[5] = Physics.OverlapSphere(this.transform.position + new Vector3(1, 0.4f, 0), 0.1f);
        top[6] = Physics.OverlapSphere(this.transform.position + new Vector3(-1, 0.4f, 0), 0.1f);
        top[7] = Physics.OverlapSphere(this.transform.position + new Vector3(0, 0.4f, 0), 0.1f);
        top[8] = Physics.OverlapSphere(this.transform.position + new Vector3(0, 0.4f, 1), 0.1f);
        top[9] = Physics.OverlapSphere(this.transform.position + new Vector3(0, 0.4f, -1), 0.1f);

        int nb = 0;
        int j = 0;
        foreach (Collider[] col in colliders)
        {
            bool index = false;
            int i = 0;
            while (i < col.Length)
            {
                if (col[i].tag == "Floor")
                {
                    nb++;
                    index = true;
                    break;
                }
                i++;
            }
            whoTrue[j] = index;
            j++;
        }


        nbCol = nb;

        bool t = false;
        bool done = false;
        foreach (Collider[] col in top)
        {
            if (!done)
            {
                int i = 0;
                while (i < col.Length)
                {
                    if (col[i].tag == "Floor")
                    {
                        t = true;
                        done = true;
                    }
                    i++;
                }
            }
        }
        colAbove = t; //si colAbove alor tu peux pas tp
    }
}