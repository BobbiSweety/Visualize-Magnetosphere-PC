using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCon : MonoBehaviour
{

    //the ButtonPauseMenu
    public GameObject ingameMenu;
    public GameObject ButtonPause;
    public string SceneName;

    public void OnPause()//点击“暂停”时执行此方法
    {
        Time.timeScale = 0;
        ingameMenu.SetActive(true);
        ButtonPause.SetActive(false);
    }

    public void OnResume()//点击“回到游戏”时执行此方法
    {
        Time.timeScale = 0.1f;
        ingameMenu.SetActive(false);
        ButtonPause.SetActive(true);
    }

    public void OnRestart()//点击“重新开始”时执行此方法
    {
        //Loading Scene0
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
        Time.timeScale = 0.1f;
       
    }

    public void NormalSpeed()
    {
        Time.timeScale = 0.1f;
    }

    public void SlowSpeed()
    {
        Time.timeScale = 0.05f;
    }

    public void FastSpeed()
    {
        Time.timeScale = 0.2f;
    }
}
