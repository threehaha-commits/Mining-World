using System.Collections;
using UnityEngine;

public class Jump
{
    public IEnumerator DoJump(Transform transform, AnimationCurve curve, float duration, float height)
    {
        var expiredTime = 0f;
        var progress = 0f;
        var startPos = transform.position;
        var layerMask = LayerMask.GetMask("Mining");
        while (progress < 1)
        {
            expiredTime += Time.deltaTime;
            progress = expiredTime / duration;
            var value = curve.Evaluate(progress);
            transform.position = new Vector3(transform.position.x, startPos.y + (value * height),
                transform.position.z);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 10f, layerMask);
            if (hit.collider != null)
            {
                if (hit.distance < 1.09f)
                    progress = 1f;
            }
            yield return null;
        }
    }
}