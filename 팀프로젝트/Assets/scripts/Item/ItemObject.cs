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
        if (Input.GetKeyDown(KeyCode.F) && IsPlayerCon)
        {
            Debug.Log(item.icon);
            Inventory.instance.AddItem(item);   // 인벤토리에 아이템 추가하기
            Destroy(gameObject);
        }
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player"&&PlayerItemContact.game == null)
            IsPlayerCon = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player"&&PlayerItemContact.game != null)
            IsPlayerCon = false;
    }
}

