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
        if (SquidMovement.paused || Time.timeSinceLevelLoad < 5f)
        {
            return;
        }
        /*if (!loss)
        {
            transform.position += new Vector3(0f, 0f, 0.15f);
        }*/
        if (loss)
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
    void FixedUpdate()
    {
        if (!SquidMovement.paused && Time.timeSinceLevelLoad > 5f && !loss)
        {
            transform.position += new Vector3(0f, 0f, 0.3f);
        }
    }
    public void onLoss()
    {
        loss = true;
    }
}
