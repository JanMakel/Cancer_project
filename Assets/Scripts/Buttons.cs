using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Buttons : MonoBehaviour
{
    //TO CHANGE SCENES

    public void MoveToScene()
    {
        SceneManager.LoadScene("Levels");
    }



    public void SceneStart ()
    {
        SceneManager.LoadScene("Start");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    
    public void Level1()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }
}   

    

