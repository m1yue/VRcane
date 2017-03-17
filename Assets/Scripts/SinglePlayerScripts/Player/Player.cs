using UnityEngine;
using System.Collections;

public class Player
{
    private float health;
    private float mana;

    private float maxHP = 1.0f;
    private float maxMana = 1.0f;

    public float manaRegenSpeed = 0.2f;
    public float manaDepletionShield = 0.2f;

    private Wand wand;

    public Player(Wand wand, float maxHP, float maxMana)
    {
        this.wand = wand;
        this.setHealth(this.maxHP = maxHP);
        this.setMana(this.maxMana = maxMana);
    }

    public Player(Wand wand)
    {
        this.wand = wand;
        this.setHealth(this.maxHP);
        this.setMana(this.maxMana);
    }

    public void shoot()
    {
        float mc = wand.getSpellCost();
        if (mana >= mc)
        {
            wand.shoot();

            setMana(false, mc);
        }

    }

    public void createShield()
    {
        if(GameObject.Find("Controller") != null)
        {
            GameObject controller = GameObject.Find("Controller");
            GameObject go = GameObject.Instantiate(Resources.Load("Shield"), controller.transform.position, 
                controller.transform.rotation) as GameObject ;
            go.transform.parent = controller.transform;
            go.transform.localPosition = new Vector3(go.transform.localPosition.x - 0.15f, 
                go.transform.localPosition.y + 0.4f, go.transform.localPosition.z + 0.7f);
            
        }
    }

    public void destroyShield()
    {
        if(GameObject.Find("Shield(Clone)") != null)
        {
            GameObject.Destroy(GameObject.Find("Shield(Clone)"));
        }
    }

    public void switchSpell(int index)
    {
        wand.switchSpellIndex(index);
    }

    public float getHealth()
    {
        return health;
    }

    public float getMana()
    {
        return mana;
    }

    public int getSpellIndex()
    {
        return wand.currSpell;
    }

    public void healthRegen()
    {
		if (this.health < (this.maxHP - 0.04f)) {
			this.health += 0.05f;
		} 
		else {
			this.health = this.maxHP;
		}
        return;
    }

    public void manaRegen()
    {
		if (this.mana < this.maxMana - 0.04f) {
			this.mana += 0.05f;
		} 
		else {
			this.mana = this.maxMana;
		}
        return;
    }

    // Sets player health to health
    public void setHealth(float health)
    {
        this.health = Mathf.Clamp(health, 0f, maxHP);
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

    public void die()
    {
        destroyShield();
        //Debug.Log("Die called");
        if (GameObject.Find("PauseMenuManager") != null)
        {
            //Debug.Log("PauseMenuManager found");
            GameObject menumanager = GameObject.Find("PauseMenuManager");
            if (menumanager.GetComponent<PauseMenu>() != null)
            {
                menumanager.GetComponent<PauseMenu>().lose();
            }
        }
    }
}
