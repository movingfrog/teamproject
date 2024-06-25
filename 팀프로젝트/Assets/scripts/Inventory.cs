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
            //UI slot �ʱ�ȭ �ϱ�
            slots[i] = new ItemSlot();
            uidSlot[i].index = i;
            uidSlot[i].Clear();
        }

        ClearSelctedItemWindow();
    }

    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        //�κ��丮 Ű�� Key �̺�Ʈ
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
            //���� �� �ִ� �������� ��� ������ �׾��ش�
            ItemSlot slotToStackTo = GetItemStack(item);
            if(slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        //���� ��� ��ĭ�� �������� �߰����ش�
        ItemSlot emptySlot = GetEmptySlot();

        if(emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        //�κ��丮�� ��ĭ�� ���� ��� ȹ���� ������ �ٽ� ������
        ThrowItem(item);
    }

    private void ThrowItem(itemData item)
    {
        //������ ������
        Instiate(item.dropPerfab, dropPosition.position);
    }

    void UpdateUI()
    {
        //slots�� �ִ� ������ �����͸� UI�� Slot �ֽ�ȭ �ϱ�
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
        //���� ������ �������� �̹� ���Կ� �ְ�, ���� �ִ������ �� �Ѱ�ٸ� �ش� �������� ��ġ�� ������ ��ġ�� �����´�
        for(int i = 0;i<slots.Length; i++)
        {
            if (slots[i].item == item&& slots[i].quantity < item.maxStackAmount)
                return slots[i];
        }
    }

    ItemSlot GetEmptySlot()
    {
        //�� ���� ã��
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    public void SelectItem(int index)
    {
        //������ ���Կ� �������� ���� ��� return
        if (slots[index].item == null)
            return;

        //������ ������ ���� ��������
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