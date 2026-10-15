
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform fill;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = fill.localScale;
    }

    public void UpdateHealth(float healthPercent)
    {
        fill.localScale = new Vector3(
            originalScale.x * healthPercent,
            originalScale.y,
            originalScale.z
        );

        fill.localPosition = new Vector3(
            -(originalScale.x - fill.localScale.x) / 2f,
            0,
            -0.01f
        );
    }
}
