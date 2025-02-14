using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevelZone : MonoBehaviour
{
    public static ResetLevelZone instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider?.tag == "Player")
        {
            ResetLevel();
        }
    }

    public void ResetLevel()
    {
        Debug.Log("Перезапуск уровня");

        string indexCurrentLevel = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(indexCurrentLevel);
    }
}
