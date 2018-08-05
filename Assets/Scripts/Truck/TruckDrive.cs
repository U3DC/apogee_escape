using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckDrive : MonoBehaviour
{
    private Vector3 averageNormalDirection;
    Ray frontHeightDetect;
    Ray rearHeightDetect;
    RaycastHit hitFront;
    RaycastHit hitRear;
    private Vector3 frontWheel;
    private Vector3 rearWheel;

    private GameObject truckChassis;


    private float wheelBase = 1.5f;

    public float speed = 0.1f;

    void Start()
    {
        truckChassis = GameObject.Find("Chassis");

    }

    void Update()
    {
        frontWheel  = gameObject.transform.localPosition + (Vector3.forward * wheelBase);//gameObject.transform.forward * wheelBase;//transform.forward * wheelBase;
        rearWheel   = gameObject.transform.localPosition + (Vector3.forward * -wheelBase);



        frontHeightDetect   = new Ray(frontWheel,   -transform.up);
        rearHeightDetect    = new Ray(rearWheel,    -transform.up);    //gameObject.transform.TransformDirection(Vector3.down));

        Debug.DrawLine(frontWheel, -gameObject.transform.up * 100);
        Debug.DrawLine(rearWheel, -gameObject.transform.up * 100);



        if (Physics.Raycast(frontHeightDetect, out hitFront, Mathf.Infinity) || Physics.Raycast(rearHeightDetect, out hitRear, Mathf.Infinity))
        {
            Vector3 hitAveraged = new Vector3((hitFront.normal.x + hitRear.normal.x) / 2, (hitFront.normal.y + hitRear.normal.y) / 2, (hitFront.normal.z + hitRear.normal.z) / 2);

           
            var targetRotation = Quaternion.FromToRotation(transform.up, hitAveraged) * transform.rotation;
            //truckChassis.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }





    }





    //        averageNormalDirections.Add(normal);
    //
    //        for (int i = 0; i < averageNormalDirections.Count; i++)
    //        {
    //            averageNormalDirection += averageNormalDirections[i];
    //        }
    //
    //        averageNormalDirection /= averageNormalDirections.Count;









}
