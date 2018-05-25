using UnityEngine;
using System.Collections;
/*
 *based on Morgan Wagnon's 2D control 
 * 
 * 
 * 
 */

public class dasPlatformCharacter : MonoBehaviour {
    [SerializeField]public float maxSpeed =2.8f;                      // The fastest the player can travel in the x axis.
    [SerializeField]public float maxClimbSpeed = 10f;                // The fastest the player can travel in the y axis.
    public bool facingRight = true;                  // For determining which way the player is currently facing.
    //[HideInInspector]
    [SerializeField]public float jumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField]private bool airControl = true;                  // Whether or not a player can steer while jumping;
    [SerializeField]private LayerMask whatIsGround;                  // A mask determining what is ground to the character

    const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    const float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up

    private Transform groundCheck;    // A position marking where to check if the player is grounded.
    private bool grounded;            // Whether or not the player is grounded.
    private Animator anim;            // Reference to the player's animator component.
    private Rigidbody thergdbody;
    private bool climb;
    public bool inair = false;
    [HideInInspector]public Vector3 respawnPoint;                        //Location of respawn when dead
    [HideInInspector]public float platformSpeedX = 0f;                   //Float to hold moving platform speed
    [HideInInspector]public bool onMovingPlatform = false;               //Track if on moving platform
    [HideInInspector]public float speedMultiplier = 1.0f;                //Allows powerups to modify speed
    [HideInInspector]public float jumpMultiplier = 1.0f;                 //Allows powerups to modify jump height
    [HideInInspector]public float gravityJumpMultiplier = 1.0f;          //Used by Gravity Inverter Controller to inverse gravity
    //private SphereCollider myCollider = this.gameObject.GetComponent<SphereCollider>();

    //
    public float distanceToGround = 0.0f;

    private void Awake()
    {
        // Setting up links to needed components
        groundCheck = transform.Find("GroundCheck");
        //anim = GetComponent<Animator>();
        thergdbody = GetComponent<Rigidbody>();
        //respawnPoint = this.gameObject.transform.position;

        //Make sure powerup values and gravity multipliers are set to 1.0 at beginning of level
        jumpMultiplier = 1.0f;
        speedMultiplier = 1.0f;
        gravityJumpMultiplier = 1.0f;
        
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("IsGrounded="+grounded);
        //Debug.Log("IsInAir="+airControl);
        //grounded = groundedCheck();
    }

    private void FixedUpdate()
    {
        //grounded = false;
        climb = false;
        if (thergdbody.velocity.y <= 0)
        {
            Ray myray = new Ray(this.gameObject.transform.position, Vector3.down);//define a downward ray

            int daslayerMask = 1 << 12 | 1 << 11;
            //daslayerMask = ~daslayerMask; //this is for except foreground and FG_Ground
            grounded= Physics.Raycast(myray, 1.0f, daslayerMask);
            if (grounded)
                { inair = false; }
            else
            {
                inair = true;
            }
            /*
            Collider[] colliders = myCollider.Raycast(myray, groundCheck.position, groundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    grounded = true;
            }
            */
            //broken code above
        }
        else
        {
            inair = true;
        }

    }
    public void Move(float move, bool jump)
    {
        Debug.Log("CharacterMoveFired");

        //only control the player if grounded or airControl is turned on
        if (grounded || airControl)
        {
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            //anim.SetFloat("hSpeed", Mathf.Abs(move));

            // Move the character
            thergdbody.velocity = new Vector3((move * maxSpeed * speedMultiplier) + platformSpeedX,thergdbody.velocity.y,0.0f);
            
            //add platform speed to "base" on moving platform


            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && facingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        // If the player should jump...
        if (grounded && jump && !inair)
        {
            // Add a vertical force to the player.
            grounded = false;
            //anim.SetBool("ground", false);
            thergdbody.AddForce(new Vector3(0f, jumpForce * jumpMultiplier * gravityJumpMultiplier,0.0f));
            inair = true;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter(Collider coll)
    {

            
            Debug.Log("I collide with"+coll);

    }

    bool groundedCheck()
    {
        if (thergdbody.velocity.y<0)
        {
            Ray myray = new Ray(this.gameObject.transform.position, Vector3.down);//define a downward ray

            int daslayerMask = 1 << 12 | 1 << 13;
            //daslayerMask = ~daslayerMask; //this is for except foreground and FG_Ground
            return Physics.Raycast(myray, 0.1f, daslayerMask);
            /*
            Collider[] colliders = myCollider.Raycast(myray, groundCheck.position, groundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    grounded = true;
            }
            */
            //broken code above
        }
        return false;
    }

}
