using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypingTesting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI word;
    [SerializeField] private TextMeshProUGUI currentLetter;


    private void Start()
    {
        currentLetter.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (currentLetter.IsActive())
        {
            CheckKeyboardInput();
        }
    }

    private void CheckKeyboardInput()
    {
        if (currentLetter.text.Equals("_"))
        {
            word.text += " ";
        }
        else if (currentLetter.text.Equals("<<"))
        {
            if (word.text.Length > 0)
            {
                word.text = word.text.Remove(word.text.Length - 1);
            }
        }
        else if (currentLetter.text.Equals("next"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            word.text += currentLetter.text;
        }
        currentLetter.gameObject.SetActive(false);
    }

}
