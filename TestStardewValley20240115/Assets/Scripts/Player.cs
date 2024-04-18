using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    public ToolbarUI toolbarui;
    private Animator anim;
    private Vector2 direction = Vector2.zero;
    private bool isTalking = false; // 控制是否可以移动的变量

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // anim.SetTrigger("ishoe");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            return; // 如果正在对话中，不执行以下移动和动画相关代码
        }

        if (direction.magnitude > 0)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("horizontal", direction.x);
            anim.SetFloat("vertical", direction.y);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (toolbarui.GetselectedSlotUI() != null
            && toolbarui.GetselectedSlotUI().GetData().item.type == ItemType.Hoe
            && Input.GetKeyDown(KeyCode.Space))
        {
            hoeManager.Instance.Uptohoe(transform.position);
            print("hoeeeee");
            anim.SetTrigger("ishoe");
        }
    }

    private void FixedUpdate()
    {
        if (!isTalking) // 只有当不在对话中时，才处理输入和移动
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            direction = new Vector2(x, y);

            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pickable" && collision.gameObject.GetComponent<Pickable>().can_pick())
        {
            print(collision.gameObject.name);
            InventoryManager.Instance.AddToBackpack(collision.GetComponent<Pickable>().type);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("撞到了");
        if (collision.gameObject.tag == "Pickable" && collision.gameObject.GetComponent<Pickable>().can_pick())
        {
            print(collision.gameObject.name);
            InventoryManager.Instance.AddToBackpack(collision.gameObject.GetComponent<Pickable>().type);
            Destroy(collision.gameObject);
        }
    }

    public void ThrowItem(GameObject itemPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = GameObject.Instantiate(itemPrefab);
            Vector2 direction = (itemPrefab.GetComponent<Pickable>().isFence()) ? new Vector2(0, 1) : Random.insideUnitCircle.normalized * 1.5f;
            go.transform.position = transform.position + new Vector3(direction.x, direction.y, 0);
            go.GetComponent<Rigidbody2D>().AddForce(direction * 3);
        }
    }

    // 用于设置角色是否在对话中
    public void SetTalking(bool talking)
    {
        isTalking = talking;
    }
}
