using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColor : MonoBehaviour {
	public Color blue = new Color32 (15, 133, 203, 255);

	bool clicked = false;
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			GetComponent<Renderer>().materials[0].color = Color.green; //modify 2nd material to normal
		}
	}
}
