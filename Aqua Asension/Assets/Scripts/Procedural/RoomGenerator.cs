using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class creates rooms by procedurally linking components together.

public enum Height
{
    Low, Medium, High
}

public struct RoomComponent
{
    public Height Height{get; set;}
}

[System.Serializable]
public class Room 
{
    public Height height;
    public Height verticalHeight;
    public Height Height {get => height; set => height = value;}
    public Height VerticalHeight {get; set;} 
}

public class RoomGenerator : MonoBehaviour
{
    
}
