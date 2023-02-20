using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speedRun = 1f;
    [SerializeField] float speedLadder = 1f;
    [SerializeField] float speedJump = 1f;
    [SerializeField] ParticleSystem exitParticle;
    [SerializeField] AudioClip dyingSound;
    AudioSource audioSource;
    bool isAlive = true;
    Vector2 moveInput;
    bool isMoving = false;
    bool isClimbing = false;
    Rigidbody2D rbody;
    Animator animator;
    CapsuleCollider2D bodyColl;
    BoxCollider2D feetColl;
    float initialGravity;
    GameSession gameSession;

    void Awake() {
       rbody = GetComponent<Rigidbody2D>();
       initialGravity = rbody.gravityScale;
       animator = GetComponent<Animator>();
       bodyColl = GetComponent<CapsuleCollider2D>();
       feetColl = GetComponent<BoxCollider2D>();
       audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (gameSession == null) {
           gameSession = FindObjectOfType<GameSession>();
        }
        
        if (isAlive) {
            Run();
            ClimbLadder();
            FlipSprite();
            Die();
        }
    }

    void OnMove(InputValue value) {
        if (isAlive) {
            moveInput = value.Get<Vector2>();
        }
    }

    void OnJump(InputValue value) {
        if (value.isPressed && CanJump()) {
            rbody.velocity += new Vector2(0f, speedJump);
        }
    }

    bool CanJump() {
        if (isAlive && feetColl.IsTouchingLayers(LayerMask.GetMask("Ground", "Bounce"))) {
            return true;
        } else {
            return false;
        }
    }

    void Climb() {
        if (isClimbing) {
            rbody.gravityScale = 0;
            if (moveInput.y < Mathf.Epsilon) {
                animator.speed = 0;
            }
            else {
                animator.speed = 1;
                rbody.velocity = new Vector2(rbody.velocity.x , moveInput.y * speedLadder);
            }
        } else {
            rbody.gravityScale = 1;
        }
        animator.SetBool("isClimbing", isClimbing);
    }

    void Run() {
        animator.speed = 1;
        rbody.velocity = new Vector2(moveInput.x * speedRun, rbody.velocity.y);
        animator.SetBool("isRunning", isMoving);
    }

    void ClimbLadder() {
        if (bodyColl.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
            isClimbing = true;
            FreezeClimbAnimation();
            rbody.gravityScale = 0f;
            rbody.velocity = new Vector2(rbody.velocity.x , moveInput.y * speedLadder);
        } else {
            rbody.gravityScale = initialGravity;
            isClimbing = false;
        }
        animator.SetBool("isClimbing", isClimbing);
    }

    void FlipSprite() {
        isMoving = Mathf.Abs(rbody.velocity.x) > Mathf.Epsilon;
        if (isMoving) {
            transform.localScale = new Vector2(Mathf.Sign(rbody.velocity.x), 1f);
        }
    }

    void FreezeClimbAnimation() {
        if (Mathf.Abs(rbody.velocity.y) < Mathf.Epsilon) {
            animator.speed = 0;
        }
        else {
            animator.speed = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Exit") {
            exitParticle.Play();
        }
    }

    void Die() {
        if (rbody.IsTouchingLayers(LayerMask.GetMask("Enemies"))) {
            isAlive = false;
            audioSource.PlayOneShot(dyingSound, 2);
            animator.SetTrigger("isDying");
            gameSession.ProcessPlayerDeath();
        }
    }
}
