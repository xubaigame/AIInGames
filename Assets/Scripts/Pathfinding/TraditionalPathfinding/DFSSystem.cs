// ****************************************************
//     文件：DFSSystem.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/3/30 21:34:9
//     功能：四方向深度优先搜索算法（简单版）
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TraditionalPathfinding
{
    public class DFSSystem
    {

        private static Node[,] map;
        private static int mapLengh;
        private static int mapWidth;
        private static int obstacleType;
        
        public static void InitMap(int[,] GameMap,int ObstacleType)
        {
            mapLengh = GameMap.GetLength(0);
            mapWidth = GameMap.GetLength(1);
            obstacleType = ObstacleType;
            map = new Node[mapLengh,mapWidth];
            for (int i = 0; i < mapLengh; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    map[i,j] = new Node(i, j, GameMap[i, j]);
                }
            }
            
        }
        
        /// <summary>
        /// DFS深度搜索算法；
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <param name="passNodeList"></param>
        public static bool Search(Node origin,Node target, ref List<Node> passNodeList)
        {
            passNodeList.Clear();
            if (DFSSearch(origin, target))
            {
                // 这里是保存最短路径；
                Node currentNode = map[target.X, target.Y]; 
                while (currentNode.Value!=origin.Value)
                {
                    passNodeList.Add(currentNode);
                    currentNode = currentNode.parent;
                }
                passNodeList.Add(origin);
            }

            return false;
            
        }
        
        /// <summary>
        /// 深度遍历当前节点；
        /// </summary>
        /// <param name="i">当前节点的X坐标</param>
        /// <param name="j">当前节点的Y坐标</param>
        /// <param name="origin">开始节点</param>
        /// <param name="target">目标节点</param>
        private static bool DFSSearch(Node currentNode,Node targetNode)
        { 
            if (map[currentNode.X,currentNode.Y].Value == targetNode.Value)
            {
                return true;
            }
            map[currentNode.X,currentNode.Y].bVisit = true;
            //获取邻居节点;
            List<Node> neighbors = getNeighbor(map[currentNode.X,currentNode.Y]);
            for (int k = 0; k < neighbors.Count; k++)
            {
                int x =neighbors[k].X;
                int y =neighbors[k].Y;
                if (!map[x,y].bVisit&&map[x,y].Value!=obstacleType)
                {
                    //递归深度遍历；
                    
                    //先设置标志位为已访问；
                    map[x,y].bVisit = true;
                    map[x,y].parent = map[currentNode.X,currentNode.Y];
                    if (DFSSearch(map[x,y], targetNode))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        // 获取邻居节点;
        private static List<Node> getNeighbor(Node currentNode)
        {
            List<Node> nodes = new List<Node>();
            int x = currentNode.X;
            int y = currentNode.Y;
            if (x-1>=0&&x-1<mapLengh)
            {
                nodes.Add(map[x-1,y]);
            }

            if (x+1>=0&&x+1<mapLengh)
            {
                nodes.Add(map[x+1,y]);
            }

            if (y-1>=0&&y-1<mapLengh)
            {
                nodes.Add(map[x,y-1]); 
            }
            
            if (y+1>=0&&y+1<mapWidth)
            {
                nodes.Add(map[x,y+1]); 
            }
            return nodes;
        }
        
        // private static void PrintMapData()
        // {
        //     Console.WriteLine("---------------------");
        //
        //     int mapLength = map.GetLength(0);
        //     int mapWidth = map.GetLength(1);
        //     for (int i = 0; i < mapLength; i++)
        //     {
        //         for (int j = 0; j < mapWidth; j++)
        //         {
        //             if (map[i, j].bVisit)
        //             {
        //                 Console.Write(8 + ", ");
        //
        //             }
        //             else
        //             {
        //                 Console.Write(map[i,j].Value + ", ");
        //
        //             }
        //         }
        //         Console.WriteLine();
        //     }
        // }
    }
}

