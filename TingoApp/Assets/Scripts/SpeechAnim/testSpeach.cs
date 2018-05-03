using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnitySentiment;

public class testSpeach : MonoBehaviour {
	private UnityAction<string> action;
	public SentimentAnalysis predictionObject;
	string input = "";
	private Rigidbody getBody;

	public AudioClip yay;
	public AudioClip surpassGreatness;
	public AudioClip aww;
	public AudioClip wee;
	public AudioClip hmm;

	private AudioSource sourceWee;
	private AudioSource sourceAww;
	private AudioSource sourceSurpass;
	private AudioSource sourceYay;
	private AudioSource sourceHmm;

	//used for animations
	float jumpTime = .7f;
	float jumpHeight = 4.0f;
	float realHeight = .5947088f;

	//used for sentiment analysis
	private bool threadStarted = false;
	private bool responseFromThread = false;
	private Vector3 SentimentAnalysisResponse;

	void Awake(){
		Application.runInBackground = true;
		action = new UnityAction<string>(ToTextAction);
		predictionObject.Initialize ();
	}

	void OnEnable(){
		SentimentAnalysis.OnErrorOccurs += Errors;
		sourceYay = GetComponent<AudioSource> ();
		sourceWee = GetComponent<AudioSource> ();
		sourceSurpass = GetComponent<AudioSource> ();
		sourceAww = GetComponent<AudioSource> ();
		sourceHmm = GetComponent<AudioSource> ();
		SentimentAnalysis.OnAnlysisFinished += GetAnalysisFromThread;
		EventManager.StartListening ("Speech", action);

		//for testing purposes 
//		predictionObject.PredictSentimentText ("good");
//		if (!threadStarted) {// Thread Started
//			threadStarted = true;
//			StartCoroutine (WaitResponseFromThread ());
//		}
	}

	void OnDisable(){
		EventManager.StopListening ("Speech", action);
	}

	void ToTextAction(string newText){
		string[] words = newText.Split (' ');
		int surpass = 0;
		int greatness = 0;
		int jump = 0;
		int flip = 0;
		int speak = 0;
		int spin = 0;
		int red = 0;
		int blue = 0;
		int green = 0;
		int pink = 0;
		int grey = 0;

		foreach (string x in words) {
			if (x == "jump") {
				jump++;
			} else if (x == "flip") {
				flip++;
			} else if (x == "yell" || x == "speak") {
				speak++;
			} else if (x == "surpass") {
				surpass++;
			} else if (x == "greatness") {
				greatness++;
			} else if (x == "blue") {
				blue++;
			} else if (x == "green") {
				green++;
			} else if (x == "red") {
				red++;
			} else if (x == "pink") {
				pink++;
			} else if (x == "grey") {
				grey++;
			}
		}
		if (speak > flip && speak > jump && speak > spin)
			input = "speak";
		else if (flip > jump && flip > speak && flip > spin)
			input = "flip";
		else if (jump > flip && jump > speak && jump > spin)
			input = "jump";
		else if (spin > flip && spin > speak && spin > jump)
			input = "spin";
		else if (greatness == 1 && surpass == 1)
			input = "surpass";
		
		else if (blue > 0)
			input = "blue";
		else if (pink > 0)
			input = "pink";
		else if (green > 0)
			input = "green";
		else if (red > 0)
			input = "red";
		else if (grey > 0)
			input = "grey";


		else {
			predictionObject.PredictSentimentText (newText);
			if (!threadStarted) {// Thread Started
				threadStarted = true;
				StartCoroutine (WaitResponseFromThread ());
			}
		}
	}

	private void GetAnalysisFromThread(Vector3 analysisResult)
	{		
		SentimentAnalysisResponse = analysisResult;
		responseFromThread = true;
		//trick to call method to the main Thread
	}

