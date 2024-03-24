using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveManager : MonoBehaviour
{
    [SerializeField] private float shockWaveTime = 0.75f;
    private Coroutine shockWaveCoroutine;
    private Material material;
    private static int waveDistanceFromCenter = Shader.PropertyToID("_WaveDistaceFromCenter");

    private int currentCallFrequency; // Current frequency of calling the method
    private Coroutine callingCoroutine; // Coroutine for calling the method

    [SerializeField] AudioClip sound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        material = GetComponent<SpriteRenderer>().material;
        currentCallFrequency = (int)GameMaster.Instance.timeMultiplayer;
        callingCoroutine = StartCoroutine(CallMethodRepeatedly());
    }

    // Update is called once per frame
    void Update()
    {        

        // If the coroutine is running, restart it with the updated frequency
        if (currentCallFrequency != (int)GameMaster.Instance.timeMultiplayer)
        {
            currentCallFrequency = (int)GameMaster.Instance.timeMultiplayer;
            StopCoroutine(callingCoroutine);
            callingCoroutine = StartCoroutine(CallMethodRepeatedly());
        }
        if (Input.GetKeyUp(KeyCode.E)) 
        {
            CallShockWave();
            Debug.Log("Wave");
        }
    }
    private IEnumerator CallMethodRepeatedly()
    {
        while (true)
        {
            // Call your method here
            if (GameMaster.Instance.isDecrease)
            {
                CallShockWaveReverb();
            }
            else
            {
                CallShockWave();
            }
            audioSource.PlayOneShot(sound);
            // Wait for the adjusted frequency before calling again
            yield return new WaitForSeconds((GameMaster.Instance.timeIncrease * 100) / (int)GameMaster.Instance.timeMultiplayer);
        }
    }
    public void CallShockWave()
    {
        shockWaveCoroutine = StartCoroutine(ShockWaveAction(1f, -0.1f));
        //shockWaveCoroutine = StartCoroutine(ShockWaveAction(-0.1f, 1f));
    }
    public void CallShockWaveReverb()
    {
        //shockWaveCoroutine = StartCoroutine(ShockWaveAction(1f, -0.1f));
        shockWaveCoroutine = StartCoroutine(ShockWaveAction(-0.1f, 1f));
    }
    private IEnumerator ShockWaveAction(float startPos, float endPos)
    {
        material.SetFloat(waveDistanceFromCenter, startPos);
        float lerpedAmount = 0f;
        float elapsedTime = 0f;
        Debug.Log("Elapsed Time" + elapsedTime);
        while (elapsedTime < shockWaveTime)
        {

            elapsedTime += Time.deltaTime;
            Debug.Log("Elapsed Time" + elapsedTime);
            lerpedAmount = Mathf.Lerp(startPos, endPos, elapsedTime / shockWaveTime);
            material.SetFloat(waveDistanceFromCenter, lerpedAmount);
            yield return null;
        }
        material.SetFloat(waveDistanceFromCenter, -0.1f);

    }
}
