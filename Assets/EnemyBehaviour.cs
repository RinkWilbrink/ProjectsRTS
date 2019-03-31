using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    [SerializeField] private GameObject looter;
    [SerializeField] private GameObject melee;
    [SerializeField] private GameObject ranged;
    [SerializeField] private GameObject spellcaster;
    [SerializeField, Range(0, 5)] private float spawnCooldown = 2f;
    private GameController game;
    public int looterUnits;
    bool unitSpawned;
    float spawnTimer;

    private void Start() {
        game = FindObjectOfType<GameController>();
        Instantiate(looter, transform.position, transform.rotation);
    }

    private void Update() {
        if ( game.enemyPoints >= 10 && looterUnits < 2 && !unitSpawned )
            InstantiateUnit(looter, 10);
        if ( game.enemyPoints >= 20 && looterUnits < 333 && !unitSpawned )
            InstantiateUnit(melee, 20);
        if ( game.enemyPoints >= 30 && looterUnits < 333 && !unitSpawned )
            InstantiateUnit(ranged, 30);
        if ( game.enemyPoints >= 30 && looterUnits < 333 && !unitSpawned )
            InstantiateUnit(spellcaster, 30);

        if ( unitSpawned )
            spawnTimer += Time.deltaTime;
        if ( spawnTimer > spawnCooldown ) {
            unitSpawned = false;
            spawnTimer = 0f;
        }
    }

    private void InstantiateUnit( GameObject go, int cost ) {
        Instantiate(go, transform.position, transform.rotation);
        //Debug.Log(go + "spawned");
        unitSpawned = true;
        //game.enemyPoints -= cost;
    }
}
