using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    //需要的UI对象
    public Text Name;
    public Text Level;
    public Text Exp;
    public Text Gold;

    //负责更新UI (一旦Model层有变化便会通知Controller层去调用View层方法更新UI)
    public void UpdateView(PlayerModel model)
    {
        Name.text = "  名字:" + model.Name;
        Level.text = "  等级:" + model.Level;
        Exp.text = "  经验:" + model.Exp;
        Gold.text = "  货币:" + model.Gold;
    }
}
