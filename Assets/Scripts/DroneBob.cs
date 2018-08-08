using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBob : MonoBehaviour {

    public float bobVelocity = 1f;
    public float bobAmount = 0.2f;
    private Vector3 originalPos;
	// Use this for initialization
	void Start () 
    {
        originalPos = gameObject.transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () 
    {
        gameObject.transform.localPosition = new Vector3(
            gameObject.transform.localPosition.x,
            originalPos.y + Mathf.Clamp((Mathf.Sin(Time.time * bobVelocity)), bobAmount * -10, bobAmount * 10) * bobAmount,
            gameObject.transform.localPosition.z);
	}
}
