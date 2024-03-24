using System.Collections;
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
    [SerializeField] AudioClip fart;
    private AudioSource audioSource;

    [SerializeField]  private AudioMixerGroup pitchBendGroup;
    public int soundNumber = 0;
    public bool farted = false;
    public float timeToNextFart = 3f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = pitchBendGroup;
        audioSource.Stop();
        audioSource.clip = sound[soundNumber];
        audioSource.loop = true; 
        audioSource.volume = 0.1f;
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
        float maxLimit = GameMaster.Instance.multiplayerUpperLimit;
        float multiplayer = GameMaster.Instance.timeMultiplayer;
        float editValue = multiplayer/ (maxLimit * (soundNumber +1));

        audioSource.volume = editValue;
        audioSource.pitch = 1f + editValue;

        if (!farted && multiplayer <= maxLimit / 6f )
        {
            audioSource.Stop();
            CallShockWave();
            farted = true;
            audioSource.volume = 1f;
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(fart);
            //Fart
        }
        else if (!audioSource.isPlaying && multiplayer <= maxLimit * 2f / 6f && multiplayer > maxLimit / 6f)
        {
            soundNumber = 0;
            audioSource.Stop();
            audioSource.clip = sound[soundNumber];
            audioSource.Play();

            farted = false;
        }
        else if (!farted && multiplayer <= maxLimit * 3f / 6f && multiplayer > maxLimit * 2f/ 6f)
        {
            audioSource.Stop();
            CallShockWave();
            farted = true;
            audioSource.volume = 1f;
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(fart);
            //Fart
        }
        else if (!audioSource.isPlaying && multiplayer <= maxLimit * 4f / 6f && multiplayer > maxLimit*3f / 6f)
        {
            soundNumber = 1;
            audioSource.Stop();
            audioSource.clip = sound[soundNumber];
            audioSource.Play();

            farted = false;
        }
        else if (!farted && multiplayer <= maxLimit * 5f / 6f && multiplayer > maxLimit * 4f / 6f)
        {
            Debug.Log("pierd");
            audioSource.Stop();
            CallShockWave();
            audioSource.volume = 1f;
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(fart);
            farted = true;
        }
        else if (!audioSource.isPlaying && multiplayer > maxLimit * 5f / 6f && multiplayer < maxLimit)
        {
            soundNumber = 2;
            audioSource.Stop();
            audioSource.clip = sound[soundNumber];
            audioSource.Play();

            farted = false;
        }
        else if(!farted && multiplayer == maxLimit)
        {
            farted = true;
            Debug.Log("pierdmax");
            audioSource.Stop();
            CallShockWave();
            audioSource.volume = 1f;
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(fart);
            timeToNextFart = 3f;
        }

        if (farted && multiplayer >= maxLimit)
        {
            if (timeToNextFart >= 0) timeToNextFart -= Time.deltaTime;
            else
            {
                farted = false;
                Debug.Log("pierd2");
            }
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
