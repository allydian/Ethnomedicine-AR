using UnityEngine;
using UnityEngine.UI;

public class ConfirmLocaleButton : MonoBehaviour
{
    [SerializeField] private LocalizationDropdownHandler dropdownHandler;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private void Awake()
    {
        confirmButton.onClick.AddListener(() => dropdownHandler.ConfirmLocaleChange());
        cancelButton.onClick.AddListener(() => dropdownHandler.CancelLocaleChange());
    }
}
