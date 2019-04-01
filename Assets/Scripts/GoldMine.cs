using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour {
    public bool isOccupied;

    private void Update() {
        //Collider[] col = Physics.OverlapSphere(transform.position, transform.localScale.x);
        //if ( col.Length > 0 )
        //    isOccupied = true;
        //else
        //    isOccupied = false;

        //print(gameObject.name + " ? " + isOccupied);
    }
}
