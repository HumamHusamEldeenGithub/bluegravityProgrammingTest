using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemPrice;
    [SerializeField]
    TextMeshProUGUI itemButtonText;

    private ItemModel item;
    private bool isShopItem;

    public void SetupUIItem(ItemModel item,bool isShopItem)
    {
        this.item = item;
        this.isShopItem = isShopItem;
        image.sprite = this.item.sprite;
        itemName.text = this.item.itemName;
        itemPrice.text = this.item.price.ToString()+"$";
        itemButtonText.text = this.isShopItem ? "Buy" : "Sell";
    }

    public void BuySellButton()
    {
        if (this.isShopItem)
        {
            PlayerController.instance.BuyItem(item);
        }
        else
        {
            PlayerController.instance.SellItem(item);
        }
    }
}
