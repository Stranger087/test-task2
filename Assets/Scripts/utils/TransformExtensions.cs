using UnityEngine;

namespace utils
{
    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform parent, bool immediate)
        {
            for (var i = parent.childCount - 1; i >= 0; i--)
            {
                var child = parent.GetChild(i).gameObject;
                if (immediate)
                    Object.DestroyImmediate(child);
                else
                    Object.Destroy(child);
            }
        }
    }
}