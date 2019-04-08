using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    [SerializeField] private GameObject[] enemiesToSpawn;
    [SerializeField, Range(0, 5)] private float spawnCooldown = 2f;
    private GameController game;
    private bool unitSpawned = false;
    private float spawnTimer;
    public int looterUnits;
    public int unitsSpawned;

    private void Start() {
        game = FindObjectOfType<GameController>();
        Instantiate(enemiesToSpawn[0], transform.position, transform.rotation);
    }
    float testTimer;
    private void Update() {
        if ( game.enemyPoints >= 10 && looterUnits <= 1 && !unitSpawned )
            InstantiateUnit(enemiesToSpawn[0], 10);
        testTimer += Time.deltaTime;
        if ( unitsSpawned <= 100 && testTimer > 4f && game.enemyPoints >= 20 ) {
            int rand = Random.Range(1, enemiesToSpawn.Length);
            InstantiateUnit(enemiesToSpawn[rand], 20);
            testTimer = 0f;
        }

        //if ( game.enemyPoints >= 20 && looterUnits < 333 && !unitSpawned )
        //    InstantiateUnit(melee, 20);
        //if ( game.enemyPoints >= 30 && looterUnits < 333 && !unitSpawned )
        //    InstantiateUnit(ranged, 30);
        //if ( game.enemyPoints >= 30 && looterUnits < 333 && !unitSpawned )
        //    InstantiateUnit(spellcaster, 30);

        if ( unitSpawned )
            spawnTimer += Time.deltaTime;
        if ( spawnTimer > spawnCooldown ) {
            unitSpawned = false;
            spawnTimer = 0f;
        }
    }

    private void InstantiateUnit( GameObject go, int cost ) {
        Instantiate(go, transform.position, transform.rotation);
        Debug.Log(go.name + " spawned");
        unitsSpawned++;
        unitSpawned = true;
        game.enemyPoints -= cost;
    }
}
