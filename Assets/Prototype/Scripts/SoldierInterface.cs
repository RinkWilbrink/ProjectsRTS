using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoldierAttack
{
<<<<<<< HEAD
    void Attack(GameObject weapon, Transform weaponpos, string whichSideThisIsFrom);
=======
    void Attack();
>>>>>>> parent of 2977d76... Mage is working
}

class targetAttacking
{
    ISoldierAttack iSoldierAttack;

    public targetAttacking(ISoldierAttack soldierAttack)
    {
        iSoldierAttack = soldierAttack;
    }

<<<<<<< HEAD
    public void Attack(GameObject weapon, Transform weaponpos, string whichSideThisIsFrom)
    {
        if(iSoldierAttack != null) // Check if iSoldierAttack is something to prefent null reference exceptions
        {
            iSoldierAttack.Attack(weapon, weaponpos, whichSideThisIsFrom);
=======
    public void Attack()
    {
        if(iSoldierAttack != null) // Check if iSoldierAttack is something to prefent null reference exceptions
        {
            iSoldierAttack.Attack();
>>>>>>> parent of 2977d76... Mage is working
        }
    }
}
