using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public Transform image1;
    public Transform image2;

    public TMP_Text text;
    public TMP_Text timer;
    // Static instance of the singleton
    private static Canvas _instance;

    // Public property to access the singleton instance
    public static Canvas Instance => _instance;
    // Start is called before the first frame update
    void Start()
    {
        text.text = Math.Round(GameMaster.Instance.timeMultiplayer,2).ToString();
        timer.text = Math.Round(GameMaster.Instance.timerModified,3).ToString();
        PlayOpeningAnimation();
    }
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

    void Update()
    {
        text.text = Math.Round(GameMaster.Instance.timeMultiplayer, 2).ToString();
        timer.text = Math.Round(GameMaster.Instance.timerModified, 3).ToString();
    }

    public void PlayOpeningAnimation()
    {
        image1.transform.DOMoveX(-960, 0.5f);
        image2.transform.DOMoveX( -960, 0.5f);

    }
    public void PlayClosingAnimation()
    {
        image1.transform.DOMoveX(0, 0.5f);
        image2.transform.DOMoveX(960*2, 0.5f);

    }
}
