using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField] private GameObject[] UI;
    public int playerPoints;
    public int enemyPoints = 10;
    public Transform[] factionSpawnPos;
    private bool isGamePaused;

    private void Update() {
        if ( Input.GetKeyDown(KeyCode.Escape) )
            isGamePaused = !isGamePaused;

        if ( isGamePaused ) {
            for ( int i = 0; i < UI.Length; i++ ) UI[i].SetActive(true);
            Time.timeScale = 0;
        } else {
            for ( int i = 0; i < UI.Length; i++ ) UI[i].SetActive(false);
            Time.timeScale = 1;
        }
    }
}
