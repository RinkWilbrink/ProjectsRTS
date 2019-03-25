using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {
    GameController game;
    private UnityEngine.UI.Image sprite;
    [Header("Cooldown for when the unit is bought")]
    [SerializeField] private float coolDown = 6f;
    [Header("The required points to buy this unit")]
    [SerializeField] private int requiredAmount;
    [Space(2)]
    [Header("Unit to spawn/buy")]
    [SerializeField] private GameObject spellToSpawn;
    [Header("Give a color when the player doesn't have enough credits")]
    [SerializeField] private Color notEnoughCredits;
    private Color standardColor;
    private bool canCastSpell = false;

    void Start() {
        game = FindObjectOfType<GameController>();
        sprite = GetComponent<UnityEngine.UI.Image>();
        standardColor = sprite.color;
    }

    private float timer;
    void Update() {
        if ( game.points < requiredAmount )
            sprite.color = notEnoughCredits;
        else if ( game.points >= requiredAmount )
            sprite.color = standardColor;
        if ( !canCastSpell )
            timer += Time.deltaTime;
        if ( sprite.fillAmount <= 1 )
            sprite.fillAmount += ( ( coolDown / 100 ) * Time.deltaTime );
        if ( canCastSpell ) {
            // drag
        }

        if ( canCastSpell && Input.GetMouseButtonDown(0) ) {
            // cast spell
            Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vec.y = 4.21f;
            vec.z = 10;
            Instantiate(spellToSpawn, vec, Quaternion.identity);
            canCastSpell = false;
        }
    }

    public void PurchaseSpell() {
        if ( game.points >= requiredAmount && timer >= coolDown && !canCastSpell ) {
            //int i = System.Convert.ToInt16(EventSystem.current.currentSelectedGameObject.name);
            sprite.fillAmount = 0;
            //print("Button name: " + i);
            game.points -= requiredAmount;
            timer = 0f;
            canCastSpell = true;
            //Instantiate(objectToSpawn, spawnPos.position, spawnPos.rotation);
            Debug.Log("Bought Spell");
        }
    }
}
