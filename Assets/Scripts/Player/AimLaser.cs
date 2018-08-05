using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimLaser : MonoBehaviour
{

	[SerializeField]
	private LineRenderer myLR;
	[SerializeField]

//	private Vector3[] startingLineRendererPoints = null;

	public static Vector3 gunLaserOrigin = new Vector3(0f,0.5f,0f);
	public float gunLaserLength = 8f;
	private GameObject laserHitSpot;

	// Use this for initialization
	void Start()
	{
		myLR = GetComponent<LineRenderer>();

		myLR.positionCount = 2;
	//	startingLineRendererPoints = new Vector3[2];
		myLR.SetPosition(1, Vector3.zero);
		myLR.SetPosition(1, Vector3.forward * 10);

		laserHitSpot = GameObject.Find("laserHit");
	}
	
	// Update is called once per frame
	void Update()
	{
		//Cast ray from player in forward direction
		Ray ray = new Ray(transform.position, transform.forward);
		//If ray hits an object
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider)
			{
				Vector3 local_point = transform.InverseTransformPoint(  hit .point);
				myLR.SetPosition(0, gunLaserOrigin);
				//myLR.SetPosition(1, hit.point-gameObject.transform.position);
				myLR.SetPosition(1, local_point);

				laserHitSpot.SetActive(true);

				//TODO: make the laserHitSpot always visibile when hitting coliders 
				//somehow make it come back towards the player/gunLaserOrigin by a qtr of a unit or smth

				//laserHitSpot.transform.LookAt(gameObject.transform);
				laserHitSpot.transform.position = hit.point;



				//Debug.Log(hit.distance);


//				Debug.DrawLine
//				(
//					hit.point, 
//					new Vector3
//					(
//						hit.point.x-(gunLaserOrigin.x * 0.1f),
//						hit.point.y-(gunLaserOrigin.y * 0.1f),
//						hit.point.z
//					)
//					,
//					Color.cyan
//				);
			}
		}
		else
		{
			myLR.SetPosition(0, gunLaserOrigin);
			myLR.SetPosition(1, gunLaserOrigin+new Vector3(0,0,gunLaserLength));

			laserHitSpot.SetActive(false);
		}
	}
}
