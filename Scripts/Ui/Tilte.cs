using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tilte : MonoBehaviour
{
    public string sceneName ="Game";

    public static Tilte instance;

    private SaveLoad theSaveLoad;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void ClickStart()
    {
        Debug.Log("로딩");
        SceneManager.LoadScene(sceneName);
    }

    public void ClickLoad()
    {
        Debug.Log("로드");

        StartCoroutine(LoadCoroutine());
    }

    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            yield return null;
        }

        theSaveLoad = FindObjectOfType<SaveLoad>();
        theSaveLoad.LoadData();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void ClickExit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
