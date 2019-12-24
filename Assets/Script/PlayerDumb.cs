using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDumb : MonoBehaviour
{
    
    public float speed = 7f;
    // Start is called before the first frame update
    void Start()
    {
        
        Vector3 characterScale = transform.localScale;
        if (transform.position.x > 0)
        {
            characterScale.x = -0.8f;
            speed = -speed;
        }
        if (transform.position.x < 0)
        {
            characterScale.x = 0.8f;
        }
        transform.localScale = characterScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate( new Vector3(speed * Random.Range(0.5f, 1.1f) * Time.deltaTime, 0,0));
    }
    private void Update()
    {
        if (transform.position.x > 10 || transform.position.x < -10)
            Destroy(this.gameObject, 1f);
    }
}
