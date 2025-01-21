using UnityEngine;

/*
this class is responsible for managing the game state 
(pause, completion, player health, speed, etc.)
*/
public class GameManager : MonoBehaviour {
    
    public static GameManager Instance;

    public int playerHealth = 10;
    public float playerSpeed = 10f;
    public bool gameEnabled = false;
    public bool gamePaused = false;

    private void Awake() {
        Debug.Log("Module started");
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        UIManager.Instance.ShowMainMenu();
        ObjectSpawner.Instance.StartSpawn();
    }

    public delegate void GameStateChanged();
    public static event GameStateChanged OnGameStarted;
    public static event GameStateChanged OnGamePaused;
    public static event GameStateChanged OnGameStopped;
    public static event GameStateChanged OnGameUnPaused;

    public void StartGame() {
        Debug.Log("Game started");
        playerHealth = 10;
        ObjectSpawner.Instance.ClearAll();
        ObjectSpawner.Instance.StartSpawn();
        gameEnabled = true;
        gamePaused = false;

        OnGameStarted?.Invoke();
    }

    public void UnPauseGame() {
        Debug.Log("Game unpaused");
        gameEnabled = true;
        gamePaused = false;

        OnGameUnPaused?.Invoke();
    }

    public void PauseGame() {
        Debug.Log("Game paused");
        gameEnabled = false;
        gamePaused = true;

        OnGamePaused?.Invoke();
    }

    public void StopGame() {
        Debug.Log("Game Stoped");
        gameEnabled = false;
        gamePaused = false;

        OnGameStopped?.Invoke();
    }

    public void ExitGame() {
        Debug.Log("Application closed");
        Application.Quit();
    }

    public void ModifyHealth(int amount)
    {
        playerHealth += amount;
        UIManager.Instance.UpdateHealthUI(playerHealth);
        if (playerHealth <= 0)
        {
            StopGame();
        }
    }

    public void ModifySpeed(float multiplier, float duration) {
        playerSpeed *= multiplier;
        Invoke(nameof(ResetSpeed), duration);
    }

    private void ResetSpeed() {
        playerSpeed = 10f;
    }

}