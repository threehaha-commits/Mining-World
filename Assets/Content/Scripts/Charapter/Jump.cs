using System.Collections;
using UnityEngine;

public class Jump
{
    public IEnumerator DoJump(Transform transform, AnimationCurve curve, float duration, float height, float _distanceToGround)
    {
        var expiredTime = 0f;
        var startPos = transform.position;
        var progressTime = 0f;
        while (progressTime < 1f)
        {
            expiredTime += Time.deltaTime;
            progressTime = expiredTime / duration;
            var value = curve.Evaluate(progressTime);
            transform.position = new Vector3(transform.position.x, startPos.y + value * height,
                transform.position.z);
            var isGround = RayHelper.IsGround(transform, _distanceToGround);
            if(isGround)
                yield break;
            yield return null;
        }
    }
}