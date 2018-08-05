using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenRendererToggle : MonoBehaviour {

	private Renderer[] rs;
	public bool enableRenderers;
	// Use this for initialization
	void Start () 
	{
		Renderer[] rs = GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rs)
			r.enabled = enableRenderers;

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
