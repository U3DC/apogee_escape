using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDeploy : MonoBehaviour
{
    public GameObject defendDrone;
    public GameObject assistDrone;
    public GameObject repairDrone;
    public GameObject refuelDrone;
    public GameObject spawnPoint;


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
      

    }

    void DeployAssist()
    {
        Debug.Log("Deploying assist drone");
        Instantiate(assistDrone, spawnPoint.transform.position, Quaternion.Euler(Vector3.zero));

    }


    void DeployRepair()
    {
        Debug.Log("Deploying Repair drone");
        Instantiate(repairDrone, spawnPoint.transform.position, Quaternion.Euler(Vector3.zero));


    }


    void DeployRefuel()
    {
        Debug.Log("Deploying refuel drone");
        Instantiate(refuelDrone, spawnPoint.transform.position, Quaternion.Euler(Vector3.zero));

    }


}
