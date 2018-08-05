using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //camera controls
    private Camera cam;

    [Header("Camera Movement")]
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
    private Transform player;
    private Rigidbody playerRB;

    private int frame5 = 6;

    void Awake()
    {
        player = GameObject.Find("player").transform;
        originalOffset = transform.position - player.position;
        originalCamPos = transform;
        playerRB = player.GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
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
        Debug.DrawLine(trackVelocity + player.position, player.position, Color.yellow);
        playerFacingInfluence = new Vector3(player.transform.forward.x * playerFacingOffset, 0, player.transform.forward.z * playerFacingOffset);
        desiredPosition = new Vector3(player.position.x + playerFacingInfluence.x + originalOffset.x, originalCamPos.position.y, player.position.z + playerFacingInfluence.z + originalOffset.z);
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        trackVelocity = (playerRB.position - lastPos) * 50;
        lastPos = playerRB.position;
    }
}