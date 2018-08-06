using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject target;
    [SerializeField]
    private float distanceToPlayer;
    public List<Vector3> positions;
    [SerializeField]
    private float distance_permitted;
    public float maxNodeDistance;
    public float minNodeDistance;
    public float speed;
    public float catchupSpeed;
    public float catchupDistance = 5f;
    public float jumpSpeed;
    private Vector3 lastLeaderPosition;
    private Rigidbody playerRB;
    private Vector3 playerHeightNeutral;

    public float someFloat;

    private Vector3 prevPos;
    [SerializeField]
    private Vector3 currVel;

    [SerializeField]
    private bool isGrounded;
    private Animator mySpriteAnim;

    private GameObject follower;

    private int frame120 = 0;

    // Use this for initialization
    void Start()
    {		

        target = GameObject.Find("player");
        playerRB = target.GetComponent<Rigidbody>();
        positions.Add(transform.position);
        distance_permitted = maxNodeDistance;
        StartCoroutine(CalcVelocity());
        mySpriteAnim = GetComponentInChildren<Animator>();//GetComponent<Animator>();
        follower = GameObject.Find("Follower");
        frame120 = 0;
    }

    void Update()
    {
        playerHeightNeutral = new Vector3(target.transform.position.x, target.transform.position.y - 0.833f, target.transform.position.z);

        if (positions.Count != 0)
        {
            if (lastLeaderPosition != positions[positions.Count - 1])
            {
                positions.Add(playerHeightNeutral);
            }        
            if (positions.Count >= distance_permitted)
            {
			
                if (gameObject.transform.position != positions[0])
                {
                    if (isGrounded == false)
                    {
                        distance_permitted = minNodeDistance;
                        transform.position = Vector3.MoveTowards(transform.position, positions[0], Time.deltaTime * jumpSpeed);
                        transform.LookAt(new Vector3(positions[+1].x, this.transform.position.y, positions[+1].z));
                    }
                    else if (isGrounded == true)
                    {
                        if (distanceToPlayer >= catchupDistance)
                        {
                            distance_permitted = catchupDistance;
                            transform.position = Vector3.MoveTowards(transform.position, positions[0], Time.deltaTime * playerRB.velocity.magnitude * catchupSpeed);
                            transform.LookAt(new Vector3(positions[+1].x, this.transform.position.y, positions[+1].z));
                            frame120++;
                            if (frame120 > 120)
                            {
                                follower.GetComponent<FollowerSpeechText>().followerToSay("Catchup");
                                frame120 = 0;


                            }
                            Debug.Log(frame120);
                               
                        }
                        else
                        {
                            distance_permitted = maxNodeDistance;
                            transform.position = Vector3.MoveTowards(transform.position, positions[0], Time.deltaTime * playerRB.velocity.magnitude * speed);
                            transform.LookAt(new Vector3(positions[+1].x, this.transform.position.y, positions[+1].z));
                        }
                    }
                }
                else
                {
                    positions.Remove(positions[0]);
                    transform.position = Vector3.MoveTowards(transform.position, positions[0], Time.deltaTime * speed);
                }
            }
        }
        else
        {
            positions.Add(playerHeightNeutral);
            distance_permitted = maxNodeDistance;
        }

        lastLeaderPosition = playerHeightNeutral;

        distanceToPlayer = Vector3.Distance(gameObject.transform.position, target.transform.position);

    }

    void FixedUpdate()
    {

        if (Physics.Raycast(transform.position - new Vector3(0f, -0.5f, 0f), -transform.up, 0.6f))
        {
            isGrounded = true;
            mySpriteAnim.SetBool("isGrounded", true);
        }
        else
        {
            isGrounded = false;
            mySpriteAnim.SetBool("isGrounded", false);
        }
    }

    void LateUpdate()
    {

        float angleLR = Vector3.Angle(Camera.main.transform.forward, gameObject.transform.right);
        float angleUD = Vector3.Angle(Camera.main.transform.forward, gameObject.transform.forward);


        //greater than 90 is facing right, less than 90 is facing left
        if (angleLR < 89)
        {
            mySpriteAnim.SetBool("facingRight", true);
        }
        else if (angleLR > 91)
        {
            mySpriteAnim.SetBool("facingRight", false);
        }

//		//greater than 90 is facing down, less than 90 is facing up
//		if (angleUD < 89)
//		{
//			//TODO: bool for animation facing up
//		}
//		else if (angleUD > 91)
//		{
//			//TODO: bool for animation facing down
//		}

        mySpriteAnim.SetFloat("moveSpeed", Mathf.Abs(currVel.x) + Mathf.Abs(currVel.y));

        someFloat = Mathf.Abs(currVel.x) + Mathf.Abs(currVel.y);

    }

    IEnumerator CalcVelocity()
    {
        while (Application.isPlaying)
        {
            // Position at frame start
            prevPos = transform.position;
            // Wait till it the end of the frame
            yield return new WaitForEndOfFrame();
            currVel = (prevPos - transform.position) / Time.deltaTime;
        }
    }

}