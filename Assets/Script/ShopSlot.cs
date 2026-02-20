using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour {
    public Image ItemIcon;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemContent;
    public Button buyButton;

    private ItemData itemData;

    private void Awake() {
        buyButton.onClick.AddListener(ItemBuy);
    }

    public void SetShopSlot(ItemData data, Sprite sprite) {
        itemData = data;
        
        ItemIcon.sprite = sprite;
        ItemName.text = data.ItemName;
        ItemContent.text = data.ItemContent;
    }

    public bool SlotRemoveCondition(string itemName) {
        return itemData.ItemName == itemName;
    }

    private void ItemBuy() {

        
        var popups = Resources.FindObjectsOfTypeAll<ItemBuyPopup>();


        ItemBuyPopup popup = null;

        if (popups != null && popups.Length > 0) {
            popup = popups[0];
        }
        else {
            var prefab = Instantiate( Resources.Load("Prefab/ItemBuyPopup") as GameObject);
            popup = prefab.GetComponent<ItemBuyPopup>();
        }
        
        popup.ShowPopup(itemData.ItemName);
    }

}
