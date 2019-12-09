using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] trafficPrefabs;
    private Vector3 spawnPosLane1 = new Vector3(-8, 0, -15);
    private Vector3 spawnPosLane2 = new Vector3(-3, 0, -15);
    private Vector3 spawnPosLane3 = new Vector3(3, 0, -15);
    private Vector3 spawnPosLane4 = new Vector3(8, 0, -15);
    private float spawnRangeX = 9.0f;
    private float spawnPosZ = 15.0f;

    public float startDelay = 2;
    public float spawnInterval = 1.5f;

    private PlayerController playerControllerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = Random.Range(1.0f, 5.0f);
        InvokeRepeating("SpawnTraffic", startDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTraffic()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        int trafficIndex = Random.Range(0, trafficPrefabs.Length);

        if (playerControllerScript.isGameOver == false)
        {
            Instantiate(trafficPrefabs[trafficIndex], spawnPos, trafficPrefabs[trafficIndex].transform.rotation);
        }
    }
}
