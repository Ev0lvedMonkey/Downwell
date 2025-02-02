using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CharacterContainer : MonoBehaviour
{
    [SerializeField] private CharacterMover _characterMover;
    private void LateUpdate()
    {
        transform.position = new Vector3(
        transform.position.x,  
            _characterMover.transform.position.y, 
            transform.position.z   
        );
    }
}
