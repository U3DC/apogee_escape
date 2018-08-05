using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGasGiantRotate : MonoBehaviour
{

    [ExecuteInEditMode]

    public float YRotateSpeed = 3f;
    public float XRotateSpeed = 0f;


    // Update is called once per frame
            void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        //transform.Rotate(Vector3.right * Time.deltaTime);

        // ...also rotate around the World's Y axis
        transform.Rotate(Vector3.up * Time.deltaTime * YRotateSpeed, Space.World);    
        transform.Rotate(Vector3.forward * Time.deltaTime * XRotateSpeed, Space.World);    

    }
}



	

