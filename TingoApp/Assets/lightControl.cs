using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Peter Yang Collab with Tingo
 * Controls the lamp
 */

public class lightControl : MonoBehaviour {
	private Light lightStatus;
	// Use this for initialization
	void Start () {
		lightStatus = GetComponent<Light> ();
	}
	//Turn on Light in Lamp
	public void turnOn(){
		lightStatus.enabled = true; //turn on light component 
	}
	//Turn off Light in Lamp
	public void turnOff(){
		lightStatus.enabled = false; //turn off light component
	}
	// Update is called once per frame
	void Update () {
		
	}
}
