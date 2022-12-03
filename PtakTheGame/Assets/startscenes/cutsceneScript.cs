using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class cutsceneScript : MonoBehaviour
{
    public TMP_Text text;
    string s1 = "Jest rok 2025. W wyniku śledztwa prowadzonego przez służby specjalne Stanów Zjednoczonych doszło do ujawnienia mrocznych faktów";
    string s2 = "Okazało sie, ze od lat poszukiwana grupa terrorystyczna wykorzystywana drapiezne ptaki jako agentów do przenoszenia informacji";
    string s3 = "W tym celu policja w porozumieniu z głową państwa zaczyna polować na te zwierzęta, wliczając w to... Ciebie";
    string s4 = "UCIEKAJ Z MIASTA!!!!";

    private IEnumerator write()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < s1.Length; i++)
        {
            text.text += s1[i];
            yield return new WaitForSeconds(1 / 2);
        }
        yield return new WaitForSeconds(5);
        text.text = "";
        for (int i = 0; i < s2.Length; i++)
        {
            text.text += s2[i];
            yield return new WaitForSeconds(1 / 2);
        }
        yield return new WaitForSeconds(5);
        text.text = "";
        for (int i = 0; i < s3.Length; i++)
        {
            text.text += s3[i];
            yield return new WaitForSeconds(1 / 2);
        }
        yield return new WaitForSeconds(5);
        text.text = s4;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("SampleScene");
    }


    void Start()
    {
        StartCoroutine(write());
       
    }

}
