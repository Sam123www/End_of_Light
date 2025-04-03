using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public string nextScene;
    Image img;
    public Animator[] dialogAnimator;
    public Sprite backImg1, backImg2;
    public TextMeshProUGUI textLabel, roleNameLabel, narrationLabel;
    public TextAsset textFile;
    List<string> textList = new List<string>();
    int index;
    void Start()
    {
        textLabel.text = "";
        roleNameLabel.text = "";
        narrationLabel.text = "";
        textList.Clear();
        img = GetComponent<Image>();
        GetText();
    }
    void Update()
    {
        if (Input.GetButtonDown("Submit") && index >= textList.Count)
        {
            AudioManager.PlayButtonClick();
            GameManager.instance.Loading(nextScene);
            gameObject.SetActive(false);
        }
        else if (Input.GetButtonDown("Submit"))
        {
            AudioManager.PlayButtonClick();
            SetText();
        }
    }
    void SetText()
    {
        Debug.Log(textList[index].Length);
        if (textList[index].Length == 2 && textList[index][0] == 'E')
        {
            int id = textList[index][1]-'0';
            index++;
            Debug.Log(id + textList[index]);
            dialogAnimator[id].Play(textList[index]);
            index++;
        }
        if (textList[index] == "N")
        {
            img.sprite = backImg1;
            roleNameLabel.text = "";
            index++;
            textLabel.text = "";
            narrationLabel.text = textList[index];
            index++;
        }
        else
        {
            img.sprite = backImg2;
            roleNameLabel.text = textList[index];
            index++;
            narrationLabel.text = "";
            textLabel.text = textList[index];
            index++;
        }
    }
    void GetText()
    {
        index = 0;
        var lineData = textFile.text.Split('\n');
        foreach (var line in lineData)
        {
            var tmp = line.Trim();
            if (tmp.Length > 0)
            {
                textList.Add(tmp);
            }
        }
        SetText();
    }
}
