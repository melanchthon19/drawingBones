using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] bool hasWeapon = false;
    [SerializeField] public bool hasKey = false;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gunGO;
    [SerializeField] GameObject exit;
    [SerializeField] AudioClip bulletSound;
    [SerializeField] AudioClip weaponSound;
    [SerializeField] AudioClip keySound;
    AudioSource audioSource;
    GameSession gameSession;
    Transform gunTransform;
    SpriteRenderer gunSprite;

    void Awake() {
        gunSprite = gunGO.GetComponent<SpriteRenderer>();
        gunTransform = gunGO.GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        //CheckWeapon();
    }

    void OnFire(InputValue value) {
        if (hasWeapon) {
            Instantiate(bullet, gunTransform.position, transform.rotation, this.transform);
            audioSource.PlayOneShot(bulletSound, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon") {
            hasWeapon = true;
            gunSprite.enabled = true;
            audioSource.PlayOneShot(weaponSound, 1);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Key") {
            hasKey = true;
            //gameSession.HighlightKey();
            ShowExit();
            Destroy(other.gameObject);
            audioSource.PlayOneShot(keySound, 1);
            //other.gameObject.SetActive(false);
        }
    }

    void ShowExit() {
        exit.SetActive(true);
    }

}
