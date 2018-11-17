using System;
using UnityEngine;

// Token: 0x0200055F RID: 1375
public class SurfaceInfo : MonoBehaviour
{
	// Token: 0x06002D9B RID: 11675 RVA: 0x000ABD68 File Offset: 0x000A9F68
	public static global::SurfaceInfoObject GetSurfaceInfoFor(Collider collider, Vector3 worldPos)
	{
		return global::SurfaceInfo.GetSurfaceInfoFor(collider.gameObject, worldPos);
	}

	// Token: 0x06002D9C RID: 11676 RVA: 0x000ABD78 File Offset: 0x000A9F78
	public static global::SurfaceInfoObject GetSurfaceInfoFor(GameObject obj, Vector3 worldPos)
	{
		global::SurfaceInfo component = obj.GetComponent<global::SurfaceInfo>();
		if (component)
		{
			return component.SurfaceObj(worldPos);
		}
		IDBase component2 = obj.GetComponent<IDBase>();
		if (component2)
		{
			global::SurfaceInfo component3 = component2.idMain.GetComponent<global::SurfaceInfo>();
			if (component3)
			{
				return component3.SurfaceObj(worldPos);
			}
		}
		return global::SurfaceInfoObject.GetDefault();
	}

	// Token: 0x06002D9D RID: 11677 RVA: 0x000ABDD8 File Offset: 0x000A9FD8
	public static void DoImpact(GameObject go, global::SurfaceInfoObject.ImpactType type, Vector3 worldPos, Quaternion rotation)
	{
		global::SurfaceInfoObject surfaceInfoFor = global::SurfaceInfo.GetSurfaceInfoFor(go, worldPos);
		Object @object = Object.Instantiate(surfaceInfoFor.GetImpactEffect(type), worldPos, rotation);
		Object.Destroy(@object, 1f);
	}

	// Token: 0x06002D9E RID: 11678 RVA: 0x000ABE08 File Offset: 0x000AA008
	public virtual global::SurfaceInfoObject SurfaceObj()
	{
		return this.surface;
	}

	// Token: 0x06002D9F RID: 11679 RVA: 0x000ABE10 File Offset: 0x000AA010
	public virtual global::SurfaceInfoObject SurfaceObj(Vector3 worldPos)
	{
		return this.surface;
	}

	// Token: 0x0400178E RID: 6030
	public global::SurfaceInfoObject surface;
}
