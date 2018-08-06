using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class Billboard : MonoBehaviour
{
    [Range(0.1f, 2f)]
    public float setYnessReduceFactor;
    public static float ynessReduceFactor;
    private int frame5;
    public bool reverseForward;

    void Update()
    {
        ynessReduceFactor = setYnessReduceFactor;
        Vector3 cameraNoY = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y * setYnessReduceFactor, Camera.main.transform.position.z);
        if (reverseForward != true)
        {
            transform.LookAt(cameraNoY, Vector3.up);
        }
        else if (reverseForward == true)
        {
            transform.LookAt(transform.position - cameraNoY, Vector3.up);
        }
    }
}

