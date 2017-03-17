using UnityEngine;
using System.Collections;

public class RotatorScript : MonoBehaviour {
	
	void Update () {
		transform.Rotate (new Vector3 (0, 45, 0) * 4 * Time.deltaTime);
	}
}
