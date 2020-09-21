using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

//manages the pause as well as saving
public class GameManager : MonoBehaviour
{
    private bool isPaused;

    [SerializeField]
    [Tooltip("Menu that will pop up when game is paused")]
    private GameObject menu;

    private GameObject DDOL;
    private string savepath;

    public GameObject enemy;
    private void Start()
    {
        menu.SetActive(false);

        DDOL = GameObject.FindGameObjectWithTag("DDOL");
        savepath = DDOL.GetComponent<MenuManager>().saveFile;
        LoadGame();
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + savepath);
        bf.Serialize(file, save);
        file.Close();

        ResetToDefault();
        Debug.Log("Game saved");
    }

    private void ResetToDefault()
    {
        GameObject[] enbullets = GameObject.FindGameObjectsWithTag("Projectile");
        GameObject[] playbullets = GameObject.FindGameObjectsWithTag("Player Projectile");

        GameObject[] bullets = enbullets.Concat(playbullets).ToArray();
        foreach(GameObject bullet in bullets)
        {
            Destroy(bullet);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    
    

    }
    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject element in enemies)
        {

            save.enemiesPos.Add(element.transform.position);
            save.enemiesRot.Add(element.transform.rotation);
            save.enemiesShootingType.Add(element.GetComponent<EnemyShooting>().GetShootingMode());
        }

        save.playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        save.playerRot = GameObject.FindGameObjectWithTag("Player").transform.rotation;

        save.multiplier = GetComponent<ScoreSystem>().GetMultiplier();
        save.health = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerHealth>().GetHealth();
        save.score = GetComponent<ScoreSystem>().GetScore();
        return save;
    }
    public void Pause()
    {
        menu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Unpause()
    {
        menu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        isPaused = false;
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + savepath))
        {
            //pauses the current game
            Pause();

            ResetToDefault();

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + savepath, FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            for(int i=0; i < save.enemiesPos.Count ; i++)
            {
                Instantiate(enemy, save.enemiesPos[i], save.enemiesRot[i]);
            }

            //position and rotate the player accordingly
            GameObject.FindGameObjectWithTag("Player").transform.position = save.playerPos;
            GameObject.FindGameObjectWithTag("Player").transform.rotation = save.playerRot;

            GetComponent<ScoreSystem>().SetMultiplier(save.multiplier);
            GetComponent<ScoreSystem>().SetScore(save.score);
            Debug.Log("Game Loaded");
            //unpauses so that game can continue/begin
            Unpause();
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
}
