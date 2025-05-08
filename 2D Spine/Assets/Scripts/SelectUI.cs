using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectUI : MonoBehaviour
{

    public void OnGameOneButton()
    {
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.GameSelectSound, this.transform.position);
        SceneManager.LoadScene("GameScene");
    }

    public void OnBackButton()
    {
        SceneManager.LoadScene("Main");
    }

}
