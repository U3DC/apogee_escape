using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckParentForText : MonoBehaviour
{
    [SerializeField]
    private Text myChildText;
    [SerializeField]
    private Image myImage;
    public float textPanelAlpha = 1f;

    void Start()
    {
        myChildText = GetComponentInChildren<Text>();
        myImage = GetComponent<Image>();
        myImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (myChildText.text == null || myChildText.text == "")
        {
            myImage.enabled = false;
        }
        else if (myChildText.text != null)
        {
            myImage.enabled = true;
        }
    }
}
