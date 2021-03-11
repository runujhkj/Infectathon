using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
public class EndMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public void PlayGame() {
        SceneManager.UnloadSceneAsync("Lvl1demo");
        SceneManager.LoadScene("Lvl1demo");
    }
 
    public void Options()
    {
        Text complete = gameObject.GetComponentInChildren<Text>();
        complete.fontSize = 30;
        complete.text = "No next level yet. It's in the blueprints.";
    }

    public void Back() 
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
 
    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}