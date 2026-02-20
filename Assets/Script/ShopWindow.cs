using System;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.U2D;


public class ItemData {
    public string spriteName;
    public string ItemName;
    public string ItemContent;
}

public class ShopWindow : MonoBehaviour {

    private readonly List<ItemData> itemDatas = new List<ItemData>() {
        new ItemData() { spriteName= "alien", ItemName = "외계인", ItemContent = "우주에서 왔습니다."},
        new ItemData() { spriteName= "tv", ItemName = "텔레비전", ItemContent = "냉장고를 부탁해 채널 고정!"},
        new ItemData() { spriteName= "skull", ItemName = "해골", ItemContent = "사람 머리에 있는 두개골. 매우 단단하다"},
        new ItemData() { spriteName= "rocket", ItemName = "로켓", ItemContent = "멀리 날라갈 수 있어요."},
        new ItemData() { spriteName= "home", ItemName = "집", ItemContent = "강남 서초동 아파트에요"},
        new ItemData() { spriteName= "mushroom", ItemName = "버섯", ItemContent = "둘이 먹다 둘다 죽어도 몰라요."},
    };

    private ShopSlot shopSlot;
    private List<ShopSlot> shopSlots = new List<ShopSlot>();

    private RectTransform slotRoot;
    private SpriteAtlas atlas;


    private List<string> buyItemNames = new List<string>();

    private void Awake() {
        shopSlot = GetComponentInChildren<ShopSlot>();

        var slotRootTrans = transform.Find("ScrollView/Viewport/Content");
        slotRoot = slotRootTrans.GetComponent<RectTransform>();
        atlas = Resources.Load<SpriteAtlas>("Atlas/Common");

        shopSlots.Clear();
        shopSlots.Add(shopSlot);
        
        buyItemNames.Clear();
    }

    private void Start() {
        for (int index = 0; index < itemDatas.Count; index++) {
            var itemData = itemDatas[index];
            
            var sprite = atlas.GetSprite(itemData.spriteName);

            ShopSlot slot = null;

            if (index < shopSlots.Count) {
                slot = shopSlots[index]; 
            }
            else {
                slot = Instantiate(shopSlot, slotRoot);
                shopSlots.Add(slot);
            }
            
            slot.SetShopSlot(itemData, sprite);
        }
    }

    public void ItemkBuySuccess(string itemName) {
        for (int index = shopSlots.Count - 1; index >= 0; index--) {

            if (shopSlots[index].SlotRemoveCondition(itemName)) {

                var slot = shopSlots[index];
                slot.transform.parent = null;
                shopSlots.RemoveAt(index);
                Destroy(slot.gameObject);
                break;
            }
        }
        
    }

}

