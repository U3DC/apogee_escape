using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpriteRandomizer : MonoBehaviour {

    public Sprite[] rockArray;

	// Use this for initialization
	void Start () 
    {
        SpriteRenderer mySR = gameObject.GetComponent<SpriteRenderer>();
        mySR.sprite = rockArray[Random.Range(0,rockArray.Length)];
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
}
