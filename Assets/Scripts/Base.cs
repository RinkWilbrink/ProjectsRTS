using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    [SerializeField] int baseHP = 100;

    private void OnGUI() {
        Vector3 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 40, 20), baseHP.ToString());
    }
}
