using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoldierAttack
{
    void Attack();
}

class targetAttacking
{
    ISoldierAttack iSoldierAttack;

    public targetAttacking(ISoldierAttack soldierAttack)
    {
        iSoldierAttack = soldierAttack;
    }

    public void Attack()
    {
        if(iSoldierAttack != null)
        {
            iSoldierAttack.Attack();
        }
    }
}
