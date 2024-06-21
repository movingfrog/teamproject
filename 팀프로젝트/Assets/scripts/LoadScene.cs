using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        Debug.Log("PlayerHealth ������Ʈ");
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
        if (playerHealth != null)
        {
            Debug.Log("������");
            playerHealth.Respawn();
        }
    }
}
