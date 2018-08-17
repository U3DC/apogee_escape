using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviourRepair : MonoBehaviour
{
    [Header("Properties")]
    public GameObject ship;
    public GameObject statusController;
    private RepairStatus repairStatus;

    [Header("Action")]
    private bool repairEffectEnabled;
    private float repairEffectTime;
    public float maxRepairEffectTime = 1.0f;
    public float minRepairEffectTime = 0.4f;

    private float repairEffectInvterval;
    public float maxRepairEffectInterval = 1f;
    public float minRepairEffectInterval = 1f;

    private enum droneState
    {
        moving,
        lowering,
        repairing,
        lifting
    };
    [SerializeField]
    private droneState currentState;
    public Transform[] repairPositions;
    private int repairPositionsCount;

    private Vector3 offsetPosition;
    private Vector3 nextRepairPoint;
    public float maxRepairTime = 8f;
    public float minRepairTime = 5f;
    private float timeToRepair;
    public GameObject repairTrail;

    [Header("Movement")]
    public float maxSpeed = 10f;
    public float minSpeed = 6f;
    private float speed;
    public float upDownRate = 0.5f;

    [Header("Position")]
    public float maxXOffset = 0.5f;
    public float minXOffset = -0.5f;
    public float maxYOffset = 0.25f;
    public float minYOffset = -0.25f;
    public float maxZOffset = 0.5f;
    public float minZOffset = -0.5f;
    //upwards amount to fly after completing a repair
    public float maxFlyUp = 3.5f;
    public float minFlyUp = 1.5f;

    private float flyUp;
    private Vector3 flyUpPoint;
    private Vector3 previousRepairPoint;
    private Vector3 desiredPosition;

    void Start()
    {
        // required to create the association when we instantiate prefabs
        statusController = GameObject.Find("_StatusController");
        ship = GameObject.Find("shipChassis"); 

        //set up array of repair points
        repairPositionsCount = 0;
        foreach (Transform child in GameObject.Find("RepairPoints").transform)
        {
            repairPositions[repairPositionsCount] = child.transform;
            repairPositionsCount++;
        }
        if (repairPositions.Length == 0)
        {
            Debug.LogError("No repair points in array");
        }

        currentState = droneState.lifting;
        previousRepairPoint = gameObject.transform.position;
        repairStatus = statusController.GetComponent<RepairStatus>();
        SetRandomisations();
    }

    void SetRandomisations()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        offsetPosition = new Vector3(Random.Range(minXOffset, maxXOffset),
                                     Random.Range(minYOffset, maxYOffset),
                                     Random.Range(minZOffset, maxZOffset)
                                    );

        flyUp = Random.Range(minFlyUp, maxFlyUp);
        flyUpPoint = new Vector3(0, flyUp, 0);

        nextRepairPoint = repairPositions[Random.Range(0, repairPositions.Length)].transform.position + offsetPosition;
        timeToRepair = Random.Range(minRepairTime, maxRepairTime);
        repairEffectTime = Random.Range(minRepairEffectTime, maxRepairEffectTime);
        repairEffectInvterval = Random.Range(minRepairEffectInterval, maxRepairEffectInterval);
    }

    void Update()
    {
        Debug.Log(ship.transform.position);
        if (currentState == droneState.lifting)
        {
            desiredPosition = Vector3.MoveTowards(desiredPosition, previousRepairPoint + flyUpPoint, (speed * upDownRate) * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, previousRepairPoint + flyUpPoint) < 0.1f)
            {
                currentState = droneState.moving;
            }
        }
        if (currentState == droneState.moving)
        {
            desiredPosition = Vector3.MoveTowards(desiredPosition, nextRepairPoint + flyUpPoint, speed * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, nextRepairPoint + flyUpPoint) < 0.1f)
            {
                currentState = droneState.lowering;
            }
        }
        if (currentState == droneState.lowering)
        {
            desiredPosition = Vector3.MoveTowards(desiredPosition, nextRepairPoint, (speed * upDownRate) * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, nextRepairPoint) < 0.1f)
            {
                StartCoroutine("RateOfRepairTimer");
            }
        }
        if (currentState == droneState.repairing)
        {
            if (repairEffectEnabled == false)
            {
                StartCoroutine(RepairEffects());
            }
        }
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * speed);
    }

    IEnumerator RateOfRepairTimer()
    {
        Debug.Log("repair started");
        currentState = droneState.repairing;
        yield return new WaitForSeconds(timeToRepair);
        repairStatus.repairDroneCompleted();
        Debug.Log("repair done");

        SetRandomisations();
        currentState = droneState.lifting;
        previousRepairPoint = gameObject.transform.position;
        yield break;
    }


    IEnumerator RepairEffects()
    {
        while (true)
        {
            if (repairEffectEnabled == false && currentState == droneState.repairing)
            {
                repairEffectEnabled = true;
                GameObject trail;
                trail = Instantiate(repairTrail, transform.position, Quaternion.Euler(Vector3.zero),transform);
                LineRenderer lr = trail.GetComponent<LineRenderer>();

                if (lr != null)
                {
                    lr.SetPosition(0, transform.position);
                    lr.SetPosition(1, ship.transform.position);
                    lr.material.SetTextureOffset("_MainTex", new Vector2(5f * Time.deltaTime, 0f));
                }

                if (currentState != droneState.repairing)
                {
                    Destroy(trail.gameObject);
                }
                else
                {
                    Destroy(trail.gameObject, repairEffectTime);
                }
            }
            yield return new WaitForSeconds(repairEffectInvterval);
            repairEffectEnabled = false;
        }
    }
}
