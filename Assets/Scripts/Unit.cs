using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour {
    GameController game;
    private UnityEngine.UI.Image sprite;
    [Header("Cooldown for when the unit is bought")]
    [SerializeField] private float coolDown = 6f;
    [Header("The required points to buy this unit")]
    [SerializeField] private int requiredAmount;
    [Space(2)]
    [Header("Where this unit will spawn | Will be deleted from here later!")]
    [SerializeField] private Transform spawnPos;
    [Header("Unit to spawn/buy")]
    [SerializeField] private GameObject objectToSpawn;
    [Header("Give a color when the player doesn't have enough credits")]
    [SerializeField] private Color notEnoughCredits;
    private bool boughtUnit = true;
    private Color standardColor;


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
        if ( boughtUnit )
            timer += Time.deltaTime;
        if ( sprite.fillAmount <= 1 )
            sprite.fillAmount += ( ( coolDown / 100 ) * Time.deltaTime );
    }

    public void PurchaseUnit() {
        if ( game.points >= requiredAmount && timer >= coolDown ) {
            //int i = System.Convert.ToInt16(EventSystem.current.currentSelectedGameObject.name);
            sprite.fillAmount = 0;
            //print("Button name: " + i);
            game.points -= requiredAmount;
            boughtUnit = true;
            timer = 0f;
            Instantiate(objectToSpawn, spawnPos.position, spawnPos.rotation);
            Debug.Log("Bought unit");
        }
    }
}
