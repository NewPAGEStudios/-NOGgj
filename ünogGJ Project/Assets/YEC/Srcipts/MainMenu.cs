using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void play(){
        SceneManager.LoadSceneAsync(1);
    }
    public void Quit(){
        Application.Quit();
    }
}
