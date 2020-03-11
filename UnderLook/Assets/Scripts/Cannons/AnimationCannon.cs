using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationCannon : MonoBehaviour
{
    public float delay;
    public float time;
    public Vector3 start;
    public Vector3 direction;
    public bool test;


    private void Update()
    {
        if (test)
        {
            test = false;
            Launch();
        }
    }
    
    public void Launch()
    {
        DOTween.Play(Movement());
    }

    Sequence Movement()
    {
        Sequence s = DOTween.Sequence();

        s.Append(transform.DOLocalMove(start, delay));//.SetEase(Ease.InBounce)) ;
        s.Append(transform.DOLocalMove(direction, time));//.SetEase(Ease.OutBounce)) ;
        s.Append(transform.DOLocalMove(start, time * 3f));//.SetEase(Ease.InBounce)) ;
        return s;
    }
}
