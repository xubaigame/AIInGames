// ****************************************************
//     文件：MyAStarNode.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/3/14 23:57:11
//     功能：自定义AStar节点
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarPathfinding
{
    public class MyAStarNode : AStarNode
    {
        public override int GetGValue(AStarNode node)
        {
            if (node is MyAStarNode)
            {
                if (node.NodeType == 4)
                    return 0;
            }
            return base.GetGValue(node);
        }

        public override List<AStarNode> NeighborNotes
        {
            get
            {
                neighborNodes.Clear();
                neighborNodes.Add(new MyAStarNode(Point.X-1,Point.Y,0));
                neighborNodes.Add(new MyAStarNode(Point.X-1,Point.Y-1,0));
                neighborNodes.Add(new MyAStarNode(Point.X-1,Point.Y+1,0));
                neighborNodes.Add(new MyAStarNode(Point.X+1,Point.Y,0));
                neighborNodes.Add(new MyAStarNode(Point.X+1,Point.Y-1,0));
                neighborNodes.Add(new MyAStarNode(Point.X+1,Point.Y+1,0));
                neighborNodes.Add(new MyAStarNode(Point.X,Point.Y-1,0));
                neighborNodes.Add(new MyAStarNode(Point.X,Point.Y+1,0));
                return neighborNodes;
            }
        }

        public MyAStarNode(AStarPoint point, int nodeType) : base(point, nodeType)
        {
            
        }

        public MyAStarNode(int x, int y, int nodeType) : base(x, y, nodeType)
        {
            
        }
    }
}


