// ****************************************************
//     文件：UIController.cs
//     作者：积极向上小木木
//     邮箱：positivemumu@126.com
//     日期：2021/4/2 23:11:59
//     功能：
// *****************************************************

using System.Collections;
using System.Collections.Generic;
using HierarchicalFiniteStatesMachine;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text systemText;
    public Text stateText;

    public int number;//盐的数量
    
    private HFSMManagerSystem manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = new HFSMManagerSystem();
        
        HomeSystem homeSystem = new HomeSystem("HomeSystem",manager);
        
        ReadBookState readBookState = new ReadBookState("ReadBookState", homeSystem, systemText, stateText);
        readBookState.AddTransition("Cook","CookState");
        CookState cookState = new CookState("CookState", homeSystem, systemText, stateText);
        cookState.AddTransition("Sleep","SleepState");
        SleepState sleepState = new SleepState("SleepState", homeSystem, systemText, stateText);
        sleepState.AddTransition("ReadBook","ReadBookState");
        
        homeSystem.AddState(readBookState);
        homeSystem.AddState(cookState);
        homeSystem.AddState(sleepState);

        MarketSystem marketSystem = new MarketSystem("MarketSystem", manager);
        
        BuyState buyState = new BuyState("BuyState", marketSystem, systemText, stateText);
        buyState.AddTransition("Pay","PayState");
        PayState payState  = new PayState("PayState", marketSystem, systemText, stateText);
        payState.AddTransition("Buy","BuyState");
        
        marketSystem.AddState(buyState);
        marketSystem.AddState(payState);
                                                                                                                                                                                                                                                              
        manager.AddSubSystem(homeSystem);
        manager.AddSubSystem(marketSystem);
    }

    // Update is called once per frame
    void Update()
    {
        manager.UpdateMethod();
    }
}
