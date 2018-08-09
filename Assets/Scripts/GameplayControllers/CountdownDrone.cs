using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownDrone : MonoBehaviour
{


    public float droneSecondsToBuld = 30f;
    public GameObject droneProgressSlider;
    public GameObject readyImage;
    public float lerpSpeed = 6f;
    [SerializeField]
    private float currentDroneProgress;
    private float desiredDroneProgress;

    void Start()
    {
        currentDroneProgress = 0f;  
        currentDroneProgress = 0f;
        //droneProgressSlider.GetComponent<Slider>().maxValue = droneSecondsToBuld;
        droneProgressSlider.GetComponent<Image>().fillAmount = 0f;;
        BeginDroneCountup();
    }


    void BeginDroneCountup()
    {
        StartCoroutine("DroneCountup");
    }

    IEnumerator DroneCountup()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (currentDroneProgress >= droneSecondsToBuld || currentDroneProgress >= droneSecondsToBuld)
            {
                currentDroneProgress = droneSecondsToBuld;
                desiredDroneProgress = droneSecondsToBuld;

            }
            else
            {
                desiredDroneProgress++;
                readyImage.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (currentDroneProgress < 0)
        {
            Debug.Log("how did we get negative drone progress?");
        }
        else if(currentDroneProgress >= droneSecondsToBuld)
        {
            DroneReadyAlert();
        }

        currentDroneProgress = Mathf.SmoothStep(currentDroneProgress, desiredDroneProgress, lerpSpeed * Time.deltaTime);
        //droneProgressSlider.GetComponent<Slider>().value = currentDroneProgress;
        droneProgressSlider.GetComponent<Image>().fillAmount = currentDroneProgress / droneSecondsToBuld;

    }
    void DroneReadyAlert()
    {
        readyImage.SetActive(true);
        //TODO: Voiceover cue.
        StopCoroutine("DroneCountup");
    }

    public void DroneAssigned()
    {
        currentDroneProgress = 0;
        desiredDroneProgress = 0;
        droneSecondsToBuld += 5f;
        StartCoroutine("DroneCountup");
    }
}
