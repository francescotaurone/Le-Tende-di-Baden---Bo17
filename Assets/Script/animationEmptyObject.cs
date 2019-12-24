using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationEmptyObject : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        Destroy(this.gameObject, 0.5f);
    }

   
}
