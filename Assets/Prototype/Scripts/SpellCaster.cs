using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour, ISoldierAttack
{
    public void Attack(GameObject weapon, Transform weaponpos)
    {
        Debug.Log("Casting Spell!");
    }
}
