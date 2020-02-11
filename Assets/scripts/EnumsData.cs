using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumsData
{
    public enum SFXEnum
    {
        dash, rotate, scaleUp, scaleDown, pass, hited, spawnObst,
        monsterLaugh, lose
    }

    public enum CubeTypeEnum
    {
        
        topLeftCorner, 
        TopRightCorner,
        BottomLeftCorner, 
        BottomRightCorner, 
        TopEdge,
        RightEdge,
        LeftEdge,
        BottomEdge,
        Any
    }

    public enum FourDirectionEnum
    {
        Top, Right, Bottom, Left
    }
}

