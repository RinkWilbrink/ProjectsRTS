using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour {

    [SerializeField] float baseHealth = 100;
    private float maxHealth;
    private GameController game;

    private void Start() {
        maxHealth = GetComponent<Soldier>().health;
        if ( GetComponent<Soldier>().health <= 0 && tag == "Swordsmen" )
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        if ( GetComponent<Soldier>().health <= 0 && tag == "Mage" )
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void Update() {
        if ( GetComponent<Soldier>().health <= 0 && tag == "Swordsmen" )
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        if ( GetComponent<Soldier>().health <= 0 && tag == "Mage" )
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    //private void OnGUI() {
    //    Vector3 targetPos;
    //    targetPos = Camera.main.WorldToScreenPoint(transform.position);
    //    GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 100, 60), gameObject.GetComponent<Soldier>().soldierType.ToString()
    //        + "\n" + "HP: " + gameObject.GetComponent<Soldier>().health.ToString() + " | " + maxHealth + "\n" + game.playerPoints);
    //}
}
