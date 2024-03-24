using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    // Static instance of the singleton
    private static GameMaster _instance ;

    // Public property to access the singleton instance
    public static GameMaster Instance => _instance;

    [SerializeField] public float timeMultiplayer;
    [SerializeField] public float timer;
    [SerializeField] public float timerModified;
    [SerializeField] public float timeIncrease;
    [SerializeField] public float timeToWait;
    [SerializeField] public float multiplayerUpperLimit = 5;
    [SerializeField] public float multiplayerLowerLimit = 1;
    [SerializeField] public bool isDecrease = false;
    [SerializeField] TypeOfCalculation calculation;

    float timerIni;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        timerIni = timeToWait;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time;
        SetTimeMultiplayer();
    }

    void SetTimeMultiplayer()
    {
        timeMultiplayer = SetMultiplayerByFunction();
        timeMultiplayer = Mathf.Clamp(timeMultiplayer, multiplayerLowerLimit, multiplayerUpperLimit);
        timerModified += Time.deltaTime * timeMultiplayer;
    }

    float SetMultiplayerByFunction()
    {
        switch (calculation)
        {
            case TypeOfCalculation.Linear:
                return isDecrease ? timeMultiplayer - Time.deltaTime * timeIncrease :  timeMultiplayer + Time.deltaTime * timeIncrease;
            case TypeOfCalculation.Custom:
                if (!(Time.time >= timerIni)) return timeMultiplayer;
                timerIni += timeToWait;
                return isDecrease ? timeMultiplayer -= timeIncrease : timeMultiplayer += timeIncrease;

        }
        
        return 0;
    }

    public void SetTimeMultiplayer(float multi)
    {
        timeMultiplayer = Mathf.Clamp(multi, multiplayerLowerLimit, multiplayerUpperLimit);
    }

    public void DecreaseMultiplayer()
    {
        timeMultiplayer = Mathf.Clamp(timeMultiplayer / 2, multiplayerLowerLimit, multiplayerUpperLimit);
    }
    public void IncreaseMultiplayer()
    {
        timeMultiplayer = Mathf.Clamp(timeMultiplayer * 2, multiplayerLowerLimit, multiplayerUpperLimit);
    }

    public void Die()
    {
        DataBase.Instance.Time = timerModified;
        StartCoroutine(RestartLevel());
    }

    public void Next()
    {
        StartCoroutine(NextLevel());
    }
    public IEnumerator RestartLevel()
    {
        Canvas.Instance.PlayClosingAnimation();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public IEnumerator NextLevel()
    {
        Canvas.Instance.PlayClosingAnimation();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}

enum TypeOfCalculation
{
    Linear =1,
    Custom =2

}
