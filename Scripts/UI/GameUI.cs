using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
    public GameObject pauseMenu;
    public GameObject inventoryMenu;
    public GameObject levelCompleteMenu;

    public static GameUI instance;

    private void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        currencyText.SetText(Player.instance.gems.ToString());

        if (Input.GetKeyDown(KeyCode.Escape) && Player.instance.inStore == false) 
        { 
            Player.instance.isPaused = !Player.instance.isPaused;

            if (Player.instance.isPaused)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0; 
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) && Player.instance.inStore == false)
        {
            Player.instance.isPaused = !Player.instance.isPaused;

            if (Player.instance.isPaused)
            {
                inventoryMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                inventoryMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void Resume()
    {
        Player.instance.isPaused = !Player.instance.isPaused;
        pauseMenu.SetActive(false);
        levelCompleteMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level 1");
        Player.instance.isPaused = !Player.instance.isPaused;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void OnCloseInventory()
    {
        Player.instance.isPaused = !Player.instance.isPaused;
        inventoryMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LevelComplete()
    {
        levelCompleteMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Player.instance.isPaused = !Player.instance.isPaused;
        Debug.Log(Player.instance.isPaused);
    }
}
