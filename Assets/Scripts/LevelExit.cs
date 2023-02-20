using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    GameSession gameSession;
    int currentSceneIndex;
    AudioSource audioSource;
    [SerializeField] AudioClip exitSound;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (gameSession == null) {
            gameSession = FindObjectOfType<GameSession>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            audioSource.PlayOneShot(exitSound, 1);
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(2);
        if (currentSceneIndex == 4) {
            SceneManager.LoadScene(0);
            gameSession.hasEnded = true;
        }
        else {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
