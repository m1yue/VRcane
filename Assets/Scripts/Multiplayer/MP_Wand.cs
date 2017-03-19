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

    //instantiates projectile of a primarySpell in controller position and orientation
    [Command]
    public int shoot()
    {
        //NOTE!!!! THESE COMMENTS ARE FOR POTENTIAL ALTERNATIVE IMPLEMENTATION
        //Getting forward direction for projectile based on GvrController 
        //Quaternion ori = GvrController.Orientation;
        //Vector3 projectileDirection = ori * Vector3.forward;

        //CHECK: GvrArmModel is correct for initial position of projectile
        //projectileClone = GameObject.Instantiate(spells[primarySpell], pointer.transform.position, GvrArmModel.Instance.pointerRotation) as GameObject;

        projectileClone = Resources.Load(spells[primarySpell]) as GameObject;

        projectileClone = GameObject.Instantiate(projectileClone, pointer.transform.position, pointer.transform.rotation) as GameObject;

        //TODO: replace 500 with spells[primarySpell].getSpeed() 
        //projectileClone.GetComponent<Rigidbody>().AddForce( (GvrArmModel.Instance.pointerRotation)*Vector3.forward * 500 *speedMultiplier);
        if (projectileClone.GetComponent<Rigidbody>())
            //projectileClone.GetComponent<Rigidbody>().AddForce(pointer.transform.forward * 500 * speedMultiplier);
            projectileClone.GetComponent<Rigidbody>().AddForce(pointer.transform.forward * projectileClone.GetComponent<MP_Projectile>().getSpeed() * speedMultiplier);

        NetworkServer.Spawn(projectileClone);   


        return projectileClone.GetComponent<MP_Projectile>().getMana();


    }


	public int getSpellCost()
	{

		//projectileClone = GameObject.Instantiate(Resources.Load("Spell_Ball"), pointer.transform.position, pointer.transform.rotation) as GameObject;
		//projectileClone = GameObject.Instantiate(Resources.Load(spells[primarySpell]), pointer.transform.position, pointer.transform.rotation) as GameObject;
		projectileClone = Resources.Load(spells[primarySpell]) as GameObject;

		int manaCost = projectileClone.GetComponent<MP_Projectile>().getMana();
		//projectileClone.GetComponent<Projectile>().updateAttributes(0,0,0,0);

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
