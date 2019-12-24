using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioMixerManag : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer masterMixer;
    private float initialBack;
    private float initialEffect;

    public Slider backSlider;
    public Slider effectSlider;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("backgroundVol")) PlayerPrefs.SetFloat("backgroundVol", 1f);
        if (!PlayerPrefs.HasKey("effectVol")) PlayerPrefs.SetFloat("effectVol", 1f);
        initialBack = PlayerPrefs.GetFloat("backgroundVol");
        initialEffect = PlayerPrefs.GetFloat("effectVol");

        SetBackgroundVolume(initialBack);
        SetEffectVolume(initialEffect);

        backSlider.value = initialBack;
        effectSlider.value = initialEffect;
    }
    public void SetBackgroundVolume(float soundLevel)
    {
        masterMixer.SetFloat("backgroundVolume", Mathf.Log(soundLevel) * 20);

        PlayerPrefs.SetFloat("backgroundVol", soundLevel);
    }

    public void SetEffectVolume(float soundLevel)
    {   
        masterMixer.SetFloat("effectVolume", Mathf.Log(soundLevel) * 20);
        PlayerPrefs.SetFloat("effectVol", soundLevel);
    }
}
