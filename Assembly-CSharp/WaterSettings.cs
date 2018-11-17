using System;
using UnityEngine;

// Token: 0x0200042C RID: 1068
public class WaterSettings : MonoBehaviour
{
	// Token: 0x0600279A RID: 10138 RVA: 0x0009AA74 File Offset: 0x00098C74
	private void Start()
	{
		GameEvent.QualitySettingsRefresh += this.RefreshSettings;
		this.RefreshSettings();
	}

	// Token: 0x0600279B RID: 10139 RVA: 0x0009AA90 File Offset: 0x00098C90
	private void OnDestroy()
	{
		GameEvent.QualitySettingsRefresh -= this.RefreshSettings;
	}

	// Token: 0x0600279C RID: 10140 RVA: 0x0009AAA4 File Offset: 0x00098CA4
	protected void RefreshSettings()
	{
		WaterBase component = base.GetComponent<WaterBase>();
		PlanarReflection component2 = base.GetComponent<PlanarReflection>();
		if (!component)
		{
			return;
		}
		if (render.level > 0.8f)
		{
			component.waterQuality = 2;
			component.edgeBlend = true;
		}
		else if (render.level > 0.5f)
		{
			component.waterQuality = 1;
			component.edgeBlend = false;
		}
		else
		{
			component.waterQuality = 0;
			component.edgeBlend = false;
		}
		if (water.level != -1)
		{
			component.waterQuality = Mathf.Clamp(water.level - 1, 0, 2);
			component.edgeBlend = (water.level == 2);
		}
		if (component2)
		{
			component2.reflectionMask = 13111296;
			if (!water.reflection)
			{
				component2.reflectionMask = 8388608;
			}
		}
	}
}
