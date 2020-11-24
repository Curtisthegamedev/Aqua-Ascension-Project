using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControl : Switch
{
    private int[] result, correctCombination;
    private void Start()
    {
        result = new int[] { 0, 0, 0, 0 };
        correctCombination = new int[] { Random.Range(0, 9), Random.Range(0, 9), Random.Range(0, 9), Random.Range(0, 9) };
        Debug.Log("Combination is: " + correctCombination[0] + correctCombination[1] + correctCombination[2] + correctCombination[3]);
        Rotate.Rotated += CheckResults;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "Wheel1":
                result[0] = number;
                break;

            case "Wheel2":
                result[1] = number;
                break;

            case "Wheel3":
                result[2] = number;
                break;

            case "Wheel4":
                result[3] = number;
                break;
        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2] && result[3] == correctCombination[3])
        {
            Debug.Log("Correct Combination has been entered!"); //this is also temporary, this would trigger the event of providing the player with the weapon upgrade or unlocking the next level!
            Unlocked(true);
        }
        else if (unlocked == true && (result[0] != correctCombination[0] || result[1] != correctCombination[1] || result[2] != correctCombination[2] || result[3] != correctCombination[3]))
        {
            Debug.Log("Oops! We went from the Correct Combination to an Incorrect Combination!");
            Unlocked(false);
        }
    }

    private void OnDestroy()
    {
        Rotate.Rotated -= CheckResults;
    }
}
