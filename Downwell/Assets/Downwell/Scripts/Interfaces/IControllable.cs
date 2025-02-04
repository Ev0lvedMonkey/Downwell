using UnityEngine;

public interface IControllable 
{
    public void Move(Vector2 direction);
    public void Jump(float force = 0);

    public bool IsOnTheGround();


}
