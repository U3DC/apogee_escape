using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviourRefuel : MonoBehaviour
{
    [Header("Properties")]
    public GameObject ship;
    public GameObject statusController;

    [Header("Action")]
    private float timeToHarvest;
    public GameObject harvestEffect;
    private bool harvestEffectEnabled;
    private float harvestEffectTime;
    public float maxHarvestEffectTime = 1.0f;
    public float minHarvestEffectTime = 0.4f;

    private float harvestEffectInterval;
    public float maxHarvestEffectInterval = 1f;
    public float minHarvestEffectInterval = 1f;

    private enum droneState
    {
        leaving,
        harvesting,
        returning,
        dropoff
    };
    [SerializeField]
    private droneState currentState;
    [SerializeField]

    private Vector3 nextHarvestPoint;
    public float maxHarvestTime = 8f;
    public float minHarvestTime = 5f;
    private GameObject harvestParticles;

    [Header("Movement")]
    public float maxSpeed = 10f;
    public float minSpeed = 6f;
    private float speed;
    public float upDownRate = 0.5f;

    [Header("Position")]
    private float harvestOffsetFloat = 4f;
    private Vector3 harvestOffset;


    private Vector3 desiredPosition;

    public List<GameObject> rockList;
    [SerializeField]

    private int rockListNext;


    void Start()
    {
        // required to create the association when we instantiate prefabs
        statusController = GameObject.Find("_StatusController");
        ship = GameObject.Find("shipChassis");


        rockList = new List<GameObject>();

        foreach (GameObject rockPrefabs in GameObject.FindGameObjectsWithTag("Rock"))
        {
            rockList.Add(rockPrefabs);
        }
        currentState = droneState.dropoff;

        harvestOffset = new Vector3(0, harvestOffsetFloat, 0);
        harvestParticles = GameObject.Find("harvestEffect");
        harvestParticles.SetActive(false);


        SetRandomisations();
    }

    void SetRandomisations()
    {
        speed = Random.Range(minSpeed, maxSpeed);



        rockListNext = (Random.Range(0, rockList.Count));
        nextHarvestPoint = rockList[rockListNext].transform.position;
        if (rockList == null)
        {
            Debug.LogError("no rocks found in list");
        }
        //rockList.Remove(rockListNext);

        //timeToHarvest = Random.Range(minHarvestTime, maxHarvestTime);
        harvestEffectTime = Random.Range(minHarvestEffectTime, maxHarvestEffectTime);
        harvestEffectInterval = Random.Range(minHarvestEffectInterval, maxHarvestEffectInterval);
    }


    void Update()
    {
        if (currentState == droneState.leaving)
        {
            desiredPosition = Vector3.MoveTowards(gameObject.transform.position, nextHarvestPoint + harvestOffset, speed * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, nextHarvestPoint + harvestOffset) < 0.1f)
            {
                currentState = droneState.harvesting;
            }
        }
        if (currentState == droneState.returning)
        {
            harvestParticles.SetActive(false);
            StopCoroutine(HarvestTime());
            desiredPosition = Vector3.MoveTowards(gameObject.transform.position, ship.transform.position + harvestOffset, speed * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, ship.transform.position + harvestOffset) < 0.1f)
            {
                currentState = droneState.dropoff;
            }
        }
        if (currentState == droneState.dropoff)
        {
            SetRandomisations();
            desiredPosition = Vector3.MoveTowards(gameObject.transform.position, ship.transform.position, (speed * upDownRate) * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, ship.transform.position) < 0.1f)
            {
                currentState = droneState.leaving;
            }
        }
        if (currentState == droneState.harvesting)
        {
            if (harvestEffectEnabled == false)
            {
                StartCoroutine(HarvestTime());
            }
        }
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * speed);
    }

    //IEnumerator RateOfRepairTimer()
    //{
    //    Debug.Log("repair started");
    //    currentState = droneState.repairing;
    //    yield return new WaitForSeconds(timeToRepair);
    //    repairStatus.repairDroneCompleted();
    //    Debug.Log("repair done");

    //    SetRandomisations();
    //    currentState = droneState.lifting;
    //    previousRepairPoint = gameObject.transform.position;
    //    yield break;
    //}

    IEnumerator HarvestTime()
    {
        while (true)
        {
            if (harvestEffectEnabled == false && currentState == droneState.harvesting)
            {
                harvestEffectEnabled = true;
                harvestParticles.SetActive(true);

                if (currentState != droneState.harvesting)
                {
                    harvestParticles.SetActive(true);
                }
            }
            yield return new WaitForSeconds(harvestEffectInterval);
            harvestEffectEnabled = false;
            currentState = droneState.returning;
            harvestParticles.SetActive(true);

        }
    }
}
