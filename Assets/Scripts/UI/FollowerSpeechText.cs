using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FollowerSpeechText : MonoBehaviour
{

    public GameObject speechBubbleObj;
    public Text speechBubble;
    private string speechText;
    public string[] _Jump;
    [Range(1f, 100f)]
    public float _JumpChance = 1f;

    public string[] _Catchup;
    [Range(1f, 100f)]
    public float _CatchupChance = 1f;
    private Camera virtualCamera;

    void Start()
    {
        virtualCamera = GameObject.Find("VirtualCamera").GetComponent<Camera>();
    }

    void Update()
    {
        //TODO: make this work screenspace canvas and virtual camera
        //Vector3 speechPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //Vector3 speechPos = virtualCamera.WorldToScreenPoint(this.transform.position);
        //Vector2 speechPos = RectTransformUtility.WorldToScreenPoint(virtualCamera,gameObject.transform.position);

        //Vector3 speechPos = virtualCamera.WorldToViewportPoint(gameObject.transform.position);
        //this.GetComponent<RectTransform>().anchoredPosition.Set(roboScreenPos.x, roboScreenPos.y + 1f);
        //Vector3 speechPos = virtualCamera.WorldToViewportPoint(gameObject.transform.TransformPoint(gameObject.transform.position));
        Vector3 speechPos = virtualCamera.WorldToScreenPoint(gameObject.transform.position);
        //speechBubble.rectTransform.anchorMax = speechPos;
        //speechBubble.rectTransform.anchorMin = speechPos;
        //speechBubble.rectTransform.anchoredPosition = speechPos;
        speechBubbleObj.transform.position = speechPos;
        //speechBubble.transform.position = speechPos;
    }

    public void followerToSay(string _sayThis)
    {
        //TODO: replace with a switch
        StopCoroutine("textTimeOut");
        if (_sayThis == "Jump")// && Random.Range(0,100) > _JumpChance)
        {
            speechText = _Jump[Random.Range(0, _Jump.Length)];
        }
        if (_sayThis == "Catchup")// && Random.Range(0,100) > _CatchupChance)
        {
            speechText = _Catchup[Random.Range(0, _Catchup.Length)];
        }
        else
        {
            speechText = "...";
        }
        speechBubble.text = speechText;
        StartCoroutine("textTimeOut");
    }

    IEnumerator textTimeOut()
    {
        yield return new WaitForSeconds(2f);
        speechBubble.text = "";
        yield  break;
    }
}
