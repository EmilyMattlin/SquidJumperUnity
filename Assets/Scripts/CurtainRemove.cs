using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainRemove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0f, 3f, 0f);
        if (Time.timeSinceLevelLoad > 5f)
        {
            Destroy(this.gameObject);
        }
    }
}
