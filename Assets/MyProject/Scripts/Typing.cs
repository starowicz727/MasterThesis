using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Typing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI word;
    [SerializeField] private TextMeshProUGUI currentLetter;
    [SerializeField] private TextMeshProUGUI results;
    [SerializeField] private Canvas resultCanvas;

    private List<Word> wordsList;
    private List<Word> doneList;

    private bool endGame;

    private void Start()
    {  
        wordsList = new List<Word> { new Word("gra"), new Word("pies"), new Word("fala"), 
            new Word("burza"), new Word("twarz"), new Word("ocean"), new Word("muzyka"), 
            new Word("chmura"), new Word("kolejka"), new Word("tulipan"), 
            new Word("komputer"), new Word("kukurydza") };
        
        doneList = new List<Word>();
        currentLetter.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(false);
        endGame = false;
        word.text = wordsList[0].ToString();
        wordsList[0].StopWatch.Start();
    }
    private void Update()
    {
        if (currentLetter.IsActive())
        {
            if (!endGame)
            {
                CheckKeyboardInput();
            }
            else
            {
                if (currentLetter.text.Equals("next"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                currentLetter.gameObject.SetActive(false);
            }
        }
    }

    private void CheckKeyboardInput()
    {
        if (wordsList[0].CheckLetter(currentLetter.text))
        {
            word.text = wordsList[0].ToString();

            if (wordsList[0].CheckWholeWord())
            {
                ShowNewWord();
            }
        }

        currentLetter.gameObject.SetActive(false);
    }

    private void ShowNewWord()
    {
        doneList.Add(wordsList[0]);
        wordsList.RemoveAt(0);

        if (wordsList.Count > 0)
        {
            word.text = wordsList[0].ToString();
            wordsList[0].StopWatch.Start();
        }
        else //endgame
        {
            endGame = true;
            int errors = 0;
            TimeSpan endTime = TimeSpan.Zero;

            DataManager.SaveData(doneList);

            string dataTxt = "";
            foreach (Word word in doneList)
            {
                errors += word.Errors;
                endTime += word.ElapsedTime;
            }
            dataTxt += "errors: "+ errors + "\n" + "time: " + endTime;
            DataManager.SaveString(dataTxt);
            results.text = dataTxt;
            resultCanvas.gameObject.SetActive(true);
        }
    }
}
