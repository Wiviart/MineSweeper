using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    void Start()
    {
        MainMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
