using UnityEngine;
using System.Collections;
using System;

public class AIBehavior : MonoBehaviour {

    private float HP = 2.0f;

    private float maxHP = 2.0f;

    //Location of the player
    public Transform player;

    //Animator
    static Animator anim;

    //Int that determines behavior.  0 = melee attack
    int currBehavior;

    private float behaviorPeriod = 3f;

    bool dead = false;

    [SerializeField]
    GameObject particles;
    bool paralyzed = false;

    float meleeAttackStrength = 0.05f;

	// Use this for initialization
	void Start () {
        if (GameObject.FindWithTag("PlayerModel") != null)
        {
            player = GameObject.FindWithTag("PlayerModel").transform;
        }
        anim = GetComponent<Animator>();

        //currBehavior = 1; // For testing
        InvokeRepeating("changeBehavior", 15f, behaviorPeriod);
        InvokeRepeating("shootAtPlayer", 17f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {

        if(Time.timeScale == 0f || dead)
        {
            return;
        }

        if (HP <= 0f && dead == false)
        {
            paralyzed = false;
            currBehavior = 3;
            anim.SetBool("Move", false);
            anim.ResetTrigger("Attack 01");
            anim.ResetTrigger("Attack 02");
            anim.ResetTrigger("Take Damage");
            anim.SetTrigger("Die");
            dead = true;
            die();
        }

        if (paralyzed)
        {
            //Debug.Log("Can't move.  Paralyzed.");
            anim.ResetTrigger("Attack 01");
            anim.ResetTrigger("Attack 02");
            return;
        }

        //Rotate to track player
        Vector3 direction = player.position - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        if (currBehavior == 0)
        {
            //If they're more than 2 meters away, walk toward them.
            if (direction.magnitude > 2.2)
            {
                anim.SetBool("Move", true);
                this.transform.Translate(0, 0, 0.05f);
            }
            //Then attack.
            else
            {
                anim.SetBool("Move", false);
                anim.SetTrigger("Attack 01");
            }
        }
	}

    private void shootAtPlayer()
    {
        if(currBehavior != 1 || paralyzed || dead)
        {
            return;
        }
        if(GameObject.FindWithTag("PlayerModel") == null)
        {
            return;
        }
        Transform playerModel = GameObject.FindWithTag("PlayerModel").transform;
        Vector3 playerPos = new Vector3(0f, -0.4f, 0f);
        playerPos += playerModel.position;
        Vector3 direction = playerPos - this.transform.position;
        Vector3 position = this.transform.position;
        position += this.transform.forward * 2;
        position += this.transform.up;
        GameObject projectileClone = GameObject.Instantiate(Resources.Load("Firebolt"), position, 
            Quaternion.LookRotation(direction)) as GameObject;

        if (projectileClone.GetComponent<Rigidbody>())
        {
            projectileClone.GetComponent<Rigidbody>().AddForce(projectileClone.transform.forward * projectileClone.GetComponent<Projectile>().getSpeed() * 1.25f);
        }
    }
    

    private void die()
    {
        if(GameObject.Find("PauseMenuManager") != null)
        {
            GameObject menumanager = GameObject.Find("PauseMenuManager");
            if(menumanager.GetComponent<PauseMenu>() != null)
            {
                menumanager.GetComponent<PauseMenu>().win();
            }
        }
    }

    void changeBehavior()
    {
        currBehavior = UnityEngine.Random.RandomRange(0, 3);
        //Debug.Log(currBehavior);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision detected");
        if(collision.gameObject.GetComponentInParent<Projectile>() != null)
        {
            //Debug.Log(collision.gameObject.name);
            if(collision.gameObject.name == "Paralyzer")
            {
                //Debug.Log("Set paralyzed");
                paralyzed = true;
                particles.SetActive(true);
                Invoke("unparalyze", 10f);
            }
            float HPcost = collision.gameObject.GetComponentInParent<Projectile>().getPower();
            setHealth(false, HPcost);
        }
    }

    public float getHP()
    {
        return HP;
    }

    void setHealth(float amount)
    {
        HP = Mathf.Clamp(amount, 0f, maxHP);
    }

    public void setHealth(bool isIncreasing, float amount)
    {
        if(isIncreasing)
        {
            setHealth(HP + amount);
        }
        else
        {
            setHealth(HP - amount);
        }
    }

    public float getMeleeAttackStrength()
    {
        return meleeAttackStrength;
    }

    void unparalyze()
    {
        paralyzed = false;
        particles.SetActive(false);
    }
}
