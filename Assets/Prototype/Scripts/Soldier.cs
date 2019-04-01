using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoldierType
{
    Looter = 0, Melee = 1,
    Ranged = 2, Mage = 3
}

public class Soldier : MonoBehaviour
{
    // Variables
    [Header("Select faction and soldier type")]
    [SerializeField] public SoldierType soldierType;

    [Header("Soldier Stats")]
    [SerializeField] public int Health;
    [SerializeField] private int damage;
    [SerializeField] private int speedMultiplier;

    [Header("Move on its own")]
    [SerializeField] private bool allowedToMove;

    [Header("Animations for the weapon")]
    [SerializeField] private GameObject fakeWeapon;
    [SerializeField] private GameObject RealWeapon;

    [Header("Attack Stats")]

    [Tooltip("The time in seconds it takes between attacks")]
    [SerializeField] private float maxAttackSpeed;

    [Tooltip("The max distance in meters the Soldier can detect an enemy and attack it")]
    [SerializeField] private float maxAttackDistance;

    [Tooltip("The time it takes for the animation to hit the target")]
    [SerializeField] private float AttackHitTime;

    [Tooltip("The time it takes for the attack animation to finish")]
    [SerializeField] private float AttckAnimationEndTime;

    private float currentTimer = 0;

    // Private Variables
    private Vector3 position;
    private string attackTag;
    private GameObject TargetToAttack;
    private float AttackTimer;
    private bool isAttacking = false;
    private bool hasHitTarget = false;
    private float currentAttackTimer;

    private GameObject[] EnemyArray; // Array of enemy's this Soldier can attack

    List<targetAttacking> l_attackTargets = new List<targetAttacking>();

    void Start()
    {
        // Set the vector equal to the position that the soldier is on at the start of its creation
        position.x = transform.position.x;
        position.y = transform.position.y;
        position.z = transform.position.z;

        currentAttackTimer = 0;

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
        switch (soldierType)
        {
            case SoldierType.Looter:
                break;
            case SoldierType.Melee:
                break;
            case SoldierType.Ranged:
                break;
            case SoldierType.Mage:
                break;
        }
        l_attackTargets.Add( new targetAttacking( null ) ); // Looter
        l_attackTargets.Add( new targetAttacking( new Melee() ) ); // Melee soldier
        l_attackTargets.Add( new targetAttacking( new Archer() ) ); // Archer soldier
        l_attackTargets.Add( new targetAttacking( new SpellCaster() ) ); // Spell Caster
    }

    void Update()
    {
        if (allowedToMove) // Let the soldiers move on their own
        {
            transform.position = new Vector3(position.x, position.y, position.z);
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            isAttacking = true;
        }

        if(isAttacking)
        {
            currentAttackTimer -= Time.deltaTime;
        }

        //Update the position of the object
        if(isAttacking == true && currentAttackTimer <= 0)
        {
            if (currentTimer < AttckAnimationEndTime)
            {
                l_attackTargets[(int)soldierType].Attack(); //full Atack Time Melee = 0.583 seconden hit time is 0.2 sec

                fakeWeapon.SetActive(false); RealWeapon.SetActive(true);

                if (currentTimer >= AttackHitTime && hasHitTarget == false)
                {
                    TargetToAttack.GetComponent<Soldier>().Health -= damage;
                    hasHitTarget = true;
                }
            }
            else
            {
                currentTimer = 0;   currentAttackTimer = maxAttackSpeed;
                hasHitTarget = false;   isAttacking = false;
                fakeWeapon.SetActive(true);
            }
            currentTimer += Time.deltaTime;
        }
        else
        {
            position.x += speedMultiplier * Time.deltaTime;
        }

        if (TargetToAttack == null)
        {
            try
            {
                EnemyArray = GameObject.FindGameObjectsWithTag(attackTag);
                TargetToAttack = GetClosestEnemy(EnemyArray);
            }
            catch
            {
                Debug.Log(gameObject.name + " Couldnt find a target!");
            }
        }

        try
        {
            if (DistanceBetween(gameObject.transform.position, TargetToAttack.transform.position) <= maxAttackDistance)
            {
                // Allowed to attack
                isAttacking = true;
            }
        } catch { Debug.Log("No new Target located yet"); }

        try
        {
            if (TargetToAttack.GetComponent<Soldier>().Health <= 0)
            {
                TargetToAttack = null;
                //isAttacking = false;
            }
        } catch { Debug.Log("There are no targets to attack now! Dammit"); }
        

        if (Health <= 0)
        {
            Die();
        }
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

    /// <summary> Check the distance between 2 Vector3D's </summary>
    /// <param name="pos1">The first Vector (Point A).</param> <param name="pos2">The second Vector (Point B).</param>
    /// <returns>A float value with the distance between Two 3D positions</returns>
    private float DistanceBetween(Vector3 pos1, Vector3 pos2)
    {
        // Calculate distance of the Vectors
        float posX = pos1.x - pos2.x;
        float posY = pos1.y - pos2.y;
        float posZ = pos1.z - pos2.z;
        // Return the distance between the XY calculated position and the Z position for the final result.
        return Mathf.Sqrt((posX * posX) + (posY * posY) + (posZ * posZ));
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has Died");
        Destroy(gameObject);
    }
}
