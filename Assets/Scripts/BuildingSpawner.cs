using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{   
    private Vector3 locationLast;
    private float halfWidthLast;
    public  GameObject[] leftWalls;
    public GameObject[] rightWalls;
    private bool leftSide;
    private const float BuildingY = 8.8f;
    private bool loss;
    private Vector3 wallPos;

    // Start is called before the first frame update
    void Start()
    {
        locationLast = leftWalls[0].transform.position;
        halfWidthLast = 4f;
        leftSide = false;
        loss = false;
        wallPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        if (!loss)
        {
            transform.position += new Vector3(0f, 0f, 0.3f);
        }
    }
    void LateUpdate()
    {
        if (SquidMovement.paused)
        {
            return;
        }
        if (!loss)
        {
            //transform.position += new Vector3(0f, 0f, 0.15f);
            if (transform.position.z >= wallPos.z)
            {
                wallPos = GetComponent<BuildingSpawner>().createBuilding();
            }
        }
    }

    public void onLoss()
    {
        loss = true;
    }

    public Vector3 createBuilding()
    {
        GameObject wall;
        if (leftSide)
        {
            wall = leftWalls[Random.Range(0, leftWalls.Length)];

        }
        else
        {
            wall = rightWalls[Random.Range(0, rightWalls.Length)];
        }

        float halfWidth = (wall.transform.GetChild(0).gameObject.GetComponent<Renderer>().bounds.size.z)/2f;

        float yPos = BuildingY + Random.Range(-6f, 6f);

        Vector3 newWalllPos = new Vector3(wall.transform.position.x, yPos, locationLast.z + halfWidthLast + halfWidth);

        halfWidthLast = halfWidth;
        locationLast = newWalllPos;
        leftSide = !leftSide;

        GameObject newWall = Instantiate(wall, newWalllPos, transform.rotation);
        newWall.SetActive(true);
        newWall.AddComponent<WallBehavior>();
        return newWalllPos;
    }
}
