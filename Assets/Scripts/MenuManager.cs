using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LanguageMenu;

    public void Close()
    {
        Application.Quit();
    }

    public void OpenLanguages()
    {
        MainMenu.SetActive(false);
        LanguageMenu.SetActive(true);
        UpdateLanguages();
    }

    public void ReturnToMain()
    {
        LanguageMenu.SetActive(false);
        MainMenu.SetActive(true);
        UpdateLanguages();
    }
    private void UpdateLanguages()
    {
        GetComponent<LanguageManager>().UpdateTextElements();
    }
}
