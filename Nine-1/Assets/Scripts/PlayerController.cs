using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float widthBoundary = 25.0f;
    private float heightBoundary = 11.0f;
    private float roadWidth = 15.0f;
    private float moveSpeed = 10.0f;
    private float turnSpeed = 1.0f;
    private float directionalInput;
    private float velocityInput;

    public float health = 100.0f;

    private Rigidbody playerRb;
    private AudioSource playerAudio;

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
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Game over situation
        if (health<1)
        {
            isGameOver = true;
            playerAudio.PlayOneShot(explosionSfx);
            explosionVisual.Play();
        }

        // Movement boundaries
        if (playerRb.transform.position.x > widthBoundary)
        {
            playerRb.transform.position = new Vector3(widthBoundary, playerRb.transform.position.y, playerRb.transform.position.z);
        }
        if (playerRb.transform.position.x < -widthBoundary)
        {
            playerRb.transform.position = new Vector3(-widthBoundary, playerRb.transform.position.y, playerRb.transform.position.z);
        }
        if (playerRb.transform.position.z > heightBoundary)
        {
            playerRb.transform.position = new Vector3(playerRb.transform.position.x, playerRb.transform.position.y, heightBoundary);
        }
        if (playerRb.transform.position.z < -heightBoundary)
        {
            playerRb.transform.position = new Vector3(playerRb.transform.position.x, playerRb.transform.position.y, -heightBoundary);
        }


        // Player Controls
        if (
            playerRb.transform.position.x < widthBoundary && 
            playerRb.transform.position.x > -widthBoundary && 
            playerRb.transform.position.z < heightBoundary && 
            playerRb.transform.position.z > -heightBoundary &&
            !isGameOver
            )
        {
            /*
            // Vehicle tire effects
            if (playerRb.transform.position.x < roadWidth &&
                playerRb.transform.position.x > -roadWidth
                )
            {
                playerAudio.PlayOneShot(roadSfx);
            }
            else
            {
                playerAudio.PlayOneShot(dirtSfx);
                dirtMarks.Play();
            }
            */

            directionalInput = Input.GetAxis("Horizontal");
            velocityInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * Time.deltaTime * velocityInput * moveSpeed);
            transform.Translate(Vector3.right * Time.deltaTime * directionalInput * moveSpeed);
            transform.Rotate(Vector3.up * Time.deltaTime * directionalInput * turnSpeed);
            
            // Weapon firing
            if (Input.GetKeyDown(KeyCode.Space))
            {
                /*
                Rigidbody bulletInstance = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation) as Rigidbody;
                bulletInstance.AddForce(firePosition.forward * bulletSpeed);
                myStuff.bullets--;
                */
                /*
                Rigidbody rocketInstance;
                rocketInstance = Instantiate(rocketPrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody;        // Spawn
                rocketInstance.AddForce(barrelEnd.forward * 5000);
                */
                /*
                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
                */
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Traffic"))
        {
            health -= 5.0f;
            playerAudio.PlayOneShot(collisionSfx);
            sparkEffects.Play();
        }
        else if (collision.rigidbody.CompareTag("Rival"))
        {
            health -= 2.0f;
            playerAudio.PlayOneShot(collisionSfx);
            sparkEffects.Play();
        }
    }
}
