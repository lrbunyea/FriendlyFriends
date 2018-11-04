﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour {

    // The target marker.
    public GameObject targetGO;
    private Transform target;

    // Speed in units per sec.
    public float speed;

    void Start()
    {
        target = targetGO.GetComponent<Transform>();
    }

    void Update()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}