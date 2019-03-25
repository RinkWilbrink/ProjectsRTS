using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private Image[] sprites;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float meleeCooldown = 2f;
    [SerializeField] private float archerCooldown = 3f;
    [SerializeField] private float spellcasterCooldown = 4f;
    [SerializeField] private float coolDown;
    private bool boughtUnit = true;
    private float timer;
    private GameController game;

    private void Start() {
        game = FindObjectOfType<GameController>();
    }

    private void Update() {
        if ( boughtUnit )
            timer += Time.deltaTime;
        // Check hoe ik de tijd goed kan krijgen
        
        if ( sprites[1].fillAmount <= 1 )
            sprites[1].fillAmount += ( ( meleeCooldown / 100 ) * Time.deltaTime );
        if ( sprites[2].fillAmount <= 1 )
            sprites[2].fillAmount += ( ( archerCooldown / 100 ) * Time.deltaTime );
        
    }

    
}
