using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Peter Yang Collab with Tingo
 * Main Script for day and night cycle
 * 
 */


public class dayNightCycle : MonoBehaviour {
	public Light lt; //Controls the light in directional light.
	public windowStatus window; //Controls the window 
	public lightControl light; //Controls the lamp
	public cameraBackground cam; //Controls the overall background

	// Use this for initialization
	void Start () {
		lt = GetComponent<Light> (); //initalize

	}
	//Slowly transition into nighttime
	void dark(){
		if (lt.color.r < 0 && lt.color.b < 0 && lt.color.g < 0 && lt.color.a < 0) {
			light.turnOn (); //once light is fully dark turn on lamp.
		} else {
			lt.color -= Color.white / 5.0F * Time.deltaTime; //slowly dim the directional lights
		}
	}
	//Slowly transition into daytime.
	void normal(){
		if (lt.color.r > 0.780 && lt.color.b > 0.881 && lt.color.g > 1 && lt.color.a > 0.780) {
			light.turnOff (); //once fully daytime turn off the lamp
		} else {
			lt.color += Color.white / 5.0F * Time.deltaTime; //slowly turn on the directional lights
		}
	}
	// Update is called once per frame
	void Update () {
		var currentTime = System.TimeSpan.Parse (System.DateTime.Now.ToString ("HH:mm:ss")); //Get current realtime
		var nightTime = System.TimeSpan.Parse ("18:59:50"); // 7:00pm;
		var dayTime = System.TimeSpan.Parse ("6:59:50"); // 7:00am
		/**
		 * Checks real time to see if it is night time 7pm-6:59am.
		 */
		if (currentTime > nightTime || currentTime < dayTime) { 
			//Starts night time functions.
			dark ();
			window.night(); //windows turns darker
			cam.nightTime (); //overall background turns darker
		} 
		/*
		 * Checks real-time to see if it is day time 7am-6:59pm.
		 */
		if(currentTime < nightTime && currentTime > dayTime){
			//Starts day time functions
			normal ();
			window.day (); //windows returns to normal
			cam.dayTime (); //overall background returns to normal.
		}
	}
}
