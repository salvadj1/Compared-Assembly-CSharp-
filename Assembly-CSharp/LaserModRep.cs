using System;
using UnityEngine;

// Token: 0x020006AF RID: 1711
public class LaserModRep : global::WeaponModRep
{
	// Token: 0x06003A5A RID: 14938 RVA: 0x000CCF80 File Offset: 0x000CB180
	protected LaserModRep(global::ItemModRepresentation.Caps caps, bool defaultOn) : base(caps, defaultOn)
	{
	}

	// Token: 0x06003A5B RID: 14939 RVA: 0x000CCF8C File Offset: 0x000CB18C
	public LaserModRep() : this(global::ItemModRepresentation.Caps.BindStateFlags, false)
	{
	}

	// Token: 0x06003A5C RID: 14940 RVA: 0x000CCF98 File Offset: 0x000CB198
	protected LaserModRep(global::ItemModRepresentation.Caps caps) : this(caps, false)
	{
	}

	// Token: 0x06003A5E RID: 14942 RVA: 0x000CCFAC File Offset: 0x000CB1AC
	public override void SetAttached(GameObject attached, bool vm)
	{
		this.is_vm = vm;
		base.SetAttached(attached, vm);
	}

	// Token: 0x06003A5F RID: 14943 RVA: 0x000CCFC0 File Offset: 0x000CB1C0
	protected override bool VerifyCompatible(GameObject attachment)
	{
		return attachment.GetComponentInChildren<global::LaserBeam>();
	}

	// Token: 0x06003A60 RID: 14944 RVA: 0x000CCFD0 File Offset: 0x000CB1D0
	protected override void OnAddAttached()
	{
		this.beams = base.attached.GetComponentsInChildren<global::LaserBeam>();
	}

	// Token: 0x06003A61 RID: 14945 RVA: 0x000CCFE4 File Offset: 0x000CB1E4
	protected override void OnRemoveAttached()
	{
		this.beams = null;
	}

	// Token: 0x06003A62 RID: 14946 RVA: 0x000CCFF0 File Offset: 0x000CB1F0
	protected override void EnableMod(global::ItemModRepresentation.Reason reason)
	{
		global::LaserBeam anyBeam = null;
		foreach (global::LaserBeam laserBeam in this.beams)
		{
			if (laserBeam)
			{
				anyBeam = laserBeam;
				laserBeam.enabled = (this.is_vm || global::LaserModRep.allow_3rd_lasers);
			}
		}
		if (reason == global::ItemModRepresentation.Reason.Explicit)
		{
			this.PlaySound(anyBeam, base.modDataBlock.onSound);
		}
	}

	// Token: 0x06003A63 RID: 14947 RVA: 0x000CD060 File Offset: 0x000CB260
	protected override void DisableMod(global::ItemModRepresentation.Reason reason)
	{
		global::LaserBeam anyBeam = null;
		foreach (global::LaserBeam laserBeam in this.beams)
		{
			if (laserBeam)
			{
				anyBeam = laserBeam;
				laserBeam.enabled = false;
			}
		}
		if (reason == global::ItemModRepresentation.Reason.Explicit)
		{
			this.PlaySound(anyBeam, base.modDataBlock.offSound);
		}
	}

	// Token: 0x06003A64 RID: 14948 RVA: 0x000CD0BC File Offset: 0x000CB2BC
	protected override void BindStateFlags(global::CharacterStateFlags flags, global::ItemModRepresentation.Reason reason)
	{
		base.BindStateFlags(flags, reason);
		base.SetOn(flags.laser, reason);
	}

	// Token: 0x06003A65 RID: 14949 RVA: 0x000CD0D4 File Offset: 0x000CB2D4
	private void PlaySound(global::LaserBeam anyBeam, AudioClip clip)
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

	// Token: 0x04001C92 RID: 7314
	private const float kVolume = 1f;

	// Token: 0x04001C93 RID: 7315
	private const float kMinAudioDistance = 1f;

	// Token: 0x04001C94 RID: 7316
	private const float kMaxAudioDistance = 4f;

	// Token: 0x04001C95 RID: 7317
	private const AudioRolloffMode kRolloffMode = 0;

	// Token: 0x04001C96 RID: 7318
	private bool is_vm;

	// Token: 0x04001C97 RID: 7319
	private global::LaserBeam[] beams;

	// Token: 0x04001C98 RID: 7320
	private static bool allow_3rd_lasers = true;
}
