using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoldierAttack
{
    void Attack(GameObject weapon, Transform weaponpos, string whichSideThisIsFrom);
}

class targetAttacking
{
    ISoldierAttack iSoldierAttack;

    public targetAttacking(ISoldierAttack soldierAttack)
    {
        iSoldierAttack = soldierAttack;
    }

    public void Attack(GameObject weapon, Transform weaponpos, string whichSideThisIsFrom)
    {
        if(iSoldierAttack != null) // Check if iSoldierAttack is something to prefent null reference exceptions
        {
            iSoldierAttack.Attack(weapon, weaponpos, whichSideThisIsFrom);
        }
    }
}
