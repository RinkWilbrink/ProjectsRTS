﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverEffect : MonoBehaviour {
    [SerializeField, Range(1, 3)] private float maxScale = 1.2f;
    [SerializeField, Range(0, 10)] private float speedEffect = 1.77f;
    bool someBool = false;

    private void Update() {
        if ( transform.localScale.x < maxScale && someBool )
            transform.localScale += new Vector3(speedEffect * Time.deltaTime, speedEffect * Time.deltaTime, speedEffect * Time.deltaTime);
        else if ( transform.localScale.x > 1f && !someBool )
            transform.localScale -= new Vector3(speedEffect * Time.deltaTime, speedEffect * Time.deltaTime, speedEffect * Time.deltaTime);
    }

    public void IncreaseSize() {
        print("hallo");
        someBool = true;
    }

    public void DecreaseSize() {
        print("doei");
        someBool = false;
    }
}
