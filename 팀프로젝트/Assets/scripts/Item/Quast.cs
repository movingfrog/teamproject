using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quast : MonoBehaviour
{
    public GameObject Interaction;
    public GameObject Object;
    public Text interText;
    public bool IsPlayer;
    public static int stack;

    private void Awake()
    {
        Object.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsPlayer)
        {
            if(stack !< 20)
            {
                Object.SetActive(true);
                interText.text = (20-stack).ToString() + "개의 부품이 부족합니다";
                Invoke("wait", 5f);
            }
            else
            {
                Debug.Log("End");
            }
        }
    }
    public void wait()
    {
        Object.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            IsPlayer = true;
            Interaction.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("player"))
        {
            IsPlayer = false;
            Interaction?.SetActive(false);
        }
    }
}
