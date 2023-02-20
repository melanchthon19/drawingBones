using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject howToPlay;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject endMsg;
    GameSession gameSession;
    AudioSource audioSource;
    [SerializeField] AudioClip exitSound;
    public bool hasEnded = false;

    void Awake() {
        gameSession = FindObjectOfType<GameSession>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (gameSession == null) {
            gameSession = FindObjectOfType<GameSession>();
        }
        if (gameSession.hasEnded) {
            endMsg.SetActive(true);
            gameSession.hasEnded = false;
        } else {
            gameSession.gameObject.SetActive(false);
        }
    }
    public void ExitGame() {
        Application.Quit();
    }

    public void LoadFirstLevel() {
        endMsg.SetActive(false);
        audioSource.PlayOneShot(exitSound, 1);
        gameSession.gameObject.SetActive(true);
        SceneManager.LoadScene("Level0");
    }

    public void ShowHowToPlay() {
        endMsg.SetActive(false);
        credits.SetActive(false);
        if (howToPlay.activeSelf == false)
            howToPlay.SetActive(true);
        else {
            howToPlay.SetActive(false);
        }
    }  

    public void ShowCredits() {
        endMsg.SetActive(false);
        howToPlay.SetActive(false);
        if (credits.activeSelf == false)
            credits.SetActive(true);
        else {
            credits.SetActive(false);
        }
    }
}
