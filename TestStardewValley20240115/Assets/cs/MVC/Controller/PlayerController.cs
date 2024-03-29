using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerView m_View;

    public Button AddLevel;
    public Button AddExp;
    public Button AddGold;

    private void Start()
    {
        PlayerModel.Instance.AddUpdateEvent(m_View.UpdateView);

        AddLevel.onClick.AddListener(PlayerModel.Instance.AddLeve);
        AddExp.onClick.AddListener(PlayerModel.Instance.AddExp);
        AddGold.onClick.AddListener(PlayerModel.Instance.AddGold);
    }
}
