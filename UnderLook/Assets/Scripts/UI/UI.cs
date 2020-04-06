using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI : MonoBehaviour
{
    [Header("Debug")]
    public bool cd;

    [Header("Global")]
    public bool hasThreeCapacities;
    public bool hasShield;
    private Color32 orange = new  Color32(255, 165, 0, 150);
    private Color32 whiteAlpha = new  Color32(255, 255, 255, 150);

    [Header("Name")]
    [SerializeField] private string NameCharacter;
    public Text text; 

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

    [Header("Ultimate")]
    public Image ultimateBar;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float CurrentUltimate;

    [Header("Health")]
    public Image HealthBar;
    [SerializeField] public float maxHP;
    [Range(0.0f, 200f)]
    public float CurrentHP;

    [Header("Shield")]
    public Image ShieldBar;
    [SerializeField] private float maxShield;
    [Range(0.0f, 200f)]
    public float CurrentShield;

    [Header("Market")]
    public bool showmarket = false;
    public GameObject market;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;

    private void Start()
    {
        NameCharacter = PhotonNetwork.player.NickName;
        percentageCooldown1 = 1;
        percentageCooldown2 = 1;

        Capacity1.GetComponent<Image>().sprite = capacityImage1;
        Capacity2.GetComponent<Image>().sprite = capacityImage2;


        capacityBG1.color = whiteAlpha;
        capacityBG2.color = whiteAlpha;
        capacityBG3.color = whiteAlpha;


        CurrentUltimate = 0;
        CurrentHP = maxHP;
        text.text = NameCharacter;


        if (hasThreeCapacities)
        {
            percentageCooldown3 = 1;
            Capacity3.GetComponent<Image>().sprite = capacityImage3;
        }
        else
        {
            capacityBG3.transform.parent.gameObject.SetActive(false);
        }

        if(hasShield)
        {
            CurrentShield = maxShield;
        }
        else
        {
            ShieldBar.transform.parent.gameObject.SetActive(false);
        }
        market.SetActive(false);

    }
    void FixedUpdate()
    {
        #region hideousCapacityRefresh
        
        if (cdRefresh1)
            capacityBG1.fillAmount = percentageCooldown1;
        if (cdRefresh1 && percentageCooldown1 == 1)
        {
            cdRefresh1 = false;
            capacityBG1.color = whiteAlpha;
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
            capacityBG2.color = whiteAlpha;
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
                capacityBG3.color = whiteAlpha;
            }
            if (CoolDown3)
            {
                CoolDown3 = false;
                DOTween.Play(CD3());
            }
        }
        #endregion hideousCapacityRefresh

        #region shield
        if (hasShield)
        {
            ShieldBar.fillAmount = CurrentShield / maxShield;
        }
        #endregion shield

        #region health

        HealthBar.fillAmount = CurrentHP / maxHP;

        #endregion health

        #region ultimate

        ultimateBar.fillAmount = CurrentUltimate;

        #endregion ultimate

        #region market
        if (!showmarket || GetComponentInParent<Market>().itembought)
        {
            market.SetActive(false);
        }
        else
        {
            market.SetActive(true);
            if (Input.GetKey(KeyCode.F2))
            {
                GetComponentInParent<Market>().item = item1.GetComponent<CardDisplay>().card;
                GetComponentInParent<Market>().itembought = true;


            }
            if (Input.GetKey(KeyCode.F3))
            {
                GetComponentInParent<Market>().item = item2.GetComponent<CardDisplay>().card;
                GetComponentInParent<Market>().itembought = true;


            }
            if (Input.GetKey(KeyCode.F4))
            {
                GetComponentInParent<Market>().item = item3.GetComponent<CardDisplay>().card;
                GetComponentInParent<Market>().itembought = true;
            }
        }
        #endregion market
    }

    Sequence CD1()
    {
        capacityBG1.color = orange;
        percentageCooldown1 = 0;
        cdRefresh1 = true;
        Sequence s = DOTween.Sequence();
        if(!cd)
            s.Append(DOTween.To(() => percentageCooldown1, x => percentageCooldown1 = x, 1, time1));
        else
            percentageCooldown1 = 1;
        return s;
    }
    Sequence CD2()
    {
        capacityBG2.color = orange;
        percentageCooldown2 = 0;
        cdRefresh2 = true;
        Sequence s = DOTween.Sequence();
        if (!cd)
            s.Append(DOTween.To(() => percentageCooldown2, x => percentageCooldown2 = x, 1, time2));
        else
            percentageCooldown2 = 1;
        return s;
    }
    Sequence CD3()
    {
        capacityBG3.color = orange;
        percentageCooldown3 = 0;
        cdRefresh3 = true;
        Sequence s = DOTween.Sequence();
        if (!cd)
            s.Append(DOTween.To(() => percentageCooldown3, x => percentageCooldown3 = x, 1, time3));
        else
            percentageCooldown3 = 1;
        return s;
    }

    public void cap(string capacity)
    {
        switch (capacity)
        {
            case "one":
                DOTween.Play(CD1());
                break;
            case "two":
                DOTween.Play(CD2());
                break;
            case "three":
                DOTween.Play(CD3());
                break;
            default:
                break;
        }
    }
}
