using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class allData : MonoBehaviour
{
    public string name, dateVar, timeVar, mission;
    public float timePlay, setTimePlay;
    public float currentTime;
    public bool playGame, gameOver;
    public string desktopPath;
    public string filePath;
    public AudioClip _audioBG;
    public GameObject bgPrefab;

    private Highscore _highscores = new Highscore();

    private void Awake()
    {
        //Cursor.visible = false;
        desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        try
        {
            if (!Directory.Exists(desktopPath + filePath))
            {
                Directory.CreateDirectory(desktopPath + filePath);

                //string filePath = Environment.GetFolderPath
                //(Environment.SpecialFolder.Desktop) + "/" + "data.csv";  // save file to desktop

                string highscores = JsonUtility.ToJson(_highscores, true);
                File.WriteAllText(desktopPath + filePath + "/scorePlayer.json", highscores);


                //addRecord("Name" + "," + "Data" + "," + "Time" + "," + "Time Score");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(desktopPath + filePath + "/DataPlayer.csv", true))
                {
                    file.WriteLine("Name" + "," + "Data" + "," + "Time" + "," + "Mission" + "," + "Status" + "," + "Time Score");
                }
            }

            DontDestroyOnLoad(this.gameObject);
        }
        catch
        {
            Debug.Log("Error");
        }

        if(GameObject.Find("sound background") == null)
        {
            createBGSound();
        }
    }

    public void createBGSound()
    {
        GameObject prefab = Instantiate(bgPrefab, transform.position, Quaternion.identity);
        prefab.name = "sound background";
        prefab.GetComponent<audio>()._audioSource.clip = _audioBG;
        prefab.GetComponent<audio>().loop = true;
        prefab.GetComponent<audio>()._audioSource.loop = true;
        prefab.GetComponent<audio>()._audioSource.volume = 0.3f;
    }

    public void Start()
    {
        //Cursor.visible = false;
    }

    [System.Serializable]
    private class Highscore
    {
        public string name, dateTime;
        public int timePlay;
    }
    public void addRecord(string name, string dateVar, string timeVar, string mission, string gameOver, string setTimePlay)
    {
        try
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(desktopPath + filePath + "/DataPlayer.csv", true))
            {
                file.WriteLine(name + "," + dateVar + "," + timeVar + "," + mission + "," + gameOver + "," + setTimePlay);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("This program did an oppsie :", ex);
        }
    }

    public void rescene()
    {
        name = "";
        dateVar = "";
        timeVar = "";
        timePlay = 0;
        setTimePlay = 90;
        playGame = false;
        gameOver = false;
    }

    private void Update()
    {
        //if (playGame)
        //{
        //    timePlay += Time.deltaTime;
        //}

        //t += Time.deltaTime;

        if (playGame && !gameOver)
        {
            setTimePlay -= Time.deltaTime;
            if(setTimePlay <= 0)
            {
                gameOver = true;
            }
        }

        //t += Time.deltaTime;
    }

    //private void OnMouseDown()
    //{
    //    t = 0;
    //}
}
