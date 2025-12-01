using UnityEngine;

public class PickupPromptController : MonoBehaviour
{
    public GameObject promptUI;

    private void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);
    }

    public void ShowPrompt()
    {
        promptUI.SetActive(true);
    }

    public void HidePrompt()
    {
        promptUI.SetActive(false);
    }
}
