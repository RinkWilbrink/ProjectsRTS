using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    // Variables
    public bool Meleehit = false;
    public bool isShooting = false;
    public bool isSpell = false;
    
    public void MeleeCall()
    {
        Meleehit = true;
    }
    public void ArcherCall()
    {
        isShooting = true;
    }
    public void MageCall()
    {
        isSpell = true;
    }

    // attacking
    public void MeleeAttack()
    {

    }
    public void ArcherAttack()
    {

    }
    public void MageAttack()
    {

    }

    public void DisableOBJ()
    {
        gameObject.SetActive(false);
    }
}
