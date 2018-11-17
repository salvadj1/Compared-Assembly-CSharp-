using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000218 RID: 536
public static class TransformHelpers
{
	// Token: 0x06000E9B RID: 3739 RVA: 0x00037B68 File Offset: 0x00035D68
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
		global::TransformHelpers.upHeightTests = array;
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x00037C28 File Offset: 0x00035E28
	public static void SetLocalPositionY(this Transform transform, float y)
	{
		Vector3 localPosition = transform.localPosition;
		localPosition.y = y;
		transform.localPosition = localPosition;
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x00037C4C File Offset: 0x00035E4C
	public static void SetLocalPositionX(this Transform transform, float x)
	{
		Vector3 localPosition = transform.localPosition;
		localPosition.x = x;
		transform.localPosition = localPosition;
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x00037C70 File Offset: 0x00035E70
	public static void SetLocalPositionZ(this Transform transform, float z)
	{
		Vector3 localPosition = transform.localPosition;
		localPosition.z = z;
		transform.localPosition = localPosition;
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x00037C94 File Offset: 0x00035E94
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
			foreach (Transform sibling in global::TransformHelpers.IterateChildren(parent, ++iChild))
			{
				yield return sibling;
			}
		}
		foreach (Transform subChild in global::TransformHelpers.IterateChildren(child, 0))
		{
			yield return subChild;
		}
		IL_1C6:
		yield break;
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x00037CCC File Offset: 0x00035ECC
	public static List<Transform> ListDecendantsByDepth(this Transform root)
	{
		return (root.childCount != 0) ? new List<Transform>(global::TransformHelpers.IterateChildren(root, 0)) : new List<Transform>(0);
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x00037CFC File Offset: 0x00035EFC
	public static bool GetGroundInfo(this Transform transform, out Vector3 pos, out Vector3 normal)
	{
		return global::TransformHelpers.GetGroundInfoNoTransform(transform.position, out pos, out normal);
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x00037D0C File Offset: 0x00035F0C
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

	// Token: 0x06000EA3 RID: 3747 RVA: 0x00037D7C File Offset: 0x00035F7C
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
			return global::TransformHelpers.LookRotationForcedUp(vector, y);
		}
		return global::TransformHelpers.LookRotationForcedUp(vector2, y);
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x00037E80 File Offset: 0x00036080
	public static bool GetGroundInfo(Vector3 startPos, out Vector3 pos, out Vector3 normal)
	{
		return global::TransformHelpers.GetGroundInfo(startPos, 100f, out pos, out normal);
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x00037E90 File Offset: 0x00036090
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

	// Token: 0x06000EA6 RID: 3750 RVA: 0x00037F00 File Offset: 0x00036100
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

	// Token: 0x06000EA7 RID: 3751 RVA: 0x00037F84 File Offset: 0x00036184
	private static bool GetGroundInfoNavMesh(Vector3 startPos, out NavMeshHit hit, float maxVariationFallback, int acceptMask)
	{
		int num = ~acceptMask;
		Vector3 vector;
		Vector3 vector2;
		vector.x = (vector2.x = startPos.x);
		vector.z = (vector2.z = startPos.z);
		for (int i = 0; i < global::TransformHelpers.upHeightTests.Length; i++)
		{
			vector2.y = startPos.y + global::TransformHelpers.upHeightTests[i].x;
			vector.y = startPos.y + global::TransformHelpers.upHeightTests[i].y;
			if (NavMesh.Raycast(vector2, vector, ref hit, num))
			{
				return true;
			}
		}
		return NavMesh.SamplePosition(startPos, ref hit, maxVariationFallback, acceptMask);
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x00038040 File Offset: 0x00036240
	public static bool GetGroundInfoNavMesh(Vector3 startPos, out Vector3 pos, float maxVariationFallback, int acceptMask)
	{
		NavMeshHit navMeshHit;
		if (global::TransformHelpers.GetGroundInfoNavMesh(startPos, out navMeshHit, maxVariationFallback, acceptMask))
		{
			pos = navMeshHit.position;
			return true;
		}
		pos = startPos;
		return false;
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x00038074 File Offset: 0x00036274
	public static bool GetGroundInfoNavMesh(Vector3 startPos, out Vector3 pos, float maxVariationFallback)
	{
		return global::TransformHelpers.GetGroundInfoNavMesh(startPos, out pos, maxVariationFallback, -1);
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x00038080 File Offset: 0x00036280
	public static bool GetGroundInfoNavMesh(Vector3 startPos, out Vector3 pos)
	{
		return global::TransformHelpers.GetGroundInfoNavMesh(startPos, out pos, 200f);
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x00038090 File Offset: 0x00036290
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

	// Token: 0x06000EAC RID: 3756 RVA: 0x000383DC File Offset: 0x000365DC
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

	// Token: 0x06000EAD RID: 3757 RVA: 0x0003842C File Offset: 0x0003662C
	private static float InvSqrt(float x)
	{
		return 1f / Mathf.Sqrt(x);
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x0003843C File Offset: 0x0003663C
	private static float InvSqrt(float x, float y)
	{
		return 1f / Mathf.Sqrt(x * x + y * y);
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x00038450 File Offset: 0x00036650
	private static float InvSqrt(float x, float y, float z)
	{
		return 1f / Mathf.Sqrt(x * x + y * y + z * z);
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x00038468 File Offset: 0x00036668
	private static float InvSqrt(float x, float y, float z, float w)
	{
		return 1f / Mathf.Sqrt(x * x + y * y + z * z + w * w);
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x00038484 File Offset: 0x00036684
	public static Quaternion LookRotationForcedUp(Quaternion rotation, Vector3 up)
	{
		float num = up.x * up.x + up.y * up.y + up.z * up.z;
		if (num < 1.401298E-45f)
		{
			return rotation;
		}
		float num2 = global::TransformHelpers.InvSqrt(num);
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
			float num5 = global::TransformHelpers.InvSqrt(vector2.x, vector2.y, vector2.z);
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
		float num6 = global::TransformHelpers.InvSqrt(vector3.x, vector3.y, vector3.z);
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

	// Token: 0x06000EB2 RID: 3762 RVA: 0x0003890C File Offset: 0x00036B0C
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

	// Token: 0x06000EB3 RID: 3763 RVA: 0x00038960 File Offset: 0x00036B60
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

	// Token: 0x06000EB4 RID: 3764 RVA: 0x00038998 File Offset: 0x00036B98
	public static float Dist2D(Vector3 a, Vector3 b)
	{
		Vector2 vector;
		vector.x = b.x - a.x;
		vector.y = b.z - a.z;
		return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x000389F8 File Offset: 0x00036BF8
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

	// Token: 0x06000EB6 RID: 3766 RVA: 0x00038A50 File Offset: 0x00036C50
	public static bool GetIDMainFromCollider(Collider collider, out IDMain main)
	{
		IDBase idbase;
		if (global::TransformHelpers.GetIDBaseFromCollider(collider, out idbase))
		{
			main = idbase.idMain;
			return main;
		}
		main = null;
		return false;
	}

	// Token: 0x04000945 RID: 2373
	private static readonly Vector2[] upHeightTests;
}
