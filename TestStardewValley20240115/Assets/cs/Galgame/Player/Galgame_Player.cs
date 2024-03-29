using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galgame_Player : MonoBehaviour
{
    /// <summary>
    /// 装饰
    /// </summary>
    public SpriteRenderer Acc;
    public Sprite acc1;
    /// <summary>
    /// 头发
    /// </summary>
    public SpriteRenderer Hair;
    public Sprite hair1;
    public Sprite hair2;
    /// <summary>
    /// 衣服
    /// </summary>
    public SpriteRenderer Body;
    public Sprite body1;
    public Sprite body2;
    /// <summary>
    /// 表情
    /// </summary>
    public SpriteRenderer Face;
    public Sprite face1;
    public Sprite face2;
    public Sprite face3;
    public Sprite face4;

    /// <summary>
    /// 切换人物信息
    /// </summary>
    public void SwitchExpression(string acc,string hair,string body,string face)
    {
        switch (acc)
        {
            case "1":
                Acc.sprite = acc1;
                break;
            default:
                Acc.sprite = null;
                break;
        }
        switch (hair)
        {
            case "1":
                Hair.sprite = hair1;
                break;
            case "2":
                Hair.sprite = hair2;
                break;
            default:
                Hair.sprite = hair1;
                break;
        }
        switch (body)
        {
            case "1":
                Body.sprite = body1;
                break;
            case "2":
                Body.sprite = body2;
                break;
            default:
                Body.sprite = body1;
                break;
        }
        switch (face)
        {
            case "1":
                Debug.Log("cs1");
                Face.sprite = face1;
                break;
            case "2":
                Debug.Log("cs2");
                Face.sprite = face2;
                break;
            case "3":
                Debug.Log("cs3");
                Face.sprite = face3;
                break;
            case "4":
                Debug.Log("cs4");
                Face.sprite = face4;
                break;
            default:
                Debug.Log("cs0");
                Face.sprite = face1;
                break;
        }
    }
}
