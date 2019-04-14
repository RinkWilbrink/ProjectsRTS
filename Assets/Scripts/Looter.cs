using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looter : MonoBehaviour {
    [SerializeField, Range(1, 5)] private float movementSpeed = 2;
    [SerializeField] private Transform basePos;
    [Range(1, 2)] public int factionIndex = 1;
    private GameController game;
    private EnemyBehaviour enemyBehaviour;
    private GameObject[] goldMine;
    private bool isGathering = false;
    private bool flipper;
    public static int goldEarned = 10;

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
    }

    float timer = 0f;
    bool abool = false;
    int testIndex;
    private void Update() {

        if ( isGathering )
            timer += Time.deltaTime;
        if ( timer > 3f && !abool ) {
            flipper = !flipper;
            abool = true;
            goldMine[testIndex].GetComponent<GoldMine>().isOccupied = false;
        }

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
                    game.playerPoints += goldEarned;
                } else if ( factionIndex == 2 ) {
                    LooterController.enemyLootersBusy--;
                    game.enemyPoints += 10;
                }
                isGathering = false;
                abool = false;
                flipper = !flipper;
                GetComponent<SpriteRenderer>().flipX = flipper;
            }
        }
    }
}
