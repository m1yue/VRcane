using UnityEngine;
using System.Collections;
using DigitalRuby.PyroParticles;

public class Wand
{
    private GameObject pointer;
    public GameObject reticle;

    public float dmgMultiplier;
    public string[] spells; //array of spells loaded onto wand -!!-NOTE: Change type to Projectile once feature is implemented
    public int currSpell; //index of spells array that is currently being used as selected spell

    private GameObject currentPrefabObject;
   
    private GameObject projectileClone;

    public Wand(GameObject pointer, GameObject reticle, string[] spellArray)
    {
        this.pointer = pointer;
        this.reticle = reticle;
        spells = spellArray;
        currSpell = 0;
    }

    //instantiates projectile of a currSpell in controller position and orientation
    public float shoot()
    {
        Transform pTran = pointer.transform;
        projectileClone = GameObject.Instantiate(Resources.Load(spells[currSpell]), pTran.position + (pTran.forward * 0.5f), pTran.rotation) as GameObject;

        return projectileClone.GetComponent<Projectile>().getMana();
    }

    public float getSpellCost()
    {
        projectileClone = Resources.Load(spells[currSpell]) as GameObject;
        float manaCost = projectileClone.GetComponent<Projectile>().getMana();
        return manaCost;
    }

    //Switches currSpell to spellIndex
    public void switchSpellIndex(int spellIndex)
    {
        currSpell = spellIndex;
    }
}
