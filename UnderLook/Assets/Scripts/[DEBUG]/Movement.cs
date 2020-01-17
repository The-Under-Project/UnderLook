using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public bool isOnTrack = false;
    public float launch = 0f;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isOnTrack)
        {
            if (characterController.isGrounded)
            {
                // We are grounded, so recalculate
                // move direction directly from axes

                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection *= speed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);
        }


    }
    Sequence Launch()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOLocalMove(new Vector3(0, 0, 0), 1.5f)); //move to end of the cannon
        s.Append(DOTween.To(() => launch, x => launch = x, 0.5f, 3f)); //change the speed
        s.OnComplete(() => {DOTween.Play(Land());});
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

    public void DOLaunch()
    {
        DOTween.Play(Launch());
    }
}
