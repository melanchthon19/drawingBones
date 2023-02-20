using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    AudioSource audioSource;
    [SerializeField] AudioClip bulletSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Instantiate(bullet, this.transform.position, transform.rotation, this.transform.parent);
            audioSource.PlayOneShot(bulletSound, 1);
        }
    }
}
