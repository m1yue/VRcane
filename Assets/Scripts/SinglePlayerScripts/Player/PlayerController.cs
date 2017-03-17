using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Player player;
    public GameObject wand;
    public bool needWand;

	public GameObject pointer; //pointer of player, origin of projectile
	public GameObject reticle;

    private Vector2 prevTouchPos;
    private Vector2 touchPos;

    private bool teleporting;
    private Vector3 newPos;
    private float movementSpeed = 400f;

	// Use this for initialization
	void Start () {
		string[] spellArray = { "Teleport", "Firebolt", "Paralyzer", "Shield" };
		Wand playerWand = new Wand(pointer, reticle, spellArray);
		player = new Player(playerWand);
        teleporting = false;
        needWand = true;
    }


    // Update is called once per frame
    void Update()
    {
        // Create Wand
        if( needWand && GameObject.Find("Controller") != null)
        {

            Quaternion wandInitRot = GameObject.Find("Controller").transform.rotation;

            GameObject controller = GameObject.Find("Controller");
            GameObject newWand = GameObject.Instantiate(Resources.Load(PersistentData.data.myWand), controller.transform.position, wandInitRot) as GameObject;

            newWand.tag = "Old";

            newWand.transform.SetParent(GameObject.Find("Controller").transform);

            newWand.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

            newWand.tag = "New";

            Destroy(GameObject.FindGameObjectWithTag("Old"));

            needWand = false;
        }

        if(Time.timeScale == 0f)
        {
            return;
        }

        if(player.getHealth() == 0f)
        {
            Debug.Log("Player died");
            player.die();
        }

        //If the player is teleporting...
        if(teleporting)
        {
            float step = movementSpeed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, step);
            if(this.transform.position == newPos)
            {
                teleporting = false;
            }
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
            player.switchSpell(index);
        }


        //If offensive spell is active, shoot.
        if ((player.getSpellIndex() == 1 || player.getSpellIndex() == 2) && GvrController.ClickButtonUp)
        {
            player.shoot();
        }

        //If defensive spell is active, create a shield and drain mana while button is held.
        if (player.getSpellIndex() == 3)
        {
            if(GvrController.ClickButtonDown && player.getMana() > 0.1f)
            {
                player.createShield();
            }
            if(GvrController.ClickButton)
            {
                player.setMana(false, player.manaDepletionShield * Time.deltaTime);
                if(player.getMana() == 0f)
                {
                    player.destroyShield();
                }
            }
            if(GvrController.ClickButtonUp)
            {
                player.destroyShield();
            }
        }

        //Constant mana regeneration
        if(player.getSpellIndex() != 3 || (player.getSpellIndex() == 3 && !GvrController.ClickButton))
        {
            player.setMana(true, player.manaRegenSpeed * Time.deltaTime);
        }
    }

    //method for detecting swipes
    int detectSwipeDirection(Vector2 prev, Vector2 final)
    {
        player.destroyShield();
        float threshold = 0.4F;
        int index = player.getSpellIndex() ;

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
 
        return index;
    }

    //Teleport (called from teleportation pad)
    public void teleport(bool needToTeleport, Vector3 _newPos)
    {
        teleporting = needToTeleport;
        newPos = _newPos;
        player.setMana(false, 0.2f);
    }

    private void OnCollisionEnter(Collision collision)
    {

        //Handle projectile collisions
        if (collision.gameObject.GetComponentInParent<Projectile>() != null)
        {
            float HPcost = collision.gameObject.GetComponentInParent<Projectile>().getPower();
            player.setHealth(false, HPcost);
        }

        //Handle fist collisions
        else if(collision.gameObject.transform.parent.parent.parent.parent.parent.GetComponent<AIBehavior>() != null)
        {
            if(player.getSpellIndex() != 3 || !GvrController.ClickButton)
            {
                float HPcost =
                collision.gameObject.transform.parent.parent.parent.parent.parent.GetComponent<AIBehavior>().getMeleeAttackStrength();
                player.setHealth(false, HPcost);
            }
        }
    }
}
