using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probe : MonoBehaviour
{
    public bool isStable;
    public bool haveBottomUnder;
    public bool havePlayerUnder;
    public bool haveParticleUnder;
    public GameObject gameManager;

    void Start()
    {
        isStable = false;
        haveBottomUnder = false;
        havePlayerUnder = false;
        haveParticleUnder = false;
        gameManager = this.transform.parent.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bottom")
        {
            isStable = true;
            haveBottomUnder = true;
        }
        else if (other.tag == "Player")
        {
            isStable = true;
            havePlayerUnder = true;
        }
        else if (other.tag == "R" || other.tag == "G" || other.tag == "B" || other.tag == "D")
        {
            isStable = true;
            haveParticleUnder = true;
            if (other.GetComponentInChildren<Probe>().havePlayerUnder && other.transform.position.y > 4.6f)
            {
                gameManager.GetComponent<SpawnParticles>().lose3 = true;
            }
            else if (other.GetComponentInChildren<Probe>().haveParticleUnder && 
                other.transform.position.y > 4.3f && other.transform.position.y < 4.6f) 
            {
                gameManager.GetComponent<SpawnParticles>().lose1 = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isStable = false;
        haveBottomUnder = false;
        havePlayerUnder = false;
        haveParticleUnder = false;
    }

}
