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
            //UI slot �ʱ�ȭ �ϱ�
            slots[i] = new ItemSlot();
            uidSlot[i].index = i;
            uidSlot[i].Clear();
        }

        ClearSelectItemWindow();
    }

    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        //�κ��丮 Ű�� Key �̺�Ʈ
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
            //���� �� �ִ� �������� ��� ������ �׾��ش�
            ItemSlot slotToStackTo = GetItemStack(item);
        Debug.Log(slotToStackTo);

            if (slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        //���� ��� ��ĭ�� �������� �߰����ش�
        ItemSlot emptySlot = GetEmptySlot();

        Debug.Log(emptySlot);
        if (emptySlot != null)
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
            //slots�� �ִ� ������ �����͸� UI�� Slot �ֽ�ȭ �ϱ�
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
        //���� ������ �������� �̹� ���Կ� �ְ�, ���� �ִ������ �� �Ѱ�ٸ� �ش� �������� ��ġ�� ������ ��ġ�� �����´�
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
                return slots[i];
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        //�� ���� ã��
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

        for (int i = 0; i < selectedItem.item.consumables.Length; i++)
        {
            //���� �� �ִ� �������� ��� ä���ִ� ü���� UI �� ǥ�����ֱ� ���� �ڵ�
            selectedItemStatName.text += selectedItem.item.consumables[i].tyep.ToString() + "\n";
            selectedItemStatValue.text += selectedItem.item.consumables[i].value.ToString() + "\n";
        }

        //������ Ÿ���� üũ�Ͽ� ��ư�� Ȱ��ȭ
        useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uidSlot[index].equipped);
        unEquipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uidSlot[index].equipped);
        dropButton.SetActive(true);
    }

    private void ClearSelectItemWindow()
    {
        //������ �ʱ�ȭ
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
        //������Ÿ���� ��� ������ ���
        if (selectedItem.item.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.item.consumables.Length; i++)
            {
                switch (selectedItem.item.consumables[i].tyep)
                {
                    //consumable Ÿ�Կ� ���� Heal�� Eat
                    case ConsumableType.Health:
                        break;
                }
            }
        }
        //����� ������ ���ֱ�
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

        //�������� ���� ������ 0�� �Ǹ�
        if (selectedItem.quantity <= 0)
        {
            //���� ���� �������� �������� �������� ��� ������Ű��
            if (uidSlot[selectedItemIndex].enabled)
                UnEquip(selectedItemIndex);
            //������ ���� �� UI ������ ������ ���� �����
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