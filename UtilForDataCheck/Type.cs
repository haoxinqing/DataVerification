using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilForDataCheck
{
    /// <summary>
    /// 验证类型
    /// </summary>
    public enum CheckType
    {
        NULL,           //无限制
        REALNUMBER,     //实数
        INTEGER,        //整数
        ANGLE,          //角度
    }

    /// <summary>
    /// 验证要求的事件类型
    /// </summary>
    public enum EventType
    {
        TEXTCHANGE, //在TextChange事件中添加验证
        LEAVE       //在Leave事件中添加验证
    }
}
