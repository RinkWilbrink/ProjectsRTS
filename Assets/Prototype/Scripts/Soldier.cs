using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Factions
{
	MagicSwordsman = 1, Magic = 2
}
public enum SoldierType
{
	looter = 1, Melee = 2,
	Ranged = 3, Mage = 4
}

public class Soldier : MonoBehaviour
{
	// Variables

	[Header("Select faction and soldier type")]
	[SerializeField] private Factions factions;
	[SerializeField] private SoldierType soldierType;

	[Header("Soldier Stats")]
	[SerializeField] private int Health;
	[SerializeField] private int damage;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
