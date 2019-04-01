using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGUI : MonoBehaviour {
    private int maxHealth;

    private void Start() {
        maxHealth = gameObject.GetComponent<Soldier>().Health;
    }

    private void OnGUI() {
        Vector3 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 60, 40), gameObject.GetComponent<Soldier>().soldierType.ToString()
            + "\n" + "HP: " + gameObject.GetComponent<Soldier>().Health.ToString() + " | " + maxHealth);
    }
}
