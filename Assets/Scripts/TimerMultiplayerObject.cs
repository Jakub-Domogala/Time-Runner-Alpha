using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMultiplayerObject : MonoBehaviour
{
    Rigidbody2D rigidbody;
    [SerializeField] ObjectOptions options;
    [SerializeField] float setMultiplayer = 3;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (options)
        {
            case ObjectOptions.SetValue:
                GameMaster.Instance.SetTimeMultiplayer(setMultiplayer);
                break;
            case ObjectOptions.Decrease:
                GameMaster.Instance.DecreaseMultiplayer();
                break;
            case ObjectOptions.Increase:
                GameMaster.Instance.IncreaseMultiplayer();
                break;
        }
        Destroy(this.gameObject);
    }
}

enum ObjectOptions
{
    SetValue = 1,
    Decrease = 2,
    Increase = 3

}