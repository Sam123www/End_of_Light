using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    Image img;
    public Sprite backImg1, backImg2;
    public TextMeshProUGUI textLabel, roleNameLabel, narrationLabel;
    public TextAsset textFile;
    List<string> textList = new List<string>();
    int index;
    void Start()
    {
        textLabel.text = "";
        roleNameLabel.text = "";
        textList.Clear();
        img = GetComponent<Image>();
        GetText();
    }
    void Update()
    {
        if (Input.GetButtonDown("Submit") && index >= textList.Count)
        {
            gameObject.SetActive(false);
        }
        else if (Input.GetButtonDown("Submit"))
        {
            SetText();
        }
    }
    void SetText()
    {
        if (textList[index][0] == 'N')
        {
            img.sprite = backImg1;
            roleNameLabel.text = "";
            index++;
            textLabel.text = "";
            narrationLabel.text = textList[index];
        }
        else
        {
            img.sprite = backImg2;
            roleNameLabel.text = textList[index];
            index++;
            narrationLabel.text = "";
            textLabel.text = textList[index];
        }
        index++;
    }
    void GetText()
    {
        index = 0;
        var lineData = textFile.text.Split('\n');
        foreach (var line in lineData)
        {
            textList.Add(line);
        }
        SetText();
    }
}
