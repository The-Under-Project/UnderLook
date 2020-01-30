using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI : MonoBehaviour
{
    [SerializeField]private float percentageCooldown;
    public bool CoolDown1;

    public Image capacity1;
    
    private void Start()
    {
        percentageCooldown = 1;
       
    }
    void FixedUpdate()
    {
        capacity1.fillAmount = percentageCooldown;
        if (CoolDown1)
        {
            CoolDown1 = false;
            DOTween.Play(CD());
        }
    }

    Sequence CD()
    {
        percentageCooldown = 0;
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => percentageCooldown, x => percentageCooldown = x, 1, 3f));
        return s;

    }
}
