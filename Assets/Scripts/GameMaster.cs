using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] float timeMultiplayer;
    [SerializeField] float timer;
    [SerializeField] float timeIncrease;

    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text text2;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time;
        timeMultiplayer += (Time.deltaTime * timeIncrease);
        text.text = timeMultiplayer.ToString();
        text2.text = (timer*timeMultiplayer).ToString();
    }
}
