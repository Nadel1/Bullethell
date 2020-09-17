using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//manages the pause as well as saving
public class GameManager : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> bullets = new List<GameObject>();

    private bool isPaused;

    [SerializeField]
    [Tooltip("Menu that will pop up when game is paused")]
    private GameObject menu;

    public void AddEnemyToList(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void AddBulletToList(GameObject bullet)
    {
        bullets.Add(bullet);
    }
    public void SaveGame()
    {
        Save save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        ResetToDefault();
        Debug.Log("Game saved");
    }

    private void ResetToDefault()
    {
        enemies.Clear();
        foreach(GameObject bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        foreach(GameObject element in enemies)
        {
            save.enemiesPositions.Add(element.transform.position);
            save.enemiesShootingType.Add(element.GetComponent<EnemyShooting>().GetShootingMode());
        }

        save.multiplier = GetComponent<ScoreSystem>().GetMultiplier();
        save.health = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerHealth>().GetHealth();
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

    private void Start()
    {
        menu.SetActive(false);
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
}
