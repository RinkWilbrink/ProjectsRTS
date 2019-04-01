using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    [SerializeField, Range(1, 10)] private float healthPoints = 5;
    private float factionIndex;
    private EnemyBehaviour enemyBehaviour;

    private void Start() {
        factionIndex = GetComponent<Looter>().factionIndex;
        healthPoints = GetComponent<Soldier>().Health;
        enemyBehaviour = FindObjectOfType<EnemyBehaviour>();
    }

    private void Update() {
        if ( healthPoints <= 0 )
            Dead();
        if ( Input.GetKeyDown(KeyCode.D) )
            healthPoints -= 1;
    }

    private void OnTriggerEnter( Collider collision ) {
        if ( collision.gameObject.tag == "DamageSpell" && factionIndex == 2 )
            healthPoints -= Spells.damage;
        if ( collision.gameObject.tag == "HealingSpell" && factionIndex == 1 )
            healthPoints += Spells.healing;
    }

    private void OnTriggerStay( Collider collision ) {
        if ( collision.gameObject.tag == "HealingSpell" && factionIndex == 1 )
            healthPoints += Spells.healing * Time.deltaTime;
    }

    private void Dead() {
        if ( factionIndex == 2 ) {
            enemyBehaviour.looterUnits--;
            enemyBehaviour.unitsSpawned--;
        }
        Destroy(gameObject);
    }
}
