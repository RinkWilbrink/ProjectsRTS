using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    [SerializeField] private GameObject looter;
    [SerializeField] private GameObject melee;
    [SerializeField] private GameObject ranged;
    [SerializeField] private GameObject spellcaster;
    public int looterUnits;
    private GameController game;
    bool unitSpawned;
    float spawnTimer;
    [SerializeField, Range(0, 5)] private float spawnCooldown = 2f;

    private void Start() {
        game = FindObjectOfType<GameController>();
        Instantiate(looter, transform.position, transform.rotation);
    }

    private void Update() {
        if ( game.enemyPoints >= 10 && looterUnits < 2 && !unitSpawned )
            InstantiateUnit(looter);
        if ( game.enemyPoints >= 20 && looterUnits < 333 && !unitSpawned )
            InstantiateUnit(melee);
        if ( game.enemyPoints >= 30 && looterUnits < 333 && !unitSpawned )
            InstantiateUnit(ranged);
        if ( game.enemyPoints >= 30 && looterUnits < 333 && !unitSpawned )
            InstantiateUnit(spellcaster);

        if ( unitSpawned )
            spawnTimer += Time.deltaTime;
        if ( spawnTimer > spawnCooldown ) {
            unitSpawned = false;
            spawnTimer = 0f;
        }
    }

    private void InstantiateUnit( GameObject go ) {
        Instantiate(go, transform.position, transform.rotation);
        unitSpawned = true;
    }
}
