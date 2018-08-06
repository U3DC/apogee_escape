using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGasGiantRotate : MonoBehaviour
{
    [ExecuteInEditMode]
    public float YRotateSpeed = 3f;
    public float XRotateSpeed = 0f;

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * YRotateSpeed, Space.World);    
        transform.Rotate(Vector3.forward * Time.deltaTime * XRotateSpeed, Space.World);    
    }
}



	

