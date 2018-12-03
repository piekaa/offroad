using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public PiekaButton MenuButton;

    public CameraController CameraController;

    public GameObject settingsMenu;

    private bool menuOpen = true;

    void Start()
    {
        MenuButton.RegisterOnClick(() =>
        {
            if (menuOpen)
            {
                menuOpen = false;
                CameraController.TurnOffBlur();
                settingsMenu.SetActive(false);
            }
            else
            {
                menuOpen = true;
                CameraController.TurnOnBlur();
                settingsMenu.SetActive(true);
            }
        });
        CameraController.TurnOnBlur();
    }

}
