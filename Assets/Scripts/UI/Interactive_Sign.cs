using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interactive_Sign : MonoBehaviour
{
    public bool showText;
    public string signText;
    private bool isShowingText;
    private Text signTextUI;

    [SerializeField]

    // Use this for initialization
	void Awake()
    {
        signTextUI = GameObject.Find("SignText").GetComponent<Text>();
    }

    void Update()
    {
        if (showText == true)
        {
            signTextUI.text = signText;
            isShowingText = true;
        }
        else if (showText == false)
        {
            if (isShowingText == true)
            {
                StartCoroutine("TextFade");
            }
            else if (isShowingText == false)
            {
                signTextUI.text = "";
            }
        }
    }

    IEnumerator TextFade()
    {
        yield return new WaitForSeconds(1);
        signTextUI.text = "";
        isShowingText = false;
        yield return false;
    }
}
