using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public itemData item;

    public void OnInteract()
    {
        Inventory.instance.AddItem(item); //아이템 추가하기
        Destroy(item);
    }

}
