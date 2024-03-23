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
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(direction.magnitude > 0)
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
            anim.SetTrigger("ishoe");
        }
   
    }
    /*测试修改代码aaaa*/
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        direction = new Vector2(x, y);

        transform.Translate(direction * speed * Time.deltaTime);
    }
    //触发trigger类型碰撞捡起物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pickable"&&collision.gameObject.GetComponent<Pickable>().can_pick())
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

    public void ThrowItem(GameObject itemPrefab,int count)
    {
        if (itemPrefab.GetComponent<Pickable>().isFence())
        {
            for (int i = 0; i < count; i++)
            {
                GameObject go = GameObject.Instantiate(itemPrefab);
                Vector2 direction = new Vector2(0,1);
                go.transform.position = transform.position + new Vector3(direction.x, direction.y, 0);
                go.GetComponent<Rigidbody2D>().AddForce(direction * 3);
            }
        }
        else {
            for (int i = 0; i < count; i++)
            {
                GameObject go = GameObject.Instantiate(itemPrefab);
                Vector2 direction = Random.insideUnitCircle.normalized * 1.5f;
                go.transform.position = transform.position + new Vector3(direction.x, direction.y, 0);
                go.GetComponent<Rigidbody2D>().AddForce(direction * 3);
            }
        }
    }
}
