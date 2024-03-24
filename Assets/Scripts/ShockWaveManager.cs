using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class ShockWaveManager : MonoBehaviour
{
    [SerializeField] private float shockWaveTime = 0.75f;
    private Coroutine shockWaveCoroutine;
    private Material material;
    private static int waveDistanceFromCenter = Shader.PropertyToID("_WaveDistaceFromCenter");

    private int currentCallFrequency; // Current frequency of calling the method
    private Coroutine callingCoroutine; // Coroutine for calling the method

    [SerializeField] AudioClip[] sound;
    private AudioSource audioSource;

    [SerializeField]  private AudioMixerGroup pitchBendGroup;
    public int soundNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = pitchBendGroup;
        audioSource.Stop();
        audioSource.clip = sound[soundNumber];
        audioSource.loop = true; 
        audioSource.volume = 1.0f;
        audioSource.pitch = 1.0f;
        audioSource.Play();
        material = GetComponent<SpriteRenderer>().material;
        currentCallFrequency = (int)GameMaster.Instance.timeMultiplayer;
    }

    // Update is called once per frame
    void Update()
    {

        //float speedup = math.remap(1f, 5f, 1f, 2.0f, GameMaster.Instance.timeMultiplayer);
        //audioSource.pitch = speedup;
        //pitchBendGroup.audioMixer.SetFloat("pitchBlend", 1f / speedup);

        SetSound();

        // If the coroutine is running, restart it with the updated frequency
        //if (currentCallFrequency != (int)GameMaster.Instance.timeMultiplayer)
        //{
        //    currentCallFrequency = (int)GameMaster.Instance.timeMultiplayer;
        //    StopCoroutine(callingCoroutine);
        //    callingCoroutine = StartCoroutine(CallMethodRepeatedly());
        //}
        if (Input.GetKeyUp(KeyCode.E)) 
        {
            CallShockWave();
        }
    }

    void SetSound()
    {
        
        if (soundNumber !=0 && GameMaster.Instance.timeMultiplayer <= GameMaster.Instance.multiplayerUpperLimit / 3f )
        {
            soundNumber = 0;
            audioSource.Stop();
            audioSource.clip = sound[soundNumber];
            CallShockWave();
            audioSource.Play();
        }
        else if (soundNumber!=1 && GameMaster.Instance.timeMultiplayer <= GameMaster.Instance.multiplayerUpperLimit * 2f / 3f && GameMaster.Instance.timeMultiplayer > GameMaster.Instance.multiplayerUpperLimit / 3f)
        {
            soundNumber = 1;
            audioSource.Stop();
            audioSource.clip = sound[soundNumber];
            CallShockWave();
            audioSource.Play();
        }
        else if (soundNumber!=2 && GameMaster.Instance.timeMultiplayer > GameMaster.Instance.multiplayerUpperLimit * 2f / 3f)
        {
            soundNumber = 2;
            audioSource.Stop();
            audioSource.clip = sound[soundNumber];
            CallShockWave();
            audioSource.Play();
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
            // Wait for the adjusted frequency before calling again
            yield return null;// new WaitForSeconds((GameMaster.Instance.timeIncrease * 100) / (int)GameMaster.Instance.timeMultiplayer);
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
        while (elapsedTime < shockWaveTime)
        {

            elapsedTime += Time.deltaTime;
            lerpedAmount = Mathf.Lerp(startPos, endPos, elapsedTime / shockWaveTime);
            material.SetFloat(waveDistanceFromCenter, lerpedAmount);
            yield return null;
        }
        material.SetFloat(waveDistanceFromCenter, -0.1f);

    }
}
