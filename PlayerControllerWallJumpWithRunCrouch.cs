using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;

    public float moveSpeed, jumpForce;
	public float runSpeed = 15f;
    public Transform groundCheckPoint;
    public LayerMask whatisGround;

    private bool isGrounded;
    public Animator anim;
	
	public Transform wallGrabPoint;
	private bool canGrab, isGrabbing;
	private float gravityStore;
	public float wallJumpTime = .1f;
	private float wallJumpCounter;
	public LayerMask whatIsGrabbable;
	
	private float dashDistance;
	private bool isDashing;
	float doubleTapTime;
	KeyCode lastKeyCode;
	
	public SpriteRenderer SpriteRenderer;
	public Sprite Standing;
	public Sprite Crouching;
	public BoxCollider2D Collider;
	public Vector2 StandingSize;
	public Vector2 CrouchingSize;
	

    // Start is called before the first frame update
    void Start()
    {
        gravityStore = theRB.gravityScale;
		// Crouch stuff sets to stand on start of game
		Collider.size = StandingSize;
		SpriteRenderer.sprite = Standing;
		standingSize = Collider.size;
    }

    // Update is called once per frame
    void Update()
    {
        if(wallJumpCounter <=0)
		{
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, theRB.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatisGround);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }
		
		if (input.GetKey (KeyCode.LeftShift))
			theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed * Time.deltaTime, theRB.velocity.y);
			isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatisGround);
		else
			theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, theRB.velocity.y);

        //flip direction
        if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1, 1f);
        }
		
		// Handle Crouching
		
		if (Input.GetKeyDown(KeyCode.C)) {
			spriteRenderer.sprite = Crouching;
			Collider.size = CrouchingSize;

		}

		if (Input.GetKeyUp(KeyCode.C)) {
		spriteRenderer.sprite = Standing;
		Collider.size = StandingSize;

		}		
		
		// handle Dashing
		
		//Dash Left can also add && !isGrounded so only dash will work in air
	if (Input.GetKeyDown(KeyCode.A)){
		if (doubleTapTime > Time.time && lastKeyCode == KeyCode.A){
		// Dash Left
		StartCoroutine(Dash(-1f));
		
		} else {
		
			doubleTapTime = Time.time + 0.5f;
		}
		lastKeyCode = KeyCode.A;
	}
	
	
		//Dash Right
	if (Input.GetKeyDown(KeyCode.D)){
		if (doubleTapTime > Time.time && lastKeyCode == KeyCode.A){
		// Dash Right
		StartCoroutine(Dash(1f));
		
		} else {
		
			doubleTapTime = Time.time + 0.5f;
		}
		lastKeyCode = KeyCode.D;
	}
	
	IEnumerator Dash (float direction) {
	isDashing = true;
	theRB.veloctity = new Vector2(theRB.veloctity.x, 0f);
	theRB.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
	float gravity = theRB.gravityScale
	theRB.gravityScale = 0;
	yield return WaitforSeconds(0.4f);
	isDashing = false;
	theRB.gravityScale = gravity;
	
	}
	
	// end dashing code
        
	// handle wall jumping
		
		canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, whatisGround);
		isGrabbing = false;
		
		if(canGrab && !isGrounded)
		{
			if((transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0) || (transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0))
			
			{
				isGrabbing = true;

			}
					
		}
		
		if(isGrabbing)
		{
			theRB.gravityScale = 0f;
			theRB.velocity = vector2.zero; //x and y set to 0
			
			if(Input.GetButtonDown("Jump"))
			{
				
			wallJumpCounter = wallJumpTime;
			theRB.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, jumpForce);
			theRB.gravityScale  = gravityStore;
			isGrabbing = false;
			}	
			
			
		} else
			
		{ 
		
		theRB.gravityScale = gravityStore;
		
		} 
		
		}
		else 
		{ 
		wallJumpCounter -= Time.deltaTime;
		}
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
		anime.SetBool("isGrabbing", isGrabbing);
    }
}