using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] trafficPrefabs;
    private float spawnRangeX = 9.0f;
    private float spawnPosZ = 15.0f;

    public int lane;
    public float startDelay = 2;
    public float spawnInterval = 1.5f;

    private PlayerController playerControllerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        // Random spawn interval
        spawnInterval = Random.Range(1.0f, 5.0f);

        // Repeat spawn traffic
        InvokeRepeating("SpawnTraffic", startDelay, spawnInterval);

        // Get player controller
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Spawning traffic method
    void SpawnTraffic()
    {
        // Spawn location
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        
        
        /* // Alternative fixed spawn locations
        Vector3 spawnPosLane1 = new Vector3(-8, 0, spawnPosZ);
        Vector3 spawnPosLane2 = new Vector3(-3, 0, spawnPosZ);
        Vector3 spawnPosLane3 = new Vector3(3, 0, spawnPosZ);
        Vector3 spawnPosLane4 = new Vector3(8, 0, spawnPosZ);

        Vector3 LanePicker()
        {
            lane = Random.Range(1, 5);

            switch (lane)
            {
                case 1:
                    return spawnPosLane1;
                    break;
                case 2:
                    return spawnPosLane2;
                    break;
                case 3:
                    return spawnPosLane1;
                    break;
                case 4:
                    return spawnPosLane4;
                    break;
                default:
                    return spawnPos;
                    break;
            }
        }
        */

        // Different types of traffic vehicles
        int trafficIndex = Random.Range(0, trafficPrefabs.Length);

        // When game is not over, spawn traffic
        if (playerControllerScript.isGameOver == false)
        {
            Instantiate(trafficPrefabs[trafficIndex], spawnPos, trafficPrefabs[trafficIndex].transform.rotation);
        }
    }
}
