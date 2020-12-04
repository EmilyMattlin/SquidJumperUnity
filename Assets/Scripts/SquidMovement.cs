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
    public GameObject floor;
    private bool touchingWall;
    public Camera camera;
    private bool loss;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        floor.SetActive(false);
        loss = false;
        rb = gameObject.GetComponent<Rigidbody>();
        squidStatus = PlayerStatus.Right;
        touchingWall = true;
    }

    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 90, 0);
    }

    void Update()
    {

        if (touchingWall)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        if (Time.time < 5f || loss)
        {
            return;
        }
        transform.position += new Vector3(0f, 0f, 0.1f);

        //Move left
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && (squidStatus == PlayerStatus.Right))
        {
            rb.AddForce(-20f, 0, 0, ForceMode.Impulse);
            if (transform.position.y < 10)
            {
                transform.position += new Vector3(0f, 0.75f, 0f);
            }
            squidStatus = PlayerStatus.Moving;
            //Vector3 destination = new Vector3(left, transform.position.y, transform.position.z + 1f);
            //StartCoroutine(Jump(1f, transform.position, destination, PlayerStatus.Left));
        }

        //Move right
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && (squidStatus == PlayerStatus.Left))
        {
            rb.AddForce(20f, 0, 0, ForceMode.Impulse);
            if (transform.position.y < 10)
            {
                transform.position += new Vector3(0f, 0.75f, 0f);
            }
            squidStatus = PlayerStatus.Moving;
            //Vector3 destination = new Vector3(right, transform.position.y, transform.position.z + 1f);
            //StartCoroutine(Jump(1f, transform.position, destination, PlayerStatus.Right));
        }
        if (transform.position.x > RIGHT || transform.position.z < camera.transform.position.z || transform.position.x < LEFT || transform.position.y < 1f)
        {
            loss = true;
            camera.GetComponent<CameraMovement>().onLoss();
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }*/

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
    /*
    IEnumerator Jump(float waitTime, Vector3 start, Vector3 dest, PlayerStatus afterStatus)
    {
        squidStatus = PlayerStatus.Moving;
        float i = 0f;
        while (i < 1f)
        {
            transform.position = Vector3.Lerp(start, dest, i);
            //if (i < 0.5f)
            //{
            //  transform.position = Vector3.Lerp(start, start + new Vector3(0, 2f, 0), i);
            //}
            i += 0.2f;
            yield return null;
        }
        squidStatus = afterStatus;
        yield return 0;
    }*/
}
