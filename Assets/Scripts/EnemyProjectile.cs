using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector2.up * GameMaster.Instance.timeMultiplayer * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        
    }
}
