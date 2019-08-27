﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class highscore : MonoBehaviour
{
    public static highscore instance;
    [SerializeField] Text nameT;
    [SerializeField] Text HSList;

    [SerializeField]  char[] name = "AAA".ToCharArray();

    int currentLetter = 0;
    float timeTochange = 0f;
    WaitForSeconds blinkDelay = new WaitForSeconds(0.25f);
    void Start()
    {
        instance = this;
        StartCoroutine(blink());
    }
    IEnumerator blink()
    {
        while (currentLetter <= 2)
        {
            yield return blinkDelay;
            if (currentLetter <= 2)
            {
                if (nameT.text[currentLetter] != name[currentLetter])
                {

                    nameT.text = new string(name);
                }
                else
                {
                    char[] t = nameT.text.ToCharArray();
                    t[currentLetter] = ' ';
                    nameT.text = new string(t);
                }
            }
            else
            {
                nameT.text = new string(name);
            }
        }
    }
    void nextLetter()
    {
        if (name[currentLetter] > 26)
        {
            name[currentLetter]--;
        }
        else
        {
            name[currentLetter] = (char)90;
        }
    }
    void previousLetter()
    {
        if (name[currentLetter] < 90)
        {
            name[currentLetter]++;
        }
        else
        {
            name[currentLetter] = (char)65;
        }
    }
    void Update()
    {
        if (currentLetter < 3)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                nextLetter();
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                previousLetter();
            }
            
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                timeTochange += Time.deltaTime;
                if (timeTochange > 1f)
                {
                    nextLetter();
                    timeTochange = 0f;
                }
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                timeTochange += Time.deltaTime;
                if (timeTochange > 1f)
                {
                    previousLetter();
                    timeTochange = 0f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentLetter < 2)
            {
                currentLetter++;
            }
            else
            {
                saveData();
                currentLetter = 5;
                GameManager.instance.changeState((int)GameManager.GameState.HighScorescreen);
            }
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentLetter > 0)
            {
                currentLetter--;
            }
        }


    }

    void saveData()
    {
        LoadData();
        if (GameManager.instance.HSData.Count == 0)
        {
            GameManager.instance.HSData.Add(new GameManager.highScoreData(nameT.text, GameManager.instance.Score));

        }
        else
        {
            for (int i = 0; i < GameManager.instance.HSData.Count; i++)
            {
                if (GameManager.instance.Score >= GameManager.instance.HSData[i].score)
                {
                    GameManager.instance.HSData.Insert(i,new GameManager.highScoreData(nameT.text, GameManager.instance.Score));
                    break;
                }
                else if (i == GameManager.instance.HSData.Count - 1)
                {
                    GameManager.instance.HSData.Add(new GameManager.highScoreData(nameT.text, GameManager.instance.Score));

                }
            }
        }
        PlayerPrefs.SetInt("HSCount", GameManager.instance.HSData.Count);
        for (int i = 0; i < GameManager.instance.HSData.Count; i++)
        {
            PlayerPrefs.SetString("HS" + i.ToString(), GameManager.instance.HSData[i].name + "," + GameManager.instance.HSData[i].score);

        }

        GameManager.instance.changeState(4);
        LoadData();
    }
    public void LoadData()
    {
        HSList.text = "";
        GameManager.instance.HSData.Clear();
        string[] data;
        for (int i = 0; i < PlayerPrefs.GetInt("HSCount"); i++)
        {
            data = PlayerPrefs.GetString("HS" + i.ToString()).Split(',');
            if (data.Length == 2)
            {
                GameManager.instance.HSData.Add(new GameManager.highScoreData(  data[0], int.Parse( data[1])));
                HSList.text += "\n" + data[0] + "               " + data[1];
            }

        }
    }


}
