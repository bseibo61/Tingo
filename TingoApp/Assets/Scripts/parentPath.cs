using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentPath : MonoBehaviour {
	double getPositionX, getPositionZ;
	bool check;

	// Use this for initialization
	void Start () {
		Debug.Log (transform.position.x);
		Debug.Log (getPositionX);
		goToPositionTwo ();
	}

	void Update(){
		getPositionX = transform.position.x;
		getPositionZ = transform.position.z;
		if ((getPositionX < -4.095 && getPositionX > -4.111) && (getPositionZ > 1.61 && getPositionZ < 1.65)) {
			goToPositionTwo ();
		}

		if ((getPositionX < 5.30 && getPositionX > 5.10) && (getPositionZ > 2.7 && getPositionZ < 2.9)) {
			goToPositionThree ();
		}

		if ((getPositionX < -8.35 && getPositionX > -8.409) && (getPositionZ > -3.38 && getPositionZ < -3.0)) {
			goToPositionOne ();
		}
	}

	void goToPositionThree(){
		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath ("pathTwo"), "speed", 4, "delay", 2, "orientToPath", true));
//		positionThree ();
//		Debug.Log("go to position three");
	}

	void goToPositionTwo(){
		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath ("pathOne"), "speed", 4, "delay", 2, "orientToPath", true));
//		StartCoroutine (waitTime ());
//		Debug.Log("go to position two");
	}
//
	void goToPositionOne(){
		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath ("pathThree"), "speed", 4, "delay", 2, "orientToPath", true));	
//		Debug.Log("go to position one");
	}

}
