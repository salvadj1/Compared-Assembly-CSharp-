using System;
using Facepunch;
using UnityEngine;

// Token: 0x0200058A RID: 1418
public class BloodHelper : MonoBehaviour
{
	// Token: 0x06002E52 RID: 11858 RVA: 0x000B1ABC File Offset: 0x000AFCBC
	private static void BleedDir(Vector3 startPos, Vector3 dir, int hitMask)
	{
		Ray ray;
		ray..ctor(startPos + dir * 0.25f, dir);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, ref raycastHit, 4f, hitMask))
		{
			if (global::BloodHelper.bloodDecalPrefab == null && !Facepunch.Bundling.Load<GameObject>("content/effect/BloodDecal", out global::BloodHelper.bloodDecalPrefab))
			{
				return;
			}
			Quaternion quaternion = Quaternion.LookRotation(raycastHit.normal);
			GameObject gameObject = Object.Instantiate(global::BloodHelper.bloodDecalPrefab, raycastHit.point + raycastHit.normal * Random.Range(0.025f, 0.035f), quaternion * Quaternion.Euler(0f, 0f, (float)Random.Range(0, 360))) as GameObject;
			Object.Destroy(gameObject, 12f);
		}
	}

	// Token: 0x040018E3 RID: 6371
	private static GameObject bloodDecalPrefab;
}
