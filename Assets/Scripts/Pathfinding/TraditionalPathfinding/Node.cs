// ****************************************************
//     文件：Node.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/3/30 20:30:22
//     功能：地图路径点
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TraditionalPathfinding
{
    public class Node
    {
        public int X;
        public int Y;
        public int Value;
        
        public bool bVisit;
        public Node parent;
 
        public Node(int x,int y,int value)
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
            this.parent = null;
            this.bVisit = false;
        }

        public bool EqualsOther(Node node)
        {
            if (node == null) return false;
            return X==node.X&&Y==node.Y;
        }
    }
}


