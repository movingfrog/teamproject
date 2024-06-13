using UnityEngine;

public class PlayerItemContact : MonoBehaviour
{
    public GameObject game;
    private bool isItemContact = false;
    public static int gears;
    public static int kits;
    public static int k;


    public void Start()
    {
        //Instantiate(game);
        k = 0;
        game = GetComponent<GameObject>();
    }

    public void Update()
    {
        if (isItemContact && Input.GetKeyDown(KeyCode.F))
        {
            switch (game.gameObject.tag)
            {
                case "kit":
                    kits++;
                    break;
                case "gear":
                    gears++;
                    break;
            }
            k++;
            Destroy(game);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kit"||collision.gameObject.tag == "gear")
        {
            game = collision.gameObject;
            isItemContact = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kit" || collision.gameObject.tag == "gear")
        {
            game = null;
            isItemContact = false;
        }
    }
}
