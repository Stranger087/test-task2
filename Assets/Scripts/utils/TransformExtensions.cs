using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace utils
{
	public static class TransformExtensions
	{
		public static void RoundPosition(this Transform transform, bool includeChildren = true)
		{
			if(includeChildren)
			{
				transform.RoundPosition(false);

				for(var i = 0; i < transform.childCount; i++)
				{
					transform.GetChild(i).RoundPosition();
				}
			}
			else
			{
				var position = transform.localPosition;
				transform.localPosition = new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z));
			}
		}

		public static void SetTransformAfterOrBefore(this Transform currentTransform, Transform target, bool isBefore)
		{
			if(currentTransform.parent != target.parent)
			{
				currentTransform.SetParent(target.parent);
			}

			var targetSiblingIndex = target.GetSiblingIndex();
			var needSiblingIndex = isBefore ? targetSiblingIndex - 1 : targetSiblingIndex + 1;
			var currenTransformSiblingIndex = currentTransform.GetSiblingIndex();

			if(needSiblingIndex != currenTransformSiblingIndex)
			{
				if(isBefore)
				{
					currentTransform.SetSiblingIndex(currenTransformSiblingIndex > targetSiblingIndex ? targetSiblingIndex : needSiblingIndex);
				}
				else
				{
					currentTransform.SetSiblingIndex(currenTransformSiblingIndex < targetSiblingIndex ? targetSiblingIndex : needSiblingIndex);
				}
			}
		}

		public static T CreateGameObjectWithComponent<T>(this Transform parent, string name) where T : Component
		{
			var go = new GameObject(name);
			var comp = go.AddComponent<T>();
			comp.transform.parent = parent;
			return comp;
		}

		public static void DestroyChildren(this Transform parent, bool immediate)
		{
			for(var i = parent.childCount - 1; i >= 0; i--)
			{
				var child = parent.GetChild(i).gameObject;
				if(immediate)
				{
					Object.DestroyImmediate(child);
				}
				else
				{
					Object.Destroy(child);
				}
			}
		}

		public static List<Transform> GetAllChildren(this Transform parent)
		{
			var result = new List<Transform>();
			for(var i = parent.childCount - 1; i >= 0; i--)
			{
				result.Add(parent.GetChild(i));
			}
			return result;
		}

		public static Transform SetX(this Transform t, float x)
		{
			t.position = new Vector3(x, t.position.y, t.position.z);
			return t;
		}

		public static Transform SetY(this Transform t, float y)
		{
			t.position = new Vector3(t.position.x, y, t.position.z);
			return t;
		}

		public static void SetTo(this Transform transform, Transform to)
		{
			transform.position = to.position;
			transform.rotation = to.rotation;
		}

		public static void ResetLocal(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localEulerAngles = Vector3.zero;
			transform.localScale = Vector3.one;
		}

		public static Transform SetZ(this Transform t, float z)
		{
			t.position = new Vector3(t.position.x, t.position.y, z);
			return t;
		}

		public static Transform SetLocalX(this Transform t, float x)
		{
			t.localPosition = new Vector3(x, t.localPosition.y, t.localPosition.z);
			return t;
		}

		public static Transform SetLocalY(this Transform t, float y)
		{
			t.localPosition = new Vector3(t.localPosition.x, y, t.localPosition.z);
			return t;
		}

		public static Transform SetLocalZ(this Transform t, float z)
		{
			t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, z);
			return t;
		}

		public static string GetFullPathInHierarchy(this Transform transform)
		{
			if(transform == null)
			{
				return string.Empty;
			}

			var path = transform.name;
			var current = transform.parent;

			while(current != null)
			{
				path = current.name + "/" + path;
				current = current.parent;
			}

			return path;
		}

		public static List<T> GetComponentsInChildrenOrdered<T>(this Transform transform, List<T> result = null) where T : Component
		{
			if(result == null)
			{
				result = new List<T>();
			}

			var component = transform.GetComponent<T>();
			if(component != null)
			{
				result.Add(component);
			}

			for(var i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).GetComponentsInChildrenOrdered(result);
			}

			return result;
		}

		public static List<T> GetComponentsInSceneOrdered<T>(this Scene scene) where T : Component
		{
			var result = new List<T>();

			// Get all root GameObjects in the scene
			var rootObjects = scene.GetRootGameObjects();
			foreach(var root in rootObjects)
			{
				root.transform.GetComponentsInChildrenOrdered(result);
			}

			return result;
		}
	}
}
