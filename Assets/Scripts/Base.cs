using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    [SerializeField] int baseHealth = 100;

    private void Start() {
        baseHealth = GetComponent<Soldier>().Health;
    }

    private void Update() {
        if ( baseHealth <= 0 && tag == "Swordsmen" )
            Debug.LogError("Player " + "2" + " has won!");
        if ( baseHealth <= 0 && tag == "Mage" )
            Debug.LogError("Player " + "1" + " has won!");
    }

    //private void OnGUI() {
    //    Vector3 targetPos;
    //    targetPos = Camera.main.WorldToScreenPoint(transform.position);
    //    GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 40, 20), baseHP.ToString());
    //}
}
