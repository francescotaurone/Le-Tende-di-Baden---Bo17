using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpawn : MonoBehaviour
{

    public GameObject[] objects;

    public float[] timeRange;
    
    private Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<objects.Length; i++)
        {
            StartCoroutine(SpawnObject(timeRange[i], objects[i]));
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator SpawnObject(float timeRange, GameObject obj)
    {
        yield return new WaitForSeconds(Random.Range(5f, 20f));
        while (true)
        {
            spawnPosition.x = 10f;
            spawnPosition.y = Random.Range(-2.5f, 2.5f);
            spawnPosition.z = 0f;
            Instantiate(obj, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.8f, 1.2f)*timeRange);
        }
    }
}
