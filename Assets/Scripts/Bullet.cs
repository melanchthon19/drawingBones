using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speedBullet = 1f;
    [SerializeField] float speedRotation = 2f;
    [SerializeField] ParticleSystem particles;
    float directionBullet;
    Rigidbody2D rbody;
    PlayerMovement playerMov;
    SpriteRenderer sprite;
    
    void Awake() {
        rbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerMov = FindObjectOfType<PlayerMovement>();
        directionBullet = playerMov.transform.localScale.x;
    }
    void Start()
    {
        
    }

    void Update()
    {
        rbody.velocity = new Vector2(directionBullet * speedBullet, 0f);
        rbody.rotation -= directionBullet * speedRotation;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            particles.Play();
            sprite.enabled = false;
            Destroy(gameObject, 0.5f);
        } else {
            Destroy(gameObject);
        }
    }
}
