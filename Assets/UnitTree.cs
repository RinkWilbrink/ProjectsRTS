using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTree : MonoBehaviour {
    [SerializeField] private GameObject unit;
    [SerializeField] private TMPro.TextMeshProUGUI requiredManaText;
    [SerializeField] private int requiredMana;
    private GameController game;

    private void Start() {
        game = FindObjectOfType<GameController>();
        requiredManaText.text = requiredMana.ToString();
    }

    GameObject[] swordsmen;
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
        if ( upgraded ) {
            swordsmen = GameObject.FindGameObjectsWithTag("Swordsmen");
            for ( int i = 0; i < swordsmen.Length; i++ ) {
                if ( swordsmen[i].GetComponent<Soldier>().soldierType != SoldierType.Looter ) {
                    swordsmen[i].GetComponent<Soldier>().health = ( swordsmen[i].GetComponent<Soldier>().health + .5f );
                    swordsmen[i].GetComponent<Soldier>().damage = ( swordsmen[i].GetComponent<Soldier>().damage + .5f );
                }
            }
        }
    }

    public void UnlockUnit() {
        if ( game.playerMana >= requiredMana && !unit.GetComponent<Unit>().isUnlocked ) {
            unit.GetComponent<Unit>().isUnlocked = true;
            game.playerMana -= requiredMana;
        }
    }

    public void UnlockSpell() {
        if ( game.playerMana >= requiredMana && !unit.GetComponent<Spell>().isUnlocked ) {
            unit.GetComponent<Spell>().isUnlocked = true;
            game.playerMana -= requiredMana;
        }
    }
    bool upgraded = false;
    public void UpgradeUnit() {
        if ( game.playerMana >= requiredMana && !upgraded ) {

            game.playerMana -= requiredMana;
        }
    }
}
