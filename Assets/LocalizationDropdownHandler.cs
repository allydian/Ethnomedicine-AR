using UnityEngine;

public class LocalizationDropdownHandler : MonoBehaviour
{
    [SerializeField] private AdvancedDropdown dropdown;
    [SerializeField] private LocaleSelector localeSelector;
    [SerializeField] private GameObject confirmLocaleChange;

    private int selectedLocaleID; //New

    private void Start()
    {
        confirmLocaleChange.SetActive(false);
    }

    private void Awake()
    {
        confirmLocaleChange.SetActive(false);
        /*
        if (dropdown != null && localeSelector != null)
        {
            dropdown.onOptionSelected += localeSelector.ChangeLocale;
        }
        */
        if (dropdown != null)
        {
            dropdown.onOptionSelected += OnLocaleSelected;
        }
    }

    private void OnDestroy()
    {
        /*
        if (dropdown != null && localeSelector != null)
        {
            dropdown.onOptionSelected -= localeSelector.ChangeLocale;
        }
        */
        if (dropdown != null)
        {
            dropdown.onOptionSelected -= OnLocaleSelected;
        }
    }

    //All onwards are new
    private void OnLocaleSelected(int localeID)
    {
        selectedLocaleID = localeID;
        confirmLocaleChange.SetActive(true); // Show confirmation popup
    }

    public void ConfirmLocaleChange()
    {
        if (localeSelector != null)
        {
            localeSelector.ChangeLocale(selectedLocaleID);
        }
        confirmLocaleChange.SetActive(false); // Hide confirmation popup after confirming
    }

    public void CancelLocaleChange()
    {
        confirmLocaleChange.SetActive(false); // Hide confirmation popup if cancelled
    }
}
