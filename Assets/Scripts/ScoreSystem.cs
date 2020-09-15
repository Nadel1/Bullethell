using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Points earnt by shooting a monster")]
    private float pointsPerShot = 100;

    [SerializeField]
    [Tooltip("Multiplier for scores")]
    [Range(0, 5)]
    private float scoreMultiplier = 1;

    public TextMeshProUGUI score;
    private float oldScore;

    private bool alreadyWaiting = false;
    public void EnemyShot()
    {
        oldScore = float.Parse(score.GetComponent<TextMeshProUGUI>().text);
        score.GetComponent<TextMeshProUGUI>().SetText(((int)(oldScore + scoreMultiplier * pointsPerShot)).ToString());
    }
    
    public void AddMultiplier(float value, float time)
    {
        scoreMultiplier += value;
        StartCoroutine(MultiplierCooldown(value, time));
    }

    IEnumerator MultiplierCooldown(float value, float time)
    {
        yield return new WaitForSeconds(time);
        scoreMultiplier -= value;
    }

    public float GetMultiplier()
    {
        return scoreMultiplier;
    }
}
