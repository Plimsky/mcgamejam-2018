using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool jump = false;
    [HideInInspector] public bool dash = false;

    private bool isDashing = false;
    public bool isDead = false; 

    public float fallMultiplier = 2.5f; 
    public float lowJumpMultiplier = 2f; 

    public float speed = 2f;
    public float dashSpeed = 4f;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public float dashFall = 30f;
    public LayerMask groundLayer;
    public float WaitTimeToRejump = 0.1f;
    public float WaitTimeToReDash = 0.01f;
    public float DashTime = 0.05f;

    public GameObject SpriteJumpSmoke;
    public GameObject ParticleToSpawnOnJump;
    public Transform SpawnTransform;
    public AudioClip jumpNoise;
    public AudioClip dashNoise2;

    public GameObject ghostSprite;
    IEnumerator ghostCoroutineAir; 
    public float ghostAirRate = 0.05f; 
    public float ghostDashRate = 0.01f; 

    //public AudioClip walkNoise;

    private bool grounded = false;
    private bool jumpable;
    private bool slowDownDash = false;
    private Animator anim;
    private Rigidbody2D rb;
    private AudioSource source;

  

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        jumpable = true;
 
        ghostCoroutineAir = SpawnGhost(ghostAirRate); 
    }

    void Update()
    {
        
        bool groundedLastFrame = grounded;
        grounded = (Physics2D.Raycast(transform.position, Vector2.down, 1.0f, groundLayer).collider != null)
            ? true
            : false;

        if (groundedLastFrame != grounded && grounded)
            StartCoroutine(WaitBeforeJumpable());

        if (groundedLastFrame && !grounded) {
            StartCoroutine(ghostCoroutineAir); 
        }

        if (!groundedLastFrame && grounded) {
              Debug.Log("GC2 stopped"); 
              StopCoroutine(ghostCoroutineAir); 
        }

        //if (grounded && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFalling"))
        if(!groundedLastFrame)
        {
            if (grounded) {
               
                anim.SetTrigger("TouchGround");
            }
        } 
        if(!grounded) {
            anim.ResetTrigger("TouchGround");
        } else {
             anim.SetTrigger("TouchGround");
        }

        if (Input.GetButtonDown("Jump") && grounded && jumpable && !(Input.GetAxis("Vertical") < 0))
        {
            jump = true;
        }
            
        if (Input.GetButtonDown("Jump") && (Input.GetAxis("Vertical") < 0))
        {
            if (!isDead && !isDashing && !dash) {
                dash = true;
                anim.SetTrigger("Dash"); 
            }
        }

        if (rb.velocity.y < 0) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y >0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        anim.SetFloat("Speed", Mathf.Abs(speed));
        anim.SetFloat("VelocityFalling", rb.velocity.y);

        if (!isDashing)
        {
            if (speed * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
                transform.Translate(Vector3.right * Time.deltaTime * speed);

            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        if (dash && !isDashing)
        {
            source.Pause();
            source.PlayOneShot(dashNoise2);
            StartCoroutine(Dash(DashTime));
        }


        if (slowDownDash)
        {
            rb.AddForce(Vector2.down * dashFall * Time.deltaTime, ForceMode2D.Force);
            if(Mathf.Abs(rb.velocity.x) < speed)
            {
                slowDownDash = false;
            }
        }

        if (jump)
        {
            Instantiate(ParticleToSpawnOnJump, SpawnTransform.position, Quaternion.identity);
            Instantiate(SpriteJumpSmoke, SpawnTransform.position, Quaternion.identity);
            anim.SetTrigger("Jump");
            source.Pause();
            source.PlayOneShot(jumpNoise);
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
            jumpable = false;
        }
    }

    private IEnumerator Dash(float dashDur)
    {
        float time = 0;
        isDashing = true;

        IEnumerator ghostCoroutine = SpawnGhost(ghostDashRate);
        StartCoroutine(ghostCoroutine); 
        while (dashDur > time)
        {
            time += Time.deltaTime;
            rb.velocity = new Vector2(dashSpeed, 0);
            //GameObject ghost = Instantiate(ghostSprite, transform.position, transform.rotation); 
            yield return null;
        }
        StartCoroutine(WaitBeforeDash());
      
        StopCoroutine(ghostCoroutine); 
        slowDownDash = true;
    }

    private IEnumerator WaitBeforeJumpable()
    {
        yield return new WaitForSeconds(WaitTimeToRejump);
        jumpable = true;
    }

    private IEnumerator WaitBeforeDash()
    {
        yield return new WaitForSeconds(WaitTimeToReDash);
        isDashing = false;
        dash = false;
    }

    private IEnumerator SpawnGhost(float spawnDelay)  
    {
        while (true) {
            GameObject ghost = Instantiate(ghostSprite, transform.position, transform.rotation); 
            yield return new WaitForSeconds(spawnDelay); 
        }
    }

    void playerDies() {
        LevelManager.Instance.PlayerDied();
    }


}