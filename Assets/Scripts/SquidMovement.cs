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
    private AudioSource audioS;
    public Camera cam;
    public GameObject marker;
    public Rigidbody rb;
    public static bool paused;
    public ParticleSystem rightBurst;
    public ParticleSystem leftBurst;
    public Canvas canvas;
    public AudioClip jumpNoise;
    public AudioClip splatNoise;
    public AudioClip dropNoise;
    
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
        audioS = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > 5f && !loss && !paused)
        {
            transform.position += new Vector3(0f, 0f, 0.3f);
        }
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
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        }


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
            //transform.Rotate(1f,0,0);
           // Vector3 relativePosition = new Vector3(-10f, transform.position.y, transform.position.z);
            //SM_Squid.transform.rotation = Quaternion.LookRotation(relativePosition);
        }
        else if (squidStatus == PlayerStatus.MovingLeft)
        {
            //transform.Rotate(-1f, 0, 0);
            //Vector3 relativePosition = new Vector3(10f, transform.position.y, transform.position.z);
            // SM_Squid.transform.rotation = Quaternion.LookRotation(relativePosition);
        }

        if (transform.position.x > RIGHT || transform.position.z < cam.transform.position.z || transform.position.x < LEFT || transform.position.y < 2.5f)
        {
            onLoss();
        }
        canvas.GetComponent<ChangeText>().updateScore((int) transform.position.z);
    }

    void Update()
    {
        if(paused && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            paused = false;
            canvas.GetComponent<ChangeText>().pauseText(false);
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
        canvas.GetComponent<ChangeText>().pauseText(true);
        paused = true;
    }

    void onLoss()
    {
        loss = true;
        rb.constraints = RigidbodyConstraints.None;
        audioS.clip = dropNoise;
        audioS.Play();
        cam.GetComponent<CameraMovement>().onLoss();
        marker.GetComponent<BuildingSpawner>().onLoss();
    }

    void OnCollisionEnter(Collision collision)
    {
        touchingWall = true;
        audioS.clip = splatNoise;
        audioS.Play();
        if (collision.gameObject.tag == "Left Wall")
        {
            squidStatus = PlayerStatus.Left;
            //transform.eulerAngles = new Vector3(90f, 90f, 0);
            leftBurst.Play(true);
        }
        if (collision.gameObject.tag == "Right Wall")
        {
            squidStatus = PlayerStatus.Right;
            //transform.eulerAngles = new Vector3(0f, 90f, 0);
            rightBurst.Play(true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        touchingWall = false;
    }

    void Jump(float sideDist)
    {
        audioS.clip = jumpNoise;
        audioS.Play();
        rb.AddForce(sideDist, 0, 0, ForceMode.Impulse);
        transform.position += new Vector3(0,0,0.2f);
        cam.transform.position += new Vector3(0, 0, 0.2f);
        if (transform.position.z - cam.transform.position.z < 5f)
        {
            transform.position += new Vector3(0f, 0f, 0.75f);
        }
        if (transform.position.y < 8f)
        {
            transform.position += new Vector3(0f, 0.75f, 0f);
        }
        if (Mathf.Abs(transform.position.x) - Mathf.Abs(cam.transform.position.x) < 7f)
        {
            rb.AddForce(0, 0.5f, 0, ForceMode.Impulse);
        }
        jumped = true;
    }
}
