using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Peter Yang Collab with Tingo
 * Controls the window
 */

public class windowStatus : MonoBehaviour {
	public Color32 nightColor = new Color32(0,24,72,255); //dark blue
	public Color32 dayColor = new Color32 (15, 133, 203, 255); //default blue
	float time = 0.0f; //Starting position for Lerp
	float modifier = 0.15f; //How fast you want the transition
	// Use this for initialization
	void Start () {
		
	}

	public void day ()
	{
		if (gameObject.tag == "Window") { //get window object that has tag of window
			time += Time.deltaTime * modifier; //control the amount of time it needs to switch colors
			time = Mathf.Clamp01 (time); 
			GetComponent<Renderer>().materials[1].color = Color32.Lerp(nightColor,dayColor,time); //modify 2nd material to normal

		}
	}

	public void night(){
		if (gameObject.tag == "Window") { //get window that has tag of window
			time -= Time.deltaTime * modifier; //control the amount of time it needs to switch colors
			time = Mathf.Clamp01 (time);
			GetComponent<Renderer>().materials[1].color = Color32.Lerp(nightColor,dayColor,time); //modify 2nd material to dark blue
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
