using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public itemData item;
    public bool IsPlayerCon;

    private void Start()
    {
        IsPlayerCon = false;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && IsPlayerCon)
        {
            Debug.Log(item.icon);
            Destroy(gameObject);
            Inventory.instance.AddItem(item);   // �κ��丮�� ������ �߰��ϱ�
        }
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
            IsPlayerCon = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
            IsPlayerCon = false;
    }
}

