using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassieMaterialDeath : MonoBehaviour
{
    public Material death;
    public int hpdebug;
    void FixedUpdate()
    {
        if (gameObject.GetComponent<Health.PlayerManagerCassie>().Health <= 0)
        {
            gameObject.GetComponent<Player.Cassie>().hp = gameObject.GetComponent<Player.Cassie>().hpmax;
            gameObject.GetComponent<Moving>().TP(gameObject.GetComponent<TeamColor>().isBlue);
        }
    }
}
