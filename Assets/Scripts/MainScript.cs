using System;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Subsystems;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public GameObject playerPrefab;
    private Rigidbody playerRb;
    [NonSerialized] public Transform target;
    public static float moveSpeed = 10f;
    public static int playerHealth;
    private int currentHp;

    public GameObject plane;
    [NonSerialized] public float planeLeft;
    [NonSerialized] public float planeRight;
    [NonSerialized] public float planeTop;
    [NonSerialized] public float planeBottom;

    private bool gameEnabled;
    private bool isPaused = false;

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
    public TextMeshProUGUI hpText;

    public GameObject damagePrefab;
    public GameObject bonusPrefab;
    public GameObject notePrefab;

    private string filePath;


    void Start()
    {
        Debug.Log("Module started");

        MainText.gameObject.SetActive(true);
        StartContinueButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        NotesListButton.gameObject.SetActive(true);
        
        StartContinueButton.interactable = true;
        StartContinueButton.onClick.AddListener(StarButtonOnClick);

        ExitButton.interactable = true;
        ExitButton.onClick.AddListener(ExitButtonOnClick);

        CloseNoteButton.interactable = true;
        CloseNoteButton.onClick.AddListener(CloseNoteButtonOnClick);

        NotesListButton.interactable = true;
        NotesListButton.onClick.AddListener(NotesListButtonOnClick);

        Vector3 planeSize = plane.transform.localScale * 10;
        Vector3 planeCenter = plane.transform.position;
        planeLeft = planeCenter.x - (planeSize.x / 2);
        planeRight = planeCenter.x + (planeSize.x / 2);
        planeTop = planeCenter.z + (planeSize.z / 2);
        planeBottom = planeCenter.z - (planeSize.z / 2);

        GameObject player = Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
        target = player.GetComponent<Transform>();
        playerRb = player.GetComponent<Rigidbody>();

        filePath = Path.Combine(Application.persistentDataPath, "Notes");

        try {
            File.WriteAllText(filePath, "");
        } catch (IOException ex) {
            Debug.LogError($"Error ocured during saving file: {ex.Message}");
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pause_game();
        }

        if (gameEnabled & playerHealth <= 0) {
            stop_game();
        }

        if (currentHp != playerHealth) {
            hpText.text = $"hp: {playerHealth}";
            currentHp = playerHealth;
        }
 
        if (playerRb.position.x < planeLeft) {
            playerRb.position = new Vector3(planeRight, playerRb.position.y, playerRb.position.z);
        }

        if (playerRb.position.x > planeRight) {
            playerRb.position = new Vector3(planeLeft, playerRb.position.y, playerRb.position.z);
        }

        if (playerRb.position.z < planeBottom) {
            playerRb.position = new Vector3(playerRb.position.x, playerRb.position.y, planeTop);
        }

        if (playerRb.position.z > planeTop) {
            playerRb.position = new Vector3(playerRb.position.x, playerRb.position.y, planeBottom);
        }

    }

    void FixedUpdate() {

        if (gameEnabled) {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.fixedDeltaTime;

        playerRb.MovePosition(playerRb.position + movement);
        }

    }

    void pause_game() {
        gameEnabled = false;
        isPaused = true;
        MainText.gameObject.SetActive(true);
        StartContinueButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        NotesListButton.gameObject.SetActive(true);
    }

    void stop_game() {
        gameEnabled = false;
        hpText.gameObject.SetActive(false);
        MainText.gameObject.SetActive(true);
        StartContinueButton.gameObject.SetActive(true);
        ExitButton.gameObject.SetActive(true);
        NotesListButton.gameObject.SetActive(true);
    }
    void StarButtonOnClick() {
        playerHealth = 10;
        currentHp = playerHealth;
        hpText.gameObject.SetActive(true);
        hpText.text = $"hp: {playerHealth}";
        gameEnabled = true;
        MainText.gameObject.SetActive(false);
        StartContinueButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        NotesListButton.gameObject.SetActive(false);
        if (!isPaused) {
            CreateObjects.ClearAll();
            CreateObjects.StartSpawn(damagePrefab, bonusPrefab, notePrefab);
            isPaused = false;
        }
    }

    void ExitButtonOnClick() {
        Debug.Log("Module finished");
        Application.Quit();
    }
    
    public void ShowAppendNote() {
        string[] notesExampleList = {
            "Урон - изменяет здоровье персонажа",
            "Бонус - временно ускоряет передвижение игрока (на 3-5 секунд).",
            "Записка - текстовая записка которая открывается на половину экрана при получении" };
        
        gameEnabled = false;
        isPaused = true;

        NoteTextPlane.gameObject.SetActive(true);
        NoteText.gameObject.SetActive(true);
        CloseNoteButton.gameObject.SetActive(true);
        hpText.gameObject.SetActive(false);
        
        string text = notesExampleList[UnityEngine.Random.Range(0, 2)];
        NoteText.text = text;

        try {
            File.AppendAllText(filePath, text + "\n");
        } catch (IOException ex) {
            Debug.LogError($"Error ocured during adding to file: {ex.Message}");
        }
    }

    public void CloseNoteButtonOnClick () {
        NoteTextPlane.gameObject.SetActive(false);
        NoteText.gameObject.SetActive(false);
        CloseNoteButton.gameObject.SetActive(false);
        hpText.gameObject.SetActive(true);

        gameEnabled = true;
        isPaused = false;
    }

    public void NotesListButtonOnClick()
    {
        bool isNotesListVisible = NotesListPlane.gameObject.activeSelf;

        if (!isNotesListVisible)
        {
            NotesListPlane.gameObject.SetActive(true);
            NotesListText.gameObject.SetActive(true);
            hpText.gameObject.SetActive(false);

            try
            {
                NotesListText.text = File.ReadAllText(filePath);
            }
            catch (IOException ex)
            {
                Debug.LogError($"Error occurred while reading file: {ex.Message}");
            }
        }
        else
        {
            NotesListPlane.gameObject.SetActive(false);
            NotesListText.gameObject.SetActive(false);
            hpText.gameObject.SetActive(true);
        }
    }

}