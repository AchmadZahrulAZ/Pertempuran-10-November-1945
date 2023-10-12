using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement_p : MonoBehaviour
{
    public float speed = 5.0f; // Kecepatan pemain
    private Rigidbody2D rb;
    //public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * speed;

        if(moveHorizontal > 0)
        {
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        }
        else if(moveHorizontal < 0)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        // // Mengatur parameter animator
        // animator.SetFloat("Horizontal", moveHorizontal);
        // animator.SetFloat("Vertical", moveVertical); 
        
        // animator.SetFloat("Speed", movement.sqrMagnitude);       
    }
}
