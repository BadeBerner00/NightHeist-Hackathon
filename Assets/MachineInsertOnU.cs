using System.Collections;
using UnityEngine;

public class MachineInsertOnU : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Animator machineAnimator;
    [SerializeField] private Animator vaultAnimator;
    [SerializeField] private EndScreenFade endScreen;   // <-- drag your Canvas (with EndScreenFade) here

    [Header("Keys / Triggers")]
    [SerializeField] private KeyCode interactKey = KeyCode.U;
    [SerializeField] private string machineInsertTrigger = "Insert";
    [SerializeField] private string vaultOpenTrigger = "Open";

    [Header("Timing")]
    [SerializeField] private float insertDuration = 1.0f; // machine insert clip length
    [SerializeField] private float endScreenDelay = 0.2f; // small delay after vault trigger

    private bool playerInRange = false;
    private bool used = false;

    private void Update()
    {
        if (used) return;
        if (!playerInRange) return;

        if (Input.GetKeyDown(interactKey))
        {
            if (KeycardManager.Instance.collected < 3)
            {
                Debug.Log($"Need {3 - KeycardManager.Instance.collected} more keycards");
                return;
            }

            used = true;
            StartCoroutine(InsertThenOpen());
        }
    }

    private IEnumerator InsertThenOpen()
    {
        if (machineAnimator != null)
            machineAnimator.SetTrigger(machineInsertTrigger);

        yield return new WaitForSeconds(insertDuration);

        if (vaultAnimator != null)
            vaultAnimator.SetTrigger(vaultOpenTrigger);

        yield return new WaitForSeconds(endScreenDelay);

        if (endScreen != null)
            endScreen.ShowEndScreen();
        else
            Debug.LogWarning("EndScreenFade reference not set on MachineInsertOnU.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}
