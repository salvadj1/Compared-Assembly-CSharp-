using System;
using UnityEngine;

// Token: 0x020004A4 RID: 1188
public class SurfaceInfo : MonoBehaviour
{
	// Token: 0x060029E9 RID: 10729 RVA: 0x000A3FD0 File Offset: 0x000A21D0
	public static SurfaceInfoObject GetSurfaceInfoFor(Collider collider, Vector3 worldPos)
	{
		return SurfaceInfo.GetSurfaceInfoFor(collider.gameObject, worldPos);
	}

	// Token: 0x060029EA RID: 10730 RVA: 0x000A3FE0 File Offset: 0x000A21E0
	public static SurfaceInfoObject GetSurfaceInfoFor(GameObject obj, Vector3 worldPos)
	{
		SurfaceInfo component = obj.GetComponent<SurfaceInfo>();
		if (component)
		{
			return component.SurfaceObj(worldPos);
		}
		IDBase component2 = obj.GetComponent<IDBase>();
		if (component2)
		{
			SurfaceInfo component3 = component2.idMain.GetComponent<SurfaceInfo>();
			if (component3)
			{
				return component3.SurfaceObj(worldPos);
			}
		}
		return SurfaceInfoObject.GetDefault();
	}

	// Token: 0x060029EB RID: 10731 RVA: 0x000A4040 File Offset: 0x000A2240
	public static void DoImpact(GameObject go, SurfaceInfoObject.ImpactType type, Vector3 worldPos, Quaternion rotation)
	{
		SurfaceInfoObject surfaceInfoFor = SurfaceInfo.GetSurfaceInfoFor(go, worldPos);
		Object @object = Object.Instantiate(surfaceInfoFor.GetImpactEffect(type), worldPos, rotation);
		Object.Destroy(@object, 1f);
	}

	// Token: 0x060029EC RID: 10732 RVA: 0x000A4070 File Offset: 0x000A2270
	public virtual SurfaceInfoObject SurfaceObj()
	{
		return this.surface;
	}

	// Token: 0x060029ED RID: 10733 RVA: 0x000A4078 File Offset: 0x000A2278
	public virtual SurfaceInfoObject SurfaceObj(Vector3 worldPos)
	{
		return this.surface;
	}

	// Token: 0x040015D1 RID: 5585
	public SurfaceInfoObject surface;
}
