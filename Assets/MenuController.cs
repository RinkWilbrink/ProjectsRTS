using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public static bool goldRush = false;

    private void Start() {
        goldRush = false;
    }

    public void StartClassicMode() {
        SceneManager.LoadScene("Classic", LoadSceneMode.Single);
        goldRush = false;
    }

    public void StartKOTH() {
        SceneManager.LoadScene("KOTH", LoadSceneMode.Single);
        goldRush = false;
    }

    public void StartGoldRush() {
        SceneManager.LoadScene("Classic", LoadSceneMode.Single);
        goldRush = true;
    }
}
