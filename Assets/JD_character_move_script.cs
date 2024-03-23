using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JD_character_move_script : MonoBehaviour
{
    public float speed = 7.0f; // The speed at which the character moves
    public Animator animator;  // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the horizontal axis (A and D keys, left and right arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move the character
        transform.Translate(horizontalInput * speed * Time.deltaTime, 0, 0);

        // Update the animator with the movement speed
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));


        // You can add more code here to handle other animations like jumping, attacking, etc.
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
    }

}
