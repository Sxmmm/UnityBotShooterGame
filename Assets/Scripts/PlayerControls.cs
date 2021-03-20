using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        checkPause();
    }

    private void checkPause()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isPaused = isPaused ? false : true;
            pauseMenu.SetActive(isPaused);
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }
}
