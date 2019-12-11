using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rival : MonoBehaviour
{
    private Rigidbody rivalRb;
    private AudioSource rivalAudio;
    private GameObject player;

    private float widthBoundary = 25.0f;
    private float heightBoundary = 11.0f;
    private float roadWidth = 15.0f;
    private float moveSpeed = 5.0f;
    private float turnSpeed = 15.0f;
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
    public float interval = 1.0f;
    public float latency = 0.0f;
    public bool isOnRoad = true;
    public bool isGameOver = false;
    public bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        // Rival's Rigidbody and AudioSource component
        rivalRb = GetComponent<Rigidbody>();
        rivalAudio = GetComponent<AudioSource>();

        // Finding player's GameObject
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RivalMovement();
        GameObjectives();
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

    // Game Objectives
    void GameObjectives()
    {
        // Game over situation
        if (health < 1)
        {
            rivalAudio.PlayOneShot(explosionSfx);
            explosionVisual.Play();
            Destroy(gameObject);
        }
    }

    // Unity collision component
    private void OnCollisionEnter(Collision collision)
    {
        // Collision with object containing "Traffic" tag
        if (collision.rigidbody.CompareTag("Traffic"))
        {
            health -= 5.0f;
            rivalAudio.PlayOneShot(collisionSfx);
            sparkEffects.Play();
        }

        // Collision with object containing "Player" tag
        if (collision.rigidbody.CompareTag("Player"))
        {
            health -= 10.0f;
            rivalAudio.PlayOneShot(collisionSfx);
            sparkEffects.Play();
        }
    }
}
