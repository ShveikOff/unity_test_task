using UnityEngine;
using System.Collections;
using TMPro;
public class CollisionHandler : MonoBehaviour
{
    public GameObject damagePrefab;
    public GameObject bonusPrefab;
    public GameObject notePrefab;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Damage(Clone)")
        {
            int xCoord = Mathf.RoundToInt(other.transform.position.x);
            int zCoord = Mathf.RoundToInt(other.transform.position.z);
            string coord = $"{xCoord},{zCoord}";

            MainScript.playerHealth -= 1;
            Destroy(other.gameObject);

            CreateObjects.SpawnObject(damagePrefab);
            CreateObjects.ClearCoord(coord);
        }
            if (other.gameObject.name == "Bonus(Clone)")
        {
            int xCoord = Mathf.RoundToInt(other.transform.position.x);
            int zCoord = Mathf.RoundToInt(other.transform.position.z);
            string coord = $"{xCoord},{zCoord}";

            Destroy(other.gameObject);
            MainScript.moveSpeed *= 1.2f;

            StartCoroutine(ResetMoveSpeed(5f));

            CreateObjects.SpawnObject(bonusPrefab);
            CreateObjects.ClearCoord(coord);
        }
            if (other.gameObject.name == "Note(Clone)")
        {
            int xCoord = Mathf.RoundToInt(other.transform.position.x);
            int zCoord = Mathf.RoundToInt(other.transform.position.z);
            string coord = $"{xCoord},{zCoord}";

            Destroy(other.gameObject);
            
            CreateObjects.SpawnObject(notePrefab);
            CreateObjects.ClearCoord(coord);

            GameObject cameraObject = Camera.main.gameObject;
            MainScript mainScript = cameraObject.GetComponent<MainScript>();
            mainScript.ShowAppendNote();       
        }
    }

    private IEnumerator ResetMoveSpeed(float delay)
    {
        yield return new WaitForSeconds(delay);
        MainScript.moveSpeed = 10f;
    }

}
