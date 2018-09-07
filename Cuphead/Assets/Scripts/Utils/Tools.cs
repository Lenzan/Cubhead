using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Color
{
    Red,
    Green,
    Yellow,
    Black,
    Gray
}

/// <summary>
/// 工具类
/// </summary>
public class Tools  {

    /// <summary>
    /// 获取一串带颜色的信息
    /// </summary>
    /// <param name="color"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static string GetColorFont(Color color , string msg)
    {
        string colorFont = "";
        switch (color)
        {
            case Color.Red:
                colorFont = "<color=#FF0000>" + msg + "</color>";
                break;
            case Color.Green:
                colorFont = "<color=#06FF00>" + msg + "</color>";
                break;
            case Color.Yellow:
                colorFont = "<color=#FDFF00>" + msg + "</color>";
                break;
            case Color.Black:
                colorFont = "<color=#000000>" + msg + "</color>";
                break;
            case Color.Gray:
                colorFont = "<color=#636363>" + msg + "</color>";
                break;
        }
        return colorFont;
    }

    /// <summary>
    /// 获取发射子弹方向
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static Vector3 GetRotation(int dir)
    {
        Vector3 newDir = Vector3.zero;
        switch (dir)
        {
            case 1:
                newDir = new Vector3(0,0,0);
                break;
            case -1:
                newDir = new Vector3(0, 180, 0);
                break;
            case 2:
                newDir = new Vector3(0, 0, 90);
                break;
            case -2:
                newDir = new Vector3(0, 0, -90);
                break;
        }
        return newDir;
    }

}
