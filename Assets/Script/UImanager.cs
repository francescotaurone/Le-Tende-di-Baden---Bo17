using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    
    public GameObject spawnManager;
    public ParticleSystem particle;
    public GameObject vittoriaScreen;
    public GameObject sconfittaScreen;
    public GameObject classificaScreen;
    Vector3 spawnPosition;
    public float lives = 10;
    private TMP_Text livesText;
    public float poles = 0;
    private TMP_Text polesCollectedText;
    public TMP_Text polesCollectedTextClassifica;
    private float catino = 0;
    private TMP_Text catinoCollectedText;
    public TMP_Text catinoCollectedTextClassifica;
    private float sovrattelo = 0;
    private TMP_Text sovratteloCollectedText;
    public TMP_Text sovratteloCollectedTextClassifica;
    private TMP_Text tendeCompleteText;
    public TMP_Text tendeCompleteTextClassifica;
    private Player Player;
    private bool trasformazione1 = false;
    private bool trasformazione2 = false;
    private bool trasformazione3 = false;

    private bool hoVinto = false;
    private bool hoPerso = false;
    public GameObject[] tendeDaCompletare;
    public GameObject tendaCompletaPrefab;
    private int tendeCompletate = 0;
    public float neededPoles = 5;
    public bool sopravvivenza = false;
    public TMP_Text totalPoints;
    private float moltiplicatorePerTendeComplete = 5f;
    private float totalPointsNumber;
    public GameObject items;

    public GameObject joystick;
    public GameObject jumpButton;
    private AudioSource sottofondo;
    private AudioSource destroyMasso;
    private AudioSource tendaCompletata;
    public AudioClip[] songsBackground;
    public GameObject lightButton;
    public GameObject lampoButton;


    private int currentSong = 0;

    public GameObject beamButton;
    public TMP_Text ilTuoPunteggio;

    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
        livesText = GameObject.Find("lives").GetComponent<TMP_Text>();
        polesCollectedText = GameObject.Find("poles").GetComponent<TMP_Text>();
        catinoCollectedText = GameObject.Find("catino").GetComponent<TMP_Text>();
        sovratteloCollectedText = GameObject.Find("sovrattelo").GetComponent<TMP_Text>();
        tendeCompleteText = GameObject.Find("complete").GetComponent<TMP_Text>();
        sottofondo = GetComponent<AudioSource>();
        sottofondo.Play();
        //destroyMasso = GameObject.Find("Hit").GetComponent<AudioSource>();
        tendaCompletata = GameObject.Find("TendaCompletata").GetComponent<AudioSource>();
        
        if (PlayerPrefs.GetInt("modSopravvivenza") == 1)
            sopravvivenza = true;
        else sopravvivenza = false;

#if UNITY_ANDROID || UNITY_IOS

#else
        joystick.SetActive(false);
        jumpButton.SetActive(false);
