using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Save
{
    public List<SerializableVector3> enemiesPos = new List<SerializableVector3>();
    public List<SerializableQuaternion> enemiesRot = new List<SerializableQuaternion>();
    public List<int> enemiesShootingType = new List<int>();
    public SerializableVector3 playerPos;
    public SerializableQuaternion playerRot;

    public float multiplier=1;
    public float health = 100;
    public int score = 0;
}

