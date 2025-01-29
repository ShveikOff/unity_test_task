using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FileManager fileManager;

    public Canvas canvas;
    public TextMeshProUGUI MainText;
    public Button StartContinueButton;
    public Button ExitButton;
    public Image NotesListPlane;
    public TextMeshProUGUI NotesListText;
    public Button NotesListButton;
    public Image NoteTextPlane;
    public TextMeshProUGUI NoteText;
    public Button CloseNoteButton;
    public TextMeshProUGUI healthText;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameManager.gameEnabled) {
                PauseGame();
            } else {
                StopGame();
            }
        }
    }

    public void ShowMainMenu(){

        MainText.gameObject.SetActive(true);
        StartContinueButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        NotesListButton.gameObject.SetActive(true);

        TextMeshProUGUI  StartContinueButtonText = StartContinueButton.GetComponentInChildren<TextMeshProUGUI>();
        if (gameManager.gamePaused) {
            StartContinueButtonText.text = "Continue";
        } else {
            StartContinueButtonText.text = "Start";
        }
        
        StartContinueButton.interactable = true;
        StartContinueButton.onClick.AddListener(StartButtonOnClick);

        ExitButton.interactable = true;
        ExitButton.onClick.AddListener(ExitButtonOnClick);

        NotesListButton.interactable = true;
        NotesListButton.onClick.AddListener(NotesListButtonOnClick);

    }

    public void HideMainMenu() {
        MainText.gameObject.SetActive(false);
        StartContinueButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        NotesListButton.gameObject.SetActive(false);
        
        StartContinueButton.interactable = false;
        StartContinueButton.onClick.RemoveAllListeners();

        ExitButton.interactable = false;
        ExitButton.onClick.RemoveAllListeners();

        NotesListButton.interactable = false;
        NotesListButton.onClick.RemoveAllListeners();
    }

    public void ShowHealthBar() {
        healthText.gameObject.SetActive(true);
    }

    public void HideHealthBar() {
        healthText.gameObject.SetActive(false);
    }

    public void ShowNote(string text) {
        gameManager.PauseGame();
        NoteTextPlane.gameObject.SetActive(true);
        NoteText.gameObject.SetActive(true);
        CloseNoteButton.gameObject.SetActive(true);
        CloseNoteButton.interactable = true;
        CloseNoteButton.onClick.AddListener(CloseNoteButtonOnClick);
        HideHealthBar();
        NoteText.text = text;
    }

    public void HideNote() {
        gameManager.UnPauseGame();
        NoteTextPlane.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        CloseNoteButton.gameObject.SetActive(false);
        CloseNoteButton.interactable = false;
        CloseNoteButton.onClick.RemoveAllListeners();
        ShowHealthBar();
    }

    public void ShowNotesList(string[] notes) {
        NotesListPlane.gameObject.SetActive(true);
        NotesListText.gameObject.SetActive(true);
        HideHealthBar();
        NotesListText.text = string.Join("\n", notes);
    }

    public void HideNotesList() {
        NotesListPlane.gameObject.SetActive(false);
        NotesListText.gameObject.SetActive(false);
        ShowHealthBar();
    }

    public void UpdateHealthUI(int health)
    {
        healthText.text = $"HP: {health}";
    }

    public void StartButtonOnClick() {
        HideMainMenu();
        ShowHealthBar();
        UpdateHealthUI(gameManager.playerHealth);
        if (gameManager.gamePaused) {
            gameManager.UnPauseGame();
        } else {
            gameManager.StartGame();
        }
    }

    public void PauseGame() {
        gameManager.PauseGame();
        ShowMainMenu();
    }

    public void UnPauseGame() {
        gameManager.UnPauseGame();
        HideMainMenu();
    }

    public void StopGame() {
        gameManager.StopGame();
        HideHealthBar();
        ShowMainMenu();
    }

    public void ExitButtonOnClick() {
        gameManager.ExitGame();
    }

    public void CloseNoteButtonOnClick () {
        HideNote();
    }

    public void NotesListButtonOnClick() {
        bool isNotesListVisible = NotesListPlane.gameObject.activeSelf;

        if (!isNotesListVisible) {
            ShowNotesList(fileManager.ReadTextFromFile());
        } else {
            HideNotesList();
        }
    }
}