using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonFulmine : MonoBehaviour
{
    public GameObject fulmineButton;
    public Player player;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) == true && fulmineButton.activeSelf == true)
        {
            Debug.Log("FulmineButton initiated");
            fulmineButton.SetActive(false);

            StartCoroutine(fulmineActivated());
        }
    }
    public void buttonFulminePressed()
    {
        StartCoroutine(fulmineActivated());
    }

    IEnumerator fulmineActivated()
    {
        GameObject.Find("AudioFulmine").GetComponent<AudioSource>().Play();
        player.increasePlayerSpeed();
        yield return new WaitForSeconds(10f);
        player.decreasePlayerSpeed();
    }
}
