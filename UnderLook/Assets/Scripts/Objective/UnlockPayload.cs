using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPayload : MonoBehaviour
{
    public int timeToUnlock;
    void Update()
    {
        float blue = this.gameObject.GetComponentInParent<PointCountPlayers>().timerBlue;
        float red = this.gameObject.GetComponentInParent<PointCountPlayers>().timerRed;

        if (blue > timeToUnlock)
        {
            this.GetComponent<Payload>().Color("Blue");
            Destroy(this);
        }
        else if (red > timeToUnlock)
        {
            this.GetComponent<Payload>().Color("Red");
            Destroy(this);
        }
    }
}
