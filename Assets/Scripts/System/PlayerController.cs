using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public bool jump = false;

    public float speed = 2f;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public LayerMask groundLayer;
    public float WaitTimeToRejump = 0.3f;

    private bool grounded = false;
    private bool jumpable;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        jumpable = true;
    }

    void Update()
    {
        bool groundedLastFrame = grounded;
        grounded = (Physics2D.Raycast(transform.position, Vector2.down, 1.0f, groundLayer).collider != null) ? true : false;

        if (groundedLastFrame != grounded && grounded == true)
            StartCoroutine(WaitBeforeJumpable());

        if (grounded && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFalling"))
            anim.SetTrigger("TouchGround");
        else
            anim.ResetTrigger("TouchGround");

        if (Input.GetButtonDown("Jump") && grounded && jumpable)
            jump = true;
    }

    private IEnumerator WaitBeforeJumpable()
    {
        yield return new WaitForSeconds(WaitTimeToRejump);
        jumpable = true;
    }

    void FixedUpdate()
    {
        anim.SetFloat("Speed", Mathf.Abs(speed));
        anim.SetFloat("VelocityFalling", GetComponent<Rigidbody2D>().velocity.y);

        if (GetComponent<Rigidbody2D>().velocity.y < 0)
            jumpable = false;

        if (speed * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if(jump)
        {
            anim.SetTrigger("Jump");
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            jump = false;
            jumpable = false;
        }
    }
}
