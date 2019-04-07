using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoldierAttack
{
    void Attack(GameObject weapon, Transform weaponpos);
}

class targetAttacking
{
    ISoldierAttack iSoldierAttack;

    public targetAttacking(ISoldierAttack soldierAttack)
    {
        iSoldierAttack = soldierAttack;
    }

    public void Attack(GameObject weapon, Transform weaponpos)
    {
        if(iSoldierAttack != null) // Check if iSoldierAttack is something to prefent null reference exceptions
        {
            iSoldierAttack.Attack(weapon, weaponpos);
        }
    }
}
