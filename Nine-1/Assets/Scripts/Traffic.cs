using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic : MonoBehaviour
{
    public float speed;
    private PlayerController playerControllerScript;
    private float bottomBound = -20.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1.0f, 5.0f);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.isGameOver && trafficHit == false)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }
        if (!playerControllerScript.isGameOver && trafficHit == true)
        {
            transform.Translate(Vector3.back * (speed / 2) * Time.deltaTime, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Traffic"))
        {
            trafficHit = true;
            //playerAudio.PlayOneShot(collisionSfx);
            //sparkEffects.Play();
        }
    }
    */
}
