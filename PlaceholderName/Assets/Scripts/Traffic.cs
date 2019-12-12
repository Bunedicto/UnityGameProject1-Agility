using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource trafficAudio;

    public float speed;
    private float bottomBound = -20.0f;
    public bool trafficHit;

    public AudioClip collisionSfx;
    public ParticleSystem sparkEffects;

    // Start is called before the first frame update
    void Start()
    {
        // Random speed for traffic
        speed = Random.Range(1.0f, 5.0f);

        // Get game manager
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Audio component for traffic
        trafficAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // When game is not over and traffic did not collide
        if (gameManager.isGameActive && trafficHit == false)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }
        // When game is not over and traffic did collide
        if (gameManager.isGameActive && trafficHit == true)
        {
            transform.Translate(Vector3.back * (speed / 2) * Time.deltaTime, Space.World);
        }
    }

    // Unity collision component
    private void OnCollisionEnter(Collision collision)
    {
        // Collision with object containing "Traffic" tag
        if (collision.rigidbody.CompareTag("Traffic"))
        {
            trafficHit = true;
            trafficAudio.PlayOneShot(collisionSfx);
            sparkEffects.Play();
            Debug.Log("Move");
        }
    }
}
