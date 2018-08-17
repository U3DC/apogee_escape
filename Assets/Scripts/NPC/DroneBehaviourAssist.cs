using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviourAssist : MonoBehaviour
{
    [Header("Action")]
    public GameObject weaponTrail;
    public float weaponTrailTime = 0.2f;
    public float rateOfFire = 1.2f;
    private bool canShoot = true;
    public int damageAmount = 40;
    private SphereCollider mySC;

    private enum droneState
    {
        patrolling,
        attacking
    };
    [SerializeField]
    private droneState currentState;
    public float orbitChangeCountdown = 5f;

    [SerializeField]
    private bool isCatchingUp = false;

    private Collider other;
    private bool triggered = false;

    [Header("Movement")]
    public float maxSpeed = 2.5f;
    public float minSpeed = 1.25f;
    private float speed;
    public float radiusSpeed;

    public float maxRadius = 5f;
    public float minRadius = 2f;
    private float radius;
    public float angle;

    [Header("Position")]
    public float maxXOffset = 5f;
    public float minXOffset = -5f;
    public float maxYOffset = 8f;
    public float minYOffset = 4f;
    public float maxZOffset = 5f;
    public float minZOffset = -5f;
    private Vector3 offsetPosition;
    public GameObject player;
    public float maxDistanceToOrbit = 3f;
    public float maxDistanceToCatchup = 6f;



    void Start()
    {
        // required to create the association when we instantiate prefabs
        player = GameObject.Find("player");

        currentState = droneState.patrolling;
        mySC = GetComponent<SphereCollider>();
    }

    void SetRandomisations()
    {
        radius = Random.Range(minRadius, maxRadius);
        speed = Random.Range(minSpeed, maxSpeed);

        //Add to the player position to create a new centre of orbit.
        offsetPosition = new Vector3(
            Random.Range(minXOffset, maxXOffset),
            Random.Range(minYOffset, maxYOffset),
            Random.Range(minZOffset, maxZOffset)
        );
    }

    void Update()
    {
        if (currentState == droneState.attacking)
        {
            transform.position = transform.position;
        }
        else if (currentState == droneState.patrolling)
        {
            OrbitMovement();
        }
    }

    void OrbitMovement()
    {
        if (currentState == droneState.patrolling)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > maxDistanceToOrbit)
            {
                Vector3 velocity = Vector3.zero;
                transform.position = Vector3.Lerp(
                    transform.position,
                    new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z),
                    (speed / 4) * Time.deltaTime);
                SetRandomisations();
            }
            else
                transform.RotateAround(player.transform.position + offsetPosition, Vector3.up, angle * Time.deltaTime);
        }
        if (currentState == droneState.attacking)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > maxDistanceToCatchup)
            {
                Debug.Log("catching up to player");

                StartCoroutine(CatchupToPlayer());
                currentState = droneState.patrolling;
                transform.position = Vector3.Lerp(
                    transform.position, 
                    new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), 
                    maxSpeed * Time.deltaTime);

            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Alien")
        {
            Debug.Log("alien detected");
            if (isCatchingUp == false)
            {
                triggered = true;
                this.other = other;
                StopCoroutine("ReturnToPatrolling");
                currentState = droneState.attacking;

                if (canShoot == true)
                {
                    GameObject trail;
                    trail = Instantiate(weaponTrail, transform.position, Quaternion.Euler(Vector3.zero), transform);
                    LineRenderer lr = trail.GetComponent<LineRenderer>();

                    if (lr != null)
                    {
                        lr.SetPosition(0, transform.position);
                        lr.SetPosition(1, other.transform.position);
                    }

                    if (currentState != droneState.attacking)
                    {
                        Destroy(trail.gameObject);
                    }
                    else
                    {
                        Destroy(trail.gameObject, weaponTrailTime);
                    }

                    if (other.GetComponent<EnemyController>() != null)
                    {
                        other.GetComponent<EnemyController>().damage(damageAmount);
                    }

                    canShoot = false;
                    StartCoroutine("RateOfFireTimer");
                    StartCoroutine("ReturnToPatrolling");
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Alien")
        {
            Debug.Log("enemy exited collider");
            currentState = droneState.patrolling;
        }
    }

    private void LateUpdate()
    {
        //in the case of when the enemy is destroyed rather than exiting trigger collider
        if (triggered && !other)
        {
            Debug.Log("enemy destroyed or exited collider");
            currentState = droneState.patrolling;
        }
    }

    IEnumerator RateOfFireTimer()
    {
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
        yield break;
    }

    IEnumerator ReturnToPatrolling()
    {
        yield return new WaitForSeconds(rateOfFire * 2f);
        currentState = droneState.patrolling;
        yield break;
    }

    IEnumerator CatchupToPlayer()
    {
        isCatchingUp = true;
        yield return new WaitForSeconds(rateOfFire * 2f);
        isCatchingUp = false;
        yield break;
    }
}