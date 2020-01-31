using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanShoot : MonoBehaviour
{

    public int gunDamage = 100;//Damage of weapon , for the moment it's fixed but we need to redefine it
    public float fireRate = .10f;//Rate of weapon 
    public float weaponRange = 100f;//Range of wepon
    public float hitForce = 0f; //Knockback of weapon
    public Transform gunPosition;//Position of gun to calculate the line

    private Camera fpscam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);//Duration of line

    private AudioSource gunAudio; //The audio of gun
    private LineRenderer gunLine; //The line of shoot 
    private float nextFire; // Time when you can do a new shot
    
    // Start is called before the first frame update
    void Start()
    {
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpscam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate; //Set the time for another shot 
            StartCoroutine(ShotEffect());

            Vector3 lineOrigin = fpscam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));//Define center
            RaycastHit hit;
            
            gunLine.SetPosition(0, gunPosition.position);

            if (Physics.Raycast(lineOrigin, fpscam.transform.forward, out hit, weaponRange))
            {
                gunLine.SetPosition(1,hit.point);
                //Here We need the system of box life -> so I can't continue here
            }
            else
            {
                gunLine.SetPosition(1, lineOrigin +fpscam.transform.forward * weaponRange);
            }

        }
    }
    
    private IEnumerator ShotEffect() // Execute effect: Line + Audio
    {
        gunAudio.Play();
        gunLine.enabled = true;
        yield return shotDuration;
        gunLine.enabled = false;
    }
}
