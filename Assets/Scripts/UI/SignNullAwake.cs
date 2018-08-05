using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignNullAwake : MonoBehaviour {

	private Text myText;
	// Use this for initialization
	void Awake () 
	{
		myText = gameObject.GetComponent<Text>();
		myText.text = "";
		myText.text = null;
	}

}
