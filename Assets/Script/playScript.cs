using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGameNormale()
    {
        
        PlayerPrefs.SetInt("modSopravvivenza", 0);
        
    }
    public void PlayGameSopravvivenza()
    {

        PlayerPrefs.SetInt("modSopravvivenza", 1);
        
    }

    public void PlayGameFacile()
    {

        PlayerPrefs.SetFloat("difficolta", 1f);
        PlayerPrefs.SetFloat("enemyRate", 3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayGameIntermedio()
    {

        PlayerPrefs.SetFloat("difficolta", 1.5f);
        PlayerPrefs.SetFloat("enemyRate", 1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayGameInsane()
    {

        PlayerPrefs.SetFloat("difficolta", 2.5f);
        PlayerPrefs.SetFloat("enemyRate", 0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
