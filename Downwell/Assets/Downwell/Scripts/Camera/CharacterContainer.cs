using UnityEngine;

public class CharacterContainer : MonoBehaviour
{
    [SerializeField] private CharacterMover _characterMover;
    [SerializeField, Range(0f, 1f)] private float _lerpSpeed = 0.1f;

    private void OnValidate()
    {
        if (transform.childCount > 0)
            _characterMover = transform.GetChild(0).GetComponent<CharacterMover>();
    }

    private void Update()
    {
        if (transform.position.y > _characterMover.transform.position.y)
        {
            transform.position = new(
                transform.position.x,
                Mathf.Lerp(transform.position.y, _characterMover.transform.position.y, _lerpSpeed)
            );
        }
    }
}
