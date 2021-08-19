using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        string playerCurrentState = this.gameObject.GetComponentInParent<playerStats>().playerCurrentStatus;

        switch (playerCurrentState)
        {
            case "Hemorragia":
                SetBleedingEmoteOn();
                break;

            case "Veneno":
                {
                    SetPoisonedEmoteOn();
                    break;
                }

            case "Infectado":

                {
                    SetInfectedEmoteOn();
                    break;
                }

            default:
                DisableAllEmotes();
                break;
        }

    }


    void SetBleedingEmoteOn()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<Animator>().SetBool("Bleeding", true);
    }

    void SetInfectedEmoteOn()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<Animator>().SetBool("Infected", true);
    }

    void SetPoisonedEmoteOn()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<Animator>().SetBool("Poisoned", true);
    }

    void DisableAllEmotes()
    {
        this.gameObject.GetComponent<Animator>().SetBool("Bleeding", false);
        this.gameObject.GetComponent<Animator>().SetBool("Infected", false);
        this.gameObject.GetComponent<Animator>().SetBool("Poisoned", false);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

}
