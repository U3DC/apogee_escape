using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviourDefence : MonoBehaviour
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
    public float orbitChangeCountdown = 7f;

    [Header("Movement")]
    public float maxSpeed = 3f;
    public float minSpeed = 1.5f;
    private float speed;
    public float radiusSpeed;

    public float maxRadius = 4f;
    public float minRadius = 8f;
    private float radius;

    public float angle;

    [Header("Position")]
    public float maxXOffset = 10f;
    public float minXOffset = 0f;
    public float maxYOffset = 5f;
    public float minYOffset = -5f;
    public float maxZOffset = 5f;
    public float minZOffset = -5f;

    // the vertical amount so that not all the drones are flying at the same altitude
    public float maxYFlyOffset = 2f;
    public float minYFlyOffset = -3f;
    private float yFlyOffset;

    private Vector3 offsetPosition;
    public GameObject ship;
    public GameObject[] defencePositions;
    private Vector3 nextDefencePosition;
    public float maxDistance = 3f;

    void Start()
    {
        currentState = droneState.patrolling;
        mySC = GetComponent<SphereCollider>();
    }

    void SetRandomisations()
    {
        radius = Random.Range(minRadius, maxRadius);
        speed = Random.Range(minSpeed, maxSpeed);

        //Add to the ship position to create a new centre of orbit.
        offsetPosition = new Vector3(
            Random.Range(minXOffset, maxXOffset),
            Random.Range(minYOffset, maxYOffset),
            Random.Range(minZOffset, maxZOffset)
        );
        nextDefencePosition = defencePositions[Random.Range(0, defencePositions.Length)].transform.position;
        yFlyOffset = Random.Range(maxYFlyOffset, maxYFlyOffset);
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
            if (Vector3.Distance(transform.position, ship.transform.position) > maxDistance)
            {
                Vector3 velocity = Vector3.zero;
                //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(ship.transform.position.x, transform.position.y, ship.transform.position.z),ref velocity,0.3f);
                transform.position = Vector3.Lerp(transform.position, new Vector3(ship.transform.position.x, transform.position.y, ship.transform.position.z), 0.1f * Time.deltaTime);
                SetRandomisations();

            }
            else
                transform.RotateAround(nextDefencePosition, Vector3.up, angle * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("alien detected");
        if (other.gameObject.tag == "Alien")
        {
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

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Alien")
        {
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
}