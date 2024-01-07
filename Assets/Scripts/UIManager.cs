using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    CanvasGroup shopPanelCanvasGroup;

    [SerializeField]
    Transform ShopScrollViewContent;

    [SerializeField]
    CanvasGroup InventoryPanelCanvasGroup;

    [SerializeField]
    Transform InventoryScrollViewContent;

    [SerializeField]
    TextMeshProUGUI CoinsText;

    [SerializeField]
    TextMeshProUGUI ShopName;

    [SerializeField]
    GameObject ItemPrefab;

    public static UIManager instance;

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

    public void UpdateShop(ShopController shopController)
    {
        ShopName.text = shopController.GetShopName();
        List<ItemModel> shopsItems = shopController.GetShopItems();
        for (int i = ShopScrollViewContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ShopScrollViewContent.GetChild(i).gameObject);
        }
        for (int i = 0; i < shopsItems.Count; i++)
        {
            GameObject newChild = Instantiate(ItemPrefab, transform.position, Quaternion.identity);
            newChild.GetComponent<UIItem>().SetupUIItem(shopsItems[i],true);
            newChild.transform.SetParent(ShopScrollViewContent);
        }
    }

    public void OpenShop()
    {
        shopPanelCanvasGroup.DOFade(1, 1);
        UpdateShop(PlayerController.instance.GetLastVisitedShop());
    }

    public void CloseShop()
    {
        shopPanelCanvasGroup.DOFade(0, 1);
    }

    public void UpdateInventory(List<ItemModel> playerItems)
    {
        for (int i = InventoryScrollViewContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(InventoryScrollViewContent.GetChild(i).gameObject);
        }
        for (int i = 0; i < playerItems.Count; i++)
        {
            GameObject newChild = Instantiate(ItemPrefab, transform.position, Quaternion.identity);
            newChild.GetComponent<UIItem>().SetupUIItem(playerItems[i],false);
            newChild.transform.SetParent(InventoryScrollViewContent);
        }
    }

    public void OpenInventory()
    {
        InventoryPanelCanvasGroup.gameObject.SetActive(true);
        UpdateInventory(PlayerController.instance.GetPlayerItems());
        InventoryPanelCanvasGroup.DOFade(1, 1);
    }

    public void CloseInventory()
    {
        InventoryPanelCanvasGroup.gameObject.SetActive(false);
        return;
    }

    public void UpdatePlayerCoins(float coins)
    {
        CoinsText.text = coins.ToString();
    }
}
