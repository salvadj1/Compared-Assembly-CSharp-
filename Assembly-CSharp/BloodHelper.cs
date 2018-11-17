using System;
using Facepunch;
using UnityEngine;

// Token: 0x020004CF RID: 1231
public class BloodHelper : MonoBehaviour
{
	// Token: 0x06002AA0 RID: 10912 RVA: 0x000A9D24 File Offset: 0x000A7F24
	private static void BleedDir(Vector3 startPos, Vector3 dir, int hitMask)
	{
		Ray ray;
		ray..ctor(startPos + dir * 0.25f, dir);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, ref raycastHit, 4f, hitMask))
		{
			if (BloodHelper.bloodDecalPrefab == null && !Bundling.Load<GameObject>("content/effect/BloodDecal", out BloodHelper.bloodDecalPrefab))
			{
				return;
			}
			Quaternion quaternion = Quaternion.LookRotation(raycastHit.normal);
			GameObject gameObject = Object.Instantiate(BloodHelper.bloodDecalPrefab, raycastHit.point + raycastHit.normal * Random.Range(0.025f, 0.035f), quaternion * Quaternion.Euler(0f, 0f, (float)Random.Range(0, 360))) as GameObject;
			Object.Destroy(gameObject, 12f);
		}
	}

	// Token: 0x04001726 RID: 5926
	private static GameObject bloodDecalPrefab;
}
