// ****************************************************
//     文件：BehaviorTreeManager.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/10 23:1:30
//     功能：行为树测试类
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class BehaviorTreeManager : MonoBehaviour
{

    private Move move;
    private Idel idel;

    // Start is called before the first frame update
    void Start()
    {
        move = new Move();
        idel = new Idel();
        
        OnButtonClick();
    }
    public void OnButtonClick()
    {
        //SelectNodeTest();
        //SequenceNodeText();
        //ParallelSelectNodeText();
        //ParallelSequenceNode();
        UntilSucceedNodeText();
    }
    
    /// <summary>
    /// 选择节点测试
    /// </summary>
    private void SelectNodeTest()
    {
        SelectNode selectNode= new SelectNode();
        selectNode.AddChild(move);
        selectNode.AddChild(idel);
        Debug.Log("选择节点执行结果为："+selectNode.DoAction());
    }

    /// <summary>
    /// 顺序节点测试
    /// </summary>
    private void SequenceNodeText()
    {
        SequenceNode sequenceNode = new SequenceNode();
        sequenceNode.AddChild(move);
        sequenceNode.AddChild(idel);
        Debug.Log("顺序节点执行结果为："+sequenceNode.DoAction());
    }

    /// <summary>
    /// 并行选择节点测试
    /// </summary>
    private void ParallelSelectNodeText()
    {
        ParallelSelectNode parallelSelectNode = new ParallelSelectNode();
        parallelSelectNode.AddChild(move);
        parallelSelectNode.AddChild(idel);
        Debug.Log("并行选择节点执行结果为："+parallelSelectNode.DoAction());
    }

    /// <summary>
    /// 并行顺序节点
    /// </summary>
    private void ParallelSequenceNode()
    {
        ParallelSequenceNode parallelSequenceNode = new ParallelSequenceNode();
        parallelSequenceNode.AddChild(move);
        parallelSequenceNode.AddChild(idel);
        Debug.Log("并行选择节点执行结果为："+parallelSequenceNode.DoAction());
    }

    /// <summary>
    /// 装饰节点测试
    /// </summary>
    private void UntilSucceedNodeText()
    {
        UntilSucceed untilSucceed = new UntilSucceed(move);
        untilSucceed.DoAction();
    }

}
