using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translations : MonoBehaviour
{
    //Translations
    public string eng;
    public string ger;

    private Dictionary<string, string> translations=new Dictionary<string, string>();
    // Start is called before the first frame update
    void Start()
    {
        AddValues();
    }
    
    private void AddValues()
    {
        if (translations.Count == 0)
        {
            translations.Add("eng", eng);
            translations.Add("ger", ger);
        }
        
    }
    public void UpdateLanguage(string key)
    {
        AddValues();
        foreach(KeyValuePair<string,string> element in translations)
        {
            if (element.Key.Equals(key))
            {
                GetComponent<TextMeshProUGUI>().SetText(translations[key]);
                break;
            }
        }
    }

}
