using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using World;

[RequireComponent(typeof(UIDocument))]
public class PauseMenu : MonoBehaviour
{
    VisualElement element;
    [SerializeField] InputAction pauseMenu;
    bool open;

    private void Awake()
    {
        element = GetComponent<UIDocument>().rootVisualElement;
        element.Q<Button>("Resume").clicked += PauseMenuToggle;
        element.Q<Button>("Map").clicked += EndScreen.GoToMainMenu;
        element.style.display = DisplayStyle.None;
    }

    private void Start()
    {
        pauseMenu.performed += PauseMenuToggle;
        open = false;
        pauseMenu.Enable();
    }

    private void OnDisable()
    {
        pauseMenu.performed -= PauseMenuToggle;
        pauseMenu.Disable();
    }

    void PauseMenuToggle(InputAction.CallbackContext _)
        => PauseMenuToggle();

    private void PauseMenuToggle()
    {
        if (open)
        {
            element.style.display = DisplayStyle.None;
            WorldController.ToggleGame(false);
        }
        else
        {
            element.style.display = DisplayStyle.Flex;
            WorldController.ToggleGame(true);
        }
        open = !open;
    }
}
