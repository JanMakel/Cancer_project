using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Buttons : MonoBehaviour
{
    //TO CHANGE SCENES

    public void MoveToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }



    /*public void SceneStart ()
    {
        SceneManager.LoadScene("Start");
    }
    public void Scene2()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Scene3()
    {
        SceneManager.LoadScene("Levels");
    }*/
}   

    

