using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eliminator : MonoBehaviour
{
    public GameObject UI;
    public GameObject UI_Red;
    public SpawnParticles gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        UI.SetActive(true);
        if (other.tag == "D")
        {
            gameManager.destroyDCount += 0.5;
            gameManager.score += 2.5;
            gameManager.SetHUD();
            Destroy(other.gameObject);
            StartCoroutine("Destruction");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        UI.SetActive(false);
    }
    IEnumerator Destruction()
    {
        UI_Red.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        UI_Red.SetActive(false);
        UI.SetActive(false);
    }

}
