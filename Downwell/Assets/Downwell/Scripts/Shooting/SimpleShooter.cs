using UnityEngine;

public class SimpleShooter : BaseShooter
{
    public override void Shot()
    {
        base.Shot();
        Debug.Log($"Simple shooter shot");
    }
}

