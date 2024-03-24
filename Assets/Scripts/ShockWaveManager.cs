using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveManager : MonoBehaviour
{
    [SerializeField] private float shockWaveTime = 0.75f;
    private Coroutine shockWaveCoroutine;
    private Material material;
    private static int waveDistanceFromCenter = Shader.PropertyToID("_WaveDistaceFromCenter");
    
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E)) 
        {
            CallShockWave();
            Debug.Log("Wave");
        }
    }

    public void CallShockWave()
    {
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
        
    }
}
