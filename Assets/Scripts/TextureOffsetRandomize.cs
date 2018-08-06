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
        offsetMultiplier = Mathf.Round(Random.Range(0f, numberOfSprites-1));
        //Debug.Log(offsetMultiplier);
        rend.material.mainTextureOffset = new Vector2((1f / numberOfSprites) * offsetMultiplier, 0f);
        rend.material.mainTextureScale = new Vector2(1f / numberOfSprites, 1f);
    }
}

