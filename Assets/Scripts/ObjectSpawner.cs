using System.IO.Compression;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner Instance;
    public GameObject damagePrefab;
    public GameObject bonusPrefab;
    public GameObject notePrefab;

    private Dictionary<string, int> coordDict = new Dictionary<string, int>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void StartSpawn () {

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

    public void ClearAll () {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Проверяем имя каждого объекта
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Damage(Clone)" || 
                obj.name == "Bonus(Clone)" || 
                obj.name == "Note(Clone)"){ 
            if (obj.name != "Plane") {
                Destroy(obj);
            }
        }
            coordDict.Clear();
        }
    }  

    public bool SpawnObject (GameObject objPrefab) {
        int xCoord = RandomNumber();
        int zCoord = RandomNumber();
        if (coordDict.ContainsKey($"{xCoord},{zCoord}")) {
            return false;
        } 
        Instantiate(objPrefab, new Vector3(xCoord, 0.5f, zCoord), Quaternion.identity);
        coordDict.Add($"{xCoord},{zCoord}", 0);
        return true;
    }

    public void SpawnDamage () {
        SpawnObject(damagePrefab);
    }

    public void SpawnBonus () {
        SpawnObject (bonusPrefab);
    }

    public void SpawnNote () {
        SpawnObject(notePrefab);
    }

    public void ClearCoord (string coord) {
        coordDict.Remove(coord);
    }
    
    private int RandomNumber() {
        return Random.Range(-9, 14);
    }
}
