using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class Billboard : MonoBehaviour
{
    [Range(0.1f, 2f)]
    public float setYnessReduceFactor;
    public static float ynessReduceFactor;
    public bool reverseForward;
    private GameObject mainCam;


    void Start()
    {
        mainCam = GameObject.Find("Main Camera");
    }
    void Update()
    {
        if (mainCam == null)
        {
            mainCam = GameObject.Find("Main Camera");
        }
        ynessReduceFactor = setYnessReduceFactor;
        Vector3 cameraNoY = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y * setYnessReduceFactor, mainCam.transform.position.z);
        if (reverseForward == false)
        {
            transform.LookAt(cameraNoY, Vector3.up);
        }
        else if (reverseForward == true)
        {
            transform.LookAt(transform.position - cameraNoY, Vector3.up);
        }
    }
}

