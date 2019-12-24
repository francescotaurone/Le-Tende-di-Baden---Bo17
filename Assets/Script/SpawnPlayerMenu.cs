using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerMenu : MonoBehaviour
{ 
    public GameObject playerDumbObj;
    private float timeRange = 1f;
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
            spawnPosition.y = Random.Range(-3f, 4.8f);
            spawnPosition.z = Random.Range(-3f, 4.8f);
            Instantiate(playerDumbObj, spawnPosition, Quaternion.identity);
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

