using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour, ISoldierAttack
{
    potionDestroying PotionDestroying;

    public void Attack(GameObject weapon, Transform weaponpos, string whichSideThisIsFrom)
    {
        Debug.Log("Casting Spell!");
    }
}
