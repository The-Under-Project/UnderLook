using System.Collections;
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

    public GameObject posEND_RED;
    public GameObject posEND_BLUE;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovementPlayer();
        if(posEND_RED == null || posEND_BLUE == null)
        {
            posEND_BLUE = GameObject.FindGameObjectWithTag("CannonBlueEnd");
            posEND_RED = GameObject.FindGameObjectWithTag("CannonRedEnd");
        }
    }
    void MovementPlayer()
    {

        if (!isOnTrack)
        {
            float moveX = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            float moveY = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            //float moveZ = 0.0f;

            if ((Input.GetButton("Jump") && characterController.isGrounded))
            {
                moveZ = jumpspeed;
            }
            moveZ -= gravity * Time.deltaTime;


            if (characterController.isGrounded)
                moveZ = 0;

            if (canMove)
                characterController.Move(transform.forward * moveX + transform.right * moveY); //time multiplié au carré
            if (gravityApplied)
                characterController.Move(transform.up * moveZ * Time.deltaTime);

        }
    }


    Sequence Launch()
    {
        Sequence s = DOTween.Sequence();
        if(!GetComponent<TeamColor>().isBlue)
            s.Append(transform.DOMove(posEND_RED.transform.position, 1.5f)); //move to end of the cannon
        else
            s.Append(transform.DOMove(posEND_BLUE.transform.position, 1.5f)); //move to end of the cannon

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
}