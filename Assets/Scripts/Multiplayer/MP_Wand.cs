using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MP_Wand
{
    private GameObject pointer;
    public GameObject reticle;

    //public GameObject GvrController;
    public float speedMultiplier, dmgMultiplier;
    public string[] spells; //array of spells loaded onto wand -!!-NOTE: Change type to Projectile once feature is implemented
    public int primarySpell; //index of spells array that is currently being used as selected spell


    private GameObject projectileClone;

    public MP_Wand()
    {
        speedMultiplier = dmgMultiplier = 1;
    }

    public MP_Wand(GameObject pointer, GameObject reticle, float speedMult,
                float dmgMult, string[] spellArray)
    {
        this.pointer = pointer;
        this.reticle = reticle;

        speedMultiplier = speedMult;
        dmgMultiplier = dmgMult;

        spells = spellArray;
        primarySpell = 0;
    }

	public float getSpellCost()
	{

		//projectileClone = GameObject.Instantiate(Resources.Load("Spell_Ball"), pointer.transform.position, pointer.transform.rotation) as GameObject;
		//projectileClone = GameObject.Instantiate(Resources.Load(spells[primarySpell]), pointer.transform.position, pointer.transform.rotation) as GameObject;
		projectileClone = Resources.Load(spells[primarySpell]) as GameObject;

		float manaCost = projectileClone.GetComponent<MP_Projectile>().getMana();

		return manaCost;
	}


    //Switches primarySpell to spellIndex
    public void switchSpellIndex(int spellIndex)
    {
        primarySpell = spellIndex;
    }


    //increment primarySpell to next spell in array 
    public void incrementSpellIndex(int x)
    {
        primarySpell = x;
    }
}
