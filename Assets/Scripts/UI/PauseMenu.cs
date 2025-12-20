using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] InputAction pauseMenu;
    bool open;
    float timeScale;

    private void Start()
    {
        pauseMenu.performed += (_) => PauseMenuToggle();
        open = false;
        pauseMenu.Enable();
    }

    private void PauseMenuToggle()
    {
        if (open)
        {
            WorldController.ToggleGame(false);
        }
        else
        {
            WorldController.ToggleGame(true);
        }
        open = !open;
    }
}
