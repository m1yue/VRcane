using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	private float power = 0.1f;
	private float speed = 1000.0f;
	private float radius = 10.0f;
	private float duration = 5.0f;
	private float manaCost = 0.27f;
	
    void Start()
    {
        string wandType = PersistentData.data.myWand;
        if (wandType.Equals("PearlWand"))
        {
            power = power * 1.0f;
        }

        else if (wandType.Equals("AshWand"))
        {
            power = power * 1.5f;
        }

        else if(wandType.Equals("FlameWand"))
        {
            power = power * 0.5f;
        }
    }

	// Update is called once per frame
	void Update () {
		updateDuration();
	}
	
	private void updateDuration()
	{
		duration = duration - Time.deltaTime;
		if(duration <= 0)
		{
			Destroy(gameObject);
		}
	}
	
	/* 
	 * Spells should have Multipliers between 0.5 ~ 1.5
	 * Shield should have Multipliers of 0, so it doesn't move or deal damage.
	 */
	public void updateAttributes(	float powerMult, float speedMult,
									float radiusMult, float durationMult)
	{
		power = power * powerMult;
		speed = speed * speedMult;
		gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
		radius = (radius * radiusMult) - 1;
		transform.localScale += new Vector3(radius,radius,radius);
		duration = duration * durationMult;
	}

	//void OnCollisionEnter(Collision collision) {
	void OnTriggerEnter(Collider collision) {

        /* does the collided object have a Player component */
		if(collision.gameObject.GetComponent<PlayerController>() != null)
		{
			// tell target to take damage
			PlayerController target = collision.gameObject.GetComponent<PlayerController>();
			target.player.setHealth(false, power);
			
            // kill projectile
			Destroy(gameObject);
		}
		
		// did it collide with another spell/shield
        if(collision.gameObject.GetComponent<Projectile>() != null)
		{
			power = power - collision.gameObject.GetComponent<Projectile>().getPower();
			if(power <= 0)
			
                // kill projectile
				Destroy(gameObject);
		}
    }
	
	public float getPower()
	{
		return power;
	}
	
	public float getMana()
	{
		return manaCost;
	}

    public float getSpeed()
    {
        return speed;
    }

    public void updatePower(float dmgMult)
    {
        power = power * dmgMult;
    }
}