using System;
using UnityEngine;

// Token: 0x02000571 RID: 1393
[Serializable]
public class ResourceHarvester : Object
{
	// Token: 0x06002DE6 RID: 11750 RVA: 0x000AD4DC File Offset: 0x000AB6DC
	public float ResourceEfficiencyForType(global::ResourceTarget.ResourceTargetType type)
	{
		return 0f;
	}

	// Token: 0x06002DE7 RID: 11751 RVA: 0x000AD4F0 File Offset: 0x000AB6F0
	public static string ResourceDBNameForType(global::ResourceType hitType)
	{
		if (hitType == global::ResourceType.Wood)
		{
			return "Wood";
		}
		if (hitType != global::ResourceType.Meat)
		{
			return string.Empty;
		}
		return "Raw Meat";
	}

	// Token: 0x040017EC RID: 6124
	public float[] efficiencies;
}
