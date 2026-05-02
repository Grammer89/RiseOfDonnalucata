using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    public GameObject visualCue;
    [Header("Ink Json")]
    public TextAsset inkJson;
    public UnityEvent onDialogueEnd;

    private bool playerInRange;
    public bool PlayerInRange => playerInRange;
    void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && DialogueManager.Instance.DialogueIsPlaying == false)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Si attiva la conversazione di " + inkJson.name);
                DialogueManager.Instance.EnterDialogueMode(inkJson);
            }

        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Utility.PlayerTag))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Utility.PlayerTag))
        {
            playerInRange = false;
        }
    }
}

