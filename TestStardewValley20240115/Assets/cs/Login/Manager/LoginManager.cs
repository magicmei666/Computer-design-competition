using UnityEngine;
using TMPro;

/// <summary>
/// 登录注册系统管理类
/// </summary>
public class LoginManager : MonoBehaviour
{
    //---游戏界面---//
    private GameObject loginBefore;
    private GameObject loginAfter;
    //---登录账号与注册账号界面---//
    private GameObject login;
    private GameObject erllon;
    //---登录账号输入密码以及输入密码框---//
    private TMP_InputField loginName;
    private TMP_InputField loginPwd;
    //---注册账号输入密码以及输入密码框---//
    private TMP_InputField erllonName;
    private TMP_InputField erllonPwd;
    //---对话框---//
    private PromptBox promptBox;
    //---数据库实例---//
    public DataBase dataBase;

    /// <summary>
    /// 获得场景中的组件给变量赋值
    /// </summary>
    private void Awake()
    {
        Singleton = this;

        loginBefore = transform.parent.GetChild(2).gameObject;
        loginAfter = transform.parent.GetChild(3).gameObject;

        login = transform.parent.GetChild(4).GetChild(0).gameObject;
        erllon = transform.parent.GetChild(4).GetChild(1).gameObject;

        promptBox = transform.parent.GetChild(4).GetChild(2).GetComponent<PromptBox>();

        loginName = transform.parent.GetChild(4).GetChild(0).GetChild(1).GetComponent<TMP_InputField>();
        loginPwd = transform.parent.GetChild(4).GetChild(0).GetChild(2).GetComponent<TMP_InputField>();

        erllonName = transform.parent.GetChild(4).GetChild(1).GetChild(1).GetComponent<TMP_InputField>();
        erllonPwd = transform.parent.GetChild(4).GetChild(1).GetChild(2).GetComponent<TMP_InputField>();
    }

    /// <summary>
    /// 加载初始化
    /// </summary>
    private void Start()
    {
        //实例化数据库
        dataBase = new DataBase();
        //初始化游戏
        Init();
    }

    /// <summary>
    /// 管理类加载数据初始化
    /// </summary>
    /// <returns>返回加载成功结果的数据</returns>
    private void Init()
    {
        //读取本地账号信息(如果是首次读取则初始化为null)
        string name = PlayerPrefs.GetString("csName", "null");
        string password = PlayerPrefs.GetString("csPwd", "null");

        //判断保存在本地的账号密码是否正确
        if (!dataBase.IsPassword(name, password) || name == "null")
        {
            loginBefore.SetActive(true);
            loginAfter.SetActive(false);
        }
        else
        {
            loginBefore.SetActive(false);
            loginAfter.SetActive(true);
        }

        //将登录账号与注册账号界面关闭
        login.SetActive(false);
        erllon.SetActive(false);
    }

    //--------------------封装方法--------------------//

    /// <summary>
    /// 登录账号方法
    /// </summary>
    /// <param name="name">输入的账号名字</param>
    /// <param name="password">输入的账号密码</param>
    /// <param name="promptBox">提示框</param>
    private void LoginAccount(TMP_InputField name, TMP_InputField password, PromptBox promptBox)
    {
        //使用元组 快速赋值 通过元组析构 拆分成多个变量
        var (_name, _password, _promptBox) = (name.text, password.text,promptBox);

        //检测输入框是否为空
        if (_name == string.Empty || _password == string.Empty)
        {
            //输入框为空提示
            _promptBox.ShowBox("输入框不可为空");
            return;
        }
        //检测是否存在该账号
        if (dataBase.IsName(_name))
        {
            //检测密码是否正确
            if(dataBase.IsPassword(_name,_password))
            {
                //---密码正确执行剩下操作---//
                //将已经登录的账号密码保存到本地
                PlayerPrefs.SetString("csName", _name);
                PlayerPrefs.SetString("csPwd", _password);
                PlayerPrefs.SetInt("csValue", dataBase.GetAccountValue(_name));
                //---更新游戏界面---//
                loginBefore.SetActive(false);
                loginAfter.SetActive(true);
                login.SetActive(false);
                erllon.SetActive(false);
                //登录成功提示
                _promptBox.ShowBox("登录成功");
                return;
            }
            //密码错误提示
            _promptBox.ShowBox("账号不存在或者密码错误");
            return;
        }
        //当账号不存在时
        else
        {
            //账号不存在提示
            _promptBox.ShowBox("账号不存在或者密码错误");
            return;
        }
    }
    
