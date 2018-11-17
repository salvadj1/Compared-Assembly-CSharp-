using System;
using UnityEngine;

// Token: 0x020005A3 RID: 1443
public class FlashlightItemRep : ItemRepresentation
{
	// Token: 0x0600346A RID: 13418 RVA: 0x000BF5D0 File Offset: 0x000BD7D0
	protected override void StateSignalReceive(Character character, bool treatedAsFirst)
	{
		this.SetLightOn(character.stateFlags.lamp);
	}

	// Token: 0x0600346B RID: 13419 RVA: 0x000BF5E4 File Offset: 0x000BD7E4
	public virtual void SetLightOn(bool on)
	{
		bool flag = base.networkViewOwner == NetCull.player;
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

	// Token: 0x04001A22 RID: 6690
	private GameObject lightEffect;

	// Token: 0x04001A23 RID: 6691
	public GameObject lightEffectPrefab1P;

	// Token: 0x04001A24 RID: 6692
	public GameObject lightEffectPrefab3P;
}
