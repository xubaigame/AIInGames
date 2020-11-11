/****************************************************
    文件：FSMTransitions.cs
	作者：积极向上小木木
    邮箱: positivemumu@126.com
    日期：2020/11/10 22:2:29
	功能：有限状态机状态转换条件枚举
*****************************************************/

public enum FSMTransitions
{
    //默认转换条件
    NullTransition = 0,

    LookPlayer = 1,
    MissPlayer = 2,
}