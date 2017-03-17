using UnityEngine;
using System.Collections;

public class TargetDummy : MonoBehaviour {

    public int health;
    public int speed;
    public float max;

    private float min;
	// Use this for initialization
	void Start () {
        health = 8;
        speed = 3;

        min = transform.position.x;
        max = 10f;
	}
	
	// Update is called once per frame
	void Update () {
        
        //!!! DISABLED: Stationary targets easier for testing other functionality
        transform.position = new Vector3(Mathf.PingPong(Time.time*speed, max)+min, transform.position.y, transform.position.z);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        health--;
        if (health <= 0)
            Destroy(gameObject);
    }
}
