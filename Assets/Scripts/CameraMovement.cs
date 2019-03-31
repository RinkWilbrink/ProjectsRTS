using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] private float screenEdgeSize = 100f;
    [SerializeField] private float speed = 2f;

    private void Update() {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -30f, 38.96f), transform.position.y, transform.position.z);

        if ( Input.mousePosition.x > Screen.width - screenEdgeSize ) {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if ( Input.mousePosition.x < screenEdgeSize ) {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
