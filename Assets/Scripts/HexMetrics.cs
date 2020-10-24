using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//六边形的外径和内径，以及他对应的六个邻居
public static class HexMetrics 
{
    //外径
    public const float outerRadius = 10f;
    //内径为外径*0.866025404f，为什么是0.866025404f参考教程推导
    public const float innerRadius = outerRadius * 0.866025404f;

    public const float solidFactor = 0.75f;

    public const float blendFactor = 1f - solidFactor;

    //六边形有两种定位方式，要么是尖的朝上，要么是平的朝上
    //这里选定尖的朝上的六边形
    //并把朝上的第一个角作为起点
    //然后顺时针添加其余角的顶点
    //多存一个顶点防止越界，所以是七个顶点
    static Vector3[] corners = {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
    };

    public static Vector3 GetFirstCorner(HexDirection direction)
    {
        return corners[(int)direction];
    }

    public static Vector3 GetSecondCorner(HexDirection direction)
    {
        return corners[(int)direction + 1];
    }

    public static Vector3 GetFirstSolidCorner(HexDirection direction)
    {
        return corners[(int)direction] * solidFactor;
    }

    public static Vector3 GetSecondSolidCorner(HexDirection direction)
    {
        return corners[(int)direction + 1] * solidFactor;
    }

    public static Vector3 GetBridge(HexDirection direction)
    {
        return (corners[(int)direction] + corners[(int)direction + 1]) *
            0.5f * blendFactor;
    }
}
