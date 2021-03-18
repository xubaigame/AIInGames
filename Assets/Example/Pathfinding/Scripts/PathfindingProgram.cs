// ****************************************************
//     文件：PathfindingProgram.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/3/13 23:45:29
//     功能：寻路算法控制类
// *****************************************************

using System.Collections.Generic;
using UnityEngine;

namespace AStarPathfinding
{
    public class PathfindingProgram : MonoBehaviour
    {
        public GameObject MapTiled;
        private int[,] numberMap;
        private GameObject[,] map;
        void Start()
        {
            //1.起点
            //2.终点
            //3.障碍
            //4.道具（必走）
            
            //构建数字地图
            numberMap = new int[6,11]
            {
                {0,0,0,0,0,0,0,0,0,0,0},
                {0,0,3,3,3,0,0,0,2,0,0},
                {0,0,4,0,3,0,0,0,0,0,0},
                {0,1,0,0,3,0,0,0,0,0,0},
                {0,0,3,3,3,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0}
            };
            
            //将数字地图实例化
            map = new GameObject[6,11];
            Transform mapBG=GameObject.Find("Map").transform;
            Color[] colors = new Color[5]
            {
                Color.white,
                Color.green,
                Color.red,
                Color.blue, 
                Color.gray
            };
            for (int i = 0; i < numberMap.GetLength(1); i++)
            {
                for (int j = 0; j <numberMap.GetLength(0) ; j++)
                {
                    GameObject temp=Instantiate(MapTiled, mapBG);
                    temp.transform.localPosition = new Vector3(i, j, 0);
                    temp.GetComponent<SpriteRenderer>().color = colors[numberMap[numberMap.GetLength(0) - 1 - j, i]];
                    map[numberMap.GetLength(0) - 1 - j, i] = temp;
                }
            }

            AStarTest();

        }

        public void AStarTest()
        {
            List<AStarNode> passNodes = new List<AStarNode>();
            AStarNode starNode = new MyAStarNode(3, 1,1);
            AStarNode endNode = new MyAStarNode(1, 8,2);
            AStarSystem aStarSystem = new AStarSystem(numberMap,3);
            bool result=aStarSystem.FindPath(starNode, endNode, ref passNodes);

            if (result)
            {
                for (int i = 0; i < passNodes.Count; i++)
                {
                    if (!passNodes[i].EqualOther(starNode) && !passNodes[i].EqualOther(endNode))
                    {
                        map[passNodes[i].Point.X, passNodes[i].Point.Y].GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                }
                Debug.Log("寻路成功！");
            }
            else
            {
                Debug.Log("寻路失败！");
            }
        }
        
    }
}

