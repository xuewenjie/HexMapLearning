using UnityEngine;
using UnityEngine.UI;

//多行多列六边形组合
public class HexGrid : MonoBehaviour
{
    //创建多少行列的六边形 height * width
    public int width = 6;
    public int height = 6;

    //六边形预制体
    public HexCell cellPrefab;

    //存储创建了的六边形
    HexCell[] cells;

    //六边形上面坐标输出文办
    public Text cellLabelPrefab;

    //grid上的canvas组件
    Canvas gridCanvas;

    //网格信息
    HexMesh hexMesh;
    public Color defaultColor = Color.white;
    public Color touchedColor = Color.magenta;

    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void Start()
    {
        hexMesh.Triangulate(cells);
    }
    

    void CreateCell(int x, int z, int i)
    {
        //确定六边形的中心位置
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        //生成单个六边形
        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.color = defaultColor;
        //连接E-W相连的单元格，第一个除外，无E方向
        if (x > 0)
        {
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        
        if (z > 0)
        {
            if ((z & 1) == 0)// z & 1 位运算，表示偶数
            {
                //连接S-E相连的单元格，先处理偶数行
                cell.SetNeighbor(HexDirection.SE, cells[i - width]);
                //连接SW方向的单元格,每一行的第一个单元格，它们SW方向是空的。
                if (x > 0)
                {
                    cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
                }
            }
            else //奇数行跟偶数行镜像的逻辑，，先处理SW，再处理SE，每行的最后一个单元格SE是空的
            {
                cell.SetNeighbor(HexDirection.SW, cells[i - width]);
                if (x < width - 1)
                {
                    cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
                }
            }
        }

        //显示坐标
        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
        
    }

    public void ColorCell(Vector3 position, Color color)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.color = color;
        hexMesh.Triangulate(cells);
    }
}
