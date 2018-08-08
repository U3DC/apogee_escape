using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidRockRandomizer : MonoBehaviour
{
    [Range(0.0f,1f)]
    public float scaleRandomization = 0.2f;
    [Range(0f,180f)]
    public float rotationRandomize = 180f;

    public Material[] rockTexture;
    public GameObject[] rockGameObjects;


    // Use this for initialization
    [ExecuteInEditMode]
    void Start()
    {
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.material = rockTexture[0];

        MeshFilter mf = gameObject.GetComponent<MeshFilter>(); // store a reference to the added component for future use
        mf.mesh = rockGameObjects[Random.Range(0, rockGameObjects.Length)].GetComponent<MeshFilter>().sharedMesh;

        gameObject.transform.localScale = new Vector3(
            100f + (100f * Random.Range(-scaleRandomization, scaleRandomization*2)), 
            100f + (100f * Random.Range(-scaleRandomization, scaleRandomization*2)), 
            100f + (100f * Random.Range(-scaleRandomization, scaleRandomization*2))
        );
        transform.rotation = Quaternion.Euler(
            Random.Range(-rotationRandomize, rotationRandomize),
            Random.Range(-rotationRandomize, rotationRandomize),
            Random.Range(-rotationRandomize, rotationRandomize)
        );
    }
}
