using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting; 
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A custom layout group for arranging UI elements in a flexible grid. 
/// Supports different grid fitting types such as uniform, fixed rows, or fixed columns.
/// </summary>
public class FlexibleGridLayout : LayoutGroup
{
    /// <summary>
    /// Enum representing the different types of grid fitting:
    /// Uniform, Width, Height, FixedRows, FixedColumns.
    /// </summary>
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public FitType fitType; // The selected type of fitting for the grid.
    
    public int rows; // The number of rows in the grid.
    public int columns; // The number of columns in the grid.
    
    public Vector2 cellSize; // The size of each cell in the grid.
    public Vector2 spacing; // The spacing between cells in the grid.
    
    public bool fitX; // Whether to fit the cell size horizontally.
    public bool fitY; // Whether to fit the cell size vertically.

    /// <summary>
    /// Calculates the horizontal layout, determining the size and position of grid items.
    /// </summary>
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        // Adjust the number of rows and columns based on the FitType.
        if(fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
        }

        // Adjust rows and columns based on fixed columns or rows fit type.
        if(fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        if(fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        // Calculate the available width and height for the grid's parent object.
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        // Compute the width and height for each cell, taking padding and spacing into account.
        float cellWidth = parentWidth / (float)columns - ((spacing.x / (float)columns)* (columns - 1)) - (padding.left / (float) columns) - (padding.right / (float) columns);
        float cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows)*2) - (padding.top / (float) rows) - (padding.bottom / (float) rows);

        // Update the cell size based on the fit settings.
        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        // Position the child elements in the grid.
        int columnCount = 0;
        int rowCount = 0;


        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount)  + (spacing.y * rowCount) + padding.top;

            // Set the position and size of each child element along the horizontal and vertical axes.
            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }


    /// <summary>
    /// Calculates & sets the vertical and horizontal layouts. Currently unused but can be extended.
    /// </summary>
    public override void CalculateLayoutInputVertical()
    {

    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {

    }
}
