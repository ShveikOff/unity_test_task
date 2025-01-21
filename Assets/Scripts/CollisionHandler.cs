using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Damage(Clone)")
        {
            int xCoord = Mathf.RoundToInt(other.transform.position.x);
            int zCoord = Mathf.RoundToInt(other.transform.position.z);
            string coord = $"{xCoord},{zCoord}";

            GameManager.Instance.ModifyHealth(-1);
            Destroy(other.gameObject);

            ObjectSpawner.Instance.SpawnDamage();
            ObjectSpawner.Instance.ClearCoord(coord);
        }
            if (other.gameObject.name == "Bonus(Clone)")
        {
            int xCoord = Mathf.RoundToInt(other.transform.position.x);
            int zCoord = Mathf.RoundToInt(other.transform.position.z);
            string coord = $"{xCoord},{zCoord}";

            Destroy(other.gameObject);
            GameManager.Instance.ModifySpeed(1.5f, 5f);

            ObjectSpawner.Instance.SpawnBonus();
            ObjectSpawner.Instance.ClearCoord(coord);
        }
            if (other.gameObject.name == "Note(Clone)")
        {
            int xCoord = Mathf.RoundToInt(other.transform.position.x);
            int zCoord = Mathf.RoundToInt(other.transform.position.z);
            string coord = $"{xCoord},{zCoord}";

            Destroy(other.gameObject);
            
            ObjectSpawner.Instance.SpawnNote();
            ObjectSpawner.Instance.ClearCoord(coord);

            string[] notesExampleList = {
                "Урон - изменяет здоровье персонажа",
                "Бонус - временно ускоряет передвижение игрока (на 3-5 секунд).",
                "Записка - текстовая записка которая открывается на половину экрана при получении" };

            GameManager.Instance.PauseGame();
            string text = notesExampleList[Random.Range(0, 2)];
            UIManager.Instance.ShowNote(text);
            FileManager.Instance.AppendTextToFile(text);
        }
    }
}
