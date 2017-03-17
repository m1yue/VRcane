using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour {

	public static PersistentData data;
	public string myWand;

	void Awake () {

        myWand = "PearlWand";

		if (data == null) {
			DontDestroyOnLoad (gameObject);
			data = this;
		}
		else if (data != this) {
			Destroy (gameObject);
		}
	}
}
