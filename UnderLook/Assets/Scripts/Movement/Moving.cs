﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Moving : MonoBehaviour
{
    CharacterController characterController;
    public float gravity;
    [HideInInspector] public float speed, jumpspeed;
    private float moveZ;
    public bool canMove = true;
    public bool gravityApplied = true;

    public bool isOnTrack = false;
    public float launch = 0f;
    public bool doTP = false;

    public Vector3 bluePos, redPos;

    [Header("Animation")]
    public GameObject body;
    public Animator animationPerso;
    private float mvX;
    private float mvY;
    private float mvXold;
    private float mvYold;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animationPerso = body.GetComponent<Animator>();
    }

    void Update()
    {
        MovementPlayer();
    }
    void MovementPlayer()
    {

        if (!isOnTrack)
        {
            mvXold = mvX;
            mvYold = mvY;
            mvY = Input.GetAxis("Horizontal");
            mvX = Input.GetAxis("Vertical");
            animationPlayer(mvY, mvX);

            float moveX = mvX * speed * Time.deltaTime;
            float moveY = mvY * speed * Time.deltaTime;
            float moveZ = 0.0f;

            if(mvYold == 1 && mvXold == 0 || mvYold == 0 && mvXold == -1 || mvYold == -1 && mvXold == 0)
            {
                if(!(mvY == 1 && mvX == 0 || mvY == 0 && mvX == -1 || mvY == -1 && mvX == 0))
                {
                    GetComponent<OffSetWeapon>().changeweapondDown = true;
                }
            }
            else
            {
                if (mvY == 1 && mvX == 0 || mvY == 0 && mvX == -1 || mvY == -1 && mvX == 0)
                {
                    GetComponent<OffSetWeapon>().changeweapondUp = true;
                }
            }

            if ((Input.GetButton("Jump") && characterController.isGrounded))
            {
                moveZ = jumpspeed;
                animationPlayer(0.42f, 0.42f);
            }
            moveZ -= gravity * Time.deltaTime;
            
            if (canMove)
                characterController.Move(transform.forward * moveX + transform.right * moveY); //time multiplié au carré
            if (gravityApplied)
                characterController.Move(transform.up * moveZ * Time.deltaTime);

        }
    }


    Sequence Launch()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOLocalMove(new Vector3(0, 0, 0), 1.5f)); //move to end of the cannon
        s.Append(DOTween.To(() => launch, x => launch = x, 0.5f, 3f)); //change the speed
        s.OnComplete(() => { DOTween.Play(Land()); });
        return s;
    }
    Sequence Land()
    {
        gameObject.transform.parent = null;
        launch = 0f;
        gameObject.GetComponent<CollideWithCannon>().dollycart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position = 0;
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOLocalRotate(new Vector3(0, 0, 0), .5f)); //rotate to 0 0 0
        isOnTrack = false;

        return s;
    }

    Sequence Move(Vector3 pos)
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOLocalMove(pos, 0.3f));//.SetEase(Ease.InBounce)) ;
        return s;
    }


    public void DOLaunch()
    {
        DOTween.Play(Launch());
    }
    public void TP(bool isBlue)
    {

        bluePos = GameObject.FindGameObjectWithTag("bluePos").transform.position;
        redPos = GameObject.FindGameObjectWithTag("redPos").transform.position;
        if (isBlue)
            DOTween.Play(Move(bluePos));
        else
            DOTween.Play(Move(redPos));
        gameObject.GetComponent<TeamColor>().enabled = true;
    }

    private void animationPlayer(float X, float Y)
    {
        if (X < -0.5f && Y == 0)
        {
            animationPerso.speed = 1.25f;
        }
        else if (X > 0.5f && Y == 0)
        {
            animationPerso.speed = 2f;
        }
        else if (X == 0 && Y > 0.5f)
        {
            animationPerso.speed = 0.75f;
        }
        else if (X == 0 && Y < -0.5f)
        {
            animationPerso.speed = 1f;
        }
        else if ( X < 0.9f && Y > 0.9f)
        {
            animationPerso.speed = 0.75f;
        }
        animationPerso.SetFloat("VelX", X);
        animationPerso.SetFloat("VelY", Y);
    }
}