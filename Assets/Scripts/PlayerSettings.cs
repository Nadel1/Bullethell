using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{


    public void Awake()
    {
        if (!PlayerPrefs.HasKey("lang"))
        {
            PlayerPrefs.SetString("lang", GetComponent<LanguageManager>().GetCurrent());
            PlayerPrefs.Save();
        }
        else
        {
            GetComponent<LanguageManager>().SetUp(PlayerPrefs.GetString("lang"));
        }
    }
}
