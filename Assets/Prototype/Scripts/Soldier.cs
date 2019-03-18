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
	[SerializeField] private bool autoMovement;

	// Private Variables
	private Vector3 position;
	private string attackTag;
    private GameObject currentTarget;

    void Start()
    {
		// Set the vector equal to the position that the soldier is on at the start of its creation
		position.x = transform.position.x;
		position.y = transform.position.y;
		position.z = transform.position.z;
        //Settings for individual sides
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
        #region Movement
        if(Input.GetKey(KeyCode.A) && !autoMovement)
        {
            position.x -= speedMultiplier * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D) && !autoMovement)
        {
            position.x += speedMultiplier * Time.deltaTime;
        }
        if(autoMovement) // Let the soldiers move on their own
        {
            position.x += speedMultiplier * Time.deltaTime;
        }
        #endregion

        if (Health <= 0)
		{
			Die();
		}
		//Update the position of the object
		transform.position = new Vector3(position.x, position.y, position.z);

        if(Input.GetKeyDown(KeyCode.P))
        {

        }

        if(Input.GetKeyDown(KeyCode.O) && gameObject.tag != "Mage")
        {
            /*
            Destroy(gameObject.GetComponent<SphereCollider>());
            SphereCollider m_collider = gameObject.AddComponent<SphereCollider>();
            m_collider.isTrigger = true;
            m_collider.center = Vector3.zero;
            m_collider.radius = 5f; */
            GameObject[] EnemyArray = GameObject.FindGameObjectsWithTag(attackTag);
            currentTarget = GetClosestEnemy(EnemyArray);
        }
    }

    private GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float distanceToTarget = directionToTarget.sqrMagnitude;
            if (distanceToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = distanceToTarget;
                bestTarget = potentialTarget;
            }
        }
        Debug.Log("The closest target is: " + bestTarget);
        return bestTarget; // Return the best target
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has Died");
    }

    private float makePositive(float toPositive)
    {
        return toPositive;
    }
}
