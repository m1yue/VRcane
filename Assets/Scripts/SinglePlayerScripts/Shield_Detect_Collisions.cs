using UnityEngine;
using System.Collections;

public class Shield_Detect_Collisions : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.GetComponentInParent<Projectile>() != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
