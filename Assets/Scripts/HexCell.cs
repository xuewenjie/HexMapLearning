using UnityEngine;

//单个六边形
public class HexCell : MonoBehaviour
{
    //存储了坐标信息
    public HexCoordinates coordinates;

    //储存相邻单元格
    [SerializeField]
    HexCell[] neighbors;

    public Color color;

    //在每个方向上去获取相邻单元格
    public HexCell GetNeighbor(HexDirection direction)
    {
        return neighbors[(int)direction];
    }

    //设置相邻单元格
    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }

}
