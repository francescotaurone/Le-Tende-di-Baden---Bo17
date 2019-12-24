using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class pauseGame : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioMixer  masterMixer;

    public GameObject schermataVittoria;
    public GameObject schermataSconfitta;
    public GameObject schermataLeaderBoard;
    private float oldVolume;
    private bool altreSchermateAttive = false;
    
    // Start is called before the first frame update
    public void Resume()
    {
        backgroundMusic.UnPause();
        Time.timeScale = 1f;
        masterMixer.SetFloat("masterVolume", oldVolume);
        this.gameObject.SetActive ( false);
    }

    public void Pause()
    {   
        Time.timeScale = 0f;
        backgroundMusic.Pause();
        masterMixer.GetFloat("masterVolume", out oldVolume);
        masterMixer.SetFloat("masterVolume", -60f);
        this.gameObject.SetActive(true);
    }

    public void pauseButtonPressed()
    {
        altreSchermateAttive = schermataLeaderBoard.activeSelf || schermataSconfitta.activeSelf || schermataVittoria.activeSelf;
        if (Time.timeScale == 1f && altreSchermateAttive == false) Pause();
        else if (Time.timeScale == 0f) Resume();
        else Debug.Log("Tentata Pausa ma altra schermata");
    }
}
