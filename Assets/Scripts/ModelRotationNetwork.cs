using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ModelRotationNetwork : NetworkBehaviour
{

    public float speed = 1.0f;
    public GameObject head;
    public GameObject body;
    public float angle;
    private Camera gCam;

    // Use this for initialization
    void Start()
    {
        gCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
             return;
        head.transform.eulerAngles = new Vector3(gCam.transform.eulerAngles.x, gCam.transform.eulerAngles.y, gCam.transform.eulerAngles.z);
        body.transform.eulerAngles = new Vector3(0, gCam.transform.eulerAngles.y, 0);
    }
}
