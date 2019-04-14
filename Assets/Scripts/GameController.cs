using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if ( MenuController.goldRush ) {
            Looter.goldEarned *= 2;
        } else
            Looter.goldEarned = 10;
    }

    float timer;
    private void Update() {
        if ( MenuController.goldRush )
            Looter.goldEarned = 20;
        else
            Looter.goldEarned = 10;

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

    public void ResumeGame() {
        isGamePaused = false;
    }

    public void BackToMenu() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
