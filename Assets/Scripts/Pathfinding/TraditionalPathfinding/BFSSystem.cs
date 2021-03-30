// ****************************************************
//     文件：BFSSystem.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/3/30 20:37:23
//     功能：四方向广度优先搜索算法
// *****************************************************

using System.Collections.Generic;
using UnityEngine;

namespace TraditionalPathfinding
{
    public class BFSSystem : MonoBehaviour
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
        /// BFS算法；
        /// </summary>
        /// <param name="origin">开始节点</param>
        /// <param name="target">目标点</param>
        /// <param name="passNodeList">最短路径列表</param>
        public static bool Search(Node origin, Node target,ref List<Node> passNodeList)
        {
            passNodeList.Clear();
            
            if (BFSSearch(origin, target))
            {
                Node currentNode = map[target.X, target.Y]; 
                while (currentNode.Value!=origin.Value)
                {
                    passNodeList.Add(currentNode);
                    currentNode = currentNode.parent;
                }
                passNodeList.Add(origin);
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// 根据当前节点，检查自己的邻居节点；
        /// </summary>
        /// <param name="currentNode">当前节点</param>
        /// <param name="origiNode">原始节点</param>
        /// <param name="target">目标节点</param>
        /// <param name="passNodeList">最短路径列表</param>
        private static bool BFSSearch(Node currentNode,Node target)
        {
            //将当前节点加入队列中；
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(currentNode);

            while (queue.Count>0)
            {
                Node head = queue.Dequeue();  
                // 检查四个邻居(上下左右);
                List<Node> neighbors = getNeighbor(head);
                for (int i = 0; i < neighbors.Count; i++)
                { 
                    // 没有访问并且可以访问；
                    if (!neighbors[i].bVisit&&neighbors[i].Value!= obstacleType)
                    {
                        //标记节点为已经访问；
                        neighbors[i].bVisit = true;
                        neighbors[i].parent = head;
                        queue.Enqueue(neighbors[i]);
                        
                        //记录中间的路径节点；
                        if (neighbors[i].Value == target.Value)
                        { 
                            return true;
                        }
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
            if (x-1>=0)
            {
                nodes.Add(map[x-1,y]);
            }

            if (x+1>=0&&x+1<mapLengh)
            {
                nodes.Add(map[x+1,y]);
            }

            if (y-1>=0)
            {
                nodes.Add(map[x,y-1]); 
            }
            
            if (y+1>=0&&y+1<mapWidth)
            {
                nodes.Add(map[x,y+1]); 
            }
            return nodes;
        }
    }
}

