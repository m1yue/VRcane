using UnityEngine;
using System.Collections;

public class ModelRotationController : MonoBehaviour {
	
	public float speed = 1.0f;
	public GameObject head;
	public GameObject body;
	public GameObject gvrCam;
	public float angle;

	// Use this for initialization
	void Start () {
		gvrCam.transform.rotation = GvrViewer.Instance.HeadPose.Orientation;
	}
	
	// Update is called once per frame
	void Update () {
        
        head.transform.eulerAngles = gvrCam.transform.eulerAngles ;
		body.transform.eulerAngles = new Vector3(0, gvrCam.transform.eulerAngles.y, 0);
	}
}
