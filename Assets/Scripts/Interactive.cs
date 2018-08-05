using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interactive : MonoBehaviour
{
	public bool playersFocus;
	private BoundBox box;
	private Interactive_Sign sign;
	private bool isSign = false;
	private bool isButton = false;
	private bool isDoor = false;

	void Start()
	{
		box = GetComponent<BoundBox>();
		if (gameObject.GetComponent<Interactive_Sign>() != null)
		{
			sign = GetComponent<Interactive_Sign>();
			isSign = true;
			isButton = false;
			isDoor = false;
		}
		else
		{
			isSign = false;
			isButton = false;
			isDoor = false;
		}
	}

	void Update()
	{
		if (playersFocus == true)
		{
			box.enabled = true;
			if (isSign == true)
			{
			sign.showText = true;
			}
		}
		else if (playersFocus == false)
		{
			box.enabled = false;
			sign.showText = false;
		}
	}


}
