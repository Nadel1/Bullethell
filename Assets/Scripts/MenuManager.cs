using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject LanguageMenu;
    public GameObject SelectMenu;

    public string saveFile;

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
    public void OpenSelect()
    {
        MainMenu.SetActive(false);
        SelectMenu.SetActive(true);
        UpdateLanguages();
    }

    public void ReturnToMain()
    {
        LanguageMenu.SetActive(false);
        SelectMenu.SetActive(false);
        MainMenu.SetActive(true);
        UpdateLanguages();
    }
    private void UpdateLanguages()
    {
        GetComponent<LanguageManager>().UpdateTextElements();
    }

    public void LoadGame(string save)
    {
        this.saveFile = save;
        SceneManager.LoadScene("Gameplay");

    }
}
