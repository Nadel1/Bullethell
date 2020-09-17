using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();

    public void AddToList(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file=


    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        int i = 0;

        foreach(GameObject element in enemies)
        {
            save.enemiesPositions.Add(element.transform.position);
            save.enemiesShootingType.Add(element.GetComponent<EnemyShooting>().GetShootingMode());
        }

        save.multiplier = GetComponent<ScoreSystem>().GetMultiplier();
        save.health = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerHealth>().GetHealth();
        return save;
    }
}
