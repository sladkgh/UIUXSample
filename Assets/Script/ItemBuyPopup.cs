using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBuyPopup : MonoBehaviour {
    private Button OKButton;
    private Button CancleButton;
    private ShopWindow shopWindow;
    private TextMeshProUGUI text;

    private string ItemName;
    private void Awake() {
        var textTrans = transform.Find("Bg2/Content");
        text = textTrans.GetComponent<TextMeshProUGUI>();
        shopWindow = FindAnyObjectByType<ShopWindow>();
        
        var buttons = GetComponentsInChildren<Button>();

        foreach (var button in buttons) {
            if (button.name == "OKButton") {
                OKButton = button;
            }
            else if (button.name == "CancelButton") {
                CancleButton = button;
            }
        }
        
        OKButton.onClick.AddListener(OKAction);
        CancleButton.onClick.AddListener(CancelAction);
    }

    public void ShowPopup(string itemName) {
        this.ItemName = itemName;
        text.text = $"{itemName} 아이템을 구매 하시겠습니까?";
        
        gameObject.SetActive(true);
    }

    private void OKAction() {
        shopWindow.ItemkBuySuccess(ItemName);
        gameObject.SetActive(false);
    }

    private void CancelAction() {
        gameObject.SetActive(false);
    }
}
