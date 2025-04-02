using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject UIPrefab;
    public int checkPointId;
    public float player_fullHP, player_fullLight;
    public Vector3 playerPosition;
    void Awake()
    {
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
    public void Loading(string nextScene)
    {
        UIPrefab.SendMessage("Enable", nextScene);
    }
}
