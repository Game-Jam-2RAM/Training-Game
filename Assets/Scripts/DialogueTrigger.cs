using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    [TextArea(2, 5)]
    public string[] linesToShow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.StartDialogueExternally(linesToShow);
            gameObject.SetActive(false); // Optional: disables the trigger after use
        }
    }
}
