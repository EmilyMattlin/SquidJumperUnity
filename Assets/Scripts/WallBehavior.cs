﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    float lifeTime = 20.0f;

    void Awake() 
    {
//        gameObject.SetActive(true);
        Destroy(gameObject, lifeTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
