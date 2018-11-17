using System;
using UnityEngine;

// Token: 0x020006AE RID: 1710
public class LampModRep : global::WeaponModRep
{
	// Token: 0x06003A50 RID: 14928 RVA: 0x000CCDF8 File Offset: 0x000CAFF8
	protected LampModRep(global::ItemModRepresentation.Caps caps, bool defaultOn) : base(caps, defaultOn)
	{
	}

	// Token: 0x06003A51 RID: 14929 RVA: 0x000CCE04 File Offset: 0x000CB004
	public LampModRep() : this(global::ItemModRepresentation.Caps.BindStateFlags, false)
	{
	}

	// Token: 0x06003A52 RID: 14930 RVA: 0x000CCE10 File Offset: 0x000CB010
	protected LampModRep(global::ItemModRepresentation.Caps caps) : this(caps, false)
	{
	}

	// Token: 0x06003A53 RID: 14931 RVA: 0x000CCE1C File Offset: 0x000CB01C
	protected override bool VerifyCompatible(GameObject attachment)
	{
		return attachment.GetComponentInChildren<Light>();
	}

	// Token: 0x06003A54 RID: 14932 RVA: 0x000CCE2C File Offset: 0x000CB02C
	protected override void OnAddAttached()
	{
		this.lights = base.attached.GetComponentsInChildren<Light>();
	}

	// Token: 0x06003A55 RID: 14933 RVA: 0x000CCE40 File Offset: 0x000CB040
	protected override void OnRemoveAttached()
	{
		this.lights = null;
	}

	// Token: 0x06003A56 RID: 14934 RVA: 0x000CCE4C File Offset: 0x000CB04C
	protected override void EnableMod(global::ItemModRepresentation.Reason reason)
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
		if (reason == global::ItemModRepresentation.Reason.Explicit)
		{
			this.PlaySound(anyLight, base.modDataBlock.onSound);
		}
	}

	// Token: 0x06003A57 RID: 14935 RVA: 0x000CCEA8 File Offset: 0x000CB0A8
	protected override void DisableMod(global::ItemModRepresentation.Reason reason)
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
		if (reason == global::ItemModRepresentation.Reason.Explicit)
		{
			this.PlaySound(anyLight, base.modDataBlock.offSound);
		}
	}

	// Token: 0x06003A58 RID: 14936 RVA: 0x000CCF04 File Offset: 0x000CB104
	protected override void BindStateFlags(global::CharacterStateFlags flags, global::ItemModRepresentation.Reason reason)
	{
		base.BindStateFlags(flags, reason);
		base.SetOn(flags.lamp, reason);
	}

	// Token: 0x06003A59 RID: 14937 RVA: 0x000CCF1C File Offset: 0x000CB11C
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

	// Token: 0x04001C8D RID: 7309
	private const float kVolume = 1f;

	// Token: 0x04001C8E RID: 7310
	private const float kMinAudioDistance = 1f;

	// Token: 0x04001C8F RID: 7311
	private const float kMaxAudioDistance = 4f;

	// Token: 0x04001C90 RID: 7312
	private const AudioRolloffMode kRolloffMode = 0;

	// Token: 0x04001C91 RID: 7313
	private Light[] lights;
}
