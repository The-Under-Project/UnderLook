using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI : MonoBehaviour
{
    [Header("Global")]
    public bool hasThreeCapacities;
    [SerializeField] private Color32 orange = new  Color32(255, 165, 0, 1);

    [Header("Capacity 1")] 
    public float percentageCooldown1;
    public float time1;
    public bool CoolDown1;
    private bool cdRefresh1;
    [SerializeField] private Image capacityBG1;
    [SerializeField] private Sprite capacityImage1;
    [SerializeField] private GameObject Capacity1;

    [Header("Capacity 2")]
    public float percentageCooldown2;
    public float time2;
    public bool CoolDown2;
    private bool cdRefresh2;
    [SerializeField] private Image capacityBG2;
    [SerializeField] private Sprite capacityImage2;
    [SerializeField] private GameObject Capacity2;

    [Header("Capacity 3")]
    public float percentageCooldown3;
    public float time3;
    public bool CoolDown3;
    private bool cdRefresh3;
    [SerializeField] private Image capacityBG3;
    [SerializeField] private Sprite capacityImage3;
    [SerializeField] private GameObject Capacity3;

    [SerializeField] private GameObject Capacity3DeactivateObject;

    private void Start()
    {
        percentageCooldown1 = 1;
        percentageCooldown2 = 1;

        Capacity1.GetComponent<Image>().sprite = capacityImage1;
        Capacity2.GetComponent<Image>().sprite = capacityImage2;

        if (hasThreeCapacities)
        {
            percentageCooldown3 = 1;
            Capacity3.GetComponent<Image>().sprite = capacityImage3;
        }
        else
        {
            Capacity3DeactivateObject.SetActive(false);
        }
       
    }
    void FixedUpdate()
    {
        if(cdRefresh1)
            capacityBG1.fillAmount = percentageCooldown1;
        if (cdRefresh1 && percentageCooldown1 == 1)
        {
            cdRefresh1 = false;
            capacityBG1.color = Color.white;
        }
        if (CoolDown1)
        {
            CoolDown1 = false;
            DOTween.Play(CD1());
        }

        if (cdRefresh2)
            capacityBG2.fillAmount = percentageCooldown2;
        if (cdRefresh2 && percentageCooldown2 == 1)
        {
            cdRefresh2 = false;
            capacityBG2.color = Color.white;
        }
        if (CoolDown2)
        {
            CoolDown2 = false;
            DOTween.Play(CD2());
        }

        if (hasThreeCapacities)
        {
            if (cdRefresh3)
                capacityBG3.fillAmount = percentageCooldown3;
            if (cdRefresh3 && percentageCooldown3 == 1)
            {
                cdRefresh3 = false;
                capacityBG3.color = Color.white;
            }
            if (CoolDown3)
            {
                CoolDown3 = false;
                DOTween.Play(CD3());
            }
        }
    }

    Sequence CD1()
    {
        capacityBG1.color = orange;
        percentageCooldown1 = 0;
        cdRefresh1 = true;
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => percentageCooldown1, x => percentageCooldown1 = x, 1, time1));
        return s;
    }
    Sequence CD2()
    {
        capacityBG2.color = orange;
        percentageCooldown2 = 0;
        cdRefresh2 = true;
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => percentageCooldown2, x => percentageCooldown2 = x, 1, time2));
        return s;
    }
    Sequence CD3()
    {
        capacityBG3.color = orange;
        percentageCooldown3 = 0;
        cdRefresh3 = true;
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => percentageCooldown3, x => percentageCooldown3 = x, 1, time3));
        return s;
    }
}
