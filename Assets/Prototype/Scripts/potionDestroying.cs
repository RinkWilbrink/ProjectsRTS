using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionDestroying : MonoBehaviour
{
    public bool hasHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        hasHit = true;
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }
}
