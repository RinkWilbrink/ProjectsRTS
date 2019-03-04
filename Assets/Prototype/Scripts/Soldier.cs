using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoldierType
{
	looter = 0, Melee = 1,
	Ranged = 2, Mage = 3
}

public class Soldier : MonoBehaviour
{
	// Variables
	[Header("Select faction and soldier type")]
	[SerializeField] public SoldierType soldierType;

	[Header("Soldier Stats")]
	[SerializeField] private int Health;
	[SerializeField] private int damage;
	[SerializeField] private int speedMultiplier;

	[Header("Debug attributes")]
	[SerializeField] private bool allowedToMove;

	// Private Variables
	private Vector3 position;
	private string attackTag;

    void Start()
    {
		// Set the vector equal to the position that the soldier is on at the start of its creation
		position.x = transform.position.x;
		position.y = transform.position.y;
		position.z = transform.position.z;
		// Settings for individual sides
		if(!allowedToMove)
		{
			speedMultiplier = 0;
		}
		switch (gameObject.tag)
		{
			case "Swordsmen":
				attackTag = "Mage";
				break;
			case "Mage":
				speedMultiplier = -speedMultiplier;
				attackTag = "Swordsmen";
				break;
		}
	}
    
    void Update()
    {
		position.x += speedMultiplier * Time.deltaTime;

		if(Health <= 0)
		{
			Die();
		}
		//Update the position of the object
		transform.position = new Vector3(position.x, position.y, position.z);
    }

	private void Die()
	{
		Debug.Log(gameObject.name + " has Died");
	}
}
