using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPathScript : MonoBehaviour
{
    [ExecuteInEditMode]
    [SerializeField]
    Transform[] theArray;
    public float verticalOffset = 2f;
    private LineRenderer myLR;

    void Start()
    {
        theArray = GetComponentsInChildren<Transform>();
        myLR = GetComponent<LineRenderer>();
        myLR.positionCount = theArray.Length;
        for (int i = 0; i < theArray.Length; i++)
        {

            myLR.SetPosition(i, new Vector3(theArray[i].position.x, theArray[i].position.y + verticalOffset, theArray[i].position.z));
        }
    }
}
