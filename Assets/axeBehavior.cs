using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeBehavior : MonoBehaviour
{
    public float enemySpeed = 6f;
    private float enemylife = 0.15f;
    private Vector3 speedOfAxe;
    public GameObject destroyAnimation;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.position.x>=0) speedOfAxe = new Vector3(Random.Range(-2f, 0f), -1f, 0);
        else speedOfAxe = new Vector3(Random.Range(0f, 2f), -1f, 0);
        //speedOfAxe = Vector3.down;
        speedOfAxe = speedOfAxe.normalized * enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speedOfAxe* Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * -500 * Time.deltaTime);

        if (transform.position.x <= -11f || transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
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
        if (enemylife <= 0f)
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
