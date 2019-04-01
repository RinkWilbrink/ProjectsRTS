using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    private EnemyBehaviour enemyBehaviour;

    private void Start() {
        enemyBehaviour = FindObjectOfType<EnemyBehaviour>();
    }

    private void Update() {
        if ( gameObject.GetComponent<Soldier>().Health <= 0 )
            Dead();
        if ( Input.GetKeyDown(KeyCode.D) )
            gameObject.GetComponent<Soldier>().Health -= 1;
    }

    private void OnTriggerEnter( Collider collision ) {
        if ( collision.gameObject.tag == "DamageSpell" && gameObject.GetComponent<Looter>().factionIndex == 2 || tag == "Mage" )
            gameObject.GetComponent<Soldier>().Health -= Spells.damage;
        if ( collision.gameObject.tag == "HealingSpell" && gameObject.GetComponent<Looter>().factionIndex == 1 || tag == "Swordsmen" )
            gameObject.GetComponent<Soldier>().Health += Spells.healing;
    }

    private void OnTriggerStay( Collider collision ) {
        if ( collision.gameObject.tag == "HealingSpell" && gameObject.GetComponent<Looter>().factionIndex == 1 || tag == "Swordsmen" )
            gameObject.GetComponent<Soldier>().Health += Spells.healing * Time.deltaTime;
    }

    private void Dead() {
        if ( gameObject.GetComponent<Looter>().factionIndex == 2 ) {
            enemyBehaviour.looterUnits--;
            enemyBehaviour.unitsSpawned--;
        }
        Destroy(gameObject);
    }
}
