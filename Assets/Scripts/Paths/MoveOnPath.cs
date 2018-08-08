using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPath : MonoBehaviour
{
    public EditorPathScript pathToFollow;

    public int currentWaypoint = 0;
    public float movementSpeed;
    public float rotationSpeed = 5.0f;
    private float reachDistance = 1.0f;
    public string pathName;

    Vector3 lastPosition;
    Vector3 currentPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(pathToFollow.path_objs[currentWaypoint].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, pathToFollow.path_objs[currentWaypoint].position, Time.deltaTime * movementSpeed);

        var rotation = Quaternion.LookRotation(pathToFollow.path_objs[currentWaypoint].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        if (distance <= reachDistance)
        {
            currentWaypoint++;
        }

        if (currentWaypoint >= pathToFollow.path_objs.Count)
        {
            currentWaypoint = 0;
        }
    }
}
