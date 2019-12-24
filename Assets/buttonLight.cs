using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class buttonLight : MonoBehaviour
{
    public GameObject lightButton;
    
    public Light2D torch;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) == true && lightButton.activeSelf == true)
        {
            Debug.Log("LightButton initiated");
            lightButton.SetActive(false);
            
            StartCoroutine(lightActivated());
        }
    }
    public void buttonLightPressed()
    {
        StartCoroutine(lightActivated());
    }

    IEnumerator lightActivated()
    {
        GameObject.Find("AudioLight").GetComponent<AudioSource>().Play();
        torch.pointLightOuterRadius *= 3f;
        torch.pointLightInnerRadius *= 3f;
        yield return new WaitForSeconds(30f);
        torch.pointLightOuterRadius /= 3f;
        torch.pointLightInnerRadius /= 3f;
    }
}
