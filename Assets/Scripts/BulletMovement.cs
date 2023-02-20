using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float speedBullet = 10f;
    [SerializeField] float speedRotation = 2f;
    [SerializeField] ParticleSystem particles;
    float directionBullet;
    SpriteRenderer sprite;
    Rigidbody2D rbody;
    float movement;
    
    void Awake() {
        rbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        directionBullet = transform.parent.localScale.x;
        transform.parent = null;
    }

    void Start()
    {
        
    }

    void Update()
    {
        movement = Mathf.Sign(directionBullet);
        rbody.velocity = new Vector2(movement * speedBullet, 0f);
        rbody.rotation -= speedRotation * directionBullet;
        //rbody.AddForce(transform.up * 5f);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ground") {
            particles.Play();
            sprite.enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }
}
