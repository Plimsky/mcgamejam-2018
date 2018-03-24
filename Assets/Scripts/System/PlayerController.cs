using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool dash = false;

    private bool isDashing = false;

    public float speed = 2f;
    public float dashSpeed = 4f;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public LayerMask groundLayer;

    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = (Physics2D.Raycast(transform.position, Vector2.down, 1.0f, groundLayer).collider != null) ? true : false;
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
        if (Input.GetButtonDown("Fire2") && grounded)
            dash = true;
    }

    void FixedUpdate()
    {
        anim.SetFloat("Speed", Mathf.Abs(speed));

        if (!isDashing)
        {
            if (speed * rb.velocity.x < maxSpeed)
                rb.AddForce(Vector2.right * speed * moveForce);

            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        if (dash)
        {
            Debug.Log("Dash!");
            StartCoroutine(Dash(0.2f));
        }

        if(jump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    IEnumerator Dash(float dashDur)
    {
        float time = 0;
        isDashing = true;
        dash = false;
        while(dashDur > time)
        {
            time += Time.deltaTime;
            rb.velocity = new Vector2(dashSpeed, 0);
            yield return 0;
        }
        isDashing = false;
    }
}
