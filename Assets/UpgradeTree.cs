using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTree : MonoBehaviour {
    [SerializeField] private GameObject[] tree;
    private bool isShowingMenu = false;

    private void Update() {
        if ( !isShowingMenu ) for ( int i = 0; i < tree.Length; i++ ) tree[i].SetActive(false);
        else for ( int i = 0; i < tree.Length; i++ ) tree[i].SetActive(true);
    }

    public void ShowMenu() {
        isShowingMenu = !isShowingMenu;
    }
}
