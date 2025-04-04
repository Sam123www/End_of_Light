using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject UIPrefab;
    public int checkPointId;
    public bool firstIntoScene;
    public float player_fullHP, player_fullLight;
    public Vector3 playerPosition;
    void Awake()
    {
        firstIntoScene = true;
        Time.timeScale = 1;
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            UIPrefab = Instantiate(UIPrefab);
            DontDestroyOnLoad(UIPrefab);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        AdjustAspect();
    }
    public void AdjustAspect()
    {
        float targetAspect = 16f / 9f;
        float currentAspect = (float) Screen.width / Screen.height;
        if (Mathf.Abs(currentAspect - targetAspect) > 0.01f && Screen.fullScreen == false){
            if (currentAspect > targetAspect)
            {
                int newWidth = Mathf.RoundToInt(Screen.height * targetAspect);
                Screen.SetResolution(newWidth, Screen.height, false);
            }
            else
            {
                int newHeight = Mathf.RoundToInt(Screen.width / targetAspect);
                Screen.SetResolution(Screen.width, newHeight, false);
            }
        }
    }
    public void Loading(string nextScene)
    {
        if(nextScene == SceneManager.GetActiveScene().name)
        {
            firstIntoScene = false;
        }
        else
        {
            firstIntoScene = true;
        }
        UIPrefab.SendMessage("Enable", nextScene);
    }
}
