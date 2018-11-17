using System;
using UnityEngine;

// Token: 0x0200073F RID: 1855
[Serializable]
public abstract class dfTweenComponentBase : dfTweenPlayableBase
{
	// Token: 0x17000D3D RID: 3389
	// (get) Token: 0x060043C1 RID: 17345 RVA: 0x00107120 File Offset: 0x00105320
	// (set) Token: 0x060043C2 RID: 17346 RVA: 0x00107140 File Offset: 0x00105340
	public override string TweenName
	{
		get
		{
			if (this.tweenName == null)
			{
				this.tweenName = base.ToString();
			}
			return this.tweenName;
		}
		set
		{
			this.tweenName = value;
		}
	}

	// Token: 0x17000D3E RID: 3390
	// (get) Token: 0x060043C3 RID: 17347 RVA: 0x0010714C File Offset: 0x0010534C
	// (set) Token: 0x060043C4 RID: 17348 RVA: 0x00107154 File Offset: 0x00105354
	public dfComponentMemberInfo Target
	{
		get
		{
			return this.target;
		}
		set
		{
			this.target = value;
		}
	}

	// Token: 0x17000D3F RID: 3391
	// (get) Token: 0x060043C5 RID: 17349 RVA: 0x00107160 File Offset: 0x00105360
	// (set) Token: 0x060043C6 RID: 17350 RVA: 0x00107168 File Offset: 0x00105368
	public AnimationCurve AnimationCurve
	{
		get
		{
			return this.animCurve;
		}
		set
		{
			this.animCurve = value;
		}
	}

	// Token: 0x17000D40 RID: 3392
	// (get) Token: 0x060043C7 RID: 17351 RVA: 0x00107174 File Offset: 0x00105374
	// (set) Token: 0x060043C8 RID: 17352 RVA: 0x0010717C File Offset: 0x0010537C
	public float Length
	{
		get
		{
			return this.length;
		}
		set
		{
			this.length = Mathf.Max(0f, value);
		}
	}

	// Token: 0x17000D41 RID: 3393
	// (get) Token: 0x060043C9 RID: 17353 RVA: 0x00107190 File Offset: 0x00105390
	// (set) Token: 0x060043CA RID: 17354 RVA: 0x00107198 File Offset: 0x00105398
	public dfEasingType Function
	{
		get
		{
			return this.easingType;
		}
		set
		{
			this.easingType = value;
			if (this.isRunning)
			{
				this.Stop();
				this.Play();
			}
		}
	}

	// Token: 0x17000D42 RID: 3394
	// (get) Token: 0x060043CB RID: 17355 RVA: 0x001071B8 File Offset: 0x001053B8
	// (set) Token: 0x060043CC RID: 17356 RVA: 0x001071C0 File Offset: 0x001053C0
	public dfTweenLoopType LoopType
	{
		get
		{
			return this.loopType;
		}
		set
		{
			this.loopType = value;
			if (this.isRunning)
			{
				this.Stop();
				this.Play();
			}
		}
	}

	// Token: 0x17000D43 RID: 3395
	// (get) Token: 0x060043CD RID: 17357 RVA: 0x001071E0 File Offset: 0x001053E0
	// (set) Token: 0x060043CE RID: 17358 RVA: 0x001071E8 File Offset: 0x001053E8
	public bool SyncStartValueWhenRun
	{
		get
		{
			return this.syncStartWhenRun;
		}
		set
		{
			this.syncStartWhenRun = value;
		}
	}

	// Token: 0x17000D44 RID: 3396
	// (get) Token: 0x060043CF RID: 17359 RVA: 0x001071F4 File Offset: 0x001053F4
	// (set) Token: 0x060043D0 RID: 17360 RVA: 0x001071FC File Offset: 0x001053FC
	public bool StartValueIsOffset
	{
		get
		{
			return this.startValueIsOffset;
		}
		set
		{
			this.startValueIsOffset = value;
		}
	}

	// Token: 0x17000D45 RID: 3397
	// (get) Token: 0x060043D1 RID: 17361 RVA: 0x00107208 File Offset: 0x00105408
	// (set) Token: 0x060043D2 RID: 17362 RVA: 0x00107210 File Offset: 0x00105410
	public bool SyncEndValueWhenRun
	{
		get
		{
			return this.syncEndWhenRun;
		}
		set
		{
			this.syncEndWhenRun = value;
		}
	}

