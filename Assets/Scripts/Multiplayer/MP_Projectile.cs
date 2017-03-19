using UnityEngine;
using UnityEngine.Networking;

public class MP_Projectile : NetworkBehaviour {
	
	public float power = 0.01f;
	public float speed = 1.0f;
	public float radius = 1.0f;
	public float duration = 5.0f;
	public float manaCost = 0.1f;
	public string trajectory = "Line";	// not implemented until a lot later
	
	// Use this for initialization
	void Start () {
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
	
	void followTrajectory(string path)
	{
		/* set extra trajectory like snaking, tomahawking, arking, and spiraling. */
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
		if(collision.gameObject.GetComponentInParent<MP_PlayerController>() != null)
		{
			// tell target to take damage
			MP_PlayerController target = collision.gameObject.GetComponentInParent<MP_PlayerController>();
			target.health -= power;
			CmdPlayerSetHealth (target.player);
			// kill projectile
			Destroy(gameObject);
		}
		
		// did it collide with another spell/shield
        if(collision.gameObject.GetComponent<MP_Projectile>() != null)
		{
			power = power - collision.gameObject.GetComponent<MP_Projectile>().getPower();
			if(power <= 0)
			// kill projectile
				Destroy(gameObject);
		}
    }

	[Command]
	void CmdPlayerSetHealth(MP_Player player)
	{
		player.setHealth(false, power);
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
}