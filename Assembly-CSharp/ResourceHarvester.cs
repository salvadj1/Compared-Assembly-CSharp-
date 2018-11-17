using System;
using UnityEngine;

// Token: 0x020004B6 RID: 1206
[Serializable]
public class ResourceHarvester : Object
{
	// Token: 0x06002A34 RID: 10804 RVA: 0x000A5744 File Offset: 0x000A3944
	public float ResourceEfficiencyForType(ResourceTarget.ResourceTargetType type)
	{
		return 0f;
	}

	// Token: 0x06002A35 RID: 10805 RVA: 0x000A5758 File Offset: 0x000A3958
	public static string ResourceDBNameForType(ResourceType hitType)
	{
		if (hitType == ResourceType.Wood)
		{
			return "Wood";
		}
		if (hitType != ResourceType.Meat)
		{
			return string.Empty;
		}
		return "Raw Meat";
	}

	// Token: 0x0400162F RID: 5679
	public float[] efficiencies;
}
