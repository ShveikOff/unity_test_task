using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ObjectSpawner objectSpawner;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private FileManager fileManager;

    void OnCollisionEnter(Collision other)
    {   

        if (other.gameObject.name == "Damage(Clone)")
        {
            int xCoord = Mathf.RoundToInt(other.transform.position.x);
            int zCoord = Mathf.RoundToInt(other.transform.position.z);
            string coord = $"{xCoord},{zCoord}";

            gameManager.ModifyHealth(-1);
            objectSpawner.DestroySpawnedObject(other.gameObject);

            objectSpawner.SpawnDamage();
            objectSpawner.ClearCoord(coord);
        }
            if (other.gameObject.name == "Bonus(Clone)")
        {
            int xCoord = Mathf.RoundToInt(other.transform.position.x);
            int zCoord = Mathf.RoundToInt(other.transform.position.z);
            string coord = $"{xCoord},{zCoord}";

            objectSpawner.DestroySpawnedObject(other.gameObject);
            gameManager.ModifySpeed(1.5f, 5f);

            objectSpawner.SpawnBonus();
            objectSpawner.ClearCoord(coord);
        }
            if (other.gameObject.name == "Note(Clone)")
        {
            int xCoord = Mathf.RoundToInt(other.transform.position.x);
            int zCoord = Mathf.RoundToInt(other.transform.position.z);
            string coord = $"{xCoord},{zCoord}";

            objectSpawner.DestroySpawnedObject(other.gameObject);
            
            objectSpawner.SpawnNote();
            objectSpawner.ClearCoord(coord);

            string[] notesExampleList = {
                "Урон - изменяет здоровье персонажа",
                "Бонус - временно ускоряет передвижение игрока (на 3-5 секунд).",
                "Записка - текстовая записка которая открывается на половину экрана при получении" };

            gameManager.PauseGame();
            string text = notesExampleList[Random.Range(0, 2)];
            uIManager.ShowNote(text);
            fileManager.AppendTextToFile(text);
        }
    }
}
