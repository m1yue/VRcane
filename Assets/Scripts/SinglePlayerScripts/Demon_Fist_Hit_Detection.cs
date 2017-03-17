using UnityEngine;
using System.Collections;

public class Demon_Fist_Hit_Detection : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detected punch");
        if(other.gameObject.GetComponent<PlayerController>() != null && 
            this.gameObject.transform.parent.parent.parent.parent.parent.GetComponent<AIBehavior>() != null)
        {
            if(!(other.gameObject.GetComponent<PlayerController>().player.getSpellIndex() == 3 && 
                GvrController.ClickButton && other.gameObject.GetComponent<PlayerController>().player.getMana() > 0f))
            {
                Debug.Log("Giving damage");
                float HPcost = this.transform.parent.parent.parent.parent.parent.GetComponent<AIBehavior>().getMeleeAttackStrength();
                other.gameObject.GetComponent<PlayerController>().player.setHealth(false, HPcost);
            }
        }
    }

}
