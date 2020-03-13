//using System.Text;
//using UnityEngine;

//public class MazeBulider : MonoBehaviour
//{
//    private const int SIZE = 20;
//    private const int RANK = 10;

//    private enum Map
//    {
//        Wall = 0,
//        Route = 1
//    }

//    private Map[,] map;

//    private void Start()
//    {
//        map = new Map[SIZE, SIZE];
//        InitMaze();

//        //最外层设置为路，防止被挖穿
//        for (int i = 0; i < SIZE; i++)
//        {
//            map[0, i] = Map.Route;
//            map[i, 0] = Map.Route;
//            map[SIZE - 1, i] = Map.Route;
//            map[i, SIZE - 1] = Map.Route;
//        }

//        CreateMaze(2, 2);
//        map[2, 1] = Map.Route;

//        for (int i = SIZE - 3; i >= 0; i--)
//        {
//            if (map[i, SIZE - 3] == Map.Route)
//            {
//                map[i, SIZE - 2] = Map.Route;
//                break;
//            }
//        }

//        StringBuilder sb = new StringBuilder();
//        for (int i = 0; i < SIZE; i++)
//        {
//            for (int j = 0; j < SIZE; j++)
//            {
//                if (map[i, j] == Map.Route)
//                {
//                    sb.Append("<color=green>玉</color>");
//                }
//                else
//                {
//                    sb.Append("<color=red>国</color>");
//                }
//            }
//            sb.Append("\n");
//        }
//        Debug.Log(sb);
//    }

//    private void InitMaze()
//    {
//        for (int i = 0; i < SIZE; i++)
//        {
//            for (int j = 0; j < SIZE; j++)
//            {
//                map[i, j] = Map.Wall;
//            }
//        }
//    }

//    public void CreateMaze(int x, int y)
//    {
//        map[x, y] = Map.Route;
//        //下，上，右，左
//        int[,] direction = new int[,] { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

//        //对四个方向进行打乱
//        for (int i = 0; i < 4; i++)
//        {
//            int r = Random.Range(0, 4);
//            //Debug.Log($"Random r :{r}");
//            int tmp = direction[0, 0];
//            direction[0, 0] = direction[r, 0];
//            direction[r, 0] = tmp;

//            tmp = direction[0, 1];
//            direction[0, 1] = direction[r, 1];
//            direction[r, 1] = tmp;
//        }

//        //向四个方向开始挖
//        for (int i = 0; i < 4; i++)
//        {
//            int nextX = x;
//            int nextY = y;

//            //挖的距离
//            int range = 1 + (RANK == 0 ? 0 : Random.Range(0, RANK));
//            while (range > 0)
//            {
//                nextX += direction[i, 0];
//                nextY += direction[i, 1];

//                //去掉回头路
//                if (map[nextX, nextY] == Map.Route)
//                    break;

//                //保证四周没有被挖过
//                int count = 0;
//                for (int j = nextX - 1; j < nextX + 2; j++)
//                {
//                    for (int k = nextY - 1; k < nextY + 2; k++)
//                    {
//                        if (Mathf.Abs(j - nextX) + Mathf.Abs(k - nextY) == 1 && map[j, k] == Map.Route)
//                            count++;
//                    }
//                }

//                //if (nextX == 1 || nextX == SIZE - 2 || nextY == 1 || nextY == SIZE - 2)
//                //    break;

//                if (count > 1) break;

//                //if (nextX == 1 || nextX == SIZE - 2 || nextY == 1 || nextY == SIZE - 2||Random.Range(0,10)==0)
//                //    break;


//                --range;
//                map[nextX, nextY] = Map.Route;
//            }

//            //没有挖穿时继续递归
//            if (range <= 0)
//                CreateMaze(nextX, nextY);
//        }
//    }
//}
