using UnityEngine;

public class VaultDoor : MonoBehaviour
{
    [SerializeField] private Animator vaultAnimator;
    [SerializeField] private string openTriggerName = "Open";

    private bool opened = false;

    public void Open()
    {
        if (opened) return;
        opened = true;

        if (vaultAnimator != null)
            vaultAnimator.SetTrigger(openTriggerName);
    }
}