	private void chooseResponse(){
		// x Vector = positve
		// y Vector = negative
		// z Vector = neutral
		Debug.Log("Got to choose Response");
		if (SentimentAnalysisResponse.x > SentimentAnalysisResponse.y && SentimentAnalysisResponse.x > SentimentAnalysisResponse.z) {
			Debug.Log ("This is a postive response");
			input = "positive";
		} else if (SentimentAnalysisResponse.y > SentimentAnalysisResponse.x && SentimentAnalysisResponse.y > SentimentAnalysisResponse.z) {
			Debug.Log ("This is a negative response");
			input = "negative";
		} else if (SentimentAnalysisResponse.z > SentimentAnalysisResponse.y && SentimentAnalysisResponse.z > SentimentAnalysisResponse.x) {
			Debug.Log ("This is a neutral response");
			input = "speak";
		} 
	}
		
	// Use this for initialization

	void Update(){
		if (input == "jump") {
			StartCoroutine(Jump ());
		}
		if (input == "flip") {
			Flip ();
		}
		if (input == "speak") {
			sourceYay.PlayOneShot(yay);
		}
		if (input == "positive") {
			chooseAction ();
//			SentimentAnalysis.OnAnlysisFinished -= GetAnalysisFromThread;
//			SentimentAnalysis.OnErrorOccurs -= Errors;
		}
		if (input == "negative") {
			chooseAudioNegative ();
			iTween.ShakePosition (gameObject, iTween.Hash ("x", 0.5f, "time", 1.0f));
//			SentimentAnalysis.OnAnlysisFinished -= GetAnalysisFromThread;
//			SentimentAnalysis.OnErrorOccurs -= Errors;
		}
		if (input == "surpass") {
			sourceSurpass.PlayOneShot (surpassGreatness);
		}
		if (input == "spin") {
			Spin ();
		}

		if (input == "blue")
			GetComponent<Renderer> ().materials [0].color = Color.blue;
		if (input == "pink")
			GetComponent<Renderer> ().materials [0].color = Color.magenta;
		if (input == "green")
			GetComponent<Renderer> ().materials [0].color = Color.green;
		if (input == "red")
			GetComponent<Renderer> ().materials [0].color = Color.red;
		if (input == "grey")
			GetComponent<Renderer> ().materials [0].color = Color.grey;

		input = null;
	}


	private IEnumerator WaitResponseFromThread()
	{
		while(!responseFromThread) // Waiting For the response
		{
			yield return null;
		}
		// Main Thread Action
		chooseResponse();
		// Reset
		responseFromThread = false;
		threadStarted = false;
	}


	IEnumerator Jump(){
		float timer = 0.0f;
		while (timer <= jumpTime) {
			float height = Mathf.Sin (timer / jumpTime * Mathf.PI) * jumpHeight;
			transform.localPosition = new Vector3(transform.localPosition.x, height, transform.localPosition.z);
			timer += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = new Vector3(transform.localPosition.x, -0.52f, transform.localPosition.z);
	}

	void Flip(){
		iTween.RotateBy (gameObject, iTween.Hash("x", -.5, "speed", 125, "delay", .001));
		StartCoroutine (Jump ());
	}
	void Spin(){
		iTween.RotateBy (gameObject, iTween.Hash("y", 1, "speed", 125, "delay", .001, "looptype", iTween.LoopType.none));
	}

	void chooseAction(){
		float pickNum = Random.Range (0f, 10.0f);
		if (pickNum <= 3.3f) {
			chooseAudioPositive ();
			StartCoroutine (Jump ());
		} else if (pickNum > 3.3f && pickNum <= 6.6f) {
			chooseAudioPositive ();
			Spin ();
		} else {
			chooseAudioPositive ();
			Flip ();
		}
	}

	void chooseAudioPositive(){
		float pickNum = Random.Range (0f, 10.0f);
		if (pickNum >= 5.0f) {
			sourceYay.PlayOneShot (yay);
		} else {
			sourceWee.PlayOneShot (wee);
		}
	}

	void chooseAudioNegative(){
		float pickNum = Random.Range (0f, 10.0f);
		if (pickNum >= 5.0f) {
			sourceHmm.PlayOneShot (hmm);
		} else {
			sourceAww.PlayOneShot (aww);
		}
	}

	private void Errors(int errorCode, string errorMessage)
	{
		Debug.Log(errorMessage + "\nCode: " + errorCode);
	}
}
