using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class Radiation : global::IDLocalCharacter
{
	// Token: 0x06000344 RID: 836 RVA: 0x00010130 File Offset: 0x0000E330
	public void AddRadiationZone(global::RadiationZone zone)
	{
		if (zone.CanAddToRadiation(this))
		{
			List<global::RadiationZone> list;
			if ((list = this.radiationZones) == null)
			{
				list = (this.radiationZones = new List<global::RadiationZone>());
			}
			list.Add(zone);
		}
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0001016C File Offset: 0x0000E36C
	public void RemoveRadiationZone(global::RadiationZone zone)
	{
		if (this.radiationZones != null && this.radiationZones.Remove(zone))
		{
			zone.RemoveFromRadiation(this);
		}
	}

	// Token: 0x06000346 RID: 838 RVA: 0x000101A0 File Offset: 0x0000E3A0
	public float CalculateExposure(bool countArmor)
	{
		if (this.radiationZones == null || this.radiationZones.Count == 0)
		{
			return 0f;
		}
		Vector3 origin = base.origin;
		float num = 0f;
		foreach (global::RadiationZone radiationZone in this.radiationZones)
		{
			num += radiationZone.GetExposureForPos(origin);
		}
		if (countArmor)
		{
			global::HumanBodyTakeDamage humanBodyTakeDamage = base.takeDamage as global::HumanBodyTakeDamage;
			if (humanBodyTakeDamage)
			{
				float armorValue = humanBodyTakeDamage.GetArmorValue(4);
				if (armorValue > 0f)
				{
					num *= 1f - Mathf.Clamp(armorValue / 200f, 0f, 1f);
				}
			}
		}
		return num;
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00010290 File Offset: 0x0000E490
	public float GetRadExposureScalar(float exposure)
	{
		return Mathf.Clamp01(exposure / 1000f);
	}

	// Token: 0x06000348 RID: 840 RVA: 0x000102A0 File Offset: 0x0000E4A0
	private void OnDestroy()
	{
		if (this.radiationZones != null)
		{
			foreach (global::RadiationZone radiationZone in this.radiationZones)
			{
				if (radiationZone)
				{
					radiationZone.RemoveFromRadiation(this);
				}
			}
			this.radiationZones = null;
		}
	}

	// Token: 0x04000218 RID: 536
	[NonSerialized]
	private List<global::RadiationZone> radiationZones;
}
