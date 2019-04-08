using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoldierType
{
    Looter = 0, Melee = 1,
    Ranged = 2, Mage = 3, Base = 4
}

public class Soldier : MonoBehaviour
{
    // Variables
    [Header("Select faction and soldier type")]
    [SerializeField] public SoldierType soldierType;

    [Header("Soldier Stats")]
    [SerializeField] public float Health;
    [SerializeField] private int damage;
    [SerializeField] private int speedMultiplier;
    [SerializeField] public float maxFreezeTimer;

    // Animation for the Soldiers weapon
    [Header("Weapon animation objects")]
    [SerializeField] private GameObject fakeWeapon;
    [SerializeField] private GameObject RealWeapon;
    [SerializeField] private GameObject ThrowingPosition;

    [Header("Attack Stats")]
    [SerializeField] private float maxAttackSpeed;
    [SerializeField] private float maxAttackDistance;
    [SerializeField] private float AttackHitTime;
    [SerializeField] private float AttckAnimationEndTime;

    [SerializeField] private bool canMove;

    // Booleans
    [HideInInspector] public bool allowedToMove = true;
    private bool isAttacking = false;
    private bool hasHitTarget = false;

    // Timers
    private float TargetDamageTimer = 0;
    private float TimerBetweenAttacks = 0;

    [HideInInspector] public float FreezeTimer = 0;
    private bool isFrozen = false;

    private float position;
    private string attackTag;

    // All variables that decide who to attack
    private GameObject TargetToAttack;
    private GameObject[] EnemyArray;

    //Soldier attacks
    List<targetAttacking> l_attackTargets = new List<targetAttacking>();

    void Start()
    {
        // Set the vector equal to the position that the soldier is on at the start of its creation
        position = transform.position.x;

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
        
        l_attackTargets.Add( new targetAttacking( null ) ); // Looter
        l_attackTargets.Add( new targetAttacking( new Melee() ) ); // Melee soldier
        l_attackTargets.Add( new targetAttacking( new Archer() ) ); // Archer soldier
        l_attackTargets.Add( new targetAttacking( new SpellCaster() ) ); // Spell Caster
    }

    void Update()
    {
        if (isAttacking && !isFrozen)
        {
            TimerBetweenAttacks -= Time.deltaTime;
            if (TimerBetweenAttacks <= 0)
            {
                allowedToMove = false;

                if(soldierType != SoldierType.Mage)
                {
                    fakeWeapon.SetActive(false); RealWeapon.SetActive(true);
                }

                TargetDamageTimer += Time.deltaTime;

                if (TargetDamageTimer >= AttackHitTime && hasHitTarget == false)
                {
                    if(soldierType == SoldierType.Mage)
                    {
                        //l_attackTargets[(int)soldierType].Attack(RealWeapon, ThrowingPosition.transform, attackTag);
                        GameObject localpotion = Instantiate(RealWeapon, ThrowingPosition.transform.position, ThrowingPosition.transform.rotation);
                        localpotion.GetComponent<Rigidbody>().AddForce(new Vector3(-5, 4, 0), ForceMode.Impulse);
                    }
                    else
                    {
                        TargetToAttack.GetComponent<Soldier>().Health -= damage;
                    }
                    hasHitTarget = true;
                }
                if(TargetDamageTimer >= AttckAnimationEndTime)
                {
                    TargetDamageTimer = 0;
                    hasHitTarget = false;
                    TimerBetweenAttacks = maxAttackSpeed;
                }
            }
            else
            {
                fakeWeapon.SetActive(true);
            }
        }
        else
        {
            if (allowedToMove && !isFrozen && canMove)
            {
                position += speedMultiplier * Time.deltaTime;
            }
        }

        if (TargetToAttack == null)
        {
            try
            {
                EnemyArray = GameObject.FindGameObjectsWithTag(attackTag);
                TargetToAttack = GetClosestEnemy(EnemyArray, gameObject.transform);
            } catch { /*Debug.Log(gameObject.name + " Couldnt find a target!");*/ }
        }
        if(soldierType == SoldierType.Mage)
        {
            Debug.Log("maxAttackDistance of " + gameObject.name + ": " + maxAttackDistance);
            Debug.Log("isAttacking of " + gameObject.name + ": " + isAttacking);
        }
        try
        {
            if (DistanceBetween(gameObject.transform.position, TargetToAttack.transform.position) <= 5f)
            {
                isAttacking = true;
            }
        } catch { /*Debug.Log("There is no target to compare its distance to");*/ }

        try
        {
            if (TargetToAttack.GetComponent<Soldier>().Health <= 0)
            {
                TimerBetweenAttacks = 0;
                isAttacking = false;
                allowedToMove = true;
                TargetToAttack = null;
            }
        } catch { /*Debug.Log("There are no targets to attack now! Dammit"); */ }

        // Check if the soldier should be frozen.
        if(FreezeTimer > 0) { FreezeTimer -= Time.deltaTime; }
        else
        { isFrozen = false; }

        if(soldierType != SoldierType.Looter )
        {
            transform.position = new Vector3(position, transform.position.y, transform.position.z);
        }

        // Check if the Health of this soldier is 0 or less and "Kill" the soldier
        if (Health <= 0)
        {
            Debug.Log(gameObject.name + " has Died");
            Destroy(gameObject);
        }
    }

    private GameObject GetClosestEnemy(GameObject[] enemies, Transform OriginObject)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = 15f; // Mathf.Infinity;
        Vector3 currentPosition = OriginObject.position;
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
        return bestTarget; // Return the best target for this soldier to attack
    }

    /// <summary> Check the distance between 2 Vector3D's </summary>
    /// <param name="pos1">The first Vector (Point A).</param> <param name="pos2">The second Vector (Point B).</param>
    private float DistanceBetween(Vector3 pos1, Vector3 pos2)
    {
        // Calculate distance of the Vectors
        float posX = pos1.x - pos2.x;
        float posY = pos1.y - pos2.y;
        float posZ = pos1.z - pos2.z;
        // Return the distance between the XY calculated position and the Z position for the final result.
        return Mathf.Sqrt((posX * posX) + (posY * posY) + (posZ * posZ));
    }

    public void OnCollisionEnter(Collision collision)
    {
        switch (gameObject.tag)
        {
            case "Mage": // Magespell
                if (collision.collider.tag == "SwordsmenSpell")
                {
                    isFrozen = true;
                    FreezeTimer = maxFreezeTimer;
                }
                break;
            case "Swordsmen": // SwordsmenSpell
                if (collision.collider.tag == "Magespell")
                {
                    isFrozen = true;
                    FreezeTimer = maxFreezeTimer;
                }
                break;
        }

    }
}
