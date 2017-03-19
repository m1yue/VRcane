using UnityEngine;
using UnityEngine.Networking;

public class MP_PlayerController : NetworkBehaviour
{


    public GameObject hud;

    private GameObject pointer; //pointer of player, origin of projectile
    private GameObject reticle;

    private Camera playerCam;
    private GameObject pauseMenu;
    private GameObject controller;
    private GameObject controllerMain;

    /// Teleport Distance
    [Range(0.4f, 10.0f)]
    public float teleportDistance = 5.0f;
    private float movementSpeed = 400f;


    public MP_Player player;
    private MP_Wand playerWand;

    private Vector2 prevTouchPos;
    private Vector2 touchPos;
    private bool teleporting;
    private Vector3 newPos;

	[SyncVar(hook="OnHealthChanged")]
	public float health = 1f;
	[SyncVar(hook="OnManaChanged")]
	float mana = 1f;
	[SyncVar(hook="OnSpellIndexChanged")]
	int spellIndex = 0;

	//private GameObject projectileClone;


    // Use this for initialization
    void Start()
    {
        string[] spellArray = { "Teleport", "MP_Firebolt", "MP_Spell_Ball", "MP_Shield" };

        // Only the local player gets the GVRViewer camera
        if (isLocalPlayer)
        {
            SetupCamera();
            this.tag = "Player";
            transform.GetChild(0).gameObject.layer = 8;
            transform.GetChild(0).GetChild(0).gameObject.layer = 8;
            transform.GetChild(0).GetChild(1).gameObject.layer = 8;

            SetupPlayerModel();
        }
		else
		{
			this.tag = "nonLocalPlayer";
		}

        // hook up the GvrController to this player when it is created
        // Note: this must be called before the wand and player can be instantiated
        SetupController();

        //begin player setup
        playerWand = new MP_Wand(pointer, reticle, 1, 1, spellArray);
        player = new MP_Player(gameObject, playerWand, teleportDistance);

        InvokeRepeating("invokeRegen", 1.0f, 1.0f);
    }


    // Update is called once per frame
    void Update()
    {
		//Debug.Log ("Current Player Health: " + player.getHealth ());
        // prevent non-local players from responding to input from the local system
        if (!isLocalPlayer)
        {
            return;
        }

        // Spell changing using gesture recognition
        if (GvrController.TouchDown)
        {
            prevTouchPos = GvrController.TouchPos;
        }

        if (GvrController.TouchUp)
        {
            touchPos = GvrController.TouchPos;
            int index = detectSwipeDirection(prevTouchPos, touchPos);
			//player.switchSpell (index);
			CmdSwitchSpell (index);
        }


        if ((GvrController.ClickButtonDown || Input.GetMouseButtonDown(0))
            && !pauseMenu.activeSelf)
        {

            if (player.getSpellIndex() != 0)
            {
				CmdShoot();
            }
        }
        else if (GvrController.AppButtonDown)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }

