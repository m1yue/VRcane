using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Health_and_Mana : MonoBehaviour {

    [SerializeField]
    RectTransform manaBar;

    [SerializeField]
    RectTransform healthBar;

    [SerializeField]
    Text spellText;

    private PlayerController controller;

    private void Start()
    {

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameObject localPlayer = GameObject.FindGameObjectWithTag("Player");
            if(localPlayer.GetComponent<PlayerController>() != null)
            {
                controller = localPlayer.GetComponent<PlayerController>();
            }
        }

    }

    void Update()
    {
        setManaAmount(controller.player.getMana());

        setHealthAmount(controller.player.getHealth());

        if(controller.player.getSpellIndex() == 0)
        {
            spellText.text = "Teleport";
        }
        else if (controller.player.getSpellIndex() == 1)
        {
            spellText.text = "Attack";
        }
        else if (controller.player.getSpellIndex() == 2)
        {
            spellText.text = "Paralyze";
        }
        else
        {
            spellText.text = "Defend";
        }
    }

    void setManaAmount(float _amount)
    {
        manaBar.localScale = new Vector3(1f, _amount, 1f);
    }

    void setHealthAmount(float _amount)
    {
        healthBar.localScale = new Vector3(1f, _amount, 1f);
    }

}
