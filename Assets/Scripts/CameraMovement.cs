using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform squid;
    private bool loss;
    public GameObject LoseUI;
    // Start is called before the first frame update
    void Start()
    {
        loss = false;
    }
    protected void LateUpdate()
    {
        if (SquidMovement.paused)
        {
            return;
        }
        if (Time.timeSinceLevelLoad < 5f)
        {
            return;
        }
        if (!loss)
        {
            transform.position += new Vector3(0f, 0f, 0.15f);
        }
        else
        {
            transform.LookAt(squid);
            LoseUI.SetActive(true);
        }
        if(transform.position.z - squid.position.z > 8f)
        {
            transform.position += new Vector3(0f, 0f, squid.position.z - 7f);
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
