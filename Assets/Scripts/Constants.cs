using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum color { Empty = 0, Wall = 1, Red = 2, Green = 3 };
public enum dir { Up = 0, Down = 1, Left = 2, Right = 3 , Stand = 4};
public enum gameState { MinWin = -1, Tie = 0, MaxWin = 1 }; 

// MinWin = (Max cant move + MinScore > MaxScore)
// MaxWin = (Min cant move + MinScore < MaxScore)
// Tie = (Min can't move + Max can't move + MinScore = MaxScore)