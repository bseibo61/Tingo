using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Peter Yang Collab with Tingo
 * Controls the camera background color
 */

public class cameraBackground : MonoBehaviour {
	public Camera cBackground;

	public Color32 nightColor = new Color32(0,24,72,255);  //dark blue
	public Color32 dayColor = new Color32 (229, 229, 229, 255); //default blue

	float time = 0.0f; //Starting position for Lerp
	float modifier = 0.15f; //How fast you want the transition
	// Use this for initialization
	void Start () {
		cBackground = GetComponent<Camera> ();
	}

	public void nightTime(){
		time -= Time.deltaTime * modifier;//control the amount of time it needs to switch colors
		time = Mathf.Clamp01 (time);
		cBackground.backgroundColor = Color32.Lerp(nightColor,dayColor,time); //modify 2nd material to dark blue
	}

	public void dayTime(){
		time += Time.deltaTime * modifier;//control the amount of time it needs to switch colors
		time = Mathf.Clamp01 (time);
		cBackground.backgroundColor = Color32.Lerp(nightColor,dayColor,time); //modify 2nd material to normal
	}
	// Update is called once per frame
	void Update () {
		
	}
}
