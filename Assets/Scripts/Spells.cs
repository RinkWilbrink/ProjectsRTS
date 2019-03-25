using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType {
    lightning = 0,
    healing,
    rage

}

public class Spells : MonoBehaviour {
    public SpellType spells;
    public static float healing = .5f;
    public static float damage = 5;

    void Start() {
        if ( spells == SpellType.lightning )
            Debug.Log("lightning spell");
    }

    float timer;
    void Update() {
        timer += Time.deltaTime;
        if ( timer >= .2f && spells == SpellType.lightning )
            Destroy(gameObject);
        if ( timer >= 4f && spells == SpellType.healing )
            Destroy(gameObject);
        if ( timer >= 4f && spells == SpellType.rage )
            Destroy(gameObject);
    }
}
