using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AI_Health_Bar : MonoBehaviour {

    [SerializeField]
    RectTransform healthBar;

    AIBehavior AIattributes;

    GameObject player;

    GameObject enemyAI;

    Vector3 distanceToPlayer;

    private void Start()
    {
        if(GameObject.FindWithTag("EnemyAI") != null)
        {
            enemyAI = GameObject.FindWithTag("EnemyAI");
            if(enemyAI.GetComponent<AIBehavior>() != null)
            {
                AIattributes = enemyAI.GetComponent<AIBehavior>();
            }
        }

        if(GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update () {
        setHealthAmount((AIattributes.getHP())/2.0f);

        distanceToPlayer = enemyAI.transform.position - player.transform.position;
        distanceToPlayer.Normalize();
        this.transform.localPosition = distanceToPlayer * 0.5f;
        this.transform.rotation = Quaternion.LookRotation(distanceToPlayer);
	}

    void setHealthAmount(float _amount)
    {
        healthBar.localScale = new Vector3(1f, _amount, 1f);
    }
}
