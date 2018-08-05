

using UnityEngine;
 using System.Collections;
 
 public class AnimationAutoDestroy : MonoBehaviour {

     // Use this for initialization
     void Start () 
     {
		//destroy self after animation ends
         Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 

         //randomize angle
	     Vector3 euler = transform.eulerAngles;
	     euler.z = Random.Range(0f, 360f);
	     transform.eulerAngles = euler;
     }
 }