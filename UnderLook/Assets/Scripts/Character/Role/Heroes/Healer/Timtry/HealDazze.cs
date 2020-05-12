using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealDazze : MonoBehaviour
{
    
    public GameObject touched;
    public int ovetimeheal;
    public float timeofheal;
    public float timesincebeginningofheal;


    // Start is called before the first frame update
    
    public void DeterminedHealed(Transform origin, string ally, float maxDistance)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("determinedheal");
        if(Physics.Raycast(origin.transform.position, origin.forward, out  hit, maxDistance))    //(Physics.Raycast(ray,out hit, maxDistance)
        {
            Debug.Log("rayinstantier");
            if (hit.transform.GetComponent<TeamColor>() != null && hit.collider.gameObject.GetComponent<TeamColor>().teamColor == ally)
            {
                Debug.Log("Worked");
                touched = hit.transform.gameObject;
                Healing();
            }
            
        }
    }
    void FixedUpdate()
    {
        if(timesincebeginningofheal < 1)
        {
            touched.GetComponent<Player.Base>().hp += ovetimeheal;
            if (touched.GetComponent<Player.Base>().hp > touched.GetComponent<Player.Base>().hpmax)
                touched.GetComponent<Player.Base>().hp = touched.GetComponent<Player.Base>().hpmax;
        }
        else if(timesincebeginningofheal == 1)
        {
            touched = null;
        }
    }
    Sequence Healing()
    {
        timesincebeginningofheal = 0;
        Sequence s = DOTween.Sequence();
        s.Append(DOTween.To(() => timesincebeginningofheal, x => timesincebeginningofheal = x, 1, timeofheal));
        return s;
    }
}
