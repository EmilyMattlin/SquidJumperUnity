using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class SquidMovement : MonoBehaviour
{
    private enum PlayerStatus { Right, Left, Moving };
    private PlayerStatus squidStatus;
    private const float LEFT = -7f;
    private const float RIGHT = 7f;
    public Rigidbody rb;
    private bool touchingWall;
    public Camera camera;
    public GameObject marker;
    private bool loss;
    private bool jumped;
    public static bool paused;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
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
        transform.localEulerAngles = new Vector3(0, 90, 0);
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
        if (touchingWall)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        transform.position += new Vector3(0f, 0f, 0.15f);

        //Move left
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && (squidStatus == PlayerStatus.Right))
        {
            Jump(-20f);
        }
        //Move right
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && (squidStatus == PlayerStatus.Left))
        {
            Jump(20f);
        }

        if (transform.position.x > RIGHT || transform.position.z < camera.transform.position.z || transform.position.x < LEFT || transform.position.y < 1f)
        {
            onLoss();
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
        camera.GetComponent<CameraMovement>().onLoss();
        marker.GetComponent<BeatEffects>().onLoss();
    }

    void OnCollisionEnter(Collision collision)
    {
        touchingWall = true;

        if (collision.gameObject.tag == "Left Wall")
        {
            squidStatus = PlayerStatus.Left;
        }
        if (collision.gameObject.tag == "Right Wall")
        {
            squidStatus = PlayerStatus.Right;
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
        if (transform.position.y < 9f)
        {
            transform.position += new Vector3(0f, 1f, 0f);
        }
        if (Mathf.Abs(transform.position.x) - Mathf.Abs(camera.transform.position.x) < 7f)
        {
            rb.AddForce(0, 0.5f, 0, ForceMode.Impulse);
        }
        squidStatus = PlayerStatus.Moving;
        jumped = true;
    }
}
