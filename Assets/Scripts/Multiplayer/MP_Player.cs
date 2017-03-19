using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MP_Player
{
    private float health;
    private float mana;

    private float maxHP;
    private float maxMana;

    public float manaRegenSpeed = 0.2f;

    private float teleportDistance;

    private MP_Wand wand;

    private GameObject player;
    private int MANA_COST = 5;


    public MP_Player(GameObject player, MP_Wand wand, float maxHP, float maxMana, float teleportDistance)
    {
        this.player = player;
        this.wand = wand;

        this.teleportDistance = teleportDistance;
        this.setHealth(this.maxHP = maxHP);
        this.setMana(this.maxMana = maxMana);
    }

    public MP_Player(GameObject player, MP_Wand wand, float teleportDistance)
    {
        this.player = player;
        this.wand = wand;
        this.teleportDistance = teleportDistance;
        this.setHealth(this.maxHP = 1.0f);
        this.setMana(this.maxMana = 1.0f);
    }


	/*
	 * Moved to PlayerController for network purposes
    public void shoot()
    {
        int mc = wand.getSpellCost();
        if (mana >= mc)
        {
            wand.shoot();

            setMana(false, mc);
        }
    }
    */

    public void switchSpell(int x)
    {
        wand.incrementSpellIndex(x);
        Debug.Log("WORKING index: " + x);
    }

    public int getSpellIndex()
    {
        return wand.primarySpell;
    }

    public float getHealth()
    {
        return health;
    }

    public float getMana()
    {
        return mana;
    }

    // Sets player health to health
    public void setHealth(float health)
    {
        this.health = Mathf.Clamp(health, 0f, 1f);
    }

    // adds to player health by int health if isIncrease is true; otherwise decrease player health by int health
    public void setHealth(bool isIncrease, float health)
    {
        if (isIncrease)
        {
            setHealth(this.health + health);
        }
        else
        {
            setHealth(this.health - health);
        }
    }

    // Sets player mana to mana
    public void setMana(float mana)
    {
        this.mana = Mathf.Clamp(mana, 0f, 1f);
    }

    // adds to player mana by mana if isIncrease is true; otherwise decrease player mana by mana
    public void setMana(bool isIncrease, float mana)
    {
        if (isIncrease)
        {
            setMana(this.mana + mana);
        }
        else
        {
            setMana(this.mana - mana);
        }
    }
}
