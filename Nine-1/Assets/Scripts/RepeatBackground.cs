using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private float speed = 30.0f;
    private PlayerController playerControllerScript;

    public Vector3 startPos;
    public float repeatHeight;
    
    // Start is called before the first frame update
    void Start()
    {
        //playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        startPos = transform.position;
        repeatHeight = 22;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!playerControllerScript.isGameOver == false)
        //{
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        //}
        
        if (transform.position.z < startPos.z - repeatHeight)
        {
            transform.position = startPos;
        }
    }
}
