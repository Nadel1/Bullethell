using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save
{
    public List<GameObject> enemies = new List<GameObject>();
    public List<int> enemiesShootingType = new List<int>();
    public Vector3 playerPos;
    public Quaternion playerRot;

    public float multiplier=1;
    public float health = 100;
    public int score = 0;
}

