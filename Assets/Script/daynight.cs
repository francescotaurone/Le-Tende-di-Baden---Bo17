using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class daynight : MonoBehaviour
{

    private float wholeDayLength = 180f;
    private float dayStart = 0f;
    private float nightStart;
    public float currentTime;
    
    //public float cycleSpeed;
   
    private Vector3 sunPosition;

    private GameObject sun;
    private GameObject moon;
   
    private Light2D sunLight;
    private Light2D moonLight;
    private Light2D globalLight;

    //Purtroppo se le aggiungo come pubbliche in un array in unity non le trova
    public Light2D[] starLights;
    public Light2D[] starShapeLights;

    public Light2D spriteSayanLight;
    public Light2D pointSayanLight;
    public float maxspriteSayanLight = 0.2f;
    public float maxPointSayanLight = 2.5f;



    [SerializeField]
    private GameObject torch;


    Color32 orange = new Color(255/255f, 161/255f, 4/255f);

    public float maxSunLight =2f;
    public float maxMoonLight = 2f;
    public float maxStarsLight = 0.6f;
    public float maxGlobalLight = 0.8f;
    public float minGlobalLight = 0.2f;
    public float maxSkyLight = 0.3f;
    public float maxStarsShapeLight = 0.6f;

    void Start()
    {
        sun = GameObject.Find("Sun");
        moon = GameObject.Find("Moon");
        globalLight = GameObject.Find("GlobalLight").GetComponent<Light2D>();
        nightStart = wholeDayLength / 2;
        
        sunLight = sun.GetComponent<Light2D>();
        moonLight = moon.GetComponent<Light2D>();

        //currentTime = wholeDayLength / 4;
        currentTime = 20f;
        
        //StartCoroutine(TimeOfDay());


    }

    void Update()
    {

        sunLight.intensity = SunLightIntensity(currentTime) * maxSunLight;
        moonLight.intensity = MoonLightIntensity(currentTime) * maxMoonLight;
        globalLight.intensity = minGlobalLight + (SunLightIntensity(currentTime) * maxGlobalLight);

        spriteSayanLight.intensity = MoonLightIntensity(currentTime) * maxspriteSayanLight;
        pointSayanLight.intensity = MoonLightIntensity(currentTime) * maxPointSayanLight;

        if (currentTime >= wholeDayLength)
        {
            currentTime = 0;
        }
        float currentTimeF = currentTime;
        float dayLengthF = wholeDayLength;
        this.transform.eulerAngles = new Vector3(0, 0, (-(currentTimeF / dayLengthF) * 360));
        if (currentTime < wholeDayLength /2) currentTime += Time.deltaTime;
        else currentTime += 1.5f*Time.deltaTime;
        globalLight.color = Color.Lerp(Color.blue, Color.white, SunLightIntensity(currentTime));
        sunLight.color = Color.Lerp(Color.white, orange, Mathf.PingPong(2*currentTime / wholeDayLength, 1));
        //globalLight.intensity = 0.1f;
        if (currentTime / wholeDayLength > 0.5)
        {
            torch.SetActive(true);

        }
        else torch.SetActive(false);


        
        for (int i =0; i< starLights.Length; i++)
        {
            starLights[i].intensity = MoonLightIntensity(currentTime) * maxStarsLight;
            starShapeLights[i].intensity = MoonLightIntensity(currentTime) * maxStarsShapeLight;
        }
        
        
    }
    
    private float SunLightIntensity(float x)
    {
        /*
        if (x < wholeDayLength/4) return 2 / wholeDayLength * x + 0.5f;
        else if (x >= wholeDayLength/4 && x < 3 * wholeDayLength / 4) return -(2 / wholeDayLength * x) + 1.5f;
        else if (x >= 3 * wholeDayLength / 4) return 2 / wholeDayLength * x - 1.5f;
        else return 0;*/
        
        return Mathf.PingPong(2 / wholeDayLength * x + 0.5f, 1);
    }

    private float MoonLightIntensity(float x)
    {
        //return (1 - SunLightIntensity(x));
        return Mathf.Pow(1 - SunLightIntensity(x), 2);
    }

}