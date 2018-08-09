using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairStatus : MonoBehaviour 
{

    public float shipRepairTarget = 100f;
    public float repairDroneEffectiveness = 30f;
    private float currentShipCondition = 0f;
    private float desiredShipConditon = 0f;
    public float lerpSpeed = 6f;


    public GameObject repairSlider;


	// Use this for initialization
	void Start () 
    {


	}
	
	// Update is called once per frame
	void Update () 
    {
        if (currentShipCondition >= shipRepairTarget)
        {
            Debug.Log("ship repair finished");
            currentShipCondition = shipRepairTarget;
        }

        currentShipCondition = Mathf.SmoothStep(currentShipCondition, desiredShipConditon, lerpSpeed * Time.deltaTime);
        repairSlider.GetComponent<Slider>().value = currentShipCondition;
        //repairSlider.GetComponent<Image>().fillAmount = currentShipCondition / shipRepairTarget;

	}

    public void repairDroneCompleted()
    {
        currentShipCondition += repairDroneEffectiveness;


    }

}
