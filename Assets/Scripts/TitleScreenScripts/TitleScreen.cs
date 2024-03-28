using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void StartDemo()
    {
        SceneManager.LoadScene("PrototypeLevel", LoadSceneMode.Single);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
