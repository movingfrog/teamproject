using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public itemData item;

    public void OnInteract()
    {
        Inventory.instance.AddItem(item); //������ �߰��ϱ�
        Destroy(item);
    }

}
