using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E7 RID: 487
public static class TransformHelpers
{
	// Token: 0x06000D53 RID: 3411 RVA: 0x00033AE0 File Offset: 0x00031CE0
	// Note: this type is marked as 'beforefieldinit'.
	static TransformHelpers()
	{
		Vector2[] array = new Vector2[4];
		int num = 0;
		Vector2 vector = default(Vector2);
		vector.y = -1000f;
		array[num] = vector;
		int num2 = 1;
		Vector2 vector2 = default(Vector2);
		vector2.x = 5f;
		vector2.y = -1000f;
		array[num2] = vector2;
		int num3 = 2;
		Vector2 vector3 = default(Vector2);
		vector3.x = 30f;
		vector3.y = -2000f;
		array[num3] = vector3;
		int num4 = 3;
		Vector2 vector4 = default(Vector2);
		vector4.x = 200f;
		vector4.y = -4000f;
		array[num4] = vector4;
		TransformHelpers.upHeightTests = array;
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x00033BA0 File Offset: 0x00031DA0
	public static void SetLocalPositionY(this Transform transform, float y)
	{
		Vector3 localPosition = transform.localPosition;
		localPosition.y = y;
		transform.localPosition = localPosition;
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x00033BC4 File Offset: 0x00031DC4
	public static void SetLocalPositionX(this Transform transform, float x)
	{
		Vector3 localPosition = transform.localPosition;
		localPosition.x = x;
		transform.localPosition = localPosition;
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x00033BE8 File Offset: 0x00031DE8
	public static void SetLocalPositionZ(this Transform transform, float z)
	{
		Vector3 localPosition = transform.localPosition;
		localPosition.z = z;
		transform.localPosition = localPosition;
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x00033C0C File Offset: 0x00031E0C
	private static IEnumerable<Transform> IterateChildren(Transform parent, int iChild)
	{
		Transform child;
		for (;;)
		{
			child = parent.GetChild(iChild);
			yield return child;
			if (child.childCount > 0)
			{
				break;
			}
			if (++iChild >= parent.childCount)
			{
				goto IL_1C6;
			}
		}
		if (iChild + 1 < parent.childCount)
		{
			foreach (Transform sibling in TransformHelpers.IterateChildren(parent, ++iChild))
			{
				yield return sibling;
			}
		}
		foreach (Transform subChild in TransformHelpers.IterateChildren(child, 0))
		{
			yield return subChild;
		}
		IL_1C6:
		yield break;
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x00033C44 File Offset: 0x00031E44
	public static List<Transform> ListDecendantsByDepth(this Transform root)
	{
		return (root.childCount != 0) ? new List<Transform>(TransformHelpers.IterateChildren(root, 0)) : new List<Transform>(0);
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x00033C74 File Offset: 0x00031E74
	public static bool GetGroundInfo(this Transform transform, out Vector3 pos, out Vector3 normal)
	{
		return TransformHelpers.GetGroundInfoNoTransform(transform.position, out pos, out normal);
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x00033C84 File Offset: 0x00031E84
	public static bool GetGroundInfoNoTransform(Vector3 transformOrigin, out Vector3 pos, out Vector3 normal)
	{
		Vector3 vector = transformOrigin;
		vector.y += 0.25f;
		Ray ray;
		ray..ctor(vector, Vector3.down);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, ref raycastHit, 1000f))
		{
			pos = raycastHit.point;
			normal = raycastHit.normal;
			return true;
		}
		pos = transformOrigin;
		normal = Vector3.up;
		return false;
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x00033CF4 File Offset: 0x00031EF4
	public static Quaternion GetGroundInfoRotation(Quaternion ang, Vector3 y)
	{
		float num = y.magnitude;
		if (Mathf.Approximately(num, 0f))
		{
			y = Vector3.up;
			num = 0f;
		}
		Vector3 vector;
		Vector3 vector2;
		vector.y = (vector.z = (vector2.x = (vector2.y = 0f)));
		vector.x = (vector2.z = num);
		vector = ang * vector;
		vector2 = ang * vector2;
		float num2 = vector2.x * y.x + vector2.y * y.y + vector2.z * y.z;
		float num3 = vector.x * y.x + vector.y * y.y + vector.z * y.z;
		if (num2 * num2 > num3 * num3)
		{
			return TransformHelpers.LookRotationForcedUp(vector, y);
		}
		return TransformHelpers.LookRotationForcedUp(vector2, y);
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x00033DF8 File Offset: 0x00031FF8
	public static bool GetGroundInfo(Vector3 startPos, out Vector3 pos, out Vector3 normal)
	{
		return TransformHelpers.GetGroundInfo(startPos, 100f, out pos, out normal);
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x00033E08 File Offset: 0x00032008
	public static bool GetGroundInfo(Vector3 startPos, float range, out Vector3 pos, out Vector3 normal)
	{
		startPos.y += 0.25f;
		Ray ray;
		ray..ctor(startPos, Vector3.down);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, ref raycastHit, range, -472317957))
		{
			pos = raycastHit.point;
			normal = raycastHit.normal;
			return true;
		}
		pos = startPos;
		normal = Vector3.up;
		return false;
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x00033E78 File Offset: 0x00032078
	public static bool GetGroundInfoTerrainOnly(Vector3 startPos, float range, out Vector3 pos, out Vector3 normal)
	{
		startPos.y += 0.25f;
		Ray ray;
		ray..ctor(startPos, Vector3.down);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, ref raycastHit, range + 0.25f) && raycastHit.collider is TerrainCollider)
		{
			pos = raycastHit.point;
			normal = raycastHit.normal;
			return true;
		}
		pos = startPos;
		normal = Vector3.up;
		return false;
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x00033EFC File Offset: 0x000320FC
	private static bool GetGroundInfoNavMesh(Vector3 startPos, out NavMeshHit hit, float maxVariationFallback, int acceptMask)
	{
		int num = ~acceptMask;
		Vector3 vector;
		Vector3 vector2;
		vector.x = (vector2.x = startPos.x);
		vector.z = (vector2.z = startPos.z);
		for (int i = 0; i < TransformHelpers.upHeightTests.Length; i++)
		{
			vector2.y = startPos.y + TransformHelpers.upHeightTests[i].x;
			vector.y = startPos.y + TransformHelpers.upHeightTests[i].y;
			if (NavMesh.Raycast(vector2, vector, ref hit, num))
			{
				return true;
			}
		}
		return NavMesh.SamplePosition(startPos, ref hit, maxVariationFallback, acceptMask);
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x00033FB8 File Offset: 0x000321B8
	public static bool GetGroundInfoNavMesh(Vector3 startPos, out Vector3 pos, float maxVariationFallback, int acceptMask)
	{
		NavMeshHit navMeshHit;
		if (TransformHelpers.GetGroundInfoNavMesh(startPos, out navMeshHit, maxVariationFallback, acceptMask))
		{
			pos = navMeshHit.position;
			return true;
		}
		pos = startPos;
		return false;
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x00033FEC File Offset: 0x000321EC
	public static bool GetGroundInfoNavMesh(Vector3 startPos, out Vector3 pos, float maxVariationFallback)
	{
		return TransformHelpers.GetGroundInfoNavMesh(startPos, out pos, maxVariationFallback, -1);
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x00033FF8 File Offset: 0x000321F8
	public static bool GetGroundInfoNavMesh(Vector3 startPos, out Vector3 pos)
	{
		return TransformHelpers.GetGroundInfoNavMesh(startPos, out pos, 200f);
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x00034008 File Offset: 0x00032208
	public static Vector3 TestBoxCorners(Vector3 origin, Quaternion rotation, Vector3 boxCenter, Vector3 boxSize, int layerMask = 1024, int iterations = 7)
	{
		boxSize.x = Mathf.Abs(boxSize.x) * 0.5f;
		boxSize.y = Mathf.Abs(boxSize.y) * 0.5f;
		boxSize.z = Mathf.Abs(boxSize.z) * 0.5f;
		Vector3 vector;
		Vector3 vector2;
		vector.x = (vector2.x = boxCenter.x - boxSize.x);
		Vector3 vector3;
		Vector3 vector4;
		vector3.x = (vector4.x = boxCenter.x + boxSize.x);
		vector2.z = (vector4.z = boxCenter.z - boxSize.z);
		vector.z = (vector3.z = boxCenter.z + boxSize.z);
		vector.y = (vector3.y = (vector2.y = (vector4.y = boxCenter.y + boxSize.y)));
		vector = rotation * vector;
		vector2 = rotation * vector2;
		vector3 = rotation * vector3;
		vector4 = rotation * vector4;
		float magnitude = vector.magnitude;
		float magnitude2 = vector2.magnitude;
		float magnitude3 = vector3.magnitude;
		float magnitude4 = vector4.magnitude;
		float num = 1f / magnitude;
		float num2 = 1f / magnitude2;
		float num3 = 1f / magnitude3;
		float num4 = 1f / magnitude4;
		Vector3 vector5 = vector * num;
		Vector3 vector6 = vector2 * num2;
		Vector3 vector7 = vector3 * num3;
		Vector3 vector8 = vector4 * num4;
		Vector3 vector9 = Vector3.Lerp(Vector3.Lerp(vector, vector4, 0.5f), Vector3.Lerp(vector3, vector2, 0.5f), 0.5f);
		for (int i = 0; i < iterations; i++)
		{
			Vector3 vector10 = origin + vector;
			Vector3 vector11 = origin + vector2;
			Vector3 vector12 = origin + vector3;
			Vector3 vector13 = origin + vector4;
			RaycastHit raycastHit;
			bool flag = Physics.Raycast(vector10, -vector5, ref raycastHit, magnitude, layerMask);
			RaycastHit raycastHit2;
			bool flag2 = Physics.Raycast(vector11, -vector6, ref raycastHit2, magnitude2, layerMask);
			RaycastHit raycastHit3;
			bool flag3 = Physics.Raycast(vector12, -vector7, ref raycastHit3, magnitude3, layerMask);
			RaycastHit raycastHit4;
			bool flag4 = Physics.Raycast(vector13, -vector8, ref raycastHit4, magnitude4, layerMask);
			if (!flag && !flag2 && !flag3 && !flag4)
			{
				break;
			}
			Vector3 vector14 = (!flag) ? vector : (raycastHit.point - origin);
			Vector3 vector15 = (!flag2) ? vector2 : (raycastHit2.point - origin);
			Vector3 vector16 = (!flag3) ? vector3 : (raycastHit3.point - origin);
			Vector3 vector17 = (!flag4) ? vector4 : (raycastHit4.point - origin);
			Vector3 vector18 = Vector3.Lerp(Vector3.Lerp(vector14, vector17, 0.5f), Vector3.Lerp(vector16, vector15, 0.5f), 0.5f);
			Vector3 vector19 = vector18 - vector9;
			vector19.y = 0f;
			origin += vector19 * 2.15f;
		}
		return origin;
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x00034354 File Offset: 0x00032554
	public static Quaternion LookRotationForcedUp(Vector3 forward, Vector3 up)
	{
		if (forward == up)
		{
			return Quaternion.LookRotation(up);
		}
		Vector3 vector = Vector3.Cross(forward, up);
		forward = Vector3.Cross(up, vector);
		if (forward == Vector3.zero)
		{
			forward = Vector3.forward;
		}
		return Quaternion.LookRotation(forward, up);
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x000343A4 File Offset: 0x000325A4
	private static float InvSqrt(float x)
	{
		return 1f / Mathf.Sqrt(x);
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x000343B4 File Offset: 0x000325B4
	private static float InvSqrt(float x, float y)
	{
		return 1f / Mathf.Sqrt(x * x + y * y);
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x000343C8 File Offset: 0x000325C8
	private static float InvSqrt(float x, float y, float z)
	{
		return 1f / Mathf.Sqrt(x * x + y * y + z * z);
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x000343E0 File Offset: 0x000325E0
	private static float InvSqrt(float x, float y, float z, float w)
	{
		return 1f / Mathf.Sqrt(x * x + y * y + z * z + w * w);
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x000343FC File Offset: 0x000325FC
	public static Quaternion LookRotationForcedUp(Quaternion rotation, Vector3 up)
	{
		float num = up.x * up.x + up.y * up.y + up.z * up.z;
		if (num < 1.401298E-45f)
		{
			return rotation;
		}
		float num2 = TransformHelpers.InvSqrt(num);
		up.x *= num2;
		up.y *= num2;
		up.z *= num2;
		Vector3 vector;
		vector.x = up.x;
		vector.y = up.y;
		vector.z = up.z;
		Vector3 vector2;
		Vector3 vector3;
		vector2.z = (vector3.x = 1f);
		vector2.y = (vector2.x = (vector3.z = (vector3.y = 0f)));
		vector2 = rotation * vector2;
		vector3 = rotation * vector3;
		float num3 = vector2.x * vector.x + vector2.y * vector.y + vector2.z * vector.z;
		float num4 = vector3.x * vector.x + vector3.y * vector.y + vector3.z * vector.z;
		Vector3 vector4;
		Vector3 vector5;
		if (num3 * num3 > num4 * num4)
		{
			vector4.x = vector.x;
			vector4.y = vector.y;
			vector4.z = vector.z;
			vector5.x = vector3.x;
			vector5.y = vector3.y;
			vector5.z = vector3.z;
			vector2.x = -(vector4.y * vector5.z - vector4.z * vector5.y);
			vector2.y = -(vector4.z * vector5.x - vector4.x * vector5.z);
			vector2.z = -(vector4.x * vector5.y - vector4.y * vector5.x);
			float num5 = TransformHelpers.InvSqrt(vector2.x, vector2.y, vector2.z);
			vector4.x = num5 * vector2.x;
			vector4.y = num5 * vector2.y;
			vector4.z = num5 * vector2.z;
		}
		else
		{
			vector4.x = vector2.x;
			vector4.y = vector2.y;
			vector4.z = vector2.z;
		}
		vector5.x = vector.x;
		vector5.y = vector.y;
		vector5.z = vector.z;
		vector3.x = vector4.y * vector5.z - vector4.z * vector5.y;
		vector3.y = vector4.z * vector5.x - vector4.x * vector5.z;
		vector3.z = vector4.x * vector5.y - vector4.y * vector5.x;
		float num6 = TransformHelpers.InvSqrt(vector3.x, vector3.y, vector3.z);
		vector5.x = vector3.x * num6;
		vector5.y = vector3.y * num6;
		vector5.z = vector3.z * num6;
		vector4.x = vector.x;
		vector4.y = vector.y;
		vector4.z = vector.z;
		vector2.x = vector4.y * vector5.z - vector4.z * vector5.y;
		vector2.y = vector4.z * vector5.x - vector4.x * vector5.z;
		vector2.z = vector4.x * vector5.y - vector4.y * vector5.x;
		if (vector2.x * vector2.x + vector2.y * vector2.y + vector2.z * vector2.z < 1.401298E-45f)
		{
			return rotation;
		}
		return Quaternion.LookRotation(vector2, up);
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x00034884 File Offset: 0x00032A84
	public static Quaternion UpRotation(Vector3 up)
	{
		float num = Vector3.Dot(up, Vector3.forward);
		float num2 = Vector3.Dot(up, Vector3.right);
		Vector3 vector;
		if (num * num < num2 * num2)
		{
			vector = Vector3.Cross(up, Vector3.forward);
		}
		else
		{
			vector = Vector3.Cross(up, Vector3.right);
		}
		return Quaternion.LookRotation(vector, up);
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x000348D8 File Offset: 0x00032AD8
	public static void DropToGround(this Transform transform, bool useNormal)
	{
		Vector3 position;
		Vector3 vector;
		if (transform.GetGroundInfo(out position, out vector))
		{
			transform.position = position;
			if (useNormal)
			{
				transform.rotation = Quaternion.LookRotation(vector);
			}
		}
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x00034910 File Offset: 0x00032B10
	public static float Dist2D(Vector3 a, Vector3 b)
	{
		Vector2 vector;
		vector.x = b.x - a.x;
		vector.y = b.z - a.z;
		return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x00034970 File Offset: 0x00032B70
	public static bool GetIDBaseFromCollider(Collider collider, out IDBase id)
	{
		if (!collider)
		{
			id = null;
			return false;
		}
		id = IDBase.Get(collider);
		if (id)
		{
			return true;
		}
		Rigidbody attachedRigidbody = collider.attachedRigidbody;
		if (attachedRigidbody)
		{
			id = attachedRigidbody.GetComponent<IDBase>();
			return id;
		}
		return false;
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x000349C8 File Offset: 0x00032BC8
	public static bool GetIDMainFromCollider(Collider collider, out IDMain main)
	{
		IDBase idbase;
		if (TransformHelpers.GetIDBaseFromCollider(collider, out idbase))
		{
			main = idbase.idMain;
			return main;
		}
		main = null;
		return false;
	}

	// Token: 0x0400082D RID: 2093
	private static readonly Vector2[] upHeightTests;
}
