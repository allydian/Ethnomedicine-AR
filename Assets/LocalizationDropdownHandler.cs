using UnityEngine;

public class LocalizationDropdownHandler : MonoBehaviour
{
    [SerializeField] private AdvancedDropdown dropdown;
    [SerializeField] private LocaleSelector localeSelector;

    private void Awake()
    {
        if (dropdown != null && localeSelector != null)
        {
            dropdown.onOptionSelected += localeSelector.ChangeLocale;
        }
    }

    private void OnDestroy()
    {
        if (dropdown != null && localeSelector != null)
        {
             dropdown.onOptionSelected -= localeSelector.ChangeLocale;
        }
    }
}
