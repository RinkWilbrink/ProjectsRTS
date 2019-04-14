using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour {
    [SerializeField] private float maxTime;
    private float mageSecureTimer;
    private float swordsmenSecureTimer;
    private bool isCappedBySwordsmen = false;
    private bool isCappedByMages = false;
    private int factionCapped = 0;
    private float capTime;
    private float swordsmenMaxCapTimer;
    private float mageMaxCapTimer;
    private float maxCapTime = 120f;
    private Vector3 zonePos;
    /// <summary>
    /// faction index:
    /// 0 = uncapped
    /// 1 = player faction
    /// 2 = AI faction
    /// </summary>
    private bool magesAreCapping = false;
    private bool swordsmenAreCapping = false;

    private void Start() {
        factionCapped = 0;
        zonePos = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
    }

    //GameObject[] swordsmen;
    //GameObject[] mages;
    //Vector3 controlPointSize = new Vector3(3, 10, 3);
    float controlPointSize = .1f;
    bool mageInZone;
    bool swordsmenInZone;
    private void Update() {
        //swordsmen = GameObject.FindGameObjectsWithTag("Swordsmen");
        //mages = GameObject.FindGameObjectsWithTag("Mage");

        //if ( mages == null )
        //    Debug.LogWarning("No Mages in the battlefield");
        //if ( swordsmen == null )
        //    Debug.LogWarning("No Swordsmen in the battlefield");

        if ( factionCapped == 0 )
            GetComponent<MeshRenderer>().material.color = Color.white;
        else if ( factionCapped == 1 )
            GetComponent<MeshRenderer>().material.color = Color.green;
        else if ( factionCapped == 2 )
            GetComponent<MeshRenderer>().material.color = Color.red;

        LayerMask swordsLayerMask = LayerMask.GetMask("SwordsLayer");
        LayerMask magesLayerMask = LayerMask.GetMask("MagesLayer");
        Collider[] swordsmen = Physics.OverlapSphere(zonePos, controlPointSize, swordsLayerMask.value);
        Collider[] mages = Physics.OverlapSphere(zonePos, controlPointSize, magesLayerMask.value);

        if ( swordsmen.Length == 0 )
            swordsmenInZone = false;
        if ( swordsmen.Length >= 1 )
            swordsmenInZone = true;
        if ( mages.Length == 0 )
            mageInZone = false;
        if ( mages.Length >= 1 )
            mageInZone = true;

        for ( int swordsmenIndex = 0; swordsmenIndex < swordsmen.Length; swordsmenIndex++ ) {
            if ( swordsmen[swordsmenIndex].tag == "Swordsmen" ) {
                if ( !isCappedBySwordsmen )
                    swordsmen[swordsmenIndex].GetComponent<Soldier>().allowedToMove = false;
                else
                    swordsmen[swordsmenIndex].GetComponent<Soldier>().allowedToMove = true;

                if ( !mageInZone && swordsmenInZone && !isCappedBySwordsmen )
                    swordsmenAreCapping = true;
                else
                    swordsmenAreCapping = false;
            }
        }

        for ( int mageIndex = 0; mageIndex < mages.Length; mageIndex++ ) {
            if ( mages[mageIndex].tag == "Mage" ) {
                if ( !isCappedByMages )
                    mages[mageIndex].GetComponent<Soldier>().allowedToMove = false;
                else
                    mages[mageIndex].GetComponent<Soldier>().allowedToMove = true;

                if ( !swordsmenInZone && mageInZone && !isCappedByMages )
                    magesAreCapping = true;
                else
                    magesAreCapping = false;
            }
        }

        //print("Swordsman is in the zone: " + swordsmenInZone);
        //print("Swordsmen are capping: " + swordsmenAreCapping);
        //print("Mages are capping: " + magesAreCapping);
        //print("Mage is in the zone: " + mageInZone);

        if ( swordsmenAreCapping && !magesAreCapping ) {
            swordsmenSecureTimer += Time.deltaTime;
            //if ( mageSecureTimer >= .1f )
            //    mageSecureTimer -= Time.deltaTime;
        }
        if ( magesAreCapping && !swordsmenAreCapping ) {
            mageSecureTimer += Time.deltaTime;
            //if ( swordsmenSecureTimer >= .1f )
            //    swordsmenSecureTimer -= Time.deltaTime;
        }

        if ( swordsmenSecureTimer >= maxTime && !isCappedBySwordsmen ) {
            isCappedByMages = false;
            isCappedBySwordsmen = true;
            swordsmenSecureTimer = 0f;
        }
        if ( mageSecureTimer >= maxTime && !isCappedByMages ) {
            isCappedBySwordsmen = false;
            isCappedByMages = true;
            mageSecureTimer = 0f;
        }

        if ( isCappedBySwordsmen ) {
            swordsmenMaxCapTimer += Time.deltaTime;
            factionCapped = 1;
        }
        if ( isCappedByMages ) {
            mageMaxCapTimer += Time.deltaTime;
            factionCapped = 2;
        }

        if ( swordsmenMaxCapTimer > maxCapTime )
            print("swordsmen have won!");
        if ( mageMaxCapTimer > maxCapTime )
            print("mages have won!");
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(zonePos, .1f);
    }

    private void OnGUI() {
        Vector3 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        if ( factionCapped == 0 ) GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 150, 30), "No faction has capped");
        else if ( factionCapped == 1 ) GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 100, 50), "Faction: " + factionCapped + "\n" + "Progress: " + (int)swordsmenMaxCapTimer + "/" + maxCapTime);
        else if ( factionCapped == 2 ) GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 100, 50), "Faction: " + factionCapped + "\n" + "Progress: " + (int)mageMaxCapTimer + "/" + maxCapTime);
    }
}
