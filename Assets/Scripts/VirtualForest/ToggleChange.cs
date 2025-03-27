using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToggleChange : MonoBehaviour
{
    public string joystickTag = "HintJoystick";  // Tag for joystick hints
    public string gyroTag = "HintGyro";         // Tag for gyro hints
    public GameObject waypointJoystick;         // Assign "waypointIndicator (joystick)"
    public GameObject waypointGyro;             // Assign "waypointIndicator (gyro)"
    public GameObject mainCameraJoystick;       // Assign "MainCamera (joystick)"
    public GameObject mainCameraGyro;           // Assign "MainCamera (gyro)"
    public GameObject joystick;                 // Assign the joystick
    public Button toggleButton;                 // Reference to the hint toggle button
    public Image hintIconImage;                     // Child Image that changes hint icon
    public Sprite hintOnIcon;                       // Hint "ON" icon
    public Sprite hintOffIcon;                      // Hint "OFF" icon
    public Image joystickIconImage;
    public Sprite joystickOnIcon;
    public Sprite joystickOffIcon;
    private bool isActive = true;               // Starts as visible
    private bool isGyroMode = false;            // Tracks the current mode
    private List<GameObject> activeHints = new List<GameObject>();
    private string currentHintTag;              // Stores the active hint type

    void Start()
    {
        // Ensure the scene starts in Joystick Mode
        isGyroMode = false;
        mainCameraJoystick.SetActive(true);
        mainCameraGyro.SetActive(false);
        waypointJoystick.SetActive(true);
        waypointGyro.SetActive(false);

        SetHintType(joystickTag);
        UpdateObjectsState(isActive);
    }

    public void ToggleVisibility()
    {
        isActive = !isActive; // Toggle state
        UpdateObjectsState(isActive);
    }

    private void UpdateObjectsState(bool state)
    {
        foreach (GameObject obj in activeHints)
        {
            if (obj != null) obj.SetActive(state);
        }

        // Change the icon on the child image
        hintIconImage.sprite = state ? hintOnIcon : hintOffIcon;
    }

    public void SetHintType(string hintTag)
    {
        currentHintTag = hintTag;
        activeHints.Clear();

        // Find all hints matching the new tag
        GameObject[] objects = GameObject.FindGameObjectsWithTag(currentHintTag);
        activeHints.AddRange(objects);

        // Apply current visibility state
        UpdateObjectsState(isActive);
    }

    public void ChangeView()
    {
        isGyroMode = !isGyroMode; // Toggle mode

        Debug.Log("ChangeView Button Clicked - isGyroMode: " + isGyroMode);

        if (isGyroMode)
        {
            Debug.Log("Switching to Gyro Mode");

            if (mainCameraJoystick != null)
            {
                mainCameraJoystick.SetActive(false);
                Debug.Log("MainCamera (joystick) DISABLED");
            }
            else
            {
                Debug.LogError("MainCamera (joystick) is NOT assigned!");
            }

            if (mainCameraGyro != null)
            {
                mainCameraGyro.SetActive(false);
                mainCameraGyro.SetActive(true); // Force enable
                Debug.Log("MainCamera (gyro) ENABLED");
            }
            else
            {
                Debug.LogError("MainCamera (gyro) is NOT assigned!");
            }

            if (waypointJoystick != null)
            {
                waypointJoystick.SetActive(false);
                Debug.Log("WaypointIndicator (joystick) DISABLED");
            }
            else
            {
                Debug.LogError("WaypointIndicator (joystick) is NOT assigned!");
            }

            if (waypointGyro != null)
            {
                waypointGyro.SetActive(true);
                Debug.Log("WaypointIndicator (gyro) ENABLED");
            }
            else
            {
                Debug.LogError("WaypointIndicator (gyro) is NOT assigned!");
            }

            if (joystick != null)
            {
                joystick.SetActive(false);
                Debug.Log("Joystick DISABLED (Gyro Mode)");
            }
            else
            {
                Debug.LogError("Joystick GameObject is NOT assigned!");
            }

            // Update Joystick Icon to OFF (Since Gyro is Active)
            if (joystickIconImage != null)
            {
                joystickIconImage.sprite = joystickOffIcon;
                Debug.Log("Joystick Icon Changed to OFF");
            }

            SetHintType(gyroTag);
        }
        else
        {
            Debug.Log("Switching to Joystick Mode");

            if (mainCameraJoystick != null)
            {
                mainCameraJoystick.SetActive(true);
                Debug.Log("MainCamera (joystick) ENABLED");
            }
            else
            {
                Debug.LogError("MainCamera (joystick) is NOT assigned!");
            }

            if (mainCameraGyro != null)
            {
                mainCameraGyro.SetActive(false);
                Debug.Log("MainCamera (gyro) DISABLED");
            }
            else
            {
                Debug.LogError("MainCamera (gyro) is NOT assigned!");
            }

            if (waypointJoystick != null)
            {
                waypointJoystick.SetActive(true);
                Debug.Log("WaypointIndicator (joystick) ENABLED");
            }
            else
            {
                Debug.LogError("WaypointIndicator (joystick) is NOT assigned!");
            }

            if (waypointGyro != null)
            {
                waypointGyro.SetActive(false);
                Debug.Log("WaypointIndicator (gyro) DISABLED");
            }
            else
            {
                Debug.LogError("WaypointIndicator (gyro) is NOT assigned!");
            }

            if (joystick != null)
            {
                joystick.SetActive(true);
                Debug.Log("Joystick ENABLED (Joystick Mode)");
            }
            else
            {
                Debug.LogError("Joystick GameObject is NOT assigned!");
            }

            // Update Joystick Icon to ON (Since Joystick Mode is Active)
            if (joystickIconImage != null)
            {
                joystickIconImage.sprite = joystickOnIcon;
                Debug.Log("Joystick Icon Changed to ON");
            }

            SetHintType(joystickTag);


        }
    }




}
