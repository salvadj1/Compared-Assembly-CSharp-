using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000823 RID: 2083
[AddComponentMenu("Daikon Forge/Tweens/Group")]
[Serializable]
public class dfTweenGroup : global::dfTweenPlayableBase
{
	// Token: 0x1400006B RID: 107
	// (add) Token: 0x06004846 RID: 18502 RVA: 0x00110F60 File Offset: 0x0010F160
	// (remove) Token: 0x06004847 RID: 18503 RVA: 0x00110F7C File Offset: 0x0010F17C
	public event global::TweenNotification TweenStarted;

	// Token: 0x1400006C RID: 108
	// (add) Token: 0x06004848 RID: 18504 RVA: 0x00110F98 File Offset: 0x0010F198
	// (remove) Token: 0x06004849 RID: 18505 RVA: 0x00110FB4 File Offset: 0x0010F1B4
	public event global::TweenNotification TweenStopped;

	// Token: 0x1400006D RID: 109
	// (add) Token: 0x0600484A RID: 18506 RVA: 0x00110FD0 File Offset: 0x0010F1D0
	// (remove) Token: 0x0600484B RID: 18507 RVA: 0x00110FEC File Offset: 0x0010F1EC
	public event global::TweenNotification TweenReset;

	// Token: 0x1400006E RID: 110
	// (add) Token: 0x0600484C RID: 18508 RVA: 0x00111008 File Offset: 0x0010F208
	// (remove) Token: 0x0600484D RID: 18509 RVA: 0x00111024 File Offset: 0x0010F224
	public event global::TweenNotification TweenCompleted;

	// Token: 0x17000DD6 RID: 3542
	// (get) Token: 0x0600484E RID: 18510 RVA: 0x00111040 File Offset: 0x0010F240
	// (set) Token: 0x0600484F RID: 18511 RVA: 0x00111048 File Offset: 0x0010F248
	public override string TweenName
	{
		get
		{
			return this.groupName;
		}
		set
		{
			this.groupName = value;
		}
	}

	// Token: 0x17000DD7 RID: 3543
	// (get) Token: 0x06004850 RID: 18512 RVA: 0x00111054 File Offset: 0x0010F254
	public override bool IsPlaying
	{
		get
		{
			for (int i = 0; i < this.Tweens.Count; i++)
			{
				if (!(this.Tweens[i] == null) && this.Tweens[i].enabled)
				{
					if (this.Tweens[i].IsPlaying)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	// Token: 0x06004851 RID: 18513 RVA: 0x001110C8 File Offset: 0x0010F2C8
	private void Update()
	{
	}

	// Token: 0x06004852 RID: 18514 RVA: 0x001110CC File Offset: 0x0010F2CC
	public void EnableTween(string TweenName)
	{
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null))
			{
				if (this.Tweens[i].TweenName == TweenName)
				{
					this.Tweens[i].enabled = true;
					break;
				}
			}
		}
	}

	// Token: 0x06004853 RID: 18515 RVA: 0x00111144 File Offset: 0x0010F344
	public void DisableTween(string TweenName)
	{
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null))
			{
				if (this.Tweens[i].name == TweenName)
				{
					this.Tweens[i].enabled = false;
					break;
				}
			}
		}
	}

	// Token: 0x06004854 RID: 18516 RVA: 0x001111BC File Offset: 0x0010F3BC
	public override void Play()
	{
		if (this.IsPlaying)
		{
			this.Stop();
		}
		this.onStarted();
		if (this.Mode == global::dfTweenGroup.TweenGroupMode.Concurrent)
		{
			base.StartCoroutine(this.runConcurrent());
		}
		else
		{
			base.StartCoroutine(this.runSequence());
		}
	}

	// Token: 0x06004855 RID: 18517 RVA: 0x0011120C File Offset: 0x0010F40C
	public override void Stop()
	{
		if (!this.IsPlaying)
		{
			return;
		}
		base.StopAllCoroutines();
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null))
			{
				this.Tweens[i].Stop();
			}
		}
		this.onStopped();
	}

	// Token: 0x06004856 RID: 18518 RVA: 0x0011127C File Offset: 0x0010F47C
	public override void Reset()
	{
		if (!this.IsPlaying)
		{
			return;
		}
		base.StopAllCoroutines();
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null))
			{
				this.Tweens[i].Reset();
			}
		}
		this.onReset();
	}

	// Token: 0x06004857 RID: 18519 RVA: 0x001112EC File Offset: 0x0010F4EC
	[HideInInspector]
	private IEnumerator runSequence()
	{
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null) && this.Tweens[i].enabled)
			{
				global::dfTweenPlayableBase tween = this.Tweens[i];
				tween.Play();
				while (tween.IsPlaying)
				{
					yield return null;
				}
			}
		}
		this.onCompleted();
		yield break;
	}

	// Token: 0x06004858 RID: 18520 RVA: 0x00111308 File Offset: 0x0010F508
	[HideInInspector]
	private IEnumerator runConcurrent()
	{
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null) && this.Tweens[i].enabled)
			{
				this.Tweens[i].Play();
			}
		}
		do
		{
			yield return null;
		}
		while (this.Tweens.Any((global::dfTweenPlayableBase tween) => tween != null && tween.IsPlaying));
		this.onCompleted();
		yield break;
	}

	// Token: 0x06004859 RID: 18521 RVA: 0x00111324 File Offset: 0x0010F524
	protected internal void onStarted()
	{
		base.SendMessage("TweenStarted", this, 1);
		if (this.TweenStarted != null)
		{
			this.TweenStarted();
		}
	}

	// Token: 0x0600485A RID: 18522 RVA: 0x0011134C File Offset: 0x0010F54C
	protected internal void onStopped()
	{
		base.SendMessage("TweenStopped", this, 1);
		if (this.TweenStopped != null)
		{
			this.TweenStopped();
		}
	}

	// Token: 0x0600485B RID: 18523 RVA: 0x00111374 File Offset: 0x0010F574
	protected internal void onReset()
	{
		base.SendMessage("TweenReset", this, 1);
		if (this.TweenReset != null)
		{
			this.TweenReset();
		}
	}

	// Token: 0x0600485C RID: 18524 RVA: 0x0011139C File Offset: 0x0010F59C
	protected internal void onCompleted()
	{
		base.SendMessage("TweenCompleted", this, 1);
		if (this.TweenCompleted != null)
		{
			this.TweenCompleted();
		}
	}

	// Token: 0x040025EF RID: 9711
	[SerializeField]
	protected string groupName = string.Empty;

	// Token: 0x040025F0 RID: 9712
	public List<global::dfTweenPlayableBase> Tweens = new List<global::dfTweenPlayableBase>();

	// Token: 0x040025F1 RID: 9713
	public global::dfTweenGroup.TweenGroupMode Mode;

	// Token: 0x02000824 RID: 2084
	public enum TweenGroupMode
	{
		// Token: 0x040025F7 RID: 9719
		Concurrent,
		// Token: 0x040025F8 RID: 9720
		Sequence
	}
}
