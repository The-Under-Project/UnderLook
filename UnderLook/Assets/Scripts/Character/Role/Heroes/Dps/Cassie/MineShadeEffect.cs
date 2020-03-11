using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MineShadeEffect : MonoBehaviour
{
    public float timeDestroy = 10f;

    public float time = 3f;
    public float size = 10f;

    private float percentage;
    void Awake()
    {
        DOTween.Play(shade());
        DOTween.Play(Des());
    }
    private void FixedUpdate()
    {
        if(percentage == 100)
        {

            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            
            
                Destroy(gameObject);
            
        }
    }
    Sequence shade()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(size, time).SetEase(Ease.OutBounce));
        //s.Join(this.GetComponent<MeshRenderer>().material.DOColor(Color.clear, time * 3));
        return s;
    }
    Sequence Des()
    {
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => percentage, x => percentage = x, 100, timeDestroy));
        return s;
    }
}
