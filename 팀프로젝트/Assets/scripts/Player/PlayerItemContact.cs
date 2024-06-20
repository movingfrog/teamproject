using UnityEngine;

public class PlayerItemContact : MonoBehaviour
{
    public static GameObject game;
    public GameObject Interaction;
    private bool isItemContact = false;


    public void Awake()
    {
        Interaction.gameObject.SetActive(false);
        game = GetComponent<GameObject>();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("item") || collision.gameObject.CompareTag("RustyParts"))
        {
            game = collision.gameObject;
            isItemContact = true;
            Interaction.SetActive(true);
            Debug.Log(game);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("item")|| collision.gameObject.CompareTag("RustyParts"))
        {
            game = collision.gameObject;
            isItemContact = true;
            Interaction.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("item") || collision.gameObject.CompareTag("RustyParts"))
        {
            game = null;
            isItemContact = false;
            Interaction.SetActive(false);
            Debug.Log(game);
        }
    }
}
