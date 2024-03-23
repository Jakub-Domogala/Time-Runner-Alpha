using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    // Static instance of the singleton
    private static GameMaster _instance ;

    // Public property to access the singleton instance
    public static GameMaster Instance => _instance;

    [SerializeField] public float timeMultiplayer;
    [SerializeField] public float timer;
    [SerializeField] public float timeIncrease;
    [SerializeField] public float multiplayerUpperLimit = 5;
    [SerializeField] public float multiplayerLowerLimit = 1;
    [SerializeField] public bool isDecrease = false;

    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text text2;
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
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time;
        SetTimeMultiplayer();
        UpdateText();
    }

    void SetTimeMultiplayer()
    {
        _ = isDecrease ? timeMultiplayer -= (Time.deltaTime * timeIncrease) : timeMultiplayer += (Time.deltaTime * timeIncrease);
        timeMultiplayer = Mathf.Clamp(timeMultiplayer, multiplayerLowerLimit, multiplayerUpperLimit);
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
    void UpdateText()
    {
        text.text = timeMultiplayer.ToString();
        //timer = Time.time * timeMultiplayer;
        text2.text = timer.ToString();
        
    }
}
