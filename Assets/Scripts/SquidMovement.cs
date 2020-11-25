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
    public float left = -4.15f;
    [SerializeField]
    public float right = 4.15f;
    public Rigidbody rb;

    void Start()
    {
        //Invoke("DelayStart", 5f); //Trying to make 5 second delay
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        rb = gameObject.GetComponent<Rigidbody>();
        squidStatus = PlayerStatus.Left;
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(5f);
    }

    void Update()
    {
        transform.position += new Vector3(0f, 0f, 0.1f);
        //Move left
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && (squidStatus == PlayerStatus.Right))
        {
            rb.AddForce(-10f, 0, 0, ForceMode.Impulse);
            //Vector3 destination = new Vector3(left, transform.position.y, transform.position.z + 1f);
            //StartCoroutine(Jump(1f, transform.position, destination, PlayerStatus.Left));
        }

        //Move right
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && (squidStatus == PlayerStatus.Left))
        {
            rb.AddForce(10f, 0, 0, ForceMode.Impulse);
            //Vector3 destination = new Vector3(right, transform.position.y, transform.position.z + 1f);
            //StartCoroutine(Jump(1f, transform.position, destination, PlayerStatus.Right));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }

    IEnumerator Jump(float waitTime, Vector3 start, Vector3 dest, PlayerStatus afterStatus)
    {
        squidStatus = PlayerStatus.Moving;
        float i = 0f;
        while (i < 1f)
        {
            transform.position = Vector3.Lerp(start, dest, i);
            /*if (i < 0.5f)
            {
                transform.position = Vector3.Lerp(start, start + new Vector3(0, 2f, 0), i);
            }*/
            i += 0.2f;
            yield return null;
        }
        squidStatus = afterStatus;
        yield return 0;
    }
}