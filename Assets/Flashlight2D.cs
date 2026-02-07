using UnityEngine;

public class Flashlight2D : MonoBehaviour
{
    public Transform pivot;  // FlashlightPivot

    // call this when your character faces left/right
    public void SetFacing(bool facingRight)
    {
        if (pivot == null) return;
        pivot.localScale = new Vector3(facingRight ? 1f : -1f, 1f, 1f);
    }
}
