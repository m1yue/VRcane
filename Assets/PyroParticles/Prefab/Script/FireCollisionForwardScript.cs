using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace DigitalRuby.PyroParticles
{
    public interface ICollisionHandler
    {
        void HandleCollision(GameObject obj, Collision c);
    }

    /// <summary>
    /// This script simply allows forwarding collision events for the objects that collide with something. This
    /// allows you to have a generic collision handler and attach a collision forwarder to your child objects.
    /// In addition, you also get access to the game object that is colliding, along with the object being
    /// collided into, which is helpful.
    /// </summary>
    public class FireCollisionForwardScript : NetworkBehaviour
    {
        public float power = 20.0f;

        public ICollisionHandler CollisionHandler;

        public void OnCollisionEnter(Collision col)
        {
            CollisionHandler.HandleCollision(gameObject, col);
        }

        void OnTriggerEnter(Collider collision)
        {
            /* does the collided object have a Player component */
            //print ("Got hit");
            if (collision.gameObject.GetComponentInParent<MP_PlayerController>() != null)
            {
                // tell target to take damage
                MP_PlayerController target = collision.gameObject.GetComponentInParent<MP_PlayerController>();
                //target.player.setHealth(false, (int)power);
                target.health -= (int)power;
                CmdPlayerSetHealth(target.player);
                Debug.Log("Got hit");
                //Debug.Log ("Current Player hp: " + target.player.getHealth());
                // kill projectile
                Destroy(gameObject);
            }

            // did it collide with another spell/shield
            if (collision.gameObject.GetComponent<MP_Projectile>() != null)
            {
                power = power - collision.gameObject.GetComponent<MP_Projectile>().getPower();
                if (power <= 0)
                    // kill projectile
                    Destroy(gameObject);
            }
        }

        [Command]
        void CmdPlayerSetHealth(MP_Player player)
        {
            player.setHealth(false, (int)power);
        }
    }
}
