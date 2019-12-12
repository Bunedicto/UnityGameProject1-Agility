using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rival : MonoBehaviour
{
    private Rigidbody rivalRb;
    private AudioSource rivalAudio;
    private GameObject player;
    private GameManager gameManager; 

    private float widthBoundary = 25.0f;
    private float heightBoundary = 11.0f;
    private float roadWidth = 11.0f;
    private float moveSpeed = 5.0f;
    [SerializeField] private float turnSpeed = 5.0f;
    private float directionalInput;
    private float velocityInput;

    public float health = 50.0f;

    public GameObject projectilePrefab;
    public AudioClip dirtSfx;
    public AudioClip roadSfx;
    public AudioClip tireSfx;
    public AudioClip boostSfx;
    public AudioClip collisionSfx;
    public AudioClip explosionSfx;
    public ParticleSystem explosionVisual;
    public ParticleSystem sparkEffects;
    public ParticleSystem tireMarks;
    public ParticleSystem dirtMarks;

    public bool delay = false;      // Usage: if "Time.time>latency" then "latency = Time.time + interval"
    public float interval = 5.0f;
    public float latency = 5.0f;
    public bool isOnRoad = true;
    public bool stop = false;

    public bool crashingPlayer = false;
    public bool crashingTraffic = false;
    public bool sample = false;

    // Start is called before the first frame update
    void Start()
    {
        // Rival's Rigidbody and AudioSource component
        rivalRb = GetComponent<Rigidbody>();
        rivalAudio = GetComponent<AudioSource>();

        // Finding player's GameObject
        player = GameObject.Find("Player");

        // Get game manager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            RivalMovement();
            GameObjectives();
        }
    }

    // Rival Movement
    void RivalMovement()
    {
        // Movement boundaries
        if (rivalRb.transform.position.x > widthBoundary)
        {
            rivalRb.transform.position = new Vector3(widthBoundary, rivalRb.transform.position.y, rivalRb.transform.position.z);
        }
        if (rivalRb.transform.position.x < -widthBoundary)
        {
            rivalRb.transform.position = new Vector3(-widthBoundary, rivalRb.transform.position.y, rivalRb.transform.position.z);
        }/*
        if (rivalRb.transform.position.z > heightBoundary)
        {
            rivalRb.transform.position = new Vector3(rivalRb.transform.position.x, rivalRb.transform.position.y, heightBoundary);
        }
        if (rivalRb.transform.position.z < -heightBoundary)
        {
            rivalRb.transform.position = new Vector3(rivalRb.transform.position.x, rivalRb.transform.position.y, -heightBoundary);
        }*/

        // Looking for player
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        // Go to player
        //rivalRb.AddForce(lookDirection * moveSpeed);

        // Rival movement
        if (transform.position.x > -roadWidth && transform.position.x < roadWidth)
        {
            if (transform.position.z < 20 && stop == false)
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            }
            if (transform.position.z > 19)
            {
                stop = true;
            }
            if (transform.position.z < 20 && stop == true)
            {
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            }
            if (transform.position.z < -20)
            {
                stop = false;
            }
        }
        else
        {
            if (!crashingPlayer || Input.GetKeyDown(KeyCode.Q))
            {
                if (transform.position.x < -roadWidth)
                {
                    transform.Translate((Vector3.back+Vector3.left) * Time.deltaTime * turnSpeed);
                    Debug.Log("Go back" + transform.position.x);
                }

                if (transform.position.x > roadWidth)
                {
                    transform.Translate((Vector3.forward + Vector3.left) * Time.deltaTime * turnSpeed);
                    Debug.Log("Go back" + transform.position.x);
                }

            }
        }

        //if (transform.position.x < -11 && transform.position.x > 11
        //    crashingPlayer == false || Input.GetKeyDown(KeyCode.Q))
        //{
        //    if (transform.position.x < -11)
        //    {
        //        transform.Translate(Vector3.back * Time.deltaTime * turnSpeed);
        //        Debug.Log("Outside");
        //    }
        //    if (transform.position.x > 11)
        //    {
        //        transform.Translate(Vector3.forward * Time.deltaTime * turnSpeed);
        //        Debug.Log("Outside");
        //        sample = true;
        //    }
        //}

        if (crashingTraffic == true)
        {
            if (transform.position.x < 0)
            {
                transform.Translate(Vector3.back * Time.deltaTime * turnSpeed);
            }
            if (transform.position.x > 0)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * turnSpeed);
            }
        }
    }

    // Game Objectives
    void GameObjectives()
    {
        // Game over situation
        if (health < 1)
        {
            //rivalAudio.PlayOneShot(explosionSfx);
            //explosionVisual.Play();
            Destroy(gameObject);
        }

        if (crashingPlayer == true)
        {
            interval -= Time.deltaTime;
            if (interval<0)
            {
                crashingPlayer = false;
                Debug.Log("success");
                interval = latency;
            }
        }
    }

    // Unity collision component
    private void OnCollisionEnter(Collision collision)
    {
        // Collision with object containing "Traffic" tag
        if (collision.gameObject.CompareTag("Traffic"))
        {
            //health -= 5.0f;
            //rivalAudio.PlayOneShot(collisionSfx);
            //sparkEffects.Play();
            crashingTraffic = true;
        }
        else
        {
            crashingTraffic = false;
        }

        // Collision with object containing "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            //health -= 10.0f;
            //rivalAudio.PlayOneShot(collisionSfx);
            //sparkEffects.Play();
            crashingPlayer = true;
        }
    }
}
