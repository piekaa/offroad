using System.Collections;
using System.Collections.Generic;
using Pieka.Cam;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private PiekaButton menuButton;
    public IPiekaButton MenuButton;

    [SerializeField]
    private CameraController cameraController;
    public ICameraController CameraController;

    public GameObject settingsMenu;

    private bool menuOpen = true;

    void Awake()
    {
        MenuButton = menuButton;
        CameraController = cameraController;
    }

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
