using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Loading : MonoBehaviour
{
    float rotZ;
    public SceneAsset sceneName;
    public GameObject shoevl;
    Animator anim;
    void Start()
    {
        StartCoroutine(LoadingScreen());
        anim = GetComponent<Animator>();
        anim.Play("Loading");
    }
    private void FixedUpdate()
    {
        if (shoevl.activeInHierarchy)
        {
            rotZ = rotZ + 6; rotZ %= 360;
            shoevl.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
    IEnumerator LoadingScreen()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName.name);
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(3);
        while (async.progress < 0.9f)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
    }
    public void LoadingAnimation()
    {

        shoevl.SetActive(true);
    }
}
