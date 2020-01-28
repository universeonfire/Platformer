using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    private int currentLevel = SceneManager.GetActiveScene().buildIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        SceneManager.LoadScene(currentLevel + 1);
        yield return new WaitForSeconds(1f);

    }
}
