
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform fill;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private bool isInitialised;

    void Initialise()
    {
        if (fill == null)
            return;

        originalScale = fill.localScale;
        originalPosition = fill.localPosition;
        isInitialised = true;

        Debug.Log("Health bar initialised: " + originalScale);
    }

    public void UpdateHealth(float healthPercent)
    {
        if (fill == null)
            return;

        if (!isInitialised)
            Initialise();

        healthPercent = Mathf.Clamp01(healthPercent);

        fill.localScale = new Vector3(
            originalScale.x * healthPercent,
            originalScale.y,
            originalScale.z
        );

        fill.localPosition = new Vector3(
            originalPosition.x - (originalScale.x - fill.localScale.x) / 2f,
            originalPosition.y,
            originalPosition.z
        );
    }
}
