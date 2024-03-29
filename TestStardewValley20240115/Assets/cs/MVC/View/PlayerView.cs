using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    //��Ҫ��UI����
    public Text Name;
    public Text Level;
    public Text Exp;
    public Text Gold;

    //�������UI (һ��Model���б仯���֪ͨController��ȥ����View�㷽������UI)
    public void UpdateView(PlayerModel model)
    {
        Name.text = "  ����:" + model.Name;
        Level.text = "  �ȼ�:" + model.Level;
        Exp.text = "  ����:" + model.Exp;
        Gold.text = "  ����:" + model.Gold;
    }
}
