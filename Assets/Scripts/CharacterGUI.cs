using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGUI : MonoBehaviour {
    private void OnGUI() {
        Vector3 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 80, 20), gameObject.GetComponent<Soldier>().soldierType.ToString());
    }
}
