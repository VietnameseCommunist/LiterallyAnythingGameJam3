using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonFunctionality : MonoBehaviour
{
     public string SceneToLoad;
     public bool reload;
     public bool ImminentDeath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Press()
    {
         if (ImminentDeath)
         {
              Debug.Log("player supposed to quit");
              Application.Quit();
              return;
         }
         if (!reload)
         {
              Debug.Log("Scene supposed to load is current");
              SceneManager.LoadScene(SceneToLoad);
              return;
         }
         Debug.Log("to load is " + SceneToLoad);
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
