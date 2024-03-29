using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : Controller
{
    public View m_View;

    public Button AddLevel;
    public Button AddExp;
    public Button AddGold;

    private void Start()
    {
        Bind(m_View,Player2Model.Instance);

        AddLevel.onClick.AddListener(Player2Model.Instance.AddLeve);
        AddExp.onClick.AddListener(Player2Model.Instance.AddExp);
        AddGold.onClick.AddListener(Player2Model.Instance.AddGold);
    }
}
