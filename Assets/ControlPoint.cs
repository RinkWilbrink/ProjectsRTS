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
    }

    GameObject[] swordsmen;
    GameObject[] mages;
    private void Update() {
        swordsmen = GameObject.FindGameObjectsWithTag("Swordsmen");
        mages = GameObject.FindGameObjectsWithTag("Mage");

        if ( mages == null )
            Debug.LogWarning("No Mages in the battlefield");
        if ( swordsmen == null )
            Debug.LogWarning("No Swordsmen in the battlefield");

        if ( factionCapped == 0 )
            GetComponent<MeshRenderer>().material.color = Color.white;
        else if ( factionCapped == 1 )
            GetComponent<MeshRenderer>().material.color = Color.green;
        else if ( factionCapped == 2 )
            GetComponent<MeshRenderer>().material.color = Color.red;
        for ( int i = 0; i < swordsmen.Length; i++ ) {
            if ( Vector3.Distance(transform.position, swordsmen[i].transform.position) <= 10f ) {
                //swordsmen[i].GetComponent<Soldier>().
                if ( magesAreCapping && !isCappedBySwordsmen )
                    swordsmenAreCapping = true;
            }
        }

        for ( int i = 0; i < mages.Length; i++ ) {
            if ( Vector3.Distance(transform.position, mages[i].transform.position) <= 10f && swordsmenAreCapping && !isCappedByMages )
                magesAreCapping = true;
        }

        if ( swordsmenAreCapping && swordsmenSecureTimer <= maxTime )
            swordsmenSecureTimer += Time.deltaTime;

        if ( !swordsmenAreCapping && swordsmenSecureTimer >= 0f )
            swordsmenSecureTimer -= Time.deltaTime;

        if ( swordsmenSecureTimer >= maxTime )
            isCappedBySwordsmen = true;

        //if ( !swordsmenAreCapping )
        //    Check(mages, magesAreCapping, mageSecureTimer, isCappedByMages, 10f);
        if ( isCappedByMages )
            factionCapped = 2;
        if ( isCappedBySwordsmen )
            factionCapped = 1;

        print("Swordsmen Timer: " + swordsmenSecureTimer);
        print("Swordsmen are capping? " + swordsmenAreCapping);
        print("Mages are capping? " + magesAreCapping);
        print("Faction capped: " + factionCapped);
    }

    private void OnGUI() {
        Vector3 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        if ( factionCapped == 0 ) GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 300, 50), "No faction has capped");
        else GUI.Box(new Rect(targetPos.x, ( Screen.height - targetPos.y ) - 35, 100, 50), "Faction: " + factionCapped);
    }
}
