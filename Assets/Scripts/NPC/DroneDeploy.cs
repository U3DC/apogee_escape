using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneDeploy : MonoBehaviour
{
    public GameObject defendDrone;
    public GameObject assistDrone;
    public GameObject repairDrone;
    public GameObject refuelDrone;
    public GameObject spawnPoint;

    public static int defendCount;
    public static int assistCount;
    public static int repairCount;
    public static int refuelCount;

    public Text defendText;
    public Text assistText;
    public Text repairText;
    public Text refuelText;

    public string droneCounterPreface = "Active: ";

    void Start()
    {
        defendCount = 0;
        assistCount = 0;
        repairCount = 0;
        refuelCount = 0;
    }

    public void Deploy(string _droneType)
    {
        switch (_droneType.Trim().ToLower())
        {
            case "defend":
                DeployDefend();
                break;

            case "assist":
                DeployAssist(); 
                break;

            case "repair":
                DeployRepair();
                break;

            case "refuel":
                DeployRefuel();
                break;

            default:
                Debug.Log("asked to deploy drone with " + _droneType + " string?");
                break;
        }
    }


    void DeployDefend()
    {
        Debug.Log("Deploying Defend drone");
        Instantiate(defendDrone, spawnPoint.transform.position, Quaternion.Euler(Vector3.zero));
        defendCount++;
        defendText.text = droneCounterPreface + defendCount;


    }

    void DeployAssist()
    {
        Debug.Log("Deploying assist drone");
        Instantiate(assistDrone, spawnPoint.transform.position, Quaternion.Euler(Vector3.zero));
        assistCount++;
        assistText.text = droneCounterPreface + assistCount;
    }


    void DeployRepair()
    {
        Debug.Log("Deploying Repair drone");
        Instantiate(repairDrone, spawnPoint.transform.position, Quaternion.Euler(Vector3.zero));
        repairCount++;
        repairText.text = droneCounterPreface + repairCount;

    }


    void DeployRefuel()
    {
        Debug.Log("Deploying refuel drone");
        Instantiate(refuelDrone, spawnPoint.transform.position, Quaternion.Euler(Vector3.zero));
        refuelCount++;
        refuelText.text = droneCounterPreface + refuelCount;
    }


}
