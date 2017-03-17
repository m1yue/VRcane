using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/* Simple hud control script for updating hp and mana of player 
 */
public class HudController : MonoBehaviour {
	public GameObject player;
    public Camera cam;

    private MP_PlayerController playerController;
    private Text toDisplay;
    //private string health, mana;

	// Use this for initialization
	void Start () {

		playerController = player.GetComponent<MP_PlayerController>();

        toDisplay = GetComponent<Text>();

        //camera part
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (cam != null)
        {
            // Tie this to the camera, and do not keep the local orientation.
            transform.SetParent(cam.GetComponent<Transform>(), true);
        }
    }
	
	// Update is called once per frame
	void Update () {

		toDisplay.text = "Health: " + (playerController.player.getHealth()).ToString() + "\n" +
						 "Mana: " + (playerController.player.getMana()).ToString();
	}
}