        if (teleporting)
        {
            float step = movementSpeed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, step);
            if (this.transform.position == newPos)
            {
                teleporting = false;
            }
        }

        /* BEN's Old stuff
        if ((GvrController.TouchDown || Input.GetMouseButtonDown(0))
            && player.getMana() >= 20
            && !pauseMenu.activeSelf)
        {
            Debug.Log("Received shoot command");
            Shoot();

            player.setMana(false, 20);
            print(player.getHealth());
        }

        else if (GvrController.AppButtonDown)
        {
            //player.teleport();

            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
        */

        //Constant mana regeneration
        if (player.getSpellIndex() != 3 || (player.getSpellIndex() == 3 && !GvrController.ClickButton))
        {
            player.setMana(true, player.manaRegenSpeed * Time.deltaTime);
        }
    }

    // Hook up the GvrViewer to this player
    void SetupCamera()
    {
        playerCam = Camera.main;

        playerCam.transform.position = transform.position + new Vector3(0,1,0);
        playerCam.transform.rotation = transform.rotation;
        playerCam.transform.SetParent(transform);

        pauseMenu = playerCam.transform.Find("PauseMenuCanvas/PauseMenu").gameObject;
        pauseMenu.SetActive(false);
    }

    // Hook up the GvrController to this player
    void SetupController()
    {
        controller = transform.Find("GvrControllerPointer").gameObject;

        if (controller != null)
        {
            Debug.Log("Found GvrControllerPointer");

            controller.transform.position = transform.position + new Vector3(0,1f,0); //to match the camera's shift
            controller.transform.rotation = transform.rotation;

            pointer = controller.transform.Find("Laser").gameObject;
            if (pointer != null)
            {
                Debug.Log("Found Laser");
                reticle = pointer.transform.Find("Reticle").gameObject;
                if (pointer != null) Debug.Log("Found Reticle\nFound all Controller objects");
            }
        }
    }

	// Disable the Player Model to prevent issues with the pointer/teleport
	void SetupPlayerModel()
	{
		GameObject playerModel = transform.Find("PlayerModel").gameObject;
		//playerModel.SetActive(false);
	}

    // Shoot command called by the Client but run on the server
	// Command methods must be called by a class extending NetworkBehaviour and must be prefixed with "Cmd"
    [Command]
    void CmdShoot()
    {
		int mc = playerWand.getSpellCost();
		if (player.getMana() >= mc)
		{
			//projectileClone = Resources.Load(playerWand.spells[playerWand.primarySpell]) as GameObject;

			string spell = playerWand.spells [playerWand.primarySpell];
			Debug.Log ("Shooting " + spell);
			var projectileClone = GameObject.Instantiate(Resources.Load(spell), pointer.transform.position, pointer.transform.rotation) as GameObject;

			//TODO: replace 500 with spells[primarySpell].getSpeed() 
			//projectileClone.GetComponent<Rigidbody>().AddForce( (GvrArmModel.Instance.pointerRotation)*Vector3.forward * 500 *speedMultiplier);
			if (projectileClone.GetComponent<Rigidbody>())
				//projectileClone.GetComponent<Rigidbody>().AddForce(pointer.transform.forward * 500 * speedMultiplier);
				projectileClone.GetComponent<Rigidbody>().AddForce(pointer.transform.forward * projectileClone.GetComponent<MP_Projectile>().getSpeed() * playerWand.speedMultiplier);

			NetworkServer.Spawn(projectileClone);

			mana -= mc;
			player.setMana(false, mc);
		}

        //var projectileClone = GameObject.Instantiate(Resources.Load("Firebolt"), pointer.transform.position, pointer.transform.rotation) as GameObject;
        //if (projectileClone.GetComponent<Rigidbody>())
            //projectileClone.GetComponent<Rigidbody>().AddForce(pointer.transform.forward * 500 * speedMultiplier);
            //projectileClone.GetComponent<Rigidbody>().AddForce(pointer.transform.forward * projectileClone.GetComponent<Projectile>().getSpeed() * playerWand.speedMultiplier);

        //projectileClone.GetComponent<Rigidbody>().AddForce(pointer.transform.forward * 500 * playerWand.speedMultiplier);

        // Create the projectile on the server and all the clients connected to the server
        // Updates are sent to the clients when state changes on the server
        //NetworkServer.Spawn(projectileClone);
    }

	/*
	 * Command for switch spell to update the client's spell index across the network
	 */
	[Command]
	void CmdSwitchSpell(int index)
	{
		spellIndex = index;
		player.switchSpell (index);
	}
		
	[Client]
	int GetSpellCost()
	{
		int manaCost = 0;
		var projectileClone = Resources.Load(playerWand.spells[playerWand.primarySpell]) as GameObject;

		if (projectileClone != null) {
			Debug.Log ("Getting mana cost");
			manaCost = projectileClone.GetComponent<MP_Projectile> ().getMana ();
		} else {
			Debug.Log ("Could not find projectile");
		}

		return manaCost;
	}

	void OnHealthChanged(float newHealth)
	{
		health = newHealth;
		player.setHealth (health);
	}

	void OnManaChanged(float newMana)
	{
		mana = newMana;
		player.setMana (mana);
	}

	void OnSpellIndexChanged(int newSpellIndex)
	{
		spellIndex = newSpellIndex;
		playerWand.primarySpell = spellIndex;
	}

    int detectSwipeDirection(Vector2 prev, Vector2 final)
    {
        float threshold = 0.3F;
        int index = player.getSpellIndex();

        float distx = final.x - prev.x;
        float disty = final.y - prev.y;

        // horizontal vs vertical priority
        if (Mathf.Abs(distx) > Mathf.Abs(disty))
        {
            // swipe right
            if (distx > threshold)
            {
                index = 1;

            }

            // swipe left
            else if (distx < -threshold)
            {
                index = 3;
            }
        }

        else
        {
            // swipe up
            if (disty > threshold)
            {
                index = 0;
            }

            // swipe down
            else if (disty < -threshold)
            {
                index = 2;
            }
        }

        // set current spell
        //Debug.Log("Distx: " + distx + " Disty: " + disty);

        return index;
    }

    public void teleport(bool needToTeleport, Vector3 _newPos)
    {
        teleporting = needToTeleport;
        newPos = _newPos;
        player.setMana(false, 0.2f);
    }
}
