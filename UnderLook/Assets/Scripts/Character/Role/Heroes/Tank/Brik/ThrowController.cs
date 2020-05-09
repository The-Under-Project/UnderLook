using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent(typeof(Animator))]
public class ThrowController : MonoBehaviour
{
    public Rigidbody weaponRb;
    private WeaponScript weaponScript;

    private Vector3 origLocPos;
    private Vector3 origLocRot;
    private Vector3 pullPosition;

    [Header("Public References")]
    public Transform weapon;
    public Transform hand;
    public Transform curvePoint;
    [Space]
    [Header("Parameters")]
    public float throwPower = 30;
    public bool hasWeapon = true;
    public bool pulling = false;
    public float returnTime = 0;

    void Start()
    {
        weaponRb = weapon.GetComponent<Rigidbody>();
        weaponScript = weapon.GetComponent<WeaponScript>();
        origLocPos = weapon.localPosition;
        origLocRot = weapon.localEulerAngles;


        weapon.transform.position = hand.position;

        weapon.transform.rotation = hand.rotation;

    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0) && hasWeapon)
        {
            WeaponThrow();
        }

        else if (Input.GetMouseButtonDown(1) && !hasWeapon)
        {
            //Reset();
            WeaponStartPull();
        }

        if (pulling)
        {

            if (returnTime < 1)
            {
                weapon.position = GetQuadraticCurvePoint(returnTime, pullPosition, curvePoint.position, hand.position);
                returnTime += Time.deltaTime * 1.5f;
            }
            else
            {
                WeaponCatch();
            }
        }
    }

    public void WeaponThrow()
    {
        weaponScript.activated = true;
        weaponRb.isKinematic = false;
        hasWeapon = false;
        weaponRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        weapon.parent = null;
        weapon.eulerAngles = new Vector3(0, 90 +  transform.eulerAngles.y, 0);


        weapon.transform.position += transform.right/5;
        weaponRb.AddForce(Camera.main.transform.forward * throwPower + transform.up * 2, ForceMode.Impulse);
    }
    public void WeaponStartPull()
    {
        pullPosition = weapon.position;
        weaponRb.Sleep();
        weaponRb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        weaponRb.isKinematic = true;
        weapon.DORotate(new Vector3(-90, -90, 0), .2f).SetEase(Ease.InOutSine);
        weapon.DOBlendableLocalRotateBy(Vector3.right * 90, .5f);
        weaponScript.activated = true;
        pulling = true;
    }
    public void WeaponCatch()
    {
        returnTime = 0;
        pulling = false;
        weapon.parent = hand;
        weaponScript.activated = false;
        //weapon.localEulerAngles = origLocRot;
        //weapon.localPosition = origLocPos;

        weapon.transform.position = hand.position;

        weapon.transform.rotation = hand.rotation;

        hasWeapon = true;
    }


    public void Reset()
    {
        weaponScript.activated = false;
        weaponRb.isKinematic = true;
        hasWeapon = true;
        weapon.parent = hand;
        weaponRb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        weapon.transform.position = hand.transform.position;
    }


    public Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t; //1 - temps
        float tt = t * t; // temps
        float uu = u * u; // (1- temps)²
        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }
}
