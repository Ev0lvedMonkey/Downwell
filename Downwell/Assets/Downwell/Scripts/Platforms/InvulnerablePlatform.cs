using UnityEngine;

public class InvulnerablePlatform : Platform
{
    protected override void SetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(Constants.GroundayerName);
    }
}
