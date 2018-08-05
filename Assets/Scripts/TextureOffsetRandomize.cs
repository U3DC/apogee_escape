using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOffsetRandomize : MonoBehaviour
{
    public int numberOfSprites;
    [SerializeField]
    private float offsetMultiplier;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        //offsetMultiplier =  Mathf.Round(Random.Range(0f, numberOfSprites) * 10) / 10;

        offsetMultiplier = Mathf.Round(Random.Range(0f, numberOfSprites-1));

        Debug.Log(offsetMultiplier);

        rend.material.mainTextureOffset = new Vector2((1f / numberOfSprites) * offsetMultiplier, 0f);
        rend.material.mainTextureScale = new Vector2(1f / numberOfSprites, 1f);
    }
    void Update()
    {



    }
}


//{
//
//    [ExecuteInEditMode]
//    public int numberOfSprites;
//    [SerializeField]
//    private float offsetMultiplier;
//    private Renderer rend;
//
//    void Start()
//    {
//        rend = GetComponent<Renderer>();
//        offsetMultiplier = Random.Range(0, numberOfSprites);
//    }
//    void Update()
//    {
//        rend.material.mainTextureOffset = new Vector2((1 / numberOfSprites) * offsetMultiplier, 0);
//        rend.material.mainTextureScale = new Vector2(1 / numberOfSprites, 0);
//    }
//}