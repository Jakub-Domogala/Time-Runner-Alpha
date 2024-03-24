using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector2.up * GameMaster.Instance.timeMultiplayer * Time.deltaTime);
        StartCoroutine("TimeToDie");
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameMaster.Instance.Die();
        }
        Destroy(this.gameObject);
    }

    IEnumerable TimeToDie()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
