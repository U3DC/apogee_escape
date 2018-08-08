using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBob : MonoBehaviour {

    public float bobVelocity = 20f;
    public float bobAmount = 0.2f;
    private Vector3 itemFlashlightPos;
	// Use this for initialization
	void Start () 
    {
        itemFlashlightPos = gameObject.transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () 
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,
            itemFlashlightPos.y + Mathf.Clamp((Mathf.Sin(Time.time * bobVelocity)), bobAmount * -10, bobAmount * 10) * bobAmount,
            gameObject.transform.localPosition.z);
	}
}
