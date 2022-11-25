using System.Collections;
using UnityEngine;

public class Jump
{
    public IEnumerator DoJump(Rigidbody2D r2d, float power, float _distanceToGround)
    {
        while (RayHelper.IsGround(r2d.transform, _distanceToGround))
        {
            r2d.AddForce(Vector2.up * power, ForceMode2D.Impulse);
            yield return null;
        }
    }
}