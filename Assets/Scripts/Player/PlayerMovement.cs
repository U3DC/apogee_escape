
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float runSpeed = 5f;
    public float turnSmoothing = 15f;
    public float jumpForce = 5f;

    public float maxSpeed = 10f;
    private float moveVelocity;
    public bool isGrounded;
    public float groundHitDistance;

    private Vector3 movement;
    private Rigidbody myRB;
    private SpriteRenderer playerSR;
    private Interactive interactor;
    private GameObject currentInteractor;
    private Animator[] playerSprites;

    [Header("Flashlight")]
    public GameObject itemFlashlight;
    private Vector3 itemFlashlightPos;
    public float flashlightBobVelocity = 20f;
    public float flashlightBobAmount = 0.2f;

    [Header("Controllers")]
    public CountdownAirSupply air;
    public CountdownTemperature cold;
    public GameObject timerController;

    RaycastHit hit;


    void Awake()
    {
        timerController = GameObject.Find("_TimerStatusController");
        myRB = GetComponent<Rigidbody>();
        playerSprites = gameObject.GetComponentsInChildren<Animator>();
        itemFlashlightPos = itemFlashlight.transform.localPosition;
    }

    void FixedUpdate()
    {
        float lh = Sinput.GetAxisRaw("Horizontal");
        float lv = Sinput.GetAxisRaw("Vertical");
        Move(lh, lv);

        if (Physics.Raycast(transform.position - new Vector3(0f, -0.5f, 0f), -transform.up, groundHitDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "AirReplenish")
        {
            timerController.GetComponent<CountdownAirSupply>().Replenish();
            Debug.Log("in the replenish zone");
        }
        if (other.gameObject.tag == "Heater")
        {
            timerController.GetComponent<CountdownTemperature>().Replenish();
            Debug.Log("in the heat zone");
        }

    }

    void Move(float lh, float lv)
    {
        movement.Set(lh, 0f, lv);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;

        if (Sinput.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        myRB.AddForce(movement * speed);

        if (lh != 0f || lv != 0f)
        {
            Rotating(lh, lv);
        }
        if (myRB.velocity.magnitude > maxSpeed)
        {
            myRB.velocity = myRB.velocity.normalized * maxSpeed;
        }
    }

    void Update()
    {
        InteractiveCheck();
        //flaslight bob
        itemFlashlight.transform.localPosition = new Vector3(itemFlashlight.transform.localPosition.x,
            itemFlashlightPos.y + Mathf.Clamp((Mathf.Sin(Time.time * flashlightBobVelocity)), flashlightBobAmount * -10, flashlightBobAmount * 10) * flashlightBobAmount,
            itemFlashlight.transform.localPosition.z);

    }

    void InteractiveCheck()
    {
        //Cast ray from player in forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward);
        //If ray hits an object

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "InteractiveObject" && hit.distance < 2.0f)
            {
                currentInteractor = hit.collider.gameObject;
                hit.collider.gameObject.GetComponent<Interactive>().playersFocus = true;
            }
            else
            {
                if (currentInteractor != null)
                {
                    currentInteractor.GetComponent<Interactive>().playersFocus = false;
                    currentInteractor = null;
                }
            }
        }
    }

    void LateUpdate()
    {
        moveVelocity = myRB.velocity.magnitude;//Mathf.Max(Mathf.Abs(myRB.velocity.normalized.x), Mathf.Abs(myRB.velocity.normalized.y));

        float angleLR = Vector3.Angle(Camera.main.transform.forward, gameObject.transform.right);
        float angleUD = Vector3.Angle(Camera.main.transform.forward, -gameObject.transform.forward);
        foreach (Animator playerSprites in playerSprites)
        {
            //set the angle for the blend tree facing
            playerSprites.SetFloat("LRfacing", (angleLR - 90) / 90);
            playerSprites.SetFloat("UDfacing", (angleUD - 90) / 90);

            if (isGrounded == true)
            {
                playerSprites.SetFloat("moveSpeed", moveVelocity);
                playerSprites.SetBool("inAir", false);

                if (moveVelocity <= 0.2)
                {
                    playerSprites.speed = Mathf.Clamp(moveVelocity * 2, 0.2f, 1f);
                }
                else
                {
                    playerSprites.speed = 1;
                }
            }
            else if (isGrounded == false)
            {
                playerSprites.SetBool("inAir", true);

                if (moveVelocity <= 0.1)
                {
                    playerSprites.speed = Mathf.Clamp(moveVelocity * 2, 0.2f, 1f);
                }
                else
                {
                    playerSprites.speed = 1;
                }
            }
        }
    }

    void Jump()
    {
        myRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);            
    }

    void Rotating(float lh, float lv)
    {
        Vector3 targetDirection = new Vector3(lh, 0f, lv);
        Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(newRotation);
    }

 
}