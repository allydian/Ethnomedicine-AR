using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class DynamicGridLayout : MonoBehaviour
{
    public int columns = 2; // Number of columns, adjust in inspector
    private GridLayoutGroup gridLayoutGroup;
    private RectTransform rectTransform;
    private Vector2 lastScreenSize;

    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();

        AdjustCellSize();
    }

    void Update()
    {
        // Only update cell size if the screen size has changed to avoid redundant calls
        if (lastScreenSize != new Vector2(Screen.width, Screen.height))
        {
            AdjustCellSize();
            lastScreenSize = new Vector2(Screen.width, Screen.height);
        }
    }

    private void AdjustCellSize()
    {
        // Ensure the Grid Layout Group is set up correctly
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = columns;

        // Get the current width of the parent RectTransform
        float parentWidth = rectTransform.rect.width;

        // Calculate the cell width based on columns, spacing, and padding
        float cellWidth = (parentWidth - ((columns - 1) * gridLayoutGroup.spacing.x) - gridLayoutGroup.padding.left - gridLayoutGroup.padding.right) / columns;

        // Set the cell size with calculated width and a fixed height or same value for square cells
        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellWidth); // For square cells

        // Force a layout rebuild to apply changes immediately
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }
}
