

//单元格六方向，东北（NE）、东（ E )、东南(SE)、西南(SW)、西(W)、西北(NW)
public enum HexDirection
{
    NE, E, SE, SW, W, NW
}

public static class HexDirectionExtensions
{
    //扩展方法，获取当前方向的反方向
    public static HexDirection Opposite(this HexDirection direction)
    {
        return (int)direction < 3 ? (direction + 3) : (direction - 3);
    }

    //跳转到上一个方向
    public static HexDirection Previous(this HexDirection direction)
    {
        return direction == HexDirection.NE ? HexDirection.NW : (direction - 1);
    }

    //跳转到下一个方向
    public static HexDirection Next(this HexDirection direction)
    {
        return direction == HexDirection.NW ? HexDirection.NE : (direction + 1);
    }

    
}