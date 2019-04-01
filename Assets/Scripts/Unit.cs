﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour {
    [Header("Cooldown for when the unit is bought")]
    [SerializeField] private float coolDown = 6f;
    [Header("The required points to buy this unit")]
    [SerializeField] private int requiredAmount;
    [Space(2)]
    [Header("Unit to spawn/buy")]
    [SerializeField] private GameObject objectToSpawn;
    [Header("Give a color when the player doesn't have enough credits")]
    [SerializeField] private Color notEnoughCredits;
    private Color standardColor;
    private Image sprite;
    private bool boughtUnit = true;
    private GameController game;

    void Start() {
        game = FindObjectOfType<GameController>();
        sprite = GetComponent<UnityEngine.UI.Image>();
        standardColor = sprite.color;
    }

    private float timer;
    void Update() {
        if ( game.playerPoints < requiredAmount )
            sprite.color = notEnoughCredits;
        else if ( game.playerPoints >= requiredAmount )
            sprite.color = standardColor;
        if ( boughtUnit )
            timer += Time.deltaTime;
        if ( sprite.fillAmount <= 1 )
            sprite.fillAmount += ( ( coolDown / 100 ) * Time.deltaTime );
    }

    public void PurchaseUnit() {
        if ( game.playerPoints >= requiredAmount && timer >= coolDown ) {
            //int i = System.Convert.ToInt16(EventSystem.current.currentSelectedGameObject.name);
            sprite.fillAmount = 0;
            //print("Button name: " + i);
            game.playerPoints -= requiredAmount;
            boughtUnit = true;
            timer = 0f;
            Instantiate(objectToSpawn, game.factionSpawnPos[0].position, game.factionSpawnPos[0].rotation);
            Debug.Log("Bought unit");
        }
    }
}