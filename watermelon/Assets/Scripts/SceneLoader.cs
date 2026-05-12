using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()  => SceneManager.LoadScene("SampleScene");
    public void LoadStart() => SceneManager.LoadScene("StartScene");
    public void Restart()   => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
