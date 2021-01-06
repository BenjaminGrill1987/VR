using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject menuDisplay, controllerRightRay, controllerLeftRay, controllerRightDirect, controllerLeftDirect;
    [SerializeField] Transform displayPosition;
    [SerializeField] private XRController controller;
    [SerializeField] private DeviceBasedContinuousMoveProvider moveController;
    [SerializeField] private DeviceBasedContinuousTurnProvider turnController;

    private float defaultMove, defaultTurn;

    private GameObject menu;

    private bool pressed;

    private void Start()
    {
        defaultMove = moveController.moveSpeed;
        defaultTurn = turnController.turnSpeed;
    }

    public void Update()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool inventory) == inventory)
        {
            if (!pressed)
            {
                pressed = true;
                if (menu == null)
                {
                    menu = Instantiate(menuDisplay, displayPosition.position, displayPosition.rotation);
                    menu.GetComponent<Canvas>().worldCamera = Camera.main;
                    ControllerSwitchRay();
                }
                else
                {
                    Destroy(menu);
                    menu = Instantiate(menuDisplay, displayPosition.position, displayPosition.rotation);
                    menu.GetComponent<Canvas>().worldCamera = Camera.main;
                }
            }
        }
        else if (controller.inputDevice.TryGetFeatureValue(CommonUsages.gripButton , out bool close) == close)
        {
            if (!pressed)
            {
                pressed = true;
                if (menu != null)
                {
                    Destroy(menu);
                    ControllerSwitchDirect();
                }
            }
        }
        else
        {
            pressed = false;
        }
    }

    private void ControllerSwitchRay()
    {
        controllerLeftDirect.SetActive(false);
        controllerRightDirect.SetActive(false);
        controllerLeftRay.SetActive(true);
        controllerRightRay.SetActive(true);
        moveController.moveSpeed = 0;
        turnController.turnSpeed = 0;
    }

    private void ControllerSwitchDirect()
    {
        controllerLeftDirect.SetActive(true);
        controllerRightDirect.SetActive(true);
        controllerLeftRay.SetActive(false);
        controllerRightRay.SetActive(false);
        moveController.moveSpeed = defaultMove;
        turnController.turnSpeed = defaultTurn;
    }
}