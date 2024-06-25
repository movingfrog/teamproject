using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using System.Linq;



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

    public bool IsOnInventory = false;

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

        ClearSelectItemWindow();
    }

    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        //인벤토리 키는 Key 이벤트

        if (context.phase == InputActionPhase.Started)

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

        if (item.canStack)
        {
            //쌓일 수 있는 아이템일 경우 스택을 쌓아준다
            ItemSlot slotToStackTo = GetItemStack(item);
        Debug.Log(slotToStackTo);

            if (slotToStackTo != null)

            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        //없을 경우 빈칸에 아이템을 추가해준다
        ItemSlot emptySlot = GetEmptySlot();


        Debug.Log(emptySlot);
        if (emptySlot != null)

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

        Debug.Log("Throw");

        Instantiate(item.dropPerfab, dropPosition.position, Quaternion.Euler(Vector3.zero));
    }

    void UpdateUI()
    {

        Debug.Log(slots.Count());
        //for (int i = 0; i < slots.Length; i++)
        //{
        //    Debug.Log(slots[i]);
        //    Debug.Log(slots[i].item);
        //    Debug.Log(slots[i].item.icon);
        //    Debug.Log(slots[i].item.icon.name);
        //}
            //slots에 있는 아이템 데이터를 UI의 Slot 최신화 하기
            for (int i = 0; i < slots.Count(); i++)
            {
                 if (slots[i].item != null)
                 {
                      Debug.Log(slots[i]);
                       Debug.Log(slots[i].item);

                     Debug.Log(slots[i].item.icon);

                      uidSlot[i].Set(slots[i]);

                 }
                  else
                         uidSlot[i].Clear();
            }

    }

    ItemSlot GetItemStack(itemData item)
    {
        //현재 선택한 아이템이 이미 슬롯에 있고, 아직 최대수량을 안 넘겼다면 해당 아이템이 위치한 슬로의 위치를 가져온다

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)

                return slots[i];
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        //빈 슬롯 찾기

        for (int i = 0; i < slots.Length; i++)
        {

            if (slots[i].item == null)
            {

                Debug.Log("asdf");
                return slots[i];
            }

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


        for (int i = 0; i < selectedItem.item.consumables.Length; i++)

        {
            //먹을 수 있는 아이템일 경우 채워주는 체력을 UI 상에 표시해주기 위한 코드
            selectedItemStatName.text += selectedItem.item.consumables[i].tyep.ToString() + "\n";
            selectedItemStatValue.text += selectedItem.item.consumables[i].value.ToString() + "\n";
        }

        //아이템 타입을 체크하여 버튼을 활성화
        useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uidSlot[index].equipped);
        unEquipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uidSlot[index].equipped);
        dropButton.SetActive(true);
    }

    private void ClearSelectItemWindow()
    {
        //아이템 초기화
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;


        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnUseButton()
    {
        //아이템타입이 사용 가능할 경우

        if (selectedItem.item.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.item.consumables.Length; i++)

            {
                switch (selectedItem.item.consumables[i].tyep)
                {
                    //consumable 타입에 따라 Heal과 Eat
                    case ConsumableType.Health:
                        break;
                }
            }
        }
        //사용한 아이템 없애기
        RemoveSelectedItem();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))

        {
            if (!IsOnInventory)
            {
                IsOnInventory = true;
                Time.timeScale = 0;
                inventoryWindow.SetActive(true);
            }
            else
            {
                IsOnInventory = false;
                Time.timeScale = 1.0f;
                inventoryWindow.SetActive(false);
            }
        }
    }

    public void OnEquipButton()
    {

    }
    void UnEquip(int index)
    {

    }
    public void OnUnEquipButton()
    {

    }
    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelectedItem();
    }

    private void RemoveSelectedItem()
    {
        selectedItem.quantity--;

        //아이템의 남은 수량이 0이 되면

        if (selectedItem.quantity <= 0)

        {
            //만약 버린 아이템이 장착중인 아이템일 경우 해제시키기
            if (uidSlot[selectedItemIndex].enabled)
                UnEquip(selectedItemIndex);
            //아이템 제거 및 UI 에서도 아이템 정보 지우기
            selectedItem.item = null;
            ClearSelectItemWindow();
        }
        UpdateUI();
    }

    public void RemoveItem(itemData item)
    {

    }

    public bool HasItems(itemData item, int quantity)
    {
        return false;
    }
}