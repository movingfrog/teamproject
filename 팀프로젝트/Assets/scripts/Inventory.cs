/*using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


public class ItemSlot
{
    public itemData item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    //public ItemSlotUI[] uidSlot;
    public ItemSlotUI[] uidSlot;
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform dropPosition;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public Text selectedItemName;
    public Text selectedItemDescription;
    public Text selectedItemStatName;
    public Text selectedItemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private int curEquipIndex;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uidSlot.Length];

        for (int i = 0; i < uidSlot.Length; i++)
        {
            //UI slot 초기화 하기
            slots[i] = new ItemSlot();
            uidSlot[i].index = i;
            uidSlot[i].Clear();
        }

        ClearSelctedItemWindow();
    }

    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        //인벤토리 키는 Key 이벤트
        if(context.phase == InputActionPhase.Started)
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke();
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(itemData item)
    {
        if(item.canStack)
        {
            //쌓일 수 있는 아이템일 경우 스택을 쌓아준다
            ItemSlot slotToStackTo = GetItemStack(item);
            if(slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        //없을 경우 빈칸에 아이템을 추가해준다
        ItemSlot emptySlot = GetEmptySlot();

        if(emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        //인벤토리에 빈칸이 업슬 경우 획득한 아이템 다시 버리기
        ThrowItem(item);
    }

    private void ThrowItem(itemData item)
    {
        //아이템 버리기
        Instiate(item.dropPerfab, dropPosition.position);
    }

    void UpdateUI()
    {
        //slots에 있는 아이템 데이터를 UI의 Slot 최신화 하기
        for (int i = 0;i<slots.Length;i++)
        {
            if (slots[i] != null)
                uidSlot[i].Set(slots[i]);
            else
                uidSlot[i].Clear();
        }
    }

    ItemSlot GetItemStack(itemData item)
    {
        //현재 선택한 아이템이 이미 슬롯에 있고, 아직 최대수량을 안 넘겼다면 해당 아이템이 위치한 슬로의 위치를 가져온다
        for(int i = 0;i<slots.Length; i++)
        {
            if (slots[i].item == item&& slots[i].quantity < item.maxStackAmount)
                return slots[i];
        }
    }

    ItemSlot GetEmptySlot()
    {
        //빈 슬롯 찾기
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        //선택한 슬롯에 아이템이 없을 경우 return
        if (slots[index].item == null)
            return;

        //선택한 아이템 정보 가져오기
        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        for(int i = 0; i < selectedItem.item.consumables.Length; i++)
        {

        }
    }
}*/