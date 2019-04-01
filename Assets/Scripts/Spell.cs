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
        StartCoroutine("CountDown", coolDown);
    }

    private float timer;
    void Update() {
        if ( game.playerPoints < requiredAmount )
            sprite.color = notEnoughCredits;
        else if ( game.playerPoints >= requiredAmount )
            sprite.color = standardColor;
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

    private IEnumerator CountDown( float time ) {
        StartCoroutine("CountDownAnimation", time);
        yield return new WaitForSeconds(time);
    }
    float animationTime;
    private IEnumerator CountDownAnimation( float time ) {
        animationTime = 0f;
        while ( animationTime <= coolDown ) {
            animationTime += Time.deltaTime;
            sprite.fillAmount = animationTime / time;
            yield return null;
        }
    }

    public void PurchaseSpell() {
        if ( game.playerPoints >= requiredAmount && animationTime >= coolDown && !canCastSpell ) {
            //int i = System.Convert.ToInt16(EventSystem.current.currentSelectedGameObject.name);
            sprite.fillAmount = 0;
            //print("Button name: " + i);
            game.playerPoints -= requiredAmount;
            timer = 0f;
            animationTime = 0f;
            canCastSpell = true;
            //Instantiate(objectToSpawn, spawnPos.position, spawnPos.rotation);
            Debug.Log("Bought Spell");
            StartCoroutine("CountDown", coolDown);
        }
    }
}
