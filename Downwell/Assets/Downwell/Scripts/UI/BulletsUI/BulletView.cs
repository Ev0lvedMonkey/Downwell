using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class BulletView : MonoBehaviour
{
    public void SetSize(float height)
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }
}

