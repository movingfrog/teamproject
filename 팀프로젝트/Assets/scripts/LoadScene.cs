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
        if (Player !=  null)
        {
            playerHealth = Player.GetComponent<PlayerHealth>();
            Debug.Log("PlayerHealth ������Ʈ");
        }
        else
        {
            Debug.Log("Player ������Ʈ�� null");
        }
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
        Debug.Log("������ ��ư�� ����");
        Debug.Log(playerHealth);
        if (playerHealth != null)
        {
            Debug.Log("������");
            playerHealth.Respawn();
        }
    }

    

    
}
