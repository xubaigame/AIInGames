// ****************************************************
//     文件：Offense.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：#CreateTime#
//     功能：进攻者管理类
// *****************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offense : MonoBehaviour
{

    public bool HasFlag;

    private Vector3 standpos;
    private Quaternion standrot;
    private GameObject flag;

    private bool test = false;
    private void Awake()
    {
        standpos = transform.position;
        standrot = transform.rotation;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flag"))
        {
            HasFlag = true;
            flag = other.gameObject;
            flag.GetComponent<Collider>().enabled = false;
            flag.transform.parent = transform;
            GameManager.Instance.ChangeToFlagState();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Defense"))
        {
            HasFlag = false;
            if (flag != null)
            {
                flag.transform.parent = null;
                flag.GetComponent<Collider>().enabled = true;
                flag = null;
                GameManager.Instance.ChangeToNotFlagState();
            }
            transform.gameObject.SetActive(false);
            transform.position = standpos;
            transform.rotation = standrot;
            
            transform.gameObject.SetActive(true);
        }
    }
}
