using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    string ShopName;
    [SerializeField]
    List<ItemModel> Items;
    [SerializeField]
    GameObject CoinIcon;
    [SerializeField]
    GameObject OpenWrapper;

    public string GetShopName()
    {
        return ShopName;
    }
    public bool SellItem(ItemModel item)
    {
       return Items.Remove(item);
    }

    public void BuyItem(ItemModel item)
    {
        Items.Add(item);
    }

    public List<ItemModel> GetShopItems()
    {
        return Items;
    }

    public void ShowShopSubChilds(bool show)
    {
        CoinIcon.SetActive(show);
        OpenWrapper.SetActive(show);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ShowShopSubChilds(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ShowShopSubChilds(false);
    }
}
