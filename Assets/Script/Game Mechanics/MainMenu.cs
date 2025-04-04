using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class MainMenu : MonoBehaviour
{
    public float mouseSpeed;
    public GameObject lightMouse, Credits, OptionsPanel;
    public string playScene;
    Vector3 preMousePos;
    private void Update()
    {
        if (Vector2.Distance(preMousePos, Input.mousePosition) > 0.1f)
        {
            preMousePos = Input.mousePosition;
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            lightMouse.transform.position = newPos;
        }
        else
        {
            float hori = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            Vector2 newPos = new Vector2(lightMouse.transform.position.x + hori * Time.deltaTime * mouseSpeed
                , lightMouse.transform.position.y + vert * Time.deltaTime * mouseSpeed);
            newPos = Camera.main.WorldToViewportPoint(newPos);
            newPos.x = Mathf.Clamp(newPos.x, 0, 1);
            newPos.y = Mathf.Clamp(newPos.y, 0, 1);
            newPos = Camera.main.ViewportToWorldPoint(newPos);
            lightMouse.transform.position = newPos;
        }
    }
    public void PlayBotton()
    {
        AudioManager.PlayEnterGame();
        GameManager.instance.Loading(playScene);
    }
    public void OptionsBotton()
    {
        AudioManager.PlayButtonClick();
        OptionsPanel.SetActive(true);
    }
    public void CreditsBotton()
    {
        AudioManager.PlayButtonClick();
        Credits.SetActive(true);
    }
    public void QuitBotton()
    {
        AudioManager.PlayButtonClick();
        Application.Quit();
    }
}
 