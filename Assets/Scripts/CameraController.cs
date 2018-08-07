using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //camera controls

    [Header("Camera Movement")]
    private Camera cam;
    public float smoothSpeed = 0.5f;
    public float playerFacingOffset = 2.0f;
    private Vector3 playerFacingInfluence;
    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;
    private Vector3 originalOffset;
    private Transform originalCamPos;

    //connecting to player
    private Vector3 trackVelocity;
    private Vector3 lastPos;
    [SerializeField]
    private bool playerMoving;
    private GameObject player;
    private Rigidbody playerRB;
    private bool isJumping = false;
    private float groundedYPos;
        

    private int frame5 = 6;

    void Awake()
    {
        player = GameObject.Find("player");
        originalOffset = transform.position - player.transform.position;
        originalCamPos = transform;
        playerRB = player.GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        isJumping = player.GetComponent<PlayerMovement>();
        groundedYPos = player.transform.position.x;
    }

    void Update()
    {
        frame5++;
        if (frame5 > 5)
        {
            if (Mathf.Abs(trackVelocity.x) == 0 && Mathf.Abs(trackVelocity.z) == 0)
            {
                playerMoving = false;
            }
            else
            {
                playerMoving = true;
            }
            frame5 = 0;
        }
    }

    void LateUpdate()
    {
        Debug.DrawLine(trackVelocity + player.transform.position, player.transform.position, Color.yellow);
        playerFacingInfluence = new Vector3(player.transform.forward.x * playerFacingOffset, 0, player.transform.forward.z * playerFacingOffset);


        desiredPosition = new Vector3(
            player.transform.position.x + playerFacingInfluence.x + originalOffset.x, 
            player.transform.position.y + playerFacingInfluence.y + originalOffset.y, 
            player.transform.position.z + playerFacingInfluence.z + originalOffset.z
        );

        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        trackVelocity = (playerRB.position - lastPos) * 50;
        lastPos = playerRB.position;


    }
}