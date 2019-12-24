using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectBehavior : MonoBehaviour
{
    public float objectSpeed = 4f;
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(-objectSpeed * Time.deltaTime, 0, 0);

        if (transform.position.x <= -11f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player got a " + this.tag);
            Instantiate(particle, transform.position, Quaternion.identity);
            other.GetComponent<Player>().objectCollected(this.tag);
            Destroy(gameObject);
        }
    }
}
