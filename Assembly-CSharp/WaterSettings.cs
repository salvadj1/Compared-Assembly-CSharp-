using System;
using UnityEngine;

// Token: 0x020004E2 RID: 1250
public class WaterSettings : MonoBehaviour
{
	// Token: 0x06002B2A RID: 11050 RVA: 0x000A09F4 File Offset: 0x0009EBF4
	private void Start()
	{
		global::GameEvent.QualitySettingsRefresh += this.RefreshSettings;
		this.RefreshSettings();
	}

	// Token: 0x06002B2B RID: 11051 RVA: 0x000A0A10 File Offset: 0x0009EC10
	private void OnDestroy()
	{
		global::GameEvent.QualitySettingsRefresh -= this.RefreshSettings;
	}

	// Token: 0x06002B2C RID: 11052 RVA: 0x000A0A24 File Offset: 0x0009EC24
	protected void RefreshSettings()
	{
		WaterBase component = base.GetComponent<WaterBase>();
		PlanarReflection component2 = base.GetComponent<PlanarReflection>();
		if (!component)
		{
			return;
		}
		if (global::render.level > 0.8f)
		{
			component.waterQuality = 2;
			component.edgeBlend = true;
		}
		else if (global::render.level > 0.5f)
		{
			component.waterQuality = 1;
			component.edgeBlend = false;
		}
		else
		{
			component.waterQuality = 0;
			component.edgeBlend = false;
		}
		if (global::water.level != -1)
		{
			component.waterQuality = Mathf.Clamp(global::water.level - 1, 0, 2);
			component.edgeBlend = (global::water.level == 2);
		}
		if (component2)
		{
			component2.reflectionMask = 13111296;
			if (!global::water.reflection)
			{
				component2.reflectionMask = 8388608;
			}
		}
	}
}
