using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField] private GameObject[] UI;
    [SerializeField] private TMPro.TextMeshProUGUI manaText;
    [SerializeField] private TMPro.TextMeshProUGUI goldText;
    public int playerPoints;
    public int playerMana;
    public int enemyPoints;
    public Transform factionSpawnPos;
    private bool isGamePaused;

    private void Start() {
        if ( factionSpawnPos == null )
            factionSpawnPos = GameObject.Find("Base01").GetComponent<Transform>();
    }

    float timer;
    private void Update() {
        if ( Input.GetKeyDown(KeyCode.Escape) )
            isGamePaused = !isGamePaused;
        timer += Time.deltaTime;
        if ( timer >= .5f ) {
            playerMana++;
            timer = 0f;
        }
        manaText.text = "Mana: " + playerMana;
        goldText.text = "Gold: " + playerPoints;
        if ( isGamePaused ) {
            for ( int i = 0; i < UI.Length; i++ ) UI[i].SetActive(true);
            Time.timeScale = 0;
        } else {
            for ( int i = 0; i < UI.Length; i++ ) UI[i].SetActive(false);
            Time.timeScale = 1;
        }
    }
}
