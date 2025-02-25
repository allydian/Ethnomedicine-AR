using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomTMPDropdown : TMP_Dropdown // Change the inheritance
{
    protected override GameObject CreateBlocker(Canvas rootCanvas)
    {
        GameObject blocker = new GameObject("Blocker");

        RectTransform blockerRect = blocker.AddComponent<RectTransform>();
        blockerRect.SetParent(rootCanvas.transform, false);
        blockerRect.anchorMin = Vector2.zero;
        blockerRect.anchorMax = Vector2.one;
        blockerRect.sizeDelta = Vector2.zero;

        Canvas blockerCanvas = blocker.AddComponent<Canvas>();
        blockerCanvas.overrideSorting = true;
        Canvas dropdownCanvas = GetComponent<Canvas>();
        blockerCanvas.sortingLayerID = dropdownCanvas.sortingLayerID;
        blockerCanvas.sortingOrder = dropdownCanvas.sortingOrder - 1;

        LayoutElement layoutElement = blocker.AddComponent<LayoutElement>();
        layoutElement.ignoreLayout = true; 

        Image blockerImage = blocker.AddComponent<Image>();
        blockerImage.color = Color.clear;

        Button blockerButton = blocker.AddComponent<Button>();
        blockerButton.onClick.AddListener(Hide);

        return blocker;
    }

    protected override void DestroyBlocker(GameObject blocker)
    {
        if (blocker != null)
        {
            Destroy(blocker);
        }
    }
}
