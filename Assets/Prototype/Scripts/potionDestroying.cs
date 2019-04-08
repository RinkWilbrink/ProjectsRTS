using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionDestroying : MonoBehaviour
{
    // Variables
    public GameObject[] enemysToFreeze;
    public GameObject originMage;
    public bool hasHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        hasHit = true;
        
        Destroy(gameObject);
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }
}
