using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool GameIsPaused = false;

    public Camera camera;

    public static bool cursorLock;
    public static bool canLock;
    public static bool pauseTime;
    public Animator pauseAnimator;

    private void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        pauseMenuUI.SetActive(false);
    }

    void CursorMode()
    {
        Vector3 view = camera.ScreenToViewportPoint(Input.mousePosition);
        bool isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;

        if (Input.GetMouseButtonDown(0) && !isOutside && !cursorLock && canLock)
        {
            cursorLock = true;
        }

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    

    private void Update()
    {
        CursorMode();
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (pauseTime)
        {
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        

    }
    

    public void Resume()
    {
        canLock = true;
        cursorLock = true;
        pauseAnimator.SetTrigger("Close");
        
        pauseTime = false;
        GameIsPaused = false;
        
    }

    void Pause()
    {
        canLock = false;
        cursorLock = false;

        pauseMenuUI.SetActive(true);
        pauseAnimator.SetTrigger("Open");
        pauseTime = true;
        GameIsPaused = true;
    }
    

    public void QuitGame()
    {
        Application.Quit();
    }
}
