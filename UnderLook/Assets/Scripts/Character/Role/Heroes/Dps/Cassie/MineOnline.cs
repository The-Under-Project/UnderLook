using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MineOnline : MonoBehaviour
{
    [Header("Online")]
    [Tooltip("Deactivate if you want it to be online")] public bool DEBUG_LOCAL;
    private bool start = true;
    private GameObject owner;
    public Vector3 direction;

    [Header("Global")]

    public string enemieColor;
    public string colorBub;

    [HideInInspector] public Rigidbody rb;
    public float percentage = 0;
    public float time;

    public int range;
    public GameObject[] playerWallHack;
    public GameObject[] oldWallHack;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Awake()
    {
        DOTween.Play(Des());

        GameObject[] mineInit = GameObject.FindGameObjectsWithTag("mine");
        foreach (var mine in mineInit)
        {
            if (mine.GetComponent<Mine>().canYouGetMyColor)
            {
                enemieColor = mine.GetComponent<Mine>().enemieColor;

                Destroy(mine);
                break;
            }
        }
        GameObject[] Cassies = GameObject.FindGameObjectsWithTag("Player");
        foreach (var cassie in Cassies)
        {
            if (cassie.GetComponent<Player.Cassie>() != null)
            {
                if (cassie.GetComponent<Player.Cassie>().enabled == true && cassie.GetComponent<Player.Cassie>().hasInstantiateAMine)
                {
                    owner = cassie;
                    cassie.GetComponent<Player.Cassie>().hasInstantiateAMine = false;
                    break;
                }
            }
        }
        if(enemieColor == "Blue")
            colorBub = "BlueBubble";
        else if (enemieColor == "Red")
            colorBub = "RedBubble";
        else 
            Debug.Log(enemieColor);
        PhotonNetwork.Instantiate(colorBub, this.transform.position, Quaternion.identity, 0);
    }

    private void FixedUpdate()
    {
        bool rightGuy = false;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            if (player.GetComponent<Player.Cassie>() != null)
            {
                if (player.GetComponent<Player.Cassie>().enabled == true && player == owner)
                {
                    rightGuy = true;
                    break;
                }
            }
        }
        if (rightGuy)
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
                        if (!isIn)
                            player.GetComponent<Renderer>().material = player.GetComponent<MaterialsApply>().basic; //make sure player not in area don't stay in it
                    }
                }
                else
                {
                    foreach (var player in playerWallHack)
                    {
                        bool playerNotNull = (player != null);

                        if (playerNotNull && enemieColor != player.GetComponent<TeamColor>().teamColor) //make sure it's the enemy color
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


        }
        if (percentage == 100)
        {
            // avant de destroy il faut changer le shader
            foreach (var player in playerWallHack)
            {
                if (player != null)
                {
                    player.GetComponent<Renderer>().material = player.GetComponent<MaterialsApply>().basic;
                }
            }
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
                Destroy(gameObject);
            
            //Destroy(gameObject);
        }
    }
    Sequence Des()
    {
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => percentage, x => percentage = x, 100, time));
        return s;
    }
}
