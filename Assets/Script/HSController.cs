using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;

public class HSController : MonoBehaviour
{
    private string secretKey = "chiaveSegreta"; // Edit this value and make sure it's the same as the one stored on the server
    private string addScoreURL = "http://bo17game.ueuo.com/addscore.php?"; //be sure to add a ? to your url
    private string highscoreURL = "http://bo17game.ueuo.com/display.php";
    public TMP_Text highScoreText;
    public TMP_Text playerName;
    public TMP_Text playerScore;
    private bool scoreSent = false;
    
    void Start() 
    {
        StartCoroutine(GetScoresCoroutine(false));
    }

    public void GetScore()
    {
        //if(scoreSent == false)
        StartCoroutine(GetScoresCoroutine(true));
    }

    public void SendScore()
    {
        if(scoreSent == false)
        {
            Int32.TryParse(playerScore.text, out int x);
            StartCoroutine(PostScores(playerName.text, x));
            scoreSent = true;
        }
        
    }

    // remember to use StartCoroutine when calling this function!
    IEnumerator PostScores(string name, int score)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(name + score + secretKey);

        string post_url = addScoreURL + "name=" + UnityWebRequest.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(post_url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(highscoreURL + ": Error: " + webRequest.error);
            }
            
        }
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScoresCoroutine(bool playerNameThere)
    {
        highScoreText.text = "Caricamento Punteggi...";
        yield return new WaitForSeconds(2f);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(highscoreURL))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(highscoreURL + ": Error: " + webRequest.error);
            }
            else
            {
                //Questa è una prova
                string[] splitArray = webRequest.downloadHandler.text.ToString().Split('#');
                
                highScoreText.text = "";

                for (int i = 0; i<splitArray.Length; i++)
                {
                    if (splitArray[i].Contains(playerName.text.ToString()) && playerNameThere)
                    {
                        //Debug.Log(splitArray[i]);
                        highScoreText.text = highScoreText.text + "<mark=#ffff00aa>" + '#' + splitArray[i] + "</mark>";
                        
                    }
                    else if (splitArray[i].CompareTo("")==1)
                    highScoreText.text = highScoreText.text + '#'+splitArray[i];
                }

                //highScoreText.text = webRequest.downloadHandler.text;
            }
        }
    }

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    public static string getBetween(string strSource, string strStart, string strEnd)
    {
        int Start, End;
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
        }
        else
        {
            return "";
        }
    }
}