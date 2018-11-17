using System;
using UnityEngine;

// Token: 0x020005F1 RID: 1521
public class LaserModRep : WeaponModRep
{
	// Token: 0x06003692 RID: 13970 RVA: 0x000C4D24 File Offset: 0x000C2F24
	protected LaserModRep(ItemModRepresentation.Caps caps, bool defaultOn) : base(caps, defaultOn)
	{
	}

	// Token: 0x06003693 RID: 13971 RVA: 0x000C4D30 File Offset: 0x000C2F30
	public LaserModRep() : this(ItemModRepresentation.Caps.BindStateFlags, false)
	{
	}

	// Token: 0x06003694 RID: 13972 RVA: 0x000C4D3C File Offset: 0x000C2F3C
	protected LaserModRep(ItemModRepresentation.Caps caps) : this(caps, false)
	{
	}

	// Token: 0x06003696 RID: 13974 RVA: 0x000C4D50 File Offset: 0x000C2F50
	public override void SetAttached(GameObject attached, bool vm)
	{
		this.is_vm = vm;
		base.SetAttached(attached, vm);
	}

	// Token: 0x06003697 RID: 13975 RVA: 0x000C4D64 File Offset: 0x000C2F64
	protected override bool VerifyCompatible(GameObject attachment)
	{
		return attachment.GetComponentInChildren<LaserBeam>();
	}

	// Token: 0x06003698 RID: 13976 RVA: 0x000C4D74 File Offset: 0x000C2F74
	protected override void OnAddAttached()
	{
		this.beams = base.attached.GetComponentsInChildren<LaserBeam>();
	}

	// Token: 0x06003699 RID: 13977 RVA: 0x000C4D88 File Offset: 0x000C2F88
	protected override void OnRemoveAttached()
	{
		this.beams = null;
	}

	// Token: 0x0600369A RID: 13978 RVA: 0x000C4D94 File Offset: 0x000C2F94
	protected override void EnableMod(ItemModRepresentation.Reason reason)
	{
		LaserBeam anyBeam = null;
		foreach (LaserBeam laserBeam in this.beams)
		{
			if (laserBeam)
			{
				anyBeam = laserBeam;
				laserBeam.enabled = (this.is_vm || LaserModRep.allow_3rd_lasers);
			}
		}
		if (reason == ItemModRepresentation.Reason.Explicit)
		{
			this.PlaySound(anyBeam, base.modDataBlock.onSound);
		}
	}

	// Token: 0x0600369B RID: 13979 RVA: 0x000C4E04 File Offset: 0x000C3004
	protected override void DisableMod(ItemModRepresentation.Reason reason)
	{
		LaserBeam anyBeam = null;
		foreach (LaserBeam laserBeam in this.beams)
		{
			if (laserBeam)
			{
				anyBeam = laserBeam;
				laserBeam.enabled = false;
			}
		}
		if (reason == ItemModRepresentation.Reason.Explicit)
		{
			this.PlaySound(anyBeam, base.modDataBlock.offSound);
		}
	}

	// Token: 0x0600369C RID: 13980 RVA: 0x000C4E60 File Offset: 0x000C3060
	protected override void BindStateFlags(CharacterStateFlags flags, ItemModRepresentation.Reason reason)
	{
		base.BindStateFlags(flags, reason);
		base.SetOn(flags.laser, reason);
	}

	// Token: 0x0600369D RID: 13981 RVA: 0x000C4E78 File Offset: 0x000C3078
	private void PlaySound(LaserBeam anyBeam, AudioClip clip)
	{
		if (anyBeam)
		{
			clip.PlayLocal(anyBeam.transform, Vector3.zero, 1f, 0, 1f, 4f);
		}
		else
		{
			clip.PlayLocal(base.itemRep.transform, Vector3.zero, 1f, 0, 1f, 4f);
		}
	}

	// Token: 0x04001AC1 RID: 6849
	private const float kVolume = 1f;

	// Token: 0x04001AC2 RID: 6850
	private const float kMinAudioDistance = 1f;

	// Token: 0x04001AC3 RID: 6851
	private const float kMaxAudioDistance = 4f;

	// Token: 0x04001AC4 RID: 6852
	private const AudioRolloffMode kRolloffMode = 0;

	// Token: 0x04001AC5 RID: 6853
	private bool is_vm;

	// Token: 0x04001AC6 RID: 6854
	private LaserBeam[] beams;

	// Token: 0x04001AC7 RID: 6855
	private static bool allow_3rd_lasers = true;
}
