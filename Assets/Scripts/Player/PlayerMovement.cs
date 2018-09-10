
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
    private Animator[] playerSprites;
    public bool inMenu = false;

    [Header("Flashlight")]
    public GameObject itemFlashlight;
    private Vector3 itemFlashlightPos;
    public float flashlightBobVelocity = 20f;
    public float flashlightBobAmount = 0.2f;

    [Header("Controllers")]
    public CountdownAirSupply air;
    public CountdownTemperature cold;
    public GameObject timerController;
    public GameObject droneChoiceMenu;
    RaycastHit hit;


    void Awake()
    {
        myRB = GetComponent<Rigidbody>();
        playerSprites = gameObject.GetComponentsInChildren<Animator>();
        itemFlashlightPos = itemFlashlight.transform.localPosition;

        air = timerController.GetComponent<CountdownAirSupply>();
        cold = timerController.GetComponent<CountdownTemperature>();
    }

    void FixedUpdate()
    {
        float lh = Sinput.GetAxisRaw("Horizontal");
        float lv = Sinput.GetAxisRaw("Vertical");
        Move(lh, lv);

        FlashlightBob();
    }

    void CheckGrounded()
    {
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
            air.Replenish();
            Debug.Log("in the replenish zone");
            droneChoiceMenu.SetActive(false);
        }
        if (other.gameObject.tag == "Heater")
        {
            cold.Replenish();
            Debug.Log("in the heat zone");
            droneChoiceMenu.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DroneZone")
        {
            DroneZone();
        }
    }

    public void DroneZone()
    {
        if (droneChoiceMenu.activeSelf == false)
        {
            droneChoiceMenu.SetActive(true);
            inMenu = true;
            Debug.Log("in the drone zone");
            Time.timeScale = 0.001f;
        }
        else
        {
            inMenu = false;
            droneChoiceMenu.SetActive(false);
            Debug.Log("leaving drone zone");
            Time.timeScale = 1f;

        }
    }


    void Move(float lh, float lv)
    {
        if (inMenu == false)
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
    }

    void FlashlightBob()
    {
        itemFlashlight.transform.localPosition = new Vector3(itemFlashlight.transform.localPosition.x,
            itemFlashlightPos.y + Mathf.Clamp((Mathf.Sin(Time.time * flashlightBobVelocity)), flashlightBobAmount * -10, flashlightBobAmount * 10) * flashlightBobAmount,
            itemFlashlight.transform.localPosition.z);
    }

    void LateUpdate()
    {
        moveVelocity = myRB.velocity.magnitude;
        AnimationFacings();

    }
    void AnimationFacings()
    {
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