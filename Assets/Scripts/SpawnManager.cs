using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private PlayerController playerControllerScript;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private float startDelay = 2.0f;
    private float repeatRate = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        int index = Random.Range(0, obstaclePrefab.Length);
        Vector3 spawnLocation = new Vector3(Random.Range(25, 35), 0, 0);


        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab[index], spawnLocation, obstaclePrefab[index].transform.rotation);
        }
    }
}
