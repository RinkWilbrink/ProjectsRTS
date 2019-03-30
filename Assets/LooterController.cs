using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooterController : MonoBehaviour {
    public static int playerLootersBusy = 0;
    public static int enemyLootersBusy = 0;

    private void Start() {
        playerLootersBusy = 0;
        enemyLootersBusy = 0;
    }

    private void Update() {
        Debug.LogWarning("Player's looters busy: " + playerLootersBusy);
        Debug.LogWarning("Enemy's looters busy: " + enemyLootersBusy);
    }
}
