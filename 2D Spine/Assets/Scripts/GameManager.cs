using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� �Ѿ�� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void GoToSongSelect()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToResult()
    {
        SceneManager.LoadScene("ResultScene");
    }
}

