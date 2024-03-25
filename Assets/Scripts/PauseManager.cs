using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Slider volumeSlider; // Slider do ustawiania g³oœnoœci
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        volumeSlider.value = DataBase.Instance.Volume;
        Debug.Log(DataBase.Instance.Volume);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.activeInHierarchy && Input.GetKeyUp(KeyCode.R) && !Input.GetKey(KeyCode.LeftShift))
        {
            Reset();
        }
        if (Input.GetKey(KeyCode.LeftShift) && !pauseMenu.activeInHierarchy && Input.GetKeyUp(KeyCode.R))
        {
            DataBase.Instance.Time = 0;
            SceneManager.LoadScene("test level");
        }
        if(!pauseMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f; // Ustawienie czasu na zero zamra¿a grê
            pauseMenu.SetActive(true);
            AudioListener.volume = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioListener.volume = DataBase.Instance.Volume;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f; 
        }
    }
    public void OnVolumeChanged()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
        DataBase.Instance.Volume = volume;
        Debug.Log(DataBase.Instance.Volume);
    }

    public void Reset()
    {
        // Pobierz nazwê bie¿¹cej sceny
        string currentSceneName = SceneManager.GetActiveScene().name;
        DataBase.Instance.Time = GameMaster.Instance.timerModified;

        // Za³aduj ponownie bie¿¹c¹ scenê
        SceneManager.LoadScene(currentSceneName);
        AudioListener.volume = DataBase.Instance.Volume;
        Time.timeScale = 1f;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Za³aduj now¹ scenê na podstawie nazwy
        Time.timeScale = 1f;
    }


}
