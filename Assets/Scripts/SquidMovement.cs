using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class SquidMovement : MonoBehaviour
{
    private enum PlayerStatus { Right, Left, Moving };
    private PlayerStatus squidStatus;
    [SerializeField]
    public float left = -5f;
    [SerializeField]
    public float right = 5f;
    public Rigidbody rb;
    public GameObject floor;
    private bool touchingWall;
    public GameObject camera;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        floor.SetActive(false);
        rb = gameObject.GetComponent<Rigidbody>();
        squidStatus = PlayerStatus.Right;
        touchingWall = true;
    }

    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
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
        if (Time.time < 5f)
        {
            return;
        }
        transform.position += new Vector3(0f, 0f, 0.1f);

        //Move left
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && (squidStatus == PlayerStatus.Right))
        {
            rb.AddForce(-20f, 10f, 0, ForceMode.Impulse);
            squidStatus = PlayerStatus.Moving;
            //Vector3 destination = new Vector3(left, transform.position.y, transform.position.z + 1f);
            //StartCoroutine(Jump(1f, transform.position, destination, PlayerStatus.Left));
        }

        //Move right
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && (squidStatus == PlayerStatus.Left))
        {
            rb.AddForce(20f, 10f, 0, ForceMode.Impulse);
            squidStatus = PlayerStatus.Moving;
            //Vector3 destination = new Vector3(right, transform.position.y, transform.position.z + 1f);
            //StartCoroutine(Jump(1f, transform.position, destination, PlayerStatus.Right));
        }
        if (transform.position.z < camera.transform.position.z || transform.position.x > right || transform.position.x < left || transform.position.y < 0f)
        {
            UnityEngine.Debug.Log("YOU LOSE");
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
