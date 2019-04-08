using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTree : MonoBehaviour {
    [SerializeField] private GameObject unit;
    [SerializeField] private int requiredMana;
    private GameController game;

    private void Start() {
        game = FindObjectOfType<GameController>();
    }

    private void Update() {
        if ( game.playerMana <= requiredMana ) {
            Color color = new Color(GetComponent<UnityEngine.UI.Image>().color.r, GetComponent<UnityEngine.UI.Image>().color.g, GetComponent<UnityEngine.UI.Image>().color.b);
            Color colorApply = GetComponent<UnityEngine.UI.Image>().color = new Color(color.r, color.g, color.b, .2f);
            GetComponent<UnityEngine.UI.Image>().color = colorApply;
        } else {
            Color color = new Color(GetComponent<UnityEngine.UI.Image>().color.r, GetComponent<UnityEngine.UI.Image>().color.g, GetComponent<UnityEngine.UI.Image>().color.b);
            Color colorApply = new Color(color.r, color.g, color.b, 1);
            GetComponent<UnityEngine.UI.Image>().color = colorApply;
        }
    }

    public void UnlockUnit() {
        if ( game.playerMana >= requiredMana && !unit.GetComponent<Unit>().isUnlocked ) {
            unit.GetComponent<Unit>().isUnlocked = true;
            game.playerMana -= requiredMana;
        }
    }
}
