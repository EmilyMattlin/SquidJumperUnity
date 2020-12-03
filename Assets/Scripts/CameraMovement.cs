using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected void LateUpdate()
    {
        if (Time.time < 5f)
        {
            return;
        }
        transform.position += new Vector3(0f, 0f, 0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
