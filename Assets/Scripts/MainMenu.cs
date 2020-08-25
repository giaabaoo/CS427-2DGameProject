using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
   {
        SceneManager.LoadScene("PlayerScene1");
        SoundManager.PlaySound("boss");
   }
   public void QuitGame()
    {
        Application.Quit();
    }
}
