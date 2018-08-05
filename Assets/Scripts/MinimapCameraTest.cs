using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraTest : MonoBehaviour {

    private GameObject target;

	// Use this for initialization
	void Start () {

        target = GameObject.Find("player");

	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position = new Vector3(target.gameObject.transform.position.x, gameObject.transform.position.y, target.gameObject.transform.position.z);

	}
}
