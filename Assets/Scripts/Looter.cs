using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looter : MonoBehaviour {
    [SerializeField, Range(1, 2)] private int factionIndex = 1;
    [SerializeField, Range(1, 5)] private float movementSpeed = 2;
    [SerializeField, Range(1, 20)] private float healthPoints = 5;
    [SerializeField] private Transform basePos;
    private GameController game;
    private EnemyBehaviour enemyBehaviour;
    private GameObject[] goldMine;
    private bool isGathering = false;
    private bool flipper;

    private void Start() {
        game = FindObjectOfType<GameController>();
        enemyBehaviour = FindObjectOfType<EnemyBehaviour>();
        basePos = GameObject.Find("Base0" + factionIndex).GetComponent<Transform>();
        goldMine = GameObject.FindGameObjectsWithTag("GoldMines");
        gameObject.name = "LooterFaction" + factionIndex;
        if ( factionIndex == 2 ) {
            flipper = true;
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<SpriteRenderer>().color = Color.red;
            movementSpeed *= -1;
            enemyBehaviour.looterUnits++;
        } else {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        maxHP = healthPoints;
    }

    float timer = 0f;
    bool abool = false;
    int testIndex;
    private void Update() {
        if ( healthPoints <= 0 )
            Dead();
        if ( isGathering )
            timer += Time.deltaTime;
        if ( timer > 3f && !abool ) {
            flipper = !flipper;
            abool = true;
        }
        if ( Input.GetKeyDown(KeyCode.D) )
            healthPoints -= 1;
        if ( timer > 3f ) {
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
            GetComponent<SpriteRenderer>().flipX = flipper;
        }

        for ( int i = 0; i < goldMine.Length; i++ ) {
            if ( Vector3.Distance(transform.position, goldMine[i].transform.position) < goldMine[i].transform.localScale.x && !isGathering && !goldMine[i].GetComponent<GoldMine>().isOccupied ) {
                // Start mining
                if ( factionIndex == 1 )
                    LooterController.playerLootersBusy++;
                else if ( factionIndex == 2 )
                    LooterController.enemyLootersBusy++;
                isGathering = true;
                goldMine[i].GetComponent<GoldMine>().isOccupied = true;
                testIndex = i;
            } else if ( Vector3.Distance(transform.position, goldMine[i].transform.position) > goldMine[i].transform.localScale.x && !isGathering ) {
                transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
            } else
                transform.Translate(Vector3.zero);

            //print(game.playerPoints);
            if ( isGathering && Vector3.Distance(transform.position, basePos.position) < basePos.localScale.x ) {
                timer = 0f;
                if ( factionIndex == 1 ) {
                    LooterController.playerLootersBusy--;
                    game.playerPoints += 10;
                } else if ( factionIndex == 2 ) {
                    LooterController.enemyLootersBusy--;
                    game.enemyPoints += 10;
                }
                isGathering = false;
                goldMine[testIndex].GetComponent<GoldMine>().isOccupied = false;
                abool = false;
                flipper = !flipper;
                GetComponent<SpriteRenderer>().flipX = flipper;
            }
        }
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

    float maxHP;
    private void OnGUI() {
        if ( factionIndex == 1 ) {
            Vector3 targetPos;
            targetPos = Camera.main.WorldToScreenPoint(transform.position);
            GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 80, 20), "HP: " + healthPoints + " / " + maxHP);
        }
    }

    private void Dead() {
        enemyBehaviour.looterUnits--;
        Destroy(gameObject);
    }
}
