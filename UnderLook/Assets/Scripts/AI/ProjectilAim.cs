using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectilAim : MonoBehaviour
{
    // position de l'ennemi à chaque frame et frame-1
    private GameObject foe;

    private float positionX;
    private float positionXnew;
    private float diffX;

    private float positionZ;
    private float positionZnew;
    private float diffZ;

    private float distanceIaFoe;
    private float distanceIaFoeNew;
    private float distanceFromFoe;

    private float positionY;
    private float vitesseFoe;
    private Vector2 vectorvitessefoe;


    // position où l'ennemi devrait être si il continue sa trajectoire
    private Vector3 prediction;

    // plus ce nombre est petit , plus l'ia sera précise
    [SerializeField] private float precision;


    [SerializeField] private float speedProj;
    [SerializeField] private GameObject ammo;

    //laps de temps entre deux actions du script
    [SerializeField] private float timer;
    [SerializeField] private float tempsentredeuxtir;



    // Start is called before the first frame update
    void Start()
    {
        foe = GameObject.FindGameObjectWithTag("Foe").gameObject;
        positionZnew = foe.transform.position.z;
        positionXnew = foe.transform.position.x;
        distanceIaFoeNew = norme(positionZnew - transform.position.z, positionXnew - transform.position.x);
        positionY = foe.transform.position.y ;
        Debug.Log(positionY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actualiser();
        vectorvitessefoe = new Vector2(diffX, diffZ);
        distanceFromFoe = norme(diffX, diffZ);
        vitesseFoe = distanceFromFoe / Time.deltaTime;
        if ((vitesseFoe - 0.01) > 0)
        {
            int i = 0;
            while ( i < 50 && Mathf.Abs((distanceFromFoe * speedProj) / vitesseFoe - distanceIaFoeNew) > precision)
            {
                Debug.Log("d'accord");
                i++;
                positionX = positionXnew;
                positionXnew += diffX;
                positionZ = positionZnew;
                positionZnew += diffZ;
                distanceFromFoe += norme(diffZ, diffX);
                distanceIaFoeNew = norme(positionXnew - transform.position.x, positionZnew - transform.position.z);
                Debug.Log("Part 1:" + (distanceFromFoe));
                Debug.Log("Part vitesseFoe:" + distanceIaFoeNew);
            }
            /*
            if (distanceIaFoeNew > distanceIaFoe)
            {
                prediction.x *= eloignement;
                prediction.z *= eloignement;
            }
            else
            {
                prediction.x *= rapprochement;
                prediction.z *= rapprochement;
            }
            */
        }
        prediction.x = positionXnew;
        prediction.z = positionZnew;
        Vector3 direction = prediction - transform.position;
        Quaternion targetrotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetrotation, Time.deltaTime * 80);
        

        //transform.LookAt(prediction); doit être enlever car réinitialise et actualise la rotation à chaque frame.

        if (timer ==100)
        {
            DOTween.Play(TC());
            LaunchProjectil();
        }
            
            
        
        
    }
    Sequence TC()
    {
        Sequence s = DOTween.Sequence();
        timer = 0;
        s.Append(DOTween.To(() => timer, x => timer = x, 100, tempsentredeuxtir));
        return s;
    }

    void LaunchProjectil()
    {
        GameObject munition = Instantiate(ammo, transform.position, Quaternion.identity) as GameObject;
        munition.GetComponent<Rigidbody>().AddForce(transform.forward *speedProj);
    }
    void actualiser()
    {
        // les six prochaines instructions permettent d'actualiser les positions x et z de l'ennemi , ainsi que la distance par rapport à "this" en usant pythagore
        positionZ = positionZnew;
        positionZnew = foe.transform.position.z;
        diffZ = positionZnew - positionZ;

        positionX = positionXnew;
        positionXnew = foe.transform.position.x;
        diffX = positionXnew - positionX;

        distanceIaFoe = distanceIaFoeNew;
        distanceIaFoeNew = norme(positionZnew - transform.position.z, positionXnew - transform.position.x);
    }

    float norme(float x, float y)
    {
        return (Mathf.Sqrt(x*x + y*y));
    }
   
  
}

