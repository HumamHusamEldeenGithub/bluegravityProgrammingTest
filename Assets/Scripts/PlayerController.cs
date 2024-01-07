using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private List<ItemModel> items;

    [SerializeField]
    private float coins;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private ShopController lastVisitedShop;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        UIManager.instance.UpdatePlayerCoins(coins);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastVisitedShop = null;
    }
    void FixedUpdate()
    {
        // TODO : Move to MovementController
        MoveCharacter();
        if (Input.GetKey(KeyCode.E))
        {
            if (lastVisitedShop != null)
            {
                UIManager.instance.OpenShop();
            }
            else
            {
                UIManager.instance.CloseShop();
            }
        }

        if (Input.GetKey(KeyCode.I))
        {
            UIManager.instance.OpenInventory();
        }
    }

    void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        spriteRenderer.flipX = horizontalInput < 0 ? true : false;

        if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        movement.Normalize();

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<ShopController>(out lastVisitedShop);        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<ShopController>(out ShopController sh ))
        {
            lastVisitedShop = null;
            UIManager.instance.CloseShop();
        }
    }

    public void BuyItem(ItemModel item)
    {
        if (coins < item.price)
        {
            return;
        }

        if (lastVisitedShop != null &&  lastVisitedShop.SellItem(item))
        {
            coins -= item.price;
            items.Add(item);
            UIManager.instance.UpdatePlayerCoins(coins);
            UIManager.instance.UpdateShop(lastVisitedShop);
            UIManager.instance.UpdateInventory(items);
        }
    }
    public void SellItem(ItemModel item)
    {
        if (lastVisitedShop != null )
        {
            coins += item.price;
            lastVisitedShop.BuyItem(item);
            items.Remove(item);
            UIManager.instance.UpdatePlayerCoins(coins);
            UIManager.instance.UpdateShop(lastVisitedShop);
            UIManager.instance.UpdateInventory(items);
        }
    }

    public ShopController GetLastVisitedShop()
    {
        return lastVisitedShop;
    }

    public List<ItemModel> GetPlayerItems()
    {
        return items;
    }
}
