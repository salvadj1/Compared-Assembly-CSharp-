using System;
using UnityEngine;

// Token: 0x020005F0 RID: 1520
public class LampModRep : WeaponModRep
{
	// Token: 0x06003688 RID: 13960 RVA: 0x000C4B9C File Offset: 0x000C2D9C
	protected LampModRep(ItemModRepresentation.Caps caps, bool defaultOn) : base(caps, defaultOn)
	{
	}

	// Token: 0x06003689 RID: 13961 RVA: 0x000C4BA8 File Offset: 0x000C2DA8
	public LampModRep() : this(ItemModRepresentation.Caps.BindStateFlags, false)
	{
	}

	// Token: 0x0600368A RID: 13962 RVA: 0x000C4BB4 File Offset: 0x000C2DB4
	protected LampModRep(ItemModRepresentation.Caps caps) : this(caps, false)
	{
	}

	// Token: 0x0600368B RID: 13963 RVA: 0x000C4BC0 File Offset: 0x000C2DC0
	protected override bool VerifyCompatible(GameObject attachment)
	{
		return attachment.GetComponentInChildren<Light>();
	}

	// Token: 0x0600368C RID: 13964 RVA: 0x000C4BD0 File Offset: 0x000C2DD0
	protected override void OnAddAttached()
	{
		this.lights = base.attached.GetComponentsInChildren<Light>();
	}

	// Token: 0x0600368D RID: 13965 RVA: 0x000C4BE4 File Offset: 0x000C2DE4
	protected override void OnRemoveAttached()
	{
		this.lights = null;
	}

	// Token: 0x0600368E RID: 13966 RVA: 0x000C4BF0 File Offset: 0x000C2DF0
	protected override void EnableMod(ItemModRepresentation.Reason reason)
	{
		Light anyLight = null;
		foreach (Light light in this.lights)
		{
			if (light)
			{
				light.enabled = true;
				anyLight = light;
			}
		}
		if (reason == ItemModRepresentation.Reason.Explicit)
		{
			this.PlaySound(anyLight, base.modDataBlock.onSound);
		}
	}

	// Token: 0x0600368F RID: 13967 RVA: 0x000C4C4C File Offset: 0x000C2E4C
	protected override void DisableMod(ItemModRepresentation.Reason reason)
	{
		Light anyLight = null;
		foreach (Light light in this.lights)
		{
			if (light)
			{
				light.enabled = false;
				anyLight = light;
			}
		}
		if (reason == ItemModRepresentation.Reason.Explicit)
		{
			this.PlaySound(anyLight, base.modDataBlock.offSound);
		}
	}

	// Token: 0x06003690 RID: 13968 RVA: 0x000C4CA8 File Offset: 0x000C2EA8
	protected override void BindStateFlags(CharacterStateFlags flags, ItemModRepresentation.Reason reason)
	{
		base.BindStateFlags(flags, reason);
		base.SetOn(flags.lamp, reason);
	}

	// Token: 0x06003691 RID: 13969 RVA: 0x000C4CC0 File Offset: 0x000C2EC0
	private void PlaySound(Light anyLight, AudioClip clip)
	{
		if (anyLight)
		{
			clip.PlayLocal(anyLight.transform, Vector3.zero, 1f, 0, 1f, 4f);
		}
		else
		{
			clip.PlayLocal(base.itemRep.transform, Vector3.zero, 1f, 0, 1f, 4f);
		}
	}

	// Token: 0x04001ABC RID: 6844
	private const float kVolume = 1f;

	// Token: 0x04001ABD RID: 6845
	private const float kMinAudioDistance = 1f;

	// Token: 0x04001ABE RID: 6846
	private const float kMaxAudioDistance = 4f;

	// Token: 0x04001ABF RID: 6847
	private const AudioRolloffMode kRolloffMode = 0;

	// Token: 0x04001AC0 RID: 6848
	private Light[] lights;
}
