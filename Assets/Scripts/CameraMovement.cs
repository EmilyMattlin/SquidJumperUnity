﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform squid;
    private bool loss;
    // Start is called before the first frame update
    void Start()
    {
        loss = false;
    }
    protected void LateUpdate()
    {
        if (Time.time < 5f)
        {
            return;
        }
        if (!loss)
        {
            transform.position += new Vector3(0f, 0f, 0.1f);
        }
        else
        {
            transform.LookAt(squid);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void onLoss()
    {
        loss = true;
    }
}
