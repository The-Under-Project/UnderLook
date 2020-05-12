using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class DropSpeed : MonoBehaviour
{
    public float time;
    public float time2;
    public float end;
    void Start()
    {
        DOTween.Play(drop());
    }
    Sequence drop()
    {
        Sequence s = DOTween.Sequence(); 
        s.Join(DOTween.To(() => GameObject.FindGameObjectWithTag("cart").GetComponent<CinemachineDollyCart>().m_Speed, x => GameObject.FindGameObjectWithTag("cart").GetComponent<CinemachineDollyCart>().m_Speed = x, 6, time2));
        s.Append(transform.DOMove(Vector3.zero, time));
        s.Join(DOTween.To(() => end, x => end = x, 1, time));
        return s;
    }
    Sequence kill()
    {
        Sequence s = DOTween.Sequence();
        s.Join(DOTween.To(() => GameObject.FindGameObjectWithTag("cart").GetComponent<CinemachineDollyCart>().m_Speed, x => GameObject.FindGameObjectWithTag("cart").GetComponent<CinemachineDollyCart>().m_Speed = x, 0, time2));
        return s;
    }
    void Update()
    {
        if(end == 1)
        {
            DOTween.Play(kill());
        }
    }
}
