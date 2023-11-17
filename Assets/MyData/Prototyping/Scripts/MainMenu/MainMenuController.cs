using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject quitMenu;

    public void LoadGameScene()
    {
        SceneManager.LoadScene("PrototypeGame");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitMenu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
