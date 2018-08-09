using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviourRepair : MonoBehaviour
{

    public GameObject ship;
    private Vector3 target;
    private float angle;


    [Header("Properties")]
    private bool canShoot = true;
    public int damageAmount = 40;

    [Header("Randomisations - Movement")]
    public GameObject[] repairPoints;

    public float maxRepairTime = 8f;
    public float minRepairTime = 3f;
    private float timeToRepair;

    public float maxSpeed = 1.90f;
    public float minSpeed = 1.25f;
    private float speed;

    [Header("Randomisations - Placement")]
    public float maxXOffset = 1f;
    public float minXOffset = -1f;
    private float xOffset;

    public float maxYOffset = 0.25f;
    public float minYOffset = -0.25f;
    private float yOffset;

    public float maxZOffset = 1f;
    public float minZOffset = -1f;
    private float zOffset;

    private Vector3 offsetPosition;
    private Vector3 nextRepairPoint;

    public GameObject statusController;
    private RepairStatus repairStatus;
    private bool isRepairing;


    void Start()
    {
        repairStatus = statusController.GetComponent<RepairStatus>();
        SetRandomisations();
        isRepairing = false;
    }

    void SetRandomisations()
    {
        Debug.Log("set randomisations");

        timeToRepair = Random.Range(minRepairTime, maxRepairTime);
        speed = Random.Range(minSpeed, maxSpeed);

        xOffset = Random.Range(minXOffset, maxXOffset);
        yOffset = Random.Range(minYOffset, maxYOffset);
        zOffset = Random.Range(minZOffset, maxZOffset);
        offsetPosition = new Vector3(xOffset, yOffset, zOffset);

        nextRepairPoint = repairPoints[Random.Range(0, repairPoints.Length)].transform.position;// + offsetPosition;

    }

    IEnumerator RateOfRepairTimer()
    {
        Debug.Log("repair started");
        isRepairing = true;
        yield return new WaitForSeconds(timeToRepair);
        repairStatus.repairDroneCompleted();

        SetRandomisations();
        isRepairing = false;
        Debug.Log("repair done");

        yield break;
    }

    void Update()
    {
        if (isRepairing == false)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, nextRepairPoint, speed * Time.deltaTime);
            Debug.Log("moving to " + nextRepairPoint);

            Debug.DrawLine(gameObject.transform.position, ship.transform.position, Color.blue);

            if (Vector3.Distance(gameObject.transform.position, nextRepairPoint) < 0.1f)
            {
                StartCoroutine("RateOfRepairTimer");
            }
        }
    }

}
