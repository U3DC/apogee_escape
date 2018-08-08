using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRockGenerator : MonoBehaviour 
{


    public float splashArea = 9.9f;

    public GameObject rainSplash;



    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(new Vector3(
        transform.position.x + Random.Range(-splashArea,splashArea), 
        transform.position.y,
        transform.position.z + Random.Range(-splashArea,splashArea)),
        Vector3.down);




        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        if (hit.collider != null)

        {
            {
                //Instantiate(rainSplash, new Vector3(hit.point.x,hit.point.y+0.1f,hit.point.z), Quaternion.LookRotation(Vector3.up, hit.normal),gameObject.transform);
                Instantiate(rainSplash, hit.point, Quaternion.LookRotation(Vector3.up, hit.normal));

            }
        }


    }
}
