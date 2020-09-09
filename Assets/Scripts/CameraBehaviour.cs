using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform follow;
    public float moveSpeed = 0.1f;
    private Vector3 moveTo;

    [SerializeField]
    private float frequency = 25;

    [SerializeField]
    Vector3 maximumTranslationShake = Vector3.one * 0.5f;
    [SerializeField]
    Vector3 maximumAngularShake = Vector3.one * 0.5f;

    [SerializeField]
    private float recoverySpeed = 1.5f;

    [SerializeField]
    private float traumaExponent = 2;

    private float trauma = 0;
    private float seed;
    // Update is called once per frame

    private void Awake()
    {
        seed = Random.value;
    }
    void FixedUpdate()
    {
        moveTo = new Vector3(follow.position.x, follow.position.y, -30);
        transform.position = Vector3.Lerp(transform.position, moveTo, moveSpeed*Time.fixedDeltaTime);


        float shakeFactor = Mathf.Pow(trauma, traumaExponent);
        Vector3 shake= new Vector3(
            maximumTranslationShake.x*(Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1),
            maximumTranslationShake.y * (Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1),
            maximumTranslationShake.z * (Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1)
            )*shakeFactor;
        transform.localRotation = Quaternion.Euler(new Vector3(
            maximumAngularShake.x * (Mathf.PerlinNoise(seed + 3, Time.time * frequency) * 2 - 1),
            maximumAngularShake.y * (Mathf.PerlinNoise(seed + 4, Time.time * frequency) * 2 - 1),
            maximumAngularShake.z * (Mathf.PerlinNoise(seed + 5, Time.time * frequency) * 2 - 1)
            ) * shakeFactor);
        transform.position += shake;

        trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
    }

    public void InduceStress(float stress)
    {
        trauma = Mathf.Clamp(trauma + stress,0,3);
    }
}
