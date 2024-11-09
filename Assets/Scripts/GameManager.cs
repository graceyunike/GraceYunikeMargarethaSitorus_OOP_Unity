using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LevelManager = GetComponentInChildren<LevelManager>();

        DontDestroyOnLoad(gameObject);
        DontDestroyObjectIfExists("Camera");
        DontDestroyObjectIfExists("Player");
        DontDestroyObjectIfExists("SceneTransition");
    }

    private void DontDestroyObjectIfExists(string objectName)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
