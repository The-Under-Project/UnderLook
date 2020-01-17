using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveFromPointToPoint : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;
    void Start()
    {
        DOTween.Play(Launch(5f));
    }


    Sequence Launch(float time)
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOLocalMove(pos1, time));
        s.Append(transform.DOLocalMove(pos2, time));
        s.OnComplete(() => { DOTween.Play(Launch( time)); });
        return s;
    }
}
