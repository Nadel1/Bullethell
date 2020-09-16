using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private Dictionary<string,int> languages = new Dictionary<string, int>();
    public int eng=1;
    public int ger=0;

    //stores all the ui elements that need to be translated
    private GameObject[] textElements;
    // position in the list which is the current language
    private string current;

    public void Start()
    {
        languages.Add("eng", 1);
        languages.Add("ger", 0);
        current = "eng";

        
        UpdateTextElements();
    }

    public Dictionary<string,int> GetLanguages()
    {
        return languages;
    }

    public void German()
    {
        Clear("ger");
        current = "ger";
        SetLanguage(current);
    }

    public void English()
    {
        Clear("eng");
        current = "eng";
        SetLanguage(current);
    }

    private void Clear(string key)
    {
        languages[key] = 0;
    }
    private void SetLanguage(string key)
    {
        foreach(KeyValuePair<string,int> element in languages)
        {
            if (element.Key.Equals(key))
            {
                languages[key] = 1;
                break;

            }
        }
    }

    public void UpdateTextElements()
    {
        textElements = GameObject.FindGameObjectsWithTag("TextElements");
        Debug.Log(textElements.Length);
        foreach (GameObject Textobject in textElements)
        {
            
                Textobject.GetComponent<Translations>().UpdateLanguage(current);
        }
    }
}
