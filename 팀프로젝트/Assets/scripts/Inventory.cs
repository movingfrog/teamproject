using UnityEngine;
using UnityEngine.UI;


public enum ItemName
{
    kit,
    gear,
}

public class Inventory : MonoBehaviour
{
    public GameObject UI;
    public GameObject[] UIs;
    public Text[] texts;
    private bool isTap = false;
    private ItemName IN;

    private void Start()
    {
        for (int i = 0; i < texts.Length; i++)
            texts[i].text = " ";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isTap)
            {
                if (PlayerItemContact.kits > 0)
                    texts[0].text = PlayerItemContact.kits.ToString();
                if(PlayerItemContact.gears > 0)
                    texts[1].text = PlayerItemContact.gears.ToString();
                    
                isTap = true;
                UI.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                isTap = false;
                UI.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    public void use()
    {
        switch (IN)
        {
            case ItemName.kit:
                if(PlayerItemContact.kits>0){
                    PlayerItemContact.kits--;
                    PlayerStats.HP += 50;
                    if (PlayerStats.HP > 100)
                        PlayerStats.HP = 100;
                    texts[0].text = PlayerItemContact.kits.ToString();
                    Debug.Log(PlayerStats.HP);
                    if (PlayerItemContact.kits >= 0)
                        texts[0].text = " ";
                }
                break;
            default:
                Debug.LogError("선택하지 않았습니다");
                break;
        }
    }
    public void gear()
    {
        for(int i = 0; i < UIs.Length; i++)
        {
            UIs[i].gameObject.SetActive(false);
        }
        IN = ItemName.gear;
    }
    public void kit()
    {
        for( int i = 0;i < UIs.Length;i++)
            UIs[i].gameObject.SetActive(true);
        IN = ItemName.kit;
    }
}