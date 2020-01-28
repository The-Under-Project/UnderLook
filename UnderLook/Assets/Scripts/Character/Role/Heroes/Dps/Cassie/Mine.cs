using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mine : MonoBehaviour
{
    public Rigidbody rb;
    public float percentage = 0;
    public float time;

    public int range;
    public GameObject[] playerWallHack;
    public GameObject[] oldWallHack;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void FixedUpdate()
    {
        int i = 0;
        if (percentage > 0.01f && percentage < 100)
        {
            //pour enlever ceux qui sont passées dans la marque mais qui n'y sont plus faire un deuxième arrey "old" si tu es dans old mais pas dans nouveaux, on enlève le wall
            playerWallHack = new GameObject[8]; //make new array, 8 players max
            Collider[] coolider = Physics.OverlapSphere(this.transform.position, range); //all collider in sphere
            foreach (var col in coolider)
            {
                if (col.tag == "Player")
                {
                    bool playerIn = false;
                    foreach (var player in playerWallHack)
                    {
                        if (player != null && col.name == player.name)
                        {
                            playerIn = true; //means player is already in, no need to add it again
                            break;
                        }
                    }

                    if (!playerIn)
                    {
                        playerWallHack[i] = col.gameObject; //if he's not in already
                        i++;
                    }
                }
            }
            int test1 = 0;
            int test2 = 0;
            for (int r = 0; r < oldWallHack.Length; r++)
            {
                if (oldWallHack[r] == null)
                {
                    test1++;
                }
            }
            for (int r = 0; r < playerWallHack.Length; r++)
            {
                if (playerWallHack[r] == null)
                {
                    test2++;
                }
            }

            if (test1 != test2) //if someone leave the area
            {
                bool isIn = false;
                GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
                foreach (var player in allPlayers)
                {
                    foreach (var player2 in playerWallHack)
                    {
                        if (player == player2)
                        {
                            isIn = true; //but for the object still in, we don't change the shader
                        }

                    }
                    if(!isIn)
                        player.GetComponent<Renderer>().material = player.GetComponent<MaterialsApply>().basic; //make sure player not in area don't stay in it
                }
            }
            else
            {
                foreach (var player in playerWallHack)
                {
                    bool playerNotNull = (player != null);

                    if (playerNotNull)
                    {
                        player.GetComponent<Renderer>().material = player.GetComponent<MaterialsApply>().wallHack; //change material
                    }

                }
            }

            oldWallHack = new GameObject[8];

            for (int a = 0; a < 8; a++)
            {
                oldWallHack[a] = playerWallHack[a];
            }
            //oldWallHack = playerWallHack; //actu




        }
        else if (percentage == 100)
        {
            // avant de destroy il faut changer le shader
            foreach (var player in playerWallHack)
            {
                if (player != null)
                {
                    player.GetComponent<Renderer>().material = player.GetComponent<MaterialsApply>().basic;
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            rb.isKinematic = true;
            //Destroy(rb);
            DOTween.Play(Des());
        }
    }
    Sequence Des()
    {
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => percentage, x => percentage = x, 100, time));
        return s;
    }

}
