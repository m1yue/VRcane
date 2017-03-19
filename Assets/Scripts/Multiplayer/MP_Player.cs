using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MP_Player
{
    private float health;
    private float mana;

    private float maxHP;
    private float maxMana;

    public float manaRegenSpeed = 0.005f;
    public float manaDepletionShield = 0.2f;

    private float teleportDistance;

    private MP_Wand wand;

    private GameObject player;
    private float MANA_COST = .05f;


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

    public float getHealth()
    {
        return health;
    }

    public float getMana()
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
    public void setHealth(float health)
    {
        this.health = Mathf.Clamp(health, 0f, 1.0f);
    }

    // adds to player health by float health if isIncrease is true; otherwise decrease player health by float health
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
        this.mana = Mathf.Clamp(mana, 0f, 1.0f);
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
