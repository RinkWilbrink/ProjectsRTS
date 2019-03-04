using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoldiers : MonoBehaviour
{
	// Variables
	[SerializeField] GameObject soldier;
	[SerializeField] private KeyCode kc_SpawnSoldier;
	
    void Start()
    {
        
    }
	
    void Update()
    {
        if(Input.GetKeyDown(kc_SpawnSoldier))
		{
			Debug.Log("Pressed " + kc_SpawnSoldier);
			Instantiate(soldier, new Vector3(0, .5f, 0), Quaternion.identity);
		}
    }
}
