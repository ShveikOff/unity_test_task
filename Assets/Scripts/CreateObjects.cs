using System.IO.Compression;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjects : MonoBehaviour
{

    public GameObject damagePrefab;
    public GameObject bonusPrefab;
    public GameObject notePrefab;

    private static Dictionary<string, int> coordDict = new Dictionary<string, int>();

    void Start()
    {
        StartSpawn (damagePrefab, bonusPrefab, notePrefab);
    }

    public static void StartSpawn (GameObject damagePrefab, GameObject bonusPrefab, GameObject notePrefab) {

        coordDict.Add("0,0", 0);
        coordDict.Add("1,0", 0);
        coordDict.Add("0,1", 0);
        coordDict.Add("1,1", 0);
        coordDict.Add("-1,0", 0);
        coordDict.Add("0,-1", 0);
        coordDict.Add("-1,1", 0);
        coordDict.Add("-1,-1", 0);
        coordDict.Add("1,-1", 0);

        for (int i = 0; i < 5; i++) { if (SpawnObject(damagePrefab)) {} else i--; }

        for (int i = 0; i < 5; i++) { if (SpawnObject(bonusPrefab)) {} else i--; }

        for (int i = 0; i < 5; i++) { if (SpawnObject(notePrefab)) {} else i--; }      

    }

    public static void ClearAll () {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Проверяем имя каждого объекта
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Damage(Clone)" || 
                obj.name == "Bonus(Clone)" || 
                obj.name == "Note(Clone)")
            {
                Destroy(obj); // Удаляем объект
            }
        }

        coordDict.Clear();

    }
    public static bool SpawnObject (GameObject objPrefab) {
        int xCoord = RandomNumber();
        int zCoord = RandomNumber();
        if (coordDict.ContainsKey($"{xCoord},{zCoord}")) {
            return false;
        } 
        Instantiate(objPrefab, new Vector3(xCoord, 0.5f, zCoord), Quaternion.identity);
        coordDict.Add($"{xCoord},{zCoord}", 0);
        return true;
    }

    public static void ClearCoord (string coord) {
        coordDict.Remove(coord);
    }
    

    private static int RandomNumber() {
        return Random.Range(-9, 14);
    }
}
