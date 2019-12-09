using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] trafficPrefabs;
    private float spawnRangeX = 9.0f;
    private float spawnPosZ = 15.0f;

    public float startDelay = 2;
    public float spawnInterval = 1.5f;

    private PlayerController playerControllerScript;

    public float lane;
    
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
        /*
        Vector3 spawnPosLane1 = new Vector3(-8, 0, spawnPosZ);
        Vector3 spawnPosLane2 = new Vector3(-3, 0, spawnPosZ);
        Vector3 spawnPosLane3 = new Vector3(3, 0, spawnPosZ);
        Vector3 spawnPosLane4 = new Vector3(8, 0, spawnPosZ);

        Vector3 LanePicker()
        {
            lane = Random.Range(1, 4);

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

        int trafficIndex = Random.Range(0, trafficPrefabs.Length);

        if (playerControllerScript.isGameOver == false)
        {
            Instantiate(trafficPrefabs[trafficIndex], spawnPos, trafficPrefabs[trafficIndex].transform.rotation);
        }
    }
}
