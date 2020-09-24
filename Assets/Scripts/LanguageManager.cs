using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class LanguageManager : MonoBehaviour
{
    private Dictionary<string,int> languages = new Dictionary<string, int>();
    public int eng=1;
    public int ger=0;

    //stores all the ui elements that need to be translated
    private GameObject[] textElements;
    // position in the list which is the current language
    private string current;

    public GameObject select1;
    public string save1;
    public GameObject select2;
    public string save2;
    public GameObject select3;
    public string save3;

    private List<string> saveSlots = new List<string>();
    private List<GameObject> saveButtons = new List<GameObject>();
    public void Start()
    {
        languages.Add("eng", 1);
        languages.Add("ger", 0);
        current = "eng";

        saveSlots.Add(save1);
        saveSlots.Add(save2);
        saveSlots.Add(save3);

        saveButtons.Add(select1);
        saveButtons.Add(select2);
        saveButtons.Add(select3);
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
        UpdateTextElements();
    }

    public void UpdateTextElements()
    {
        textElements = GameObject.FindGameObjectsWithTag("TextElements");
        Debug.Log(textElements.Length);
        foreach (GameObject Textobject in textElements)
        {
            
                Textobject.GetComponent<Translations>().UpdateLanguage(current);
        }
        for(int i=0; i<saveButtons.Count;i++)
        {
            if (File.Exists(Application.persistentDataPath + saveSlots[i]))
            {
                saveButtons[i].GetComponent<SecondaryTranslation>().UpdateLanguage(current);
            }
        }
    }
    
    public string GetCurrent()
    {
        return current;
    }

    public void SetUp(string current)
    {
        this.current = current;
        UpdateTextElements();
    }
}
