using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierGeneric : MonoBehaviour
{
    [SerializeField] private SoldierType soldierType;

    [SerializeField] public float Health;
    [SerializeField] public int Damage;
    [SerializeField] private int speedMultiplier;
    [SerializeField] private float maxFreezeTime;

    [SerializeField] private GameObject fakeWeapon;
    [SerializeField] private GameObject realWeapon;
    [SerializeField] private GameObject throwPositionObj;

    [SerializeField] private float maxAttackSpeed;
    [SerializeField] private float maxAttackDistance;
    [SerializeField] private float AttackHitTime;
    [SerializeField] private float AttackAnimationTime;

    [HideInInspector] public bool allowedToMove = true;
    private bool canAttack = false;
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
        //Only in debug to make sure they have atleast some health.
        if(Health<= 0)
        {
            Health = 1;
        }
    }

    void Update()
    {
        if(canAttack)
        {
            allowedToMove = false;

            TargetDamageTimer += Time.deltaTime;

            if(TargetDamageTimer >= AttackHitTime && hasHitTarget == false)
            {
                if(soldierType == SoldierType.Mage)
                {
                    GameObject localpotion = Instantiate(realWeapon, throwPositionObj.transform.position, throwPositionObj.transform.rotation);
                    switch (attackTag)
                    {
                        case "Mage":
                            localpotion.GetComponent<Rigidbody>().AddForce(new Vector3(-5, 4, 0), ForceMode.Impulse);
                            localpotion.tag = "SwordsmenSpell";
                            break;
                        case "Swordsmen":
                            localpotion.GetComponent<Rigidbody>().AddForce(new Vector3(5, 4, 0), ForceMode.Impulse);
                            localpotion.tag = "MageSpell";
                            break;
                    }
                }
                else
                {
                    TargetToAttack.GetComponent<SoldierGeneric>().Health -= Damage;
                }
                hasHitTarget = true;
            }
            if(TargetDamageTimer >= AttackAnimationTime)
            {
                TargetDamageTimer = 0;
                hasHitTarget = false;
                TimerBetweenAttacks = maxAttackSpeed;
            }
            else
            {
                if(allowedToMove == true && isFrozen == false)
                {
                    position += speedMultiplier * Time.deltaTime;
                }
            }

            // Check if there is no target
            if(TargetToAttack == null)
            {
                try
                {
                    EnemyArray = GameObject.FindGameObjectsWithTag(attackTag);
                    TargetToAttack = GetClosestEnemy(EnemyArray, gameObject.transform);
                }
                catch
                { Debug.Log("Couldnt find a target!"); }
            }
        }
        else
        {
            fakeWeapon.SetActive(true);
        }
    }

    private GameObject GetClosestEnemy(GameObject[] enemies, Transform OriginObject)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = 30f; // Mathf.Infinity;
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

    private float DistanceBetween(Vector3 pos1, Vector3 pos2)
    {
        // Calculate distance of the Vectors
        float posX = pos1.x - pos2.x;
        float posY = pos1.y - pos2.y;
        float posZ = pos1.z - pos2.z;
        // Return the distance between the XY calculated position and the Z position for the final result.
        return Mathf.Sqrt((posX * posX) + (posY * posY) + (posZ * posZ));
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (gameObject.tag)
        {
            case "Mage":
                    isFrozen = true;
                    FreezeTimer = maxFreezeTime;
                break;
            case "Swordsmen":
                    isFrozen = true;
                    FreezeTimer = maxFreezeTime;
                break;
        }
    }
}
