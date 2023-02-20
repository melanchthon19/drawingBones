using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] float speedMove = 10f;
    float directionMove = 1;
    Rigidbody2D rbody;
    Animator animator;
    CapsuleCollider2D bodyColl;
    AudioSource audioSource;
    [SerializeField] AudioClip dyingSound;
    
    void Awake() {
        animator = GetComponent<Animator>(); 
        rbody = GetComponent<Rigidbody2D>();
        bodyColl = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Bullet") {
            animator.Play("Dying-Skeleton");
            audioSource.PlayOneShot(dyingSound, 1);
            Destroy(gameObject.GetComponent<CapsuleCollider2D>());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject, 0.5f);
        }
        
    }

    void Move() {
        transform.localScale = new Vector2(Mathf.Sign(rbody.velocity.x), 1f);
        rbody.velocity = new Vector2(directionMove * speedMove, rbody.velocity.y);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Ground") {
            directionMove *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
    }
}
