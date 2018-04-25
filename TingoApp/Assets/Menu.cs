using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	//change scene

	GameObject levelObj;
	GameObject sliderObj;
	PedometerPlugin pedometerPlugin;

	int STEPS_PER_BERRY = 1000;
	int STEPS_PER_XP = 1700;

	public void changeScene(string loadScene){
		SceneManager.LoadScene(loadScene);
	}

	public void buyBerries(){
		pedometerPlugin = PedometerPlugin.GetInstance ();
		if((pedometerPlugin.GetTotalStep() - PlayerPrefs.GetInt("baseSubtract") - PlayerPrefs.GetInt("berrySpent") > STEPS_PER_BERRY)){
			PlayerPrefs.SetInt ("berries", PlayerPrefs.GetInt ("berries") + 1);
			PlayerPrefs.SetInt ("berrySpent", PlayerPrefs.GetInt ("berrySpent") + STEPS_PER_BERRY);
		}
	}

	public void Start(){
		

	}

	public void Update(){
		
		pedometerPlugin = PedometerPlugin.GetInstance ();

		levelObj = GameObject.Find ("level");
		sliderObj = GameObject.Find("xpSlider");

		if (levelObj != null && sliderObj != null) {
			//set level text from memory

			//int steps = pedometerPlugin.GetTotalStep() - PlayerPrefs.GetInt ("baseSubtract") - PlayerPrefs.GetInt("berrySpent");
			int stepsXP = pedometerPlugin.GetTotalStep() - PlayerPrefs.GetInt ("baseSubtract");

			int levelNumber = stepsXP / STEPS_PER_XP;
			float toNextLevel = (stepsXP % STEPS_PER_XP) / (float)STEPS_PER_XP;

			Text levelText = levelObj.GetComponent<Text> ();
			levelText.GetComponentInChildren<Text>().text = "" + levelNumber;

			//set xp slider fullness
			Slider xpSlider = sliderObj.GetComponent<Slider>();
			xpSlider.value = toNextLevel;
		}
	}
}
