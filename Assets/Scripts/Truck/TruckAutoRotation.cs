using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAutoRotation : MonoBehaviour
{
    private Vector3 averageNormalDirection;
    Ray frontHeightDetect;
    Ray rearHeightDetect;
    RaycastHit hitFront;
    RaycastHit hitRear;


    private float wheelBaseHalved;
    public float wheelBase = 3f;
    public float speed = 0.1f;

    void Start()
    {
        wheelBaseHalved = wheelBase / 2;
    }
    void Update()
    {
        Vector3 frontWheel = Vector3.forward * wheelBaseHalved;
        Vector3 rearWheel = Vector3.forward * -wheelBaseHalved;

        frontHeightDetect = new Ray(transform.position, Vector3.down);
        rearHeightDetect = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(frontHeightDetect, out hitFront, Mathf.Infinity) || Physics.Raycast(rearHeightDetect, out hitRear, Mathf.Infinity))
        {
            Vector3 hitAveraged = new Vector3((hitFront.normal.x + hitRear.normal.x) / 2, (hitFront.normal.y + hitRear.normal.y) / 2, (hitFront.normal.z + hitRear.normal.z) / 2);
            var targetRotation = Quaternion.FromToRotation(transform.up, hitAveraged) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }
    }
}
