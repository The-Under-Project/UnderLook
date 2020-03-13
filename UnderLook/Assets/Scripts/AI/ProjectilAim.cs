using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProjectilAim : MonoBehaviour
{
    // position de l'ennemi à chaque frame et frame-1 //private all


    private GameObject foe;
    [Header("Debug")]
    public float posX;
    public float posX2;
    public float diffX;
    public float vitX;

    public float posZ;
    public float posZ2;
    public float diffZ;
    public float vitZ;

    private float posY;

    public float distanceIA;


    [Header("Fair")]
    // position où l'ennemi devrait être si il continue sa trajectoire
    public Vector3 prediction;

    // plus ce nombre est petit , plus l'ia sera précise
    [SerializeField] private float precision;


    [SerializeField] private float speedProj;
    [SerializeField] private float realSpeed;
    [SerializeField] private GameObject ammo;

    //laps de temps entre deux actions du script
    [SerializeField] private float timer;
    //[SerializeField] private float tpsActu;
    public float tpsShoot;

    [SerializeField] private GameObject shade;
    public GameObject actualShade;

    // Start is called before the first frame update
    void Start()
    {

        realSpeed = speedProj / 50; //calculer pour speed = 120, 150. Demander à Brice pourquoi, il sait :cool:

        foe = GameObject.FindGameObjectWithTag("Foe").gameObject;
        posZ = foe.transform.position.z;
        posX = foe.transform.position.x;
        posY = foe.transform.position.y;


        actualShade = Instantiate(shade, transform.position, transform.rotation) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {


        distanceIA = Vector3.Distance(transform.position, foe.transform.position);


        Timer2();

    }

    void Timer2()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            actualiser();
            timer = tpsShoot;
        }
    }

    void LaunchProjectile()
    {
        GameObject munition = Instantiate(ammo, transform.position, Quaternion.identity) as GameObject;
        munition.GetComponent<Rigidbody>().AddForce(transform.forward * speedProj);
        //realSpeed = munition.GetComponent<Rigidbody>().velocity; ici elle est = à 0 car la valeur est prise trop tot, todo la calculer
        
    }
    void actualiser()
    {
        // les six prochaines instructions permettent d'actualiser les positions x et z de l'ennemi , ainsi que la distance par rapport à "this" en usant pythagore


        tpsShoot = shoot(distanceIA);

        posZ = posZ2;
        posZ2 = foe.transform.position.z;
        diffZ = (posZ2 - posZ);
        vitZ = diffZ / tpsShoot;

        posX = posX2;
        posX2 = foe.transform.position.x;
        diffX = (posX2 - posX);
        vitX = diffX / tpsShoot;


        prediction = vitesse(vitX, 0, vitZ, posX2, posY, posZ2);

        //donc j'ai sa vitesse et le temps.

        for (int i = 0; i < 10; i++)
        {

            tpsShoot = shoot((Vector3.Distance(transform.position, prediction) + distanceIA) / 2);

            vitZ = diffZ / tpsShoot;
            vitX = diffX / tpsShoot;

            prediction = vitesse(vitX, 0, vitZ, posX2, posY, posZ2);
        }

        actualShade.transform.position = prediction;

        transform.LookAt(actualShade.transform.position);
        LaunchProjectile();
        //maintenant seconde équation j'ai la vitesse et la position mais pas temps

    }

    Vector3 vitesse(float vitX, float vitY, float vitZ, float x0, float y0, float z0) // permet de calculer la prédiction
    {
        //equation du type x(t) = v(t) * t + x0
        Vector3 result = new Vector3(vitX * tpsShoot + x0, vitY * tpsShoot + y0, vitZ * tpsShoot + z0);
        return result;

    }




    float shoot(float d)//cette valeur de shoot va nous faire le refresh
    {
        //équation du type x(t) = v(t) * t + x0 donc [x(t)-x0]/v(t) (m / (m/s)) -> s
        //or ici le joueur est immobile donc x0 = 0
        //donc t = x(t)/v(t)
        Debug.Log("d: " + d + ", distanceIA: " + distanceIA+ ", realSpeed: " + realSpeed + ", temps: " + distanceIA / realSpeed);
        return  d / realSpeed;
    }

    float norme(float x, float y) // <- == Vector2.Magnitude
    {
        return (Mathf.Sqrt(x * x + y * y));
    }


}

