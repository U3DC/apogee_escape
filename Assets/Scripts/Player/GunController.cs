using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour 
{

	private Vector3 firePoint;
	private bool readyToFire;
	private Light myLight;
	private ParticleSystem shellPS;
	// Use this for initialization
	void Start () 
	{
		firePoint = AimLaser.gunLaserOrigin;
		readyToFire = true;
		myLight = gameObject.GetComponent<Light>();
		myLight.enabled = false;
		shellPS = gameObject.GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update()
	{
		if (readyToFire == true)
		{
			if (Input.GetKey(KeyCode.Z))
			{
				Shoot();
			}	
		}
	}
	void Shoot()
	{
	readyToFire = false;

	//TODO: fire bullet
	StartCoroutine("muzzleLightFlash");
	shellPS.Emit(1);

	StartCoroutine("fireWait");
	}
	IEnumerator fireWait()
	{
	 yield return new WaitForSeconds(1);
	 readyToFire = true;
	 yield return false;
	}

	IEnumerator muzzleLightFlash()
	{
		myLight.enabled = true;
		yield return new WaitForSeconds(0.025f);
		myLight.enabled = false;
		yield return false;
	}
}
