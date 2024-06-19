using UnityEngine;

public class PlayerItemContact : MonoBehaviour
{
    public static GameObject game;
    private bool isItemContact = false;


    public void Awake()
    {
        game = GetComponent<GameObject>();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            game = collision.gameObject;
            isItemContact = true;
            Debug.Log(game);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            game = collision.gameObject;
            isItemContact = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            game = null;
            isItemContact = false;
            Debug.Log(game);
        }
    }
}
