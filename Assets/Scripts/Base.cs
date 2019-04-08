using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    [SerializeField] float baseHealth = 100;
    private float maxHealth;
    private GameController game;

    private void Start() {
        maxHealth = GetComponent<Soldier>().Health;
        baseHealth = maxHealth;
        game = FindObjectOfType<GameController>();
    }

    private void Update() {
        if ( baseHealth <= 0 && tag == "Swordsmen" )
            Debug.LogError("Player " + "2" + " has won!");
        if ( baseHealth <= 0 && tag == "Mage" )
            Debug.LogError("Player " + "1" + " has won!");
    }

    private void OnGUI() {
        Vector3 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 100, 60), gameObject.GetComponent<Soldier>().soldierType.ToString()
            + "\n" + "HP: " + gameObject.GetComponent<Soldier>().Health.ToString() + " | " + maxHealth + "\n" + game.playerPoints);
    }
}