    /// <summary>
    /// 注册账号方法
    /// </summary>
    /// <param name="name">输入的账号名字</param>
    /// <param name="password">输入的账号密码</param>
    /// <param name="promptBox">提示框</param>
    private void ErllonAccount(TMP_InputField name, TMP_InputField password, PromptBox promptBox)
    {
        var (_name, _password, _promptBox) = (name.text, password.text, promptBox);

        //检测输入框是否为空
        if (_name == string.Empty || _password == string.Empty)
        {
            //输入框为空提示
            _promptBox.ShowBox("输入框不可为空");
            return;
        }
        //检测是否不存在该账号
        if (!dataBase.IsName(_name))
        {
            //创建临时账号信息
            Account account = new Account()
            {
                Name = _name,
                Password = _password,
                Value = 0
            };
            //添加账号至游戏数据中
            dataBase.Add(account);
            //---更新游戏界面---//
            login.SetActive(false);
            erllon.SetActive(false);
            //注册成功提示
            _promptBox.ShowBox("注册成功");
            return;
        }
        //当账号存在时
        else
        {
            //账号存在提示
            _promptBox.ShowBox("当前账号已存在");
            return;
        }
    }

    /// <summary>
    /// 对话框显示
    /// </summary>
    /// <param name="promptBox">需要显示的对话框</param>
    /// <param name="body">对话框显示的内容</param>
    private void ShowBox(PromptBox promptBox, string body)
    {
        promptBox.ShowBox(body);
    }

    //--------------------按钮事件部分--------------------//

    /// <summary>
    /// 登录账号方法
    /// </summary>
    public void LoginAccount()
    {
        LoginAccount(loginName, loginPwd, promptBox);
    }

    /// <summary>
    /// 注册账号方法
    /// </summary>
    public void ErllonAccount()
    {
        ErllonAccount(erllonName, erllonPwd, promptBox);
    }

    /// <summary>
    /// 退出游戏方法
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// 退出登录方法
    /// </summary>
    public void QuitLogin()
    {
        //---将本地数据删除---//
        PlayerPrefs.SetString("csName", "null");
        PlayerPrefs.SetString("csPwd", "null");
        PlayerPrefs.SetInt("csValue", 0);
        //---更新游戏界面---//
        loginBefore.SetActive(true);
        loginAfter.SetActive(false);
        login.SetActive(false);
        erllon.SetActive(false);
    }

    /// <summary>
    /// 对话框显示
    /// </summary>
    /// <param name="body">对话框显示的内容</param>
    public void ShowBox(string body)
    {
        ShowBox(promptBox, body);
    }

    //--------------------管理类单例模式--------------------//

    private static LoginManager Singleton;

    [System.Obsolete]
    public static LoginManager Instance
    {
        get
        {
            if (Singleton != null)
                return Singleton;

            // 根据Unity的更新建议，使用新的查找方法
            Singleton = FindObjectOfType<LoginManager>();

            if (Singleton == null)
            {
                Singleton = new GameObject("loginManager").AddComponent<LoginManager>();
            }
            else
            {
                var items = FindObjectsOfType<LoginManager>();
                foreach (var item in items)
                {
                    if (item != Singleton)
                        Destroy(item.gameObject);
                }
            }
            return Singleton;
        }
    }

    // 注意：此处的 FindObjectOfType 和 FindObjectsOfType 方法需要替换为新的API调用
    // 实际的替换代码取决于你的Unity版本和官方文档的最新建议

}
