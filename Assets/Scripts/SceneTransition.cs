using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            if(sceneToLoad == "Stage_boss" || sceneToLoad == "PlayerScene1")
                SoundManager.PlaySound("boss");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
   
}
