using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MP_Player
{
    private int health;
    private int mana;

    private int maxHP;
    private int maxMana;

    private float teleportDistance;

    private MP_Wand wand;

    private GameObject player;
    private int MANA_COST = 5;


    public MP_Player(GameObject player, MP_Wand wand, int maxHP, int maxMana, float teleportDistance)
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
        this.setHealth(this.maxHP = 100);
        this.setMana(this.maxMana = 100);
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

    public void teleport()
    {
        // teleport set distance in the direction the reticle
        // this only teleports horizontaly right now
        if (mana >= MANA_COST)
        {
            setMana(false, MANA_COST);
            Vector3 teleportDir = new Vector3(this.wand.reticle.transform.position.x - this.player.transform.position.x,
                                          0, this.wand.reticle.transform.position.z - this.player.transform.position.z);
            this.player.transform.position = this.player.transform.position + (teleportDir.normalized * teleportDistance);
        }
    }

    public int getHealth()
    {
        return health;
    }

    public int getMana()
    {
        return mana;
    }


    public void healthRegen()
    {
        if (this.health < this.maxHP)
        {
            this.health += 5;
        }
        return;
    }

    public void manaRegen()
    {
        if (this.mana < this.maxMana)
        {
            this.mana += 5;
        }
        return;
    }

    // Sets player health to health
    public void setHealth(int health)
    {
        this.health = health;
    }

    // adds to player health by int health if isIncrease is true; otherwise decrease player health by int health
    public void setHealth(bool isIncrease, int health)
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
    public void setMana(int mana)
    {
        this.mana = mana;
    }

    // adds to player mana by mana if isIncrease is true; otherwise decrease player mana by mana
    public void setMana(bool isIncrease, int mana)
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
