using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float enemySpeed = 4;
    private float enemylife = 0.15f;

    public GameObject destroyAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-enemySpeed * Time.deltaTime,0 , 0);

        if (transform.position.x <= -11f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player hit!");
            Instantiate(destroyAnimation, transform.position, Quaternion.identity);
            other.GetComponent<Player>().playerHit();
            Destroy(gameObject);
        }
    }

    public void subtractLife()
    {
        enemylife -= Time.deltaTime;
        if(enemylife<= 0f)
        {
            Instantiate(destroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void changeEnemyLifeWhenInstanced(float size)
    {
        enemylife = enemylife * size;
    }
}
