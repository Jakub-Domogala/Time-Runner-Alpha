using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    // Start is called before the first frame update
    public float beltSpeed = 5f;
    public bool rotated = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotated)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        gameObject.GetComponent<Animator>().speed = GameMaster.Instance.timeMultiplayer;
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (rotated)
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * beltSpeed * GameMaster.Instance.timeMultiplayer);
            else
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * beltSpeed);
        }
    }
}
