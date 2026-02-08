using UnityEngine;

public class KeycardManager : MonoBehaviour
{
    public static KeycardManager Instance { get; private set; }

    public int totalRequired = 3;
    public int collected = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddCard(int amount = 1)
    {
        collected = Mathf.Clamp(collected + amount, 0, totalRequired);
        Debug.Log("Keycards: " + collected);
    }

    public bool HasAllCards() => collected >= totalRequired;
}
