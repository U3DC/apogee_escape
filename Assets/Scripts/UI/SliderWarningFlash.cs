using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderWarningFlash : MonoBehaviour
{

    private Slider mySlider;
    public Image imageToFlash;
    private Color originalColour;
    public Color flashColour;
    public float warningFloat = 0f;
    public float flashingRate = 0.5f;
    private bool isFlashing = false;

    void Start()
    {
        mySlider = GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {

        originalColour = imageToFlash.color;

        if (mySlider.value < warningFloat)
        {
            if (isFlashing == false)
            {
                StartCoroutine("WarningFlash");
            }

        }
        else if (mySlider.value > warningFloat)
        {

            if (isFlashing == true)
            {
                StopCoroutine("WarningFlash");
                isFlashing = false;

            }
        }
//        {
//            imageToFlash.color = originalColour;
//            StopCoroutine("WarningFlash");
//        }
//        else

    }

    IEnumerator WarningFlash()
    {
        while (true)
        {
            isFlashing = true;
            yield return new WaitForSeconds(flashingRate);
            imageToFlash.color = flashColour;
            yield return new WaitForSeconds(flashingRate);
            imageToFlash.color = originalColour;

        }

    }
}
