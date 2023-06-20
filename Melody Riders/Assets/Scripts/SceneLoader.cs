using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game Scene"); 
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings Scene"); 
    }
}
