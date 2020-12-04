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

    // Start is called before the first frame update
    void Start()
    {
        locationLast = leftWalls[0].transform.position;
        halfWidthLast = 4f;
        leftSide = false;
    }

    // Update is called once per frame
    void Update()
    {
        leftSide = !leftSide;
        if ((Time.time % 1f) < .1f)
        {
            createBuilding();
        }
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
        
        GameObject newWall = Instantiate(wall, newWalllPos, transform.rotation);
        newWall.SetActive(true);
        newWall.AddComponent<WallBehavior>();
        return newWalllPos;
    }
}
