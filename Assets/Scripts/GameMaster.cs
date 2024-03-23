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
        timeMultiplayer += (Time.deltaTime * timeIncrease);
        //UpdateText();
    }

    void UpdateText()
    {
        text.text = timeMultiplayer.ToString();
        text2.text = (timer * timeMultiplayer).ToString();
    }
}
