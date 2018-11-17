using System;
using UnityEngine;

// Token: 0x02000661 RID: 1633
public class FlashlightItemRep : global::ItemRepresentation
{
	// Token: 0x06003832 RID: 14386 RVA: 0x000C782C File Offset: 0x000C5A2C
	protected override void StateSignalReceive(global::Character character, bool treatedAsFirst)
	{
		this.SetLightOn(character.stateFlags.lamp);
	}

	// Token: 0x06003833 RID: 14387 RVA: 0x000C7840 File Offset: 0x000C5A40
	public virtual void SetLightOn(bool on)
	{
		bool flag = base.networkViewOwner == global::NetCull.player;
		if (on)
		{
			if (!flag)
			{
				Vector3 position = base.transform.position;
				Quaternion rotation = base.transform.rotation;
				this.lightEffect = (Object.Instantiate(this.lightEffectPrefab3P, position, rotation) as GameObject);
				this.lightEffect.transform.localPosition = position;
				this.lightEffect.transform.localRotation = rotation;
			}
		}
		else
		{
			Object.Destroy(this.lightEffect);
		}
	}

	// Token: 0x04001BF3 RID: 7155
	private GameObject lightEffect;

	// Token: 0x04001BF4 RID: 7156
	public GameObject lightEffectPrefab1P;

	// Token: 0x04001BF5 RID: 7157
	public GameObject lightEffectPrefab3P;
}