#endif

    }

    private void Update()
    {
        totalPointsNumber = moltiplicatorePerTendeComplete * tendeCompletate + poles + catino + sovrattelo;
        if (poles>= neededPoles && catino>=1 && sovrattelo >= 1)
        {
            tendaCompletata.Play();
            if (tendeCompletate < tendeDaCompletare.Length)
                tendeDaCompletare[tendeCompletate].SetActive(true);
            else
                Instantiate(tendaCompletaPrefab, new Vector3(Random.Range(-8f, 8f), -1.80f, 0), Quaternion.Euler(new Vector3(0, 180*Random.Range(0,1), 0)));
            tendeCompletate++;
            tendeCompleteText.text = tendeCompletate.ToString();
            poles -= neededPoles;
            polesCollectedText.text = poles.ToString();
            catino--;
            catinoCollectedText.text = catino.ToString();
            sovrattelo--;
            sovratteloCollectedText.text = sovrattelo.ToString();
        }
        hoVinto = true;
        for (int i = 0; i < tendeDaCompletare.Length; i++)
        {
            hoVinto = hoVinto && tendeDaCompletare[i].activeSelf;
        }
        if (hoVinto == true && hoPerso == false && sopravvivenza == false) Vittoria();

        if (lives <= 0)
        {
            hoPerso = true;
            Sconfitta();
        }

        if (totalPointsNumber >= 10 && trasformazione1 == false)
        {
            Player.trasformazione1();
            trasformazione1 = true;
        }
        if (totalPointsNumber >= 30 && trasformazione2 == false)
        {
            Player.trasformazione2();
            trasformazione2 = true;
        }
        if (totalPointsNumber >= 50 && trasformazione3 == false) 
        {
            Player.trasformazione3();
            trasformazione3 = true;
        }

        if (sottofondo.time>=sottofondo.clip.length -0.2f)
        {
            sottofondo.Stop();
            currentSong = (currentSong + 1) % songsBackground.Length;
            sottofondo.clip = songsBackground[currentSong];
            sottofondo.Play();
        }
        
    }
    
    public void subtractLife()
    {
        lives--;
        livesText.text = lives.ToString();
        //destroyMasso.Play();
    }

    public void poleCollected()
    {
        poles++;
        polesCollectedText.text = poles.ToString();
    }

    public void catinoCollected()
    {
        catino++;
        catinoCollectedText.text = catino.ToString();
    }
    public void sovratteloCollected()
    {
        sovrattelo++;
        sovratteloCollectedText.text = sovrattelo.ToString();
    }

    private void Vittoria()
    {
        StopGameActivities();

        vittoriaScreen.SetActive(true);
        StartCoroutine(fuochidartificio());
    }

    IEnumerator fuochidartificio()
    {
        while (true)
        {
            spawnPosition.x = Random.Range(-10f,10f);
            spawnPosition.y = Random.Range(-5f, 5f);
            spawnPosition.z = -1f;
            Instantiate(particle, spawnPosition, Quaternion.identity);
            yield return new WaitForSecondsRealtime(Random.Range(1f, 2f));
        }
    }

    private void Sconfitta()
    {
        StopGameActivities();
        //jumpButton.SetActive(false);
        //joystick.SetActive(false);
        if (sopravvivenza == true)
        {
            items.SetActive(false);
            switch (PlayerPrefs.GetFloat("difficolta"))
            {
                case 1f:
                    ilTuoPunteggio.text = "<size=45><b>x1</b></size> con difficoltà <size=45><b>FACILE</b></size>\nIl tuo punteggio è:";
                    totalPointsNumber *= 1f;
                    totalPoints.text = totalPointsNumber.ToString();
                    break;
                case 1.5f:
                    ilTuoPunteggio.text = "<size=45><b>x1.5</b></size> con difficoltà <size=45><b>INTERMEDIO</b></size>\nIl tuo punteggio è:";
                    totalPointsNumber *= 1.5f;
                    totalPoints.text = totalPointsNumber.ToString();
                    break;
                case 2.5f:
                    ilTuoPunteggio.text = "<size=45><b>x2.5</b></size> con difficoltà <size=45><b>INSANE!</b></size>\nIl tuo punteggio è:";
                    totalPointsNumber *= 2.5f;
                    totalPoints.text = totalPointsNumber.ToString();
                    break;
                default:
                    Debug.Log("inconsistent difficulty level");
                    break;


            }
            tendeCompleteTextClassifica.text = tendeCompletate.ToString();
            polesCollectedTextClassifica.text = poles.ToString();
            catinoCollectedTextClassifica.text = catino.ToString();
            sovratteloCollectedTextClassifica.text = sovrattelo.ToString();
            classificaScreen.SetActive(true);
            
            int partialPoints = 0;
            while(partialPoints < totalPointsNumber)
            {
                partialPoints += 1;
                totalPoints.text = partialPoints.ToString();
            }
            

        }
        else
        {
            sconfittaScreen.SetActive(true);
        }
        
    }

    public void TornaAlMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void StopGameActivities()
    {   
        
        sottofondo.Stop();
        spawnManager.SetActive(false);
    }

    public void AddLives()
    {
        lives += 3;
        livesText.text = lives.ToString();
    }

    public void ActivateBeamButton()
    {
        beamButton.SetActive(true);
    }
    public void ActivateLightButton()
    {
        lightButton.SetActive(true);
    }
    public void ActivateLampoButton()
    {
        lampoButton.SetActive(true);
    }

}
