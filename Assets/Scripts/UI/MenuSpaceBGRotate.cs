﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpaceBGRotate : MonoBehaviour
{
    [ExecuteInEditMode]
    public float rotateSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        //transform.Rotate(Vector3.right * Time.deltaTime);

        // ...also rotate around the World's Y axis
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed, Space.World);    
    }
}





