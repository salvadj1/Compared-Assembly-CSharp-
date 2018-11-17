using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000743 RID: 1859
[AddComponentMenu("Daikon Forge/Tweens/Group")]
[Serializable]
public class dfTweenGroup : dfTweenPlayableBase
{
	// Token: 0x1400006B RID: 107
	// (add) Token: 0x060043F2 RID: 17394 RVA: 0x00107880 File Offset: 0x00105A80
	// (remove) Token: 0x060043F3 RID: 17395 RVA: 0x0010789C File Offset: 0x00105A9C
	public event TweenNotification TweenStarted;

	// Token: 0x1400006C RID: 108
	// (add) Token: 0x060043F4 RID: 17396 RVA: 0x001078B8 File Offset: 0x00105AB8
	// (remove) Token: 0x060043F5 RID: 17397 RVA: 0x001078D4 File Offset: 0x00105AD4
	public event TweenNotification TweenStopped;

	// Token: 0x1400006D RID: 109
	// (add) Token: 0x060043F6 RID: 17398 RVA: 0x001078F0 File Offset: 0x00105AF0
	// (remove) Token: 0x060043F7 RID: 17399 RVA: 0x0010790C File Offset: 0x00105B0C
	public event TweenNotification TweenReset;

	// Token: 0x1400006E RID: 110
	// (add) Token: 0x060043F8 RID: 17400 RVA: 0x00107928 File Offset: 0x00105B28
	// (remove) Token: 0x060043F9 RID: 17401 RVA: 0x00107944 File Offset: 0x00105B44
	public event TweenNotification TweenCompleted;

	// Token: 0x17000D4A RID: 3402
	// (get) Token: 0x060043FA RID: 17402 RVA: 0x00107960 File Offset: 0x00105B60
	// (set) Token: 0x060043FB RID: 17403 RVA: 0x00107968 File Offset: 0x00105B68
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

	// Token: 0x17000D4B RID: 3403
	// (get) Token: 0x060043FC RID: 17404 RVA: 0x00107974 File Offset: 0x00105B74
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

	// Token: 0x060043FD RID: 17405 RVA: 0x001079E8 File Offset: 0x00105BE8
	private void Update()
	{
	}

	// Token: 0x060043FE RID: 17406 RVA: 0x001079EC File Offset: 0x00105BEC
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

	// Token: 0x060043FF RID: 17407 RVA: 0x00107A64 File Offset: 0x00105C64
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

	// Token: 0x06004400 RID: 17408 RVA: 0x00107ADC File Offset: 0x00105CDC
	public override void Play()
	{
		if (this.IsPlaying)
		{
			this.Stop();
		}
		this.onStarted();
		if (this.Mode == dfTweenGroup.TweenGroupMode.Concurrent)
		{
			base.StartCoroutine(this.runConcurrent());
		}
		else
		{
			base.StartCoroutine(this.runSequence());
		}
	}

	// Token: 0x06004401 RID: 17409 RVA: 0x00107B2C File Offset: 0x00105D2C
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

	// Token: 0x06004402 RID: 17410 RVA: 0x00107B9C File Offset: 0x00105D9C
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

	// Token: 0x06004403 RID: 17411 RVA: 0x00107C0C File Offset: 0x00105E0C
	[HideInInspector]
	private IEnumerator runSequence()
	{
		for (int i = 0; i < this.Tweens.Count; i++)
		{
			if (!(this.Tweens[i] == null) && this.Tweens[i].enabled)
			{
				dfTweenPlayableBase tween = this.Tweens[i];
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

	// Token: 0x06004404 RID: 17412 RVA: 0x00107C28 File Offset: 0x00105E28
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
		while (this.Tweens.Any((dfTweenPlayableBase tween) => tween != null && tween.IsPlaying));
		this.onCompleted();
		yield break;
	}

	// Token: 0x06004405 RID: 17413 RVA: 0x00107C44 File Offset: 0x00105E44
	protected internal void onStarted()
	{
		base.SendMessage("TweenStarted", this, 1);
		if (this.TweenStarted != null)
		{
			this.TweenStarted();
		}
	}

	// Token: 0x06004406 RID: 17414 RVA: 0x00107C6C File Offset: 0x00105E6C
	protected internal void onStopped()
	{
		base.SendMessage("TweenStopped", this, 1);
		if (this.TweenStopped != null)
		{
			this.TweenStopped();
		}
	}

	// Token: 0x06004407 RID: 17415 RVA: 0x00107C94 File Offset: 0x00105E94
	protected internal void onReset()
	{
		base.SendMessage("TweenReset", this, 1);
		if (this.TweenReset != null)
		{
			this.TweenReset();
		}
	}

	// Token: 0x06004408 RID: 17416 RVA: 0x00107CBC File Offset: 0x00105EBC
	protected internal void onCompleted()
	{
		base.SendMessage("TweenCompleted", this, 1);
		if (this.TweenCompleted != null)
		{
			this.TweenCompleted();
		}
	}

	// Token: 0x040023C2 RID: 9154
	[SerializeField]
	protected string groupName = string.Empty;

	// Token: 0x040023C3 RID: 9155
	public List<dfTweenPlayableBase> Tweens = new List<dfTweenPlayableBase>();

	// Token: 0x040023C4 RID: 9156
	public dfTweenGroup.TweenGroupMode Mode;

	// Token: 0x02000744 RID: 1860
	public enum TweenGroupMode
	{
		// Token: 0x040023CA RID: 9162
		Concurrent,
		// Token: 0x040023CB RID: 9163
		Sequence
	}
}
