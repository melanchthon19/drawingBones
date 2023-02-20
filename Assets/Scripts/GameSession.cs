using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] Image[] heartImages;
    [SerializeField] Image key;
    Image heart;
    [SerializeField] int waitToLoad = 3;
    [SerializeField] int playerLives = 3;
    int currentSceneIndex;
    PlayerFire player;
    public bool hasEnded = false;

    void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update() {
        if (player == null) {
            player = FindObjectOfType<PlayerFire>();
        }
        if (hasEnded) {
            FindObjectOfType<MainMenu>().hasEnded = true;
        }
        
        HighlightKey();
    }

    public void ProcessPlayerDeath() {
        TakePlayerLife();
        if (playerLives >= 1) {
            StartCoroutine(LoadSameScene());
        } else {
            ResetGameSession();
        }
    }

    void TakePlayerLife() {
        playerLives--;
        heart = heartImages[playerLives];
        heart.color = new Color(0,0,0,100);
        key.color = new Color(0,0,0,100);
    }

    public void HighlightKey() {
        if (player.hasKey) {
            key.color = new Color(255,255,255,255);
        } else {
            key.color = new Color(0,0,0,100);
        }
    }
        
    void ResetGameSession() {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadSameScene() {
        yield return new WaitForSecondsRealtime(waitToLoad);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator LoadGame() {
        yield return new WaitForSecondsRealtime(waitToLoad);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
