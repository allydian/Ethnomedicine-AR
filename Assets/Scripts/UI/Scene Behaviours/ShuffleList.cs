using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A utility class that provides a method for shuffling items in a list.
/// This class is abstract and contains only static methods.
/// </summary>
public abstract class ShuffleList
{
    /// <summary>
    /// Shuffles the elements of a given list in random order and returns a new list with the shuffled items.
    /// </summary>
    /// <typeparam name="E">The type of the elements in the list.</typeparam>
    /// <param name="inputList">The list of items to be shuffled.</param>
    /// <returns>A new list with the items shuffled in random order.</returns>
    public static List<E> ShuffleListItems<E>(List<E> inputList)
    {
        List<E> originalList = new List<E>();  // Create a copy of the input list to preserve the original list
        originalList.AddRange(inputList);

        List<E> randomList = new List<E>(); // Create an empty list to store the shuffled items

        System.Random r = new System.Random();  // Initialise a random number generator
        int randomIndex = 0;

        // Continue shuffling until all items are moved to the random list
        while (originalList.Count > 0)
        {
            randomIndex = r.Next(0, originalList.Count); // Generate a random index from the original list
            randomList.Add(originalList[randomIndex]); // Add the item at the random index to the shuffled list
            originalList.RemoveAt(randomIndex); // Remove the item from the original list to avoid duplicates
        }

        return randomList; // Return the shuffled list
    }
}
