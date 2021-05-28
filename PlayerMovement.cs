using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpPower = 15f;
    public int extraJumps = 1;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform feet;
    [SerializeField] AudioSource jumpsound;
    Vector3 characterScale;
    float characterScaleX;

    int jumpCount = 0;
    bool isGrounded;
    float mx;
    float jumpCoolDown;
    

    void Start()
    {
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;
        jumpsound = GetComponent<AudioSource>();
    }

    private void Update()
    {

        // Flip the Character:
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -characterScaleX;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = characterScaleX;
        }
        transform.localScale = characterScale;

        mx = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        CheckGrounded();

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mx * speed, rb.velocity.y);
    }

    void Jump()
    {
        if(isGrounded || jumpCount < extraJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCount++;
            jumpsound = GetComponent<AudioSource>();
            jumpsound.Play();
        }
    }

    void CheckGrounded()
    {
        if (Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer))
        {
            isGrounded = true;
            jumpCount = 0;
            jumpCoolDown = Time.time + 0.2f;
        } 
        
        else if (Time.time < jumpCoolDown)
        {
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }


    }

}