	// Token: 0x17000D46 RID: 3398
	// (get) Token: 0x060043D3 RID: 17363 RVA: 0x0010721C File Offset: 0x0010541C
	// (set) Token: 0x060043D4 RID: 17364 RVA: 0x00107224 File Offset: 0x00105424
	public bool EndValueIsOffset
	{
		get
		{
			return this.endValueIsOffset;
		}
		set
		{
			this.endValueIsOffset = value;
		}
	}

	// Token: 0x17000D47 RID: 3399
	// (get) Token: 0x060043D5 RID: 17365 RVA: 0x00107230 File Offset: 0x00105430
	// (set) Token: 0x060043D6 RID: 17366 RVA: 0x00107238 File Offset: 0x00105438
	public bool AutoRun
	{
		get
		{
			return this.autoRun;
		}
		set
		{
			this.autoRun = value;
		}
	}

	// Token: 0x17000D48 RID: 3400
	// (get) Token: 0x060043D7 RID: 17367 RVA: 0x00107244 File Offset: 0x00105444
	public override bool IsPlaying
	{
		get
		{
			return base.enabled && this.isRunning;
		}
	}

	// Token: 0x17000D49 RID: 3401
	// (get) Token: 0x060043D8 RID: 17368 RVA: 0x0010725C File Offset: 0x0010545C
	// (set) Token: 0x060043D9 RID: 17369 RVA: 0x00107264 File Offset: 0x00105464
	public bool IsPaused
	{
		get
		{
			return this.isPaused;
		}
		set
		{
			if (value != this.isPaused)
			{
				if (value && !this.isRunning)
				{
					this.isPaused = false;
					return;
				}
				this.isPaused = value;
				if (value)
				{
					this.onPaused();
				}
				else
				{
					this.onResumed();
				}
			}
		}
	}

	// Token: 0x060043DA RID: 17370
	protected internal abstract void onPaused();

	// Token: 0x060043DB RID: 17371
	protected internal abstract void onResumed();

	// Token: 0x060043DC RID: 17372
	protected internal abstract void onStarted();

	// Token: 0x060043DD RID: 17373
	protected internal abstract void onStopped();

	// Token: 0x060043DE RID: 17374
	protected internal abstract void onReset();

	// Token: 0x060043DF RID: 17375
	protected internal abstract void onCompleted();

	// Token: 0x060043E0 RID: 17376 RVA: 0x001072B4 File Offset: 0x001054B4
	public void LateUpdate()
	{
		if (this.autoRun && !this.wasAutoStarted)
		{
			this.wasAutoStarted = true;
			this.Play();
		}
	}

	// Token: 0x060043E1 RID: 17377 RVA: 0x001072DC File Offset: 0x001054DC
	public override string ToString()
	{
		if (this.Target != null && this.Target.IsValid)
		{
			string name = this.target.Component.name;
			return string.Format("{0} ({1}.{2})", this.TweenName, name, this.target.MemberName);
		}
		return this.TweenName;
	}

	// Token: 0x040023A0 RID: 9120
	[SerializeField]
	protected string tweenName = string.Empty;

	// Token: 0x040023A1 RID: 9121
	[SerializeField]
	protected dfComponentMemberInfo target;

	// Token: 0x040023A2 RID: 9122
	[SerializeField]
	protected dfEasingType easingType;

	// Token: 0x040023A3 RID: 9123
	[SerializeField]
	protected AnimationCurve animCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x040023A4 RID: 9124
	[SerializeField]
	protected float length = 1f;

	// Token: 0x040023A5 RID: 9125
	[SerializeField]
	protected bool syncStartWhenRun;

	// Token: 0x040023A6 RID: 9126
	[SerializeField]
	protected bool startValueIsOffset;

	// Token: 0x040023A7 RID: 9127
	[SerializeField]
	protected bool syncEndWhenRun;

	// Token: 0x040023A8 RID: 9128
	[SerializeField]
	protected bool endValueIsOffset;

	// Token: 0x040023A9 RID: 9129
	[SerializeField]
	protected dfTweenLoopType loopType;

	// Token: 0x040023AA RID: 9130
	[SerializeField]
	protected bool autoRun;

	// Token: 0x040023AB RID: 9131
	[SerializeField]
	protected bool skipToEndOnStop;

	// Token: 0x040023AC RID: 9132
	protected bool isRunning;

	// Token: 0x040023AD RID: 9133
	protected bool isPaused;

	// Token: 0x040023AE RID: 9134
	protected dfEasingFunctions.EasingFunction easingFunction;

	// Token: 0x040023AF RID: 9135
	protected dfObservableProperty boundProperty;

	// Token: 0x040023B0 RID: 9136
	protected bool wasAutoStarted;
}
