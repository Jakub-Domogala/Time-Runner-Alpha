using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }


    public void QuitGame()
    {
        Debug.Log("Wyjœcie z gry");
        Application.Quit(); // Zamkniêcie aplikacji (dzia³a tylko w buildzie)
    }
    // Metoda wywo³ywana po naciœniêciu przycisku zmiany sceny
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Za³aduj now¹ scenê na podstawie nazwy
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void GoToSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
}
