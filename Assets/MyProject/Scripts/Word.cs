using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class Word 
{
    private string word;
    private int lettersChecked;
    private TimeSpan elapsedTime;
    private int errors;
    [NonSerialized]
    private Stopwatch stopwatch;
    
    public Word(string word)
    {
        this.word = word;
        lettersChecked = 0;
        stopwatch = new Stopwatch();
        errors= 0;
    }
    public Stopwatch StopWatch
    {
        get { return stopwatch; }
        set { stopwatch = value; }
    }
    public int Errors
    {
        get { return errors; }
    }
    public TimeSpan ElapsedTime
    {
        get { return elapsedTime; }
    }
    public bool CheckLetter(string letter)
    {
        bool isCorrect;

        if(word[lettersChecked].ToString().Equals(letter))
        {
            lettersChecked++;
            isCorrect = true;
        }
        else
        {
            errors++;
            isCorrect = false;
        }

        return isCorrect;
    }
    public bool CheckWholeWord()
    {
        bool isDone;

        if (word.Length == lettersChecked)
        {
            stopwatch.Stop();
            elapsedTime = stopwatch.Elapsed;
            isDone = true;
        }
        else
        {
            isDone  = false;
        }

        return isDone;
    }
    public override string ToString()
    {
        return "<color=green>" + word.Substring(0, lettersChecked) 
            + "</color>" + word.Substring(lettersChecked);
    }
    public string ReadData()
    {
        return word + "!!" + elapsedTime.ToString() 
                    + "!!" + errors.ToString();
    }
}
