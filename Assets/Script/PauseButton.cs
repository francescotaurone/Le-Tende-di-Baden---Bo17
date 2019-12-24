using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();   
    }
    // Start is called before the first frame update
    private void Update()
    {
        Vector2 cursorPos = Input.mousePosition;
        
        if (Vector2.Distance(cursorPos, transform.position) < 30f)
            anim.SetBool("CursorOver", true);
        else anim.SetBool("CursorOver", false);
    }
}
