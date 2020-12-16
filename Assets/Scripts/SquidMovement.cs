using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class SquidMovement : MonoBehaviour
{
    private enum PlayerStatus { Right, Left, MovingLeft, MovingRight };
    private PlayerStatus squidStatus;
    private const float LEFT = -7f;
    private const float RIGHT = 7f;
    private bool touchingWall;
    private bool loss;
    private bool jumped;
    public Camera camera;
    public GameObject marker;
    public Rigidbody rb;
    public static bool paused;
    public ParticleSystem rightBurst;
    public ParticleSystem leftBurst;
    public GameObject rotator;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        squidStatus = PlayerStatus.Right;
        touchingWall = true;
        loss = false;
        jumped = false;
        paused = false;
    }

    protected void LateUpdate()
    {
        if (Time.timeSinceLevelLoad < 5f || loss || paused)
        {
            return;
        }
        if (touchingWall)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        transform.position += new Vector3(0f, 0f, 0.15f);

        //Move left
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && (squidStatus == PlayerStatus.Right))
        {
            squidStatus = PlayerStatus.MovingLeft;
            Jump(-20f);
        }
        //Move right
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && (squidStatus == PlayerStatus.Left))
        {
            squidStatus = PlayerStatus.MovingRight;
            Jump(20f);
        }

        if(squidStatus == PlayerStatus.MovingLeft)
        {
           // Vector3 relativePosition = new Vector3(-10f, transform.position.y, transform.position.z);
            //SM_Squid.transform.rotation = Quaternion.LookRotation(relativePosition);
        }
        else if (squidStatus == PlayerStatus.MovingLeft)
        {
            //Vector3 relativePosition = new Vector3(10f, transform.position.y, transform.position.z);
           // SM_Squid.transform.rotation = Quaternion.LookRotation(relativePosition);
        }

        if (transform.position.x > RIGHT || transform.position.z < camera.transform.position.z || transform.position.x < LEFT || transform.position.y < 2.5f)
        {
            onLoss();
        }
    }

    void Update()
    {
        if(paused && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            paused = false;
        }
        else if (Time.timeSinceLevelLoad < 5f || loss || paused)
        {
            return;
        }
        else if (transform.position.z > 15f && !jumped)
        {
            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(transform.position.x - 8f, transform.position.y, transform.position.z), 4f); // 4 is z axis radius of building
            if (hitColliders.Length > 0)
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        UnityEngine.Debug.Log("Jump to the left!");
        paused = true;
    }

    void onLoss()
    {
        loss = true;
        rb.constraints = RigidbodyConstraints.None;
        camera.GetComponent<CameraMovement>().onLoss();
        marker.GetComponent<BuildingSpawner>().onLoss();
    }

    void OnCollisionEnter(Collision collision)
    {
        touchingWall = true;

        if (collision.gameObject.tag == "Left Wall")
        {
            squidStatus = PlayerStatus.Left;
            leftBurst.Play(true);
        }
        if (collision.gameObject.tag == "Right Wall")
        {
            squidStatus = PlayerStatus.Right;
            rightBurst.Play(true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        touchingWall = false;
    }

    void Jump(float sideDist)
    {
        rb.AddForce(sideDist, 0, 0, ForceMode.Impulse);
        transform.position += new Vector3(0,0,0.1f);
        camera.transform.position += new Vector3(0, 0, 0.1f);
        if (transform.position.z - camera.transform.position.z < 5f)
        {
            transform.position += new Vector3(0f, 0f, 0.75f);
        }
        if (transform.position.y < 7.5f)
        {
            transform.position += new Vector3(0f, 0.75f, 0f);
        }
        if (Mathf.Abs(transform.position.x) - Mathf.Abs(camera.transform.position.x) < 7f)
        {
            rb.AddForce(0, 0.5f, 0, ForceMode.Impulse);
        }
        jumped = true;
    }
}
