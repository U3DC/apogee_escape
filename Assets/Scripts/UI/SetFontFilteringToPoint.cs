using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SetFontFilteringToPoint : MonoBehaviour
{
    private Text[] theTexts;

    void Start ()
    {
        theTexts = gameObject.GetComponentsInChildren<Text>();

        foreach (Text theTexts in theTexts)
        {
            theTexts.font.material.mainTexture.filterMode = FilterMode.Point;
        }
    }
}