using UnityEngine;

public static class RayHelper
{
    public static float DistanceToGround(Transform transform)
    {
        var offset = 0.01f; //Небольшое отклонение дистанции для точного срабатываения условия IsGround => hit.distance <= distance
        var value = 2f; //Берем любое значение выше 1.09f, тк от игрока до земли ~1.09f
        var layerMask = LayerMask.GetMask("Mining");//тк землей считаются кубы со слоем Mining
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, value, layerMask);
        return hit.distance +offset;
    }
    
    public static bool IsGround(Transform transform, float distance)
    {
        var layerMask = LayerMask.GetMask("Mining");//тк землей считаются кубы со слоем Mining
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, distance, layerMask);
        if (hit.collider != null)
        {
            if (hit.distance <= distance)
                return true;
        }
        return false;
    }
}