using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour, ISoldierAttack
{
    public void Attack(GameObject weapon, Transform weaponpos, string whichSideThisIsFrom)
    {
        Debug.Log("Casting Spell!");
        GameObject localPotion = Instantiate(weapon, weaponpos.transform.position, weaponpos.transform.rotation);
        localPotion.GetComponent<Rigidbody>().AddForce(new Vector3(-5, 4, 0), ForceMode.Impulse);
        switch (whichSideThisIsFrom)
        {
            case "Swordsmen":
                localPotion.tag = "SwordsmenSpell";
                break;
            case "Mage":
                localPotion.tag = "SwordsmenSpell";
                break;
        }
    }
}
