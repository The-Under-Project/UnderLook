using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeleteBullet : MonoBehaviour
{
    private float timer = 0;
    public float time;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

        DOTween.Play(TC());


    }
    Sequence TC()
    {
        Sequence s = DOTween.Sequence();
        timer = 0;
        s.Append(DOTween.To(() => timer, x => timer = x, 100, time));
        return s;
    }


    // Update is called once per frame
    void Update()
    {

        speed = GetComponent<Rigidbody>().velocity.magnitude;
        if (timer == 100)
        {
            Destroy(gameObject);

        }

    }
}
