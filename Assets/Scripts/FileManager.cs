using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour {
    public static FileManager Instance;
    public string fileName = "Notes";
    private string FilePath => Path.Combine(Application.persistentDataPath, fileName);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

public void SaveTextToFile(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            Debug.LogWarning("Attempted to save empty or null text.");
            return;
        }

        try
        {
            File.WriteAllText(FilePath, text);
            Debug.Log($"File Saved: {FilePath}");
        }
        catch (IOException ex)
        {
            Debug.LogError($"Error during saving file: {ex.Message}");
        }
    }

    public void AppendTextToFile(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            Debug.LogWarning("Attempted to append empty or null text.");
            return;
        }

        try
        {
            File.AppendAllText(FilePath, text + "\n");
            Debug.Log($"Text added to file: {FilePath}");
        }
        catch (IOException ex)
        {
            Debug.LogError($"Error during adding to file: {ex.Message}");
        }
    }

    public string[] ReadTextFromFile()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                string[] lines = File.ReadAllLines(FilePath);
                Debug.Log($"File read successfully: {FilePath}");
                return lines;
            }
            else
            {
                Debug.LogWarning($"File not found: {FilePath}");
                return new string[0];
            }
        }
        catch (IOException ex)
        {
            Debug.LogError($"Error during reading file: {ex.Message}");
            return new string[0];
        }
    }

    public void ClearFile()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, string.Empty);
                Debug.Log($"File cleared: {FilePath}");
            }
            else
            {
                Debug.LogWarning($"File not found to clear: {FilePath}");
            }
        }
        catch (IOException ex)
        {
            Debug.LogError($"Error during clearing file: {ex.Message}");
        }
    }
}