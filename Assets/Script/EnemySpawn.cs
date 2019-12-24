using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float speedMultiplier = 1;
    public GameObject[] enemies;
    public float[] timeBtwEnemy;
    private float timeRangeMultiplier = 3f;

    private float randomScaleAdjustment;
    
    private Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        speedMultiplier = PlayerPrefs.GetFloat("difficolta");
        timeRangeMultiplier = PlayerPrefs.GetFloat("enemyRate");
        for (int i=0; i<enemies.Length; i++)
        {
            StartCoroutine(SpawnAnEnemy(enemies[i], i));
        }
        
        
        
    }


    IEnumerator SpawnAnEnemy(GameObject chosenEnemy, int index)
    {
        
        while (true)
        {
            if (chosenEnemy.tag == "axe")
            {
                yield return new WaitForSeconds(Random.Range(timeRangeMultiplier * timeBtwEnemy[index] / 3f, timeRangeMultiplier * timeBtwEnemy[index]));

                spawnPosition.x = Random.Range(-9f, 9f);
                spawnPosition.y = 5.33f;
                spawnPosition.z = 0f;
                GameObject enemy = Instantiate(chosenEnemy, spawnPosition, Quaternion.identity) as GameObject;
                //randomScaleAdjustment = Random.Range(0.5f, 2f);
                //enemy.transform.localScale = randomScaleAdjustment * enemy.transform.localScale;
                axeBehavior enemybh = enemy.GetComponent<axeBehavior>();
                enemybh.enemySpeed = Random.Range(0.7f, 1.3f) * speedMultiplier * 6f;
                enemybh.changeEnemyLifeWhenInstanced(randomScaleAdjustment);
                
                
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1f, timeRangeMultiplier * timeBtwEnemy[index]));
                spawnPosition.x = 10f;
                spawnPosition.y = Random.Range(-2.5f, 2.5f);
                spawnPosition.z = 0f;
                GameObject enemy = Instantiate(chosenEnemy, spawnPosition, Quaternion.identity) as GameObject;
                randomScaleAdjustment = Random.Range(0.5f, 2f);
                enemy.transform.localScale = randomScaleAdjustment * enemy.transform.localScale;
                EnemyBehavior enemybh = enemy.GetComponent<EnemyBehavior>();
                enemybh.enemySpeed = Random.Range(0.7f, 1.3f) * speedMultiplier * 4f;
                enemybh.changeEnemyLifeWhenInstanced(randomScaleAdjustment);
                ParticleSystem partSys = enemy.GetComponentInChildren<ParticleSystem>();
                partSys.transform.localScale = randomScaleAdjustment * partSys.transform.localScale;
                
            }
            
        }
    }


}
