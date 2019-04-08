using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour, ISoldierAttack
{
<<<<<<< HEAD
<<<<<<< HEAD
    potionDestroying PotionDestroying;

    public void Attack(GameObject weapon, Transform weaponpos, string whichSideThisIsFrom)
    {
        Debug.Log("Casting Spell!");
        GameObject localPotion = Instantiate(weapon, weaponpos.transform.position, weaponpos.transform.rotation);
        localPotion.GetComponent<potionDestroying>().originMage = gameObject;
        localPotion.GetComponent<Rigidbody>().AddForce(new Vector3(-5, 4, 0), ForceMode.Impulse);
        switch (whichSideThisIsFrom)
        {
            case "Swordsmen":
                localPotion.tag = "SwordsmenSpell";
                break;
            case "Mage":
                localPotion.tag = "MageSpell";
                break;
        }
=======
    // Variables
	
    public void Attack()
    {
        //Debug.Log("Casting Spell!");
>>>>>>> parent of 2977d76... Mage is working
=======
    // Variables
	
    public void Attack()
    {
        //Debug.Log("Casting Spell!");
>>>>>>> parent of 2977d76... Mage is working
    }
}
