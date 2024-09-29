using UnityEngine;

namespace Extension
{
    public static class Extension
    {
        public static bool DotTest(this Transform transform, Transform otherTransform, Vector2 direction)
        {
            Vector2 getDirection = otherTransform.position - transform.position;
            return Vector2.Dot(direction.normalized, getDirection) > 0.25f;
        }
    }
}
