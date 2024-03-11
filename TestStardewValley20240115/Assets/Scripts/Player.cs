using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;

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

        
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        direction = new Vector2(x, y);

        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pickable")
        {
            InventoryManager.Instance.AddToBackpack(collision.GetComponent<Pickable>().type);
            Destroy(collision.gameObject);
        }
    }

    public void ThrowItem(GameObject itemPrefab,int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject go = GameObject.Instantiate(itemPrefab);
            Vector2 direction = Random.insideUnitCircle.normalized * 1.5f;
            go.transform.position = transform.position + new Vector3(direction.x, direction.y, 0);
            go.GetComponent<Rigidbody2D>().AddForce(direction*3);
        }
    }
}