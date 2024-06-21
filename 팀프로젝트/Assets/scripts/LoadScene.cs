using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Image Menu;

    PlayerHealth playerHealth;
    public GameObject Player;

    private void Start()
    {
        playerHealth = Player.GetComponent<PlayerHealth>(); 
        Debug.Log("PlayerHealth 컴포넌트");
        Debug.Log(playerHealth);
        Debug.Log(Player);
    }

    public void PlayBtn()
    {
        SceneManager.LoadScene("Play");
    }

    public void ExitBtn()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void MainBtn()
    {
        SceneManager.LoadScene("Main");
    }

    public void RespawBtn()
    {
        Debug.Log("리스폰 버튼이 눌림");
        Debug.Log(playerHealth);
        if (playerHealth != null)
        {
            Debug.Log("리스폰");
            playerHealth.Respawn();
        }
    }

    

    public void MenuBtn()
    {
        Menu.gameObject.SetActive(true);
    }
}
