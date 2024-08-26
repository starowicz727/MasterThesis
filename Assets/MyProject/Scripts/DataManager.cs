using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.SceneManagement;

public class DataManager 
{
    private static string path = Application.persistentDataPath + "/FinalData.txt";
    public static void SaveData(List<Word> wordsList)
    {
        StreamWriter sw = null;

        try
        {
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fs);

            sw.WriteLine(SceneManager.GetActiveScene().name);
            foreach(Word word in wordsList)
            {
                sw.WriteLine(word.ReadData());
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            sw.Close();
        }
    }

    public static void SaveString(string stringToSave)
    {
        StreamWriter sw = null;
        try
        {
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fs);
            sw.WriteLine(stringToSave);
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            sw.Close();
        }
    }

}
