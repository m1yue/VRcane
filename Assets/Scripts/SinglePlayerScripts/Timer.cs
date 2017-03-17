using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/** Countdown timer for the start of Single Player. */
public class Timer : MonoBehaviour {

	// Instance vars used as help;
	private float myTimer = 10;
	private Text timerText;
	private GameObject monster;

	void Start () {

		// Set text
		timerText = GetComponent<Text> ();

		// Save Magma Demon-Orange monster to var for activation later
		monster = GameObject.Find ("Magma Demon-Orange");
		monster.SetActive (false);
	}
	
	void Update () {
		
		myTimer -= Time.deltaTime;
		// Print to screen countdown for last 5 seconds of myTimer
		if (myTimer <= 5) {
			timerText.text = "Battle Begins in...\n" + myTimer.ToString ("f0");

			if (myTimer <= 0) {
				monster.SetActive (true);
				GameObject.Find ("TimerText").SetActive (false);
			}
		}
	}
}
