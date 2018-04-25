using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Feed : MonoBehaviour {
	
	float berryValue = .1f;				//How much berries raise health by
	float healthDrainRate = .027f;		//How much health is lost per hour (this takes 36 houres to starve)

	int steps = 1375;				//This will be replaced with the actual steps that were gathered that day

	private PedometerPlugin pedometerPlugin;


	public void OnClick(){
		GameObject sliderObj = GameObject.Find("HealthSlider");
		GameObject feedObject = GameObject.Find ("berryCount");

		//sometimes there is an error and the GameObjects can't be found and are null
		if (sliderObj != null && feedObject != null) {
			Slider healthSlider = sliderObj.GetComponent<Slider>();
			var health = healthSlider.value;

			//tries to feed the Tingo if under full health
			if ((health + berryValue) < 1 && PlayerPrefs.GetInt("berries") > 0) {
				healthSlider.value = berryValue + health;
				PlayerPrefs.SetFloat("health", berryValue + health);
				PlayerPrefs.SetInt ("berries", PlayerPrefs.GetInt ("berries") - 1);
				//else tops off the Tingo's health
			} else {
				if (PlayerPrefs.GetInt ("berries") >= 1 && health < 1) {
					healthSlider.value = 1;
					PlayerPrefs.SetInt ("berries", PlayerPrefs.GetInt ("berries") - 1);
				}

			}
			//change text on berries button
			Text berriesText = feedObject.GetComponent<Text> ();
			feedObject.GetComponentInChildren<Text>().text = ""+PlayerPrefs.GetInt ("berries");
		}
	}

	public void changeScene(string loadScene){
		SceneManager.LoadScene(loadScene);
	}

	//Kill the Tingo when health is zero
	public void OnHealthChange(){
		GameObject sliderObj = GameObject.Find("HealthSlider");
		if (sliderObj != null) {
			Slider healthSlider = sliderObj.GetComponent<Slider>();
			var health = healthSlider.value;
			PlayerPrefs.SetFloat ("health", health);
			if (health <= 0) {
				//Check if app has been installed for the first time
				if (PlayerPrefs.GetInt ("hasRan") == 0) {
					PlayerPrefs.SetInt ("berries",5);
					PlayerPrefs.SetFloat ("xp", 0);
					PlayerPrefs.SetFloat ("health", 1);
					PlayerPrefs.SetInt ("level", 0);
					PlayerPrefs.SetInt ("steps", 0);
					PlayerPrefs.SetInt ("hasRan",1);
					healthSlider.value = 1;
				} else {
					PlayerPrefs.SetInt ("baseSubtract", pedometerPlugin.GetTotalStep ());
					changeScene ("death");
				}

			}
		}
	
	}

	//Runs each time game starts
	public void Start () {
		
		pedometerPlugin = PedometerPlugin.GetInstance ();
		pedometerPlugin.SetDebug (0);

		pedometerPlugin.Init ();
		pedometerPlugin.StartPedometerService (SensorDelay.SENSOR_DELAY_FASTEST);
		steps = pedometerPlugin.GetStepToday ();

		//get time as ascii value
		int d1 = DateTime.Now.Hour;
		System.DateTime epochStart = new System.DateTime(2018, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalHours;

		//check last time health was calculated
		float healthDeduction = PlayerPrefs.GetFloat ("health") - (healthDrainRate * (cur_time - PlayerPrefs.GetFloat ("timeOfLastHealthCheck")));

		//Steps should be zero if its a new day
		int oldSteps = PlayerPrefs.GetInt("steps");
		if (oldSteps > steps) {
			oldSteps = steps;
			PlayerPrefs.SetInt ("steps", steps);
		}

		//reduce health
		PlayerPrefs.SetFloat ("health", healthDeduction);
		PlayerPrefs.SetFloat ("timeOfLastHealthCheck", cur_time);

		//Load scene from PlayerPrefs
		GameObject sliderObj = GameObject.Find ("HealthSlider");
		if (sliderObj != null) {
			Slider healthSlider = sliderObj.GetComponent<Slider> ();
			healthSlider.value = PlayerPrefs.GetFloat ("health");
		} else {
			Debug.Log ("Slider is null");
		}

		//Load number of berries in button
		GameObject feedObject = GameObject.Find ("berryCount");
		if (feedObject != null) {
			Text berriesText = feedObject.GetComponent<Text> ();
			feedObject.GetComponentInChildren<Text>().text = ""+PlayerPrefs.GetInt ("berries");
		}


		//load slider
	}

}
	
