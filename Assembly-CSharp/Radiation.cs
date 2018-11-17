using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class Radiation : IDLocalCharacter
{
	// Token: 0x060002D2 RID: 722 RVA: 0x0000EB88 File Offset: 0x0000CD88
	public void AddRadiationZone(RadiationZone zone)
	{
		if (zone.CanAddToRadiation(this))
		{
			List<RadiationZone> list;
			if ((list = this.radiationZones) == null)
			{
				list = (this.radiationZones = new List<RadiationZone>());
			}
			list.Add(zone);
		}
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
	public void RemoveRadiationZone(RadiationZone zone)
	{
		if (this.radiationZones != null && this.radiationZones.Remove(zone))
		{
			zone.RemoveFromRadiation(this);
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
	public float CalculateExposure(bool countArmor)
	{
		if (this.radiationZones == null || this.radiationZones.Count == 0)
		{
			return 0f;
		}
		Vector3 origin = base.origin;
		float num = 0f;
		foreach (RadiationZone radiationZone in this.radiationZones)
		{
			num += radiationZone.GetExposureForPos(origin);
		}
		if (countArmor)
		{
			HumanBodyTakeDamage humanBodyTakeDamage = base.takeDamage as HumanBodyTakeDamage;
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

	// Token: 0x060002D5 RID: 725 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
	public float GetRadExposureScalar(float exposure)
	{
		return Mathf.Clamp01(exposure / 1000f);
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x0000ECF8 File Offset: 0x0000CEF8
	private void OnDestroy()
	{
		if (this.radiationZones != null)
		{
			foreach (RadiationZone radiationZone in this.radiationZones)
			{
				if (radiationZone)
				{
					radiationZone.RemoveFromRadiation(this);
				}
			}
			this.radiationZones = null;
		}
	}

	// Token: 0x040001B6 RID: 438
	[NonSerialized]
	private List<RadiationZone> radiationZones;
}
