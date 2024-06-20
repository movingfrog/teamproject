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
            if (gameObject.CompareTag("RustyParts"))
            {
                Quast.stack++;
            }
            Destroy(gameObject);
            Inventory.instance.AddItem(item);   // 인벤토리에 아이템 추가하기
        }
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
            IsPlayerCon = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("player")&&PlayerItemContact.game == this)
        {
            IsPlayerCon = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
            IsPlayerCon = false;
    }
}

