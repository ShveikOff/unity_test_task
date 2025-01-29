using System.IO.Compression;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject damagePrefab;
    public GameObject bonusPrefab;
    public GameObject notePrefab;

    private Dictionary<string, int> coordDict = new Dictionary<string, int>();
    private List<GameObject> spawnedObjects = new List<GameObject>();

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
        foreach (GameObject obj in spawnedObjects) {
            if (obj != null) {
                Destroy(obj);
            }
        }
        coordDict.Clear();
        spawnedObjects.Clear();
    }  

    public bool SpawnObject (GameObject objPrefab) {
        int xCoord = RandomNumber();
        int zCoord = RandomNumber();
        string coordKey = $"{xCoord},{zCoord}";
        if (coordDict.ContainsKey(coordKey)) {
            return false;
        } 
        GameObject newObject = Instantiate(objPrefab, new Vector3(xCoord, 0.5f, zCoord), Quaternion.identity);
        spawnedObjects.Add(newObject);
        coordDict.Add(coordKey, 0);
        return true;
    }

    public void DestroySpawnedObject(GameObject obj) {
        if (spawnedObjects.Contains(obj)) {
            spawnedObjects.Remove(obj);
        }
        Destroy(obj);
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
