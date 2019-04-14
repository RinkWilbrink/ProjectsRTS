using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour {
    public bool isOccupied;

    private void Update() {
        LayerMask swordsLayerMask = LayerMask.GetMask("SwordsLayer");
        LayerMask magesLayerMask = LayerMask.GetMask("MagesLayer");
        Collider[] swordsmen = Physics.OverlapSphere(transform.position, transform.localScale.x, swordsLayerMask.value);
        Collider[] mages = Physics.OverlapSphere(transform.position, transform.localScale.x, magesLayerMask.value);
        for ( int i = 0; i < swordsmen.Length; i++ ) {
            if ( swordsmen[i].GetComponent<Looter>() )
                if ( isOccupied && swordsmen[i].GetComponent<Looter>().factionIndex == 1 ) {
                    GetComponent<MeshRenderer>().material.color = Color.green;
                }
        }

        for ( int i = 0; i < mages.Length; i++ ) {
            if ( mages[i].GetComponent<Looter>() )
                if ( isOccupied && mages[i].GetComponent<Looter>().factionIndex == 2 ) {
                    GetComponent<MeshRenderer>().material.color = Color.red;
                }
        }

        if ( !isOccupied ) {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
