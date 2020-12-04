using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    float lifeTime = 20.0f;
    public Material pinkMaterial;
    public Material purpleMaterial;
    public Material greenMaterial;
    private enum Color { Pink, Purple, Green };

    void Awake() 
    {
        Destroy(gameObject, lifeTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        Color thisColor = (Color)Random.Range(0, System.Enum.GetValues(typeof(Color)).Length);

        if (thisColor == Color.Pink)
        {
            Material mat = Resources.Load("M_Building1", typeof(Material)) as Material;
            gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = mat;
        }
        else if (thisColor == Color.Purple)
        {
            Material mat = Resources.Load("M_Building3", typeof(Material)) as Material;
            gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = mat;
        }
        else
        {
            Material mat = Resources.Load("M_Building2", typeof(Material)) as Material;
            //gameObject.GetComponent<Renderer>().material = mat;
            gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = mat;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
