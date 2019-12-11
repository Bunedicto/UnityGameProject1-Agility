using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private PlayerController playerControllerScript;
    public float speed = 0.0f;
    public float speedLimit = 10.0f;

    public Vector3 startPos;
    public float repeatHeight = 91.7f;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get player controller
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Starting position
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < 200.0f)
        {
            for (int i = 0; i < speedLimit; i++)
            {
                speed += 0.01f;
            }
        }

        // When game is not over and has the "BackgroundUp" tag
        if (/*!playerControllerScript.isGameOver == false && */gameObject.CompareTag("BackgroundUp"))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        
        // When game is not over and has the "BackgroundDown" tag
        if (/*!playerControllerScript.isGameOver == false && */gameObject.CompareTag("BackgroundDown"))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        
        // When it has reached the optimal position to repeat
        if (transform.position.z < startPos.z - repeatHeight)
        {
            transform.position = startPos;
        }
    }
}
