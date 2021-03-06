﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPointerUI : MonoBehaviour
{

    public GameObject pointTowards;
    public GameObject player;
	
    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.LookAt(ship.transform);
        //Vector3 newDir = Vector3.RotateTowards(player.transform.position, ship.transform.position, 1f * Time.deltaTime, 0.0f);

        Vector3 newDir = pointTowards.transform.position - player.transform.position;
        Quaternion rotation = Quaternion.LookRotation(newDir) ;
        rotation *= Quaternion.Euler(0, 0, 90);

        gameObject.transform.rotation = (rotation) ;
        //gameObject.transform.rotation = Quaternion.Euler(newDir.x, newDir.y, newDir.z + 90f);

    }
}
