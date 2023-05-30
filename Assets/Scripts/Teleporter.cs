using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{



    private IEnumerator TimerTp()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level2");
    }





    private void OnTriggerEnter(Collider other)
    {

        StartCoroutine(TimerTp());
          
        
    }
    
    
}
