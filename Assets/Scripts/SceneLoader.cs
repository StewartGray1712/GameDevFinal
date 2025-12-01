using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); 
    }
}