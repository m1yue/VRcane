using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip tele;
	public AudioClip zap;
	public AudioClip punch;
	public AudioClip loseSound;

	AudioSource musicAudio;
	AudioSource soundAudio;

	public bool isPaused;
	public bool isTeleport;
	public bool isParalyze;
	public bool punched;
	public bool youLost;

	// Use this for initialization
	void Start () {
		musicAudio = GetComponent<AudioSource> ();
		soundAudio = GetComponent<AudioSource> ();
	}

	public void pauseBGMusic() {
		isPaused = true;
	}

	public void unPauseBGMusic () {
		isPaused = false;
 		musicAudio.UnPause();
	}
		

	public void setTeleport() {
		isTeleport = true;
	}

	public void setParalyze() {
        isParalyze = true;
	}

	public void gotPunched() {
		punched = true;
	}

	public void setLoser() {
		isPaused = true;
		youLost = true;
	}

	public void playLoseSound() {
		soundAudio.PlayOneShot (loseSound);
	}

	// Update is called once per frame
	void Update() {
		if (isPaused) {
			musicAudio.Pause ();

		    if (youLost) {
				playLoseSound ();
				isPaused = false;
			}

		}

		if (isTeleport) {
			soundAudio.PlayOneShot (tele);
			isTeleport = false;
		}

		if (isParalyze) {
			soundAudio.PlayOneShot (zap);
			isParalyze = false;
		}

		if (punched) {
			soundAudio.PlayOneShot (punch);
			punched = false;
		}
	}
}