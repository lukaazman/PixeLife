using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MojMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private bool isGrounded;
    private Animator animator;

    private bool facingRight = true;
    private bool wasGroundedLastFrame = true;

    private AudioSource JumpSound;
    private AudioSource WalkSound; // Private AudioSource for walking sound

    public float walkSoundStartDelay = 0.1f;
    private bool isReadyToPlayWalkSound = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        JumpSound = audioSources[0];
        WalkSound = audioSources[1]; // Assuming the second AudioSource is for walking
        WalkSound.loop = true;

        isGrounded = true;
        isReadyToPlayWalkSound = true;
    }

    void Update()
    {
        // Block player input if time is stopped (e.g., map is open)
        if (Time.timeScale == 0f)
        {
            return;  // Don't allow movement or jumping if time is paused
        }

        if (DialogueManager.GetInstance() != null && DialogueManager.GetInstance().dialogueIsPlaying)
        {
            moveVelocity = Vector2.zero;
            rb.velocity = new Vector2(0, rb.velocity.y);
            WalkSound.Stop();
            return;
        }

        float moveInput = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(moveInput, 0);
        moveVelocity = move * speed;

        if (Math.Abs(moveInput) > 0.1f && isGrounded)
        {
            if (isReadyToPlayWalkSound && !WalkSound.isPlaying)
            {
                WalkSound.Play();
            }
        }
        else
        {
            WalkSound.Stop();
            if (!isGrounded)
            {
                isReadyToPlayWalkSound = false;
            }
        }

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Jumping logic (blocked if time is stopped)
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            Jump();
        }

        animator.SetBool("isJumping", !isGrounded);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);

        wasGroundedLastFrame = isGrounded;
    }

    void FixedUpdate()
    {
        // Stop movement if time is paused
        if (Time.timeScale == 0f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        // Apply the movement velocity to the Rigidbody2D
        rb.velocity = new Vector2(moveVelocity.x, rb.velocity.y);
    }

    void Jump()
    {
        JumpSound.Play();
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            StartCoroutine(EnableWalkSoundAfterDelay(walkSoundStartDelay));
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            WalkSound.Stop();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    IEnumerator EnableWalkSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isGrounded && Math.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            isReadyToPlayWalkSound = true;
        }
    }

    // Public method to get the WalkSound AudioSource
    public AudioSource GetWalkSound()
    {
        return WalkSound;
    }

    // Public method to stop WalkSound manually
    public void StopWalkSound()
    {
        if (WalkSound.isPlaying)
        {
            WalkSound.Stop();
        }
    }

    // Public method to get the JumpSound AudioSource
    public AudioSource GetJumpSound()
    {
        return JumpSound;
    }

    // Public method to stop JumpSound manually
    public void StopJumpSound()
    {
        if (JumpSound.isPlaying)
        {
            JumpSound.Stop();
        }
    }
}
