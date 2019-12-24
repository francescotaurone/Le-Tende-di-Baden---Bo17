using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawn : MonoBehaviour
{
    public GameObject birdObject;
    private float timeRange = 3f;
    private float timeCounter = 0;
    private float sign;
    private Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCounter <= 0)
        {
            spawnPosition.x = randomSign() * 10f;
            spawnPosition.y = Random.Range(3f, 4.8f);
            spawnPosition.z = 0f;
            Instantiate(birdObject, spawnPosition, Quaternion.identity);
            timeCounter = timeRange;
        }
        else timeCounter -= Time.deltaTime;
    }

    float randomSign()
    {
        float r = Random.Range(-1, 1);
        return Mathf.Sign(r);
    }

}
