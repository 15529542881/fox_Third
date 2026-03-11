using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SimpleDialogueSystem;

public class NPCCollider : MonoBehaviour
{
    // Start is called before the first frame update
    bool isin = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isin)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (name == "Bear")
                {
                    isin = false;
                    transform.GetChild(0).gameObject.SetActive(false);
                    int bearInt = GameManager.Instance.planState[0];
                    switch (bearInt)
                    {
                        case 1: GameManager.Instance.talkSystem.ShowDialogue(DialogueType.Bear_Task); GameManager.Instance.planState[0] = 2; break;
                        case 2: GameManager.Instance.talkSystem.ShowDialogue(DialogueType.Bear_Doing);/* GameManager.Instance.planState[0] = 3;*/ break;
                        case 3: GameManager.Instance.talkSystem.ShowDialogue(DialogueType.Bear_Complete); GetComponent<Collider>().enabled = false; break;
                    }
                }
                else if (name == "Rabbit")
                {
                    isin = false;
                    transform.GetChild(0).gameObject.SetActive(false);
                    int rabbitInt = GameManager.Instance.planState[1];
                    switch (rabbitInt)
                    {
                        case 1: GameManager.Instance.talkSystem.ShowDialogue(DialogueType.Rabbit_Task); GameManager.Instance.planState[1] = 2; break;
                        case 2: GameManager.Instance.talkSystem.ShowDialogue(DialogueType.Rabbit_Doing);/* GameManager.Instance.planState[0] = 3;*/ break;
                        case 3: GameManager.Instance.talkSystem.ShowDialogue(DialogueType.Rabbit_Complete); GetComponent<Collider>().enabled = false; break;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Animal")
        {
            isin = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Animal")
        {
            isin = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
