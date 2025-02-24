using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PSVisibility : MonoBehaviour
{
    public string targetTag = "PointerSphere";  // Tag to find objects
    public Button toggleButton;                // Reference to the button
    public Image iconImage;                    // Child Image that changes icon
    public Sprite onIcon;                      // "ON" icon
    public Sprite offIcon;                     // "OFF" icon
    private bool isActive = true;              // Starts as visible
    private List<GameObject> taggedObjects = new List<GameObject>();

    void Start()
    {
        // Find and store tagged objects
        GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag);
        taggedObjects.AddRange(objects);

        // Set the initial state
        UpdateObjectsState(isActive);
    }

    public void ToggleVisibility()
    {
        isActive = !isActive; // Toggle state
        UpdateObjectsState(isActive);
    }

    private void UpdateObjectsState(bool state)
    {
        foreach (GameObject obj in taggedObjects)
        {
            if (obj != null) obj.SetActive(state);
        }

        // Change the icon on the child image
        iconImage.sprite = state ? onIcon : offIcon;
    }
}
