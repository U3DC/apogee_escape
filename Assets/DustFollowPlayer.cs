using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustFollowPlayer : MonoBehaviour {

    public GameObject player;
    private int frame60 = 0;

	// Update is called once per frame
	void Update () 
    {

        frame60++;

        if (frame60 > 60)
        {
            frame60 = 0;
            gameObject.transform.position = player.transform.position;
        }

	}
}
