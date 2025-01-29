using System;
using UnityEngine;

/*
this class is responsible for managing the game state 
(pause, completion, player health, speed, etc.)
*/
public class GameManager : MonoBehaviour {
    
    [SerializeField] private UIManager uIManager;
    [SerializeField] private ObjectSpawner objectSpawner;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private FileManager fileManager;

    [SerializeField] private GameObject player;

    public int playerHealth = 10;
    public float playerSpeed = 10f;
    public bool gameEnabled = false;
    public bool gamePaused = false;

    private void Start() {
        uIManager.ShowMainMenu();
        objectSpawner.StartSpawn();
    }

    public event Action OnGameStarted;
    public event Action OnGamePaused;
    public event Action OnGameStopped;
    public event Action OnGameUnPaused;

    public void StartGame() {
        Debug.Log("Game started");
        player.SetActive(true);
        playerController.ResetPosition();
        playerHealth = 10;
        objectSpawner.ClearAll();
        objectSpawner.StartSpawn();
        gameEnabled = true;
        gamePaused = false;

        OnGameStarted?.Invoke();
        uIManager.UpdateHealthUI(playerHealth);
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
        player.SetActive(false);

        OnGameStopped?.Invoke();
    }

    public void ExitGame() {
        Debug.Log("Application closed");
        Application.Quit();
    }

    public void ModifyHealth(int amount)
    {
        playerHealth += amount;
        uIManager.UpdateHealthUI(playerHealth);
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