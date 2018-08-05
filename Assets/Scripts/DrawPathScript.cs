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





        //for (int i = 0; i < path_objs.Count; i++)
        //{
        //   myLR.SetPositions(path_objs[);
        myLR.positionCount = theArray.Length;

        //myLR.SetPositions(theV3Array);

        for (int i = 0; i < theArray.Length; i++)
        {
//            foreach (LineRenderer r in myLR)
            //myLR.SetPosition(i, theArray[i].position);
            myLR.SetPosition(i, new Vector3(theArray[i].position.x,theArray[i].position.y+verticalOffset,theArray[i].position.z));

        }

        // }

    }

}
