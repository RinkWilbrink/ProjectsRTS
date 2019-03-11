using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looter : MonoBehaviour {
    [SerializeField, Range(1, 2)] private int factionIndex = 1;
    [SerializeField, Range(1, 5)] private float movementSpeed = 2;
    private Rigidbody rb;
    private Transform pointToMine;
    private Transform pointToMine2;
    private GameController game;
    private bool isGathering = false;

    void Start() {
        game = FindObjectOfType<GameController>();
        pointToMine = GameObject.Find("GathererPointStart" + factionIndex).GetComponent<Transform>();
        pointToMine2 = GameObject.Find("GathererPoint" + factionIndex).GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        if ( factionIndex == 2 )
            movementSpeed *= -1;
    }

    float timer = 0f;
    void Update() {
        if ( isGathering )
            timer += Time.deltaTime;
        if ( timer > 3f ) {
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
        }
        if ( Vector3.Distance(transform.position, pointToMine.position) < pointToMine.localScale.x && !isGathering ) {
            // Start mining
            isGathering = true;
        } else if ( Vector3.Distance(transform.position, pointToMine.position) > pointToMine.localScale.x && !isGathering ) {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        }
        print(game.points);
        if ( isGathering && Vector3.Distance(transform.position, pointToMine2.position) < pointToMine2.localScale.x ) {
            timer = 0f;
            game.points += 10;
            isGathering = false;
        }
    }

    private void OnDrawGizmos() {
        Color color = Color.green;
        Gizmos.color = color;
        Gizmos.color.a.Equals(0);
        Gizmos.DrawSphere(pointToMine.position, pointToMine.localScale.x);
    }
}
