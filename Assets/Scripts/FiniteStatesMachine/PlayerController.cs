// ****************************************************
//     文件：PlayerController.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/3/12 15:58:33
//     功能：玩家控制类
// *****************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;

    private int x, y;
    private void Update()
    {
        x = 0;
        y = 0;
        if (Input.GetKey(KeyCode.W))
        {
            y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x = 1;
        }
        transform.Translate(new Vector3(x,0,y).normalized* Speed * Time.deltaTime);
    }
}
