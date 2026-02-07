using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBusted : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] MonoBehaviour movementScript;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;

    [Header("UI")]
    [SerializeField] GameObject bustedUI;
    [SerializeField] KeyCode restartKey = KeyCode.R;

    [Header("Anim")]
    [SerializeField] string caughtTrigger = "Caught";
    [SerializeField] float freezeDelay = 0.5f; // let the animation play a bit first

    bool busted;

    void Awake()
    {
        if (!movementScript) movementScript = GetComponent<PlayerController2D>();
        if (!rb) rb = GetComponent<Rigidbody2D>();
        if (!anim) anim = GetComponent<Animator>();
        if (bustedUI) bustedUI.SetActive(false);
    }

    public void Busted()
    {
        if (busted) return;
        busted = true;

        if (movementScript) movementScript.enabled = false;
        if (rb)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        // trigger anim (Animator Update Mode should be Unscaled Time)
        if (anim) anim.SetTrigger(caughtTrigger);

        StartCoroutine(FreezeAfterDelay());
    }

    IEnumerator FreezeAfterDelay()
    {
        yield return new WaitForSecondsRealtime(freezeDelay); // IMPORTANT
        Time.timeScale = 0f;
        if (bustedUI) bustedUI.SetActive(true);
    }

    void Update()
    {
        if (!busted) return;

        if (Input.GetKeyDown(restartKey))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnDisable()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
    }
}
