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
    private GameObject TargetToAttack;
    private float maxAttackDistance;

    List<targetAttacking> l_attackTargets = new List<targetAttacking>();

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

        //Add all the classes that need have the attack function
        l_attackTargets.Add( new targetAttacking( null ) ); // Looter
        l_attackTargets.Add( new targetAttacking( new Melee() ) ); // Melee soldier
        l_attackTargets.Add( new targetAttacking( new Archer() ) ); // Archer soldier
        l_attackTargets.Add( new targetAttacking( new SpellCaster() ) ); // Spell Caster
    }

    void Update()
    {
        #region Movement
        if (Input.GetKey(KeyCode.A) && !autoMovement)
        {
            position.x -= speedMultiplier * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) && !autoMovement)
        {
            position.x += speedMultiplier * Time.deltaTime;
        }
        if (autoMovement) // Let the soldiers move on their own
        {
            position.x += speedMultiplier * Time.deltaTime;
        }
        #endregion

        //Attack target
        if (Input.GetKeyDown(KeyCode.P))
        {
            l_attackTargets[(int)soldierType].Attack();
        }
        
        if (Health <= 0)
        {
            Die();
        }

        //Update the position of the object
        transform.position = new Vector3(position.x, position.y, position.z);

        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject[] EnemyArray = GameObject.FindGameObjectsWithTag(attackTag);
            TargetToAttack = GetClosestEnemy(EnemyArray);
            //Find distance between
            DistanceBetween(gameObject.transform.position, TargetToAttack.transform.position);
        }

        /*
        if(TargetToAttack.GetComponent<Soldier>().Health <= 0)
        {
            TargetToAttack = null;
        }*/
    }

    public GameObject GetClosestEnemy(GameObject[] enemies)
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
        //Debug.Log("The closest target is: " + bestTarget);
        return bestTarget; // Return the best target
    }

    private float DistanceBetween(Vector3 pos1, Vector3 pos2)
    {
        // Calculate distance of the Vectors
        float posX = Mathf.Sqrt((pos1.x * pos1.x) + (pos2.x * pos2.x));
        float posY = Mathf.Sqrt((pos1.y * pos1.y) + (pos2.y * pos2.y));
        float posZ = Mathf.Sqrt((pos1.z * pos1.z) + (pos2.z * pos2.z));
        //Calculate the distance between X and Y.
        float posXY = Mathf.Sqrt((posX * posX) + (posY * posY));

        // Return the distance between the XY calculated position and the Z position for the final result.
        return Mathf.Sqrt((posXY * posXY) + (posZ * posZ));
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has Died");
    }
}
