using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!pauseMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f; // Ustawienie czasu na zero zamra¿a grê
            pauseMenu.SetActive(true);
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f; 
        }
    }

    public void Reset()
    {
        // Pobierz nazwê bie¿¹cej sceny
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Za³aduj ponownie bie¿¹c¹ scenê
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1f;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Za³aduj now¹ scenê na podstawie nazwy
    }


}
