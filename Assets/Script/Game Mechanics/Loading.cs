using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    float rotZ;
    public GameObject shoevl;
    Animator anim;
    bool isLoading;

    public void Enable(string nextScene)
    {
        if(isLoading) return;
        isLoading = true;
        anim = GetComponent<Animator>();
        anim.Play("Loading");
        StartCoroutine(LoadingScreen(nextScene));
    }
    private void FixedUpdate()
    {
        if (shoevl.activeInHierarchy)
        {
            rotZ = rotZ + 6; rotZ %= 360;
            shoevl.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
    IEnumerator LoadingScreen(string nextScene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
        async.allowSceneActivation = false;
        yield return new WaitForSecondsRealtime(2);
        while (async.progress < 0.9f)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
        shoevl.SetActive(false);
        anim.Play("LoadEnd");
        isLoading = false;
    }
    public void LoadingAnimation()
    {
        Time.timeScale = 1f;
        shoevl.SetActive(true);
    }
}
