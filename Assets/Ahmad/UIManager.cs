using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class UnityEventMethod : UnityEvent<int, int, string> {

}

public class UIManager : MonoBehaviour {
    [SerializeField] private Image[] sprites;
    private GameController game;
    private float looterCooldown = 6f;
    private float meleeCooldown = 2f;
    private float archerCooldown = 3f;
    private float spellcasterCooldown = 4f;
    private bool archerBought = false;
    private bool spellcasterBought = false;
    private bool meleeBought = false;
    private bool looterBought = false;

    private void Start() {
        game = FindObjectOfType<GameController>();
    }

    private void Update() {
        if ( sprites[0].fillAmount <= 1 ) {
            sprites[0].fillAmount += ( ( looterCooldown / 2 ) * Time.deltaTime );
        }
        if ( sprites[1].fillAmount <= 1 ) {
            sprites[1].fillAmount += ( ( meleeCooldown / 2 ) * Time.deltaTime );
        }
        if ( sprites[2].fillAmount <= 1 ) {
            sprites[2].fillAmount += ( ( archerCooldown / 2 ) * Time.deltaTime );
        }
        if ( sprites[3].fillAmount <= 1 ) {
            sprites[3].fillAmount += ( ( spellcasterCooldown / 2 ) * Time.deltaTime );
        }
    }

    // Purchase Unit
    /// <summary>
    /// Unit ID = number in array
    /// </summary>

    public void PurchaseUnit(  ) {
        //if ( /*!looterBought && */game.points >= requiredPoints ) {
        //sprites[unitID].fillAmount = 0;
        //game.points -= requiredPoints;
        //Debug.LogWarning("Bought " + unitName + "unit");
        //} else
        //    Debug.LogError("Too expensive");
    }
}
