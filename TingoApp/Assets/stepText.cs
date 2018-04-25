using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class stepText : MonoBehaviour {

	public Text myText;

	private PedometerPlugin pedometerPlugin;

	//private string demoName = "[PedometerDemo] ";
	private UtilsPlugin utilsPlugin;

	bool isReady = false;

	// Use this for initialization
	void Start () {
		//get the instance of pedometer plugin
		pedometerPlugin = PedometerPlugin.GetInstance ();
		isReady = true;

//		pedometerPlugin.OnStepCount(OnStepCount,OnStepDetect);

//		//set to zero to hide debug toast messages
//		pedometerPlugin.SetDebug (0);
//
//		utilsPlugin = UtilsPlugin.GetInstance ();
//		utilsPlugin.SetDebug (0);
//
//		//check if step detector is supported
//		bool hasStepDetector = utilsPlugin.HasStepDetector ();
//		bool hasStepCounter = utilsPlugin.HasStepCounter ();
//
//		//if (hasStepDetector) {
//		if (hasStepCounter) {
//			//UpdateStepDetectorStatus ("available");
//			// event listeners
//			//AddEventListeners ();
//			//initialze pedometer
//			pedometerPlugin.Init ();
//			//pedometerPlugin.StartPedometerService (SensorDelay.SENSOR_DELAY_FASTEST);
//			isReady = true;
//
//
//
//		} else {
//			//UpdateStepDetectorStatus ("not available");
//		}
	}

	private void OnStepCount(int count){
		if(isReady)myText.text = "" + count;
		Debug.Log("OnStepCount count " + count);
	}

	//step detect event is triggered
	private void OnStepDetect(){
		//if(isReady)myText.text = "" + count;
		Debug.Log("OnStepDetect");
	}
	
	// Update is called once per frame
	void Update () {
		int t = (pedometerPlugin.GetTotalStep() - PlayerPrefs.GetInt("baseSubtract") - PlayerPrefs.GetInt("berrySpent"));
		if (t < 0)
			t = 0;
		if(isReady)myText.text = "" + t;
	}
}
