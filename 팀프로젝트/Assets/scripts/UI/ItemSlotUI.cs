
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    public Text quatityText;
    private ItemSlot curSlot;
    private Outline outline;

    public int index;
    public bool equipped;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        // 장착한 아이템이 있으면 outline을 켜준다.
        outline.enabled = equipped;
    }

    // 아이템 Slot 정보 전달
    public void Set(ItemSlot slot)
    {
        Debug.Log(slot);
        Debug.Log(slot.item);
        Debug.Log(slot.item.icon);
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        Debug.Log(icon.sprite.name);
        quatityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;

        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }

    public void OnButtonClick()
    {
        Inventory.instance.SelectItem(index);
    }
}