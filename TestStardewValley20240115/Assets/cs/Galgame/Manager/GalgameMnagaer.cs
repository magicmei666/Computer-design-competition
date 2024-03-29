using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GalgameMnagaer : MonoBehaviour
{
    //对话信息
    [SerializeField]
    private List<string> body;

    [SerializeField]
    //当前是第几段对话
    private int index = 0;

    //文本组件
    private Text text;

    [SerializeField]
    //所有人物
    private Galgame_Player[] player;

    //组件初始化
    private void Awake()
    {
        text = this.transform.parent.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
    }
    //对象初始化
    private void OnEnable()
    {
        body = InitBody();
    }
    //初始化对话内容
    private List<string> InitBody()
    {
        string vs = File.ReadAllText("D:\\unity\\new unity\\MVC cs\\Assets\\cs\\Galgame\\body\\body.txt");

        string[] vs1 = vs.Split('\n');

        List<string> vs2=new List<string>();

        for (int i = 0; i < vs1.Length; i++)
        {
            vs1[i].Trim();

            if (!(vs1[i].Length <2 || (vs1[i][0] + vs1[i][1].ToString()) == "//"))
            {
                vs2.Add(vs1[i]);
            }
        }

        return vs2;
    }

    /// <summary>
    /// 执行下一段对话
    /// </summary>
    public void LoadNextBody()
    {
        try
        {
            index++;

            string[] vs = body[index - 1].Split('#');

            StopAllCoroutines();
            StartCoroutine(ShowBody(vs[0]));

            SwitchPlayer(player[int.Parse(vs[1])], vs[2], vs[3], vs[4], vs[5]);
        }
        catch
        {
            StopAllCoroutines();
            StartCoroutine(ShowBody("发生错误"));
        }
    }

    /// <summary>
    /// 显示内容
    /// </summary>
    /// <param name="body">需要显示的内容</param>
    private IEnumerator ShowBody(string body)
    {
        var time = new WaitForSecondsRealtime(0.05f);
        text.text = string.Empty;
        for (int i = 0; i < body.Length; i++)
        {
            text.text += body[i];
            yield return time;
        }
        text.text += " ▼";
    }

    /// <summary>
    /// 切换人物信息
    /// </summary>
    public void SwitchPlayer(Galgame_Player player, string acc, string hair, string body, string face)
    {
        if (this.player.Length == 0)
        {
            StopAllCoroutines();
            StartCoroutine(ShowBody("发生错误,找不到人物"));
            return;
        }
        try
        {
            Debug.Log("acc" + acc + "hair" + hair + "body" + body + "face" + face);
            player.SwitchExpression(acc, hair, body, face);
        }
        catch
        {
            StopAllCoroutines();
            StartCoroutine(ShowBody("发生错误,人物填写错误"));
        }

    }

}
