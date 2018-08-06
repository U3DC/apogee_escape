using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraTest : MonoBehaviour
{
    private GameObject target;

    void Start()
    {
        target = GameObject.Find("player");
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(target.gameObject.transform.position.x, gameObject.transform.position.y, target.gameObject.transform.position.z);
    }
}
