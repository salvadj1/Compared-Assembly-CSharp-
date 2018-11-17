using System;
using UnityEngine;

// Token: 0x0200081E RID: 2078
[Serializable]
public abstract class dfTweenComponentBase : global::dfTweenPlayableBase
{
	// Token: 0x17000DC9 RID: 3529
	// (get) Token: 0x06004813 RID: 18451 RVA: 0x001107E4 File Offset: 0x0010E9E4
	// (set) Token: 0x06004814 RID: 18452 RVA: 0x00110804 File Offset: 0x0010EA04
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

	// Token: 0x17000DCA RID: 3530
	// (get) Token: 0x06004815 RID: 18453 RVA: 0x00110810 File Offset: 0x0010EA10
	// (set) Token: 0x06004816 RID: 18454 RVA: 0x00110818 File Offset: 0x0010EA18
	public global::dfComponentMemberInfo Target
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

	// Token: 0x17000DCB RID: 3531
	// (get) Token: 0x06004817 RID: 18455 RVA: 0x00110824 File Offset: 0x0010EA24
	// (set) Token: 0x06004818 RID: 18456 RVA: 0x0011082C File Offset: 0x0010EA2C
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

	// Token: 0x17000DCC RID: 3532
	// (get) Token: 0x06004819 RID: 18457 RVA: 0x00110838 File Offset: 0x0010EA38
	// (set) Token: 0x0600481A RID: 18458 RVA: 0x00110840 File Offset: 0x0010EA40
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

	// Token: 0x17000DCD RID: 3533
	// (get) Token: 0x0600481B RID: 18459 RVA: 0x00110854 File Offset: 0x0010EA54
	// (set) Token: 0x0600481C RID: 18460 RVA: 0x0011085C File Offset: 0x0010EA5C
	public global::dfEasingType Function
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

	// Token: 0x17000DCE RID: 3534
	// (get) Token: 0x0600481D RID: 18461 RVA: 0x0011087C File Offset: 0x0010EA7C
	// (set) Token: 0x0600481E RID: 18462 RVA: 0x00110884 File Offset: 0x0010EA84
	public global::dfTweenLoopType LoopType
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

	// Token: 0x17000DCF RID: 3535
	// (get) Token: 0x0600481F RID: 18463 RVA: 0x001108A4 File Offset: 0x0010EAA4
	// (set) Token: 0x06004820 RID: 18464 RVA: 0x001108AC File Offset: 0x0010EAAC
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

	// Token: 0x17000DD0 RID: 3536
	// (get) Token: 0x06004821 RID: 18465 RVA: 0x001108B8 File Offset: 0x0010EAB8
	// (set) Token: 0x06004822 RID: 18466 RVA: 0x001108C0 File Offset: 0x0010EAC0
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

	// Token: 0x17000DD1 RID: 3537
	// (get) Token: 0x06004823 RID: 18467 RVA: 0x001108CC File Offset: 0x0010EACC
	// (set) Token: 0x06004824 RID: 18468 RVA: 0x001108D4 File Offset: 0x0010EAD4
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

	// Token: 0x17000DD2 RID: 3538
	// (get) Token: 0x06004825 RID: 18469 RVA: 0x001108E0 File Offset: 0x0010EAE0
	// (set) Token: 0x06004826 RID: 18470 RVA: 0x001108E8 File Offset: 0x0010EAE8
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

	// Token: 0x17000DD3 RID: 3539
	// (get) Token: 0x06004827 RID: 18471 RVA: 0x001108F4 File Offset: 0x0010EAF4
	// (set) Token: 0x06004828 RID: 18472 RVA: 0x001108FC File Offset: 0x0010EAFC
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

	// Token: 0x17000DD4 RID: 3540
	// (get) Token: 0x06004829 RID: 18473 RVA: 0x00110908 File Offset: 0x0010EB08
	public override bool IsPlaying
	{
		get
		{
			return base.enabled && this.isRunning;
		}
	}

	// Token: 0x17000DD5 RID: 3541
	// (get) Token: 0x0600482A RID: 18474 RVA: 0x00110920 File Offset: 0x0010EB20
	// (set) Token: 0x0600482B RID: 18475 RVA: 0x00110928 File Offset: 0x0010EB28
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

	// Token: 0x0600482C RID: 18476
	protected internal abstract void onPaused();

	// Token: 0x0600482D RID: 18477
	protected internal abstract void onResumed();

	// Token: 0x0600482E RID: 18478
	protected internal abstract void onStarted();

	// Token: 0x0600482F RID: 18479
	protected internal abstract void onStopped();

	// Token: 0x06004830 RID: 18480
	protected internal abstract void onReset();

	// Token: 0x06004831 RID: 18481
	protected internal abstract void onCompleted();

	// Token: 0x06004832 RID: 18482 RVA: 0x00110978 File Offset: 0x0010EB78
	public void LateUpdate()
	{
		if (this.autoRun && !this.wasAutoStarted)
		{
			this.wasAutoStarted = true;
			this.Play();
		}
	}

	// Token: 0x06004833 RID: 18483 RVA: 0x001109A0 File Offset: 0x0010EBA0
	public override string ToString()
	{
		if (this.Target != null && this.Target.IsValid)
		{
			string name = this.target.Component.name;
			return string.Format("{0} ({1}.{2})", this.TweenName, name, this.target.MemberName);
		}
		return this.TweenName;
	}

	// Token: 0x040025CC RID: 9676
	[SerializeField]
	protected string tweenName = string.Empty;

	// Token: 0x040025CD RID: 9677
	[SerializeField]
	protected global::dfComponentMemberInfo target;

	// Token: 0x040025CE RID: 9678
	[SerializeField]
	protected global::dfEasingType easingType;

	// Token: 0x040025CF RID: 9679
	[SerializeField]
	protected AnimationCurve animCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f, 0f, 1f),
		new Keyframe(1f, 1f, 1f, 0f)
	});

	// Token: 0x040025D0 RID: 9680
	[SerializeField]
	protected float length = 1f;

	// Token: 0x040025D1 RID: 9681
	[SerializeField]
	protected bool syncStartWhenRun;

	// Token: 0x040025D2 RID: 9682
	[SerializeField]
	protected bool startValueIsOffset;

	// Token: 0x040025D3 RID: 9683
	[SerializeField]
	protected bool syncEndWhenRun;

	// Token: 0x040025D4 RID: 9684
	[SerializeField]
	protected bool endValueIsOffset;

	// Token: 0x040025D5 RID: 9685
	[SerializeField]
	protected global::dfTweenLoopType loopType;

	// Token: 0x040025D6 RID: 9686
	[SerializeField]
	protected bool autoRun;

	// Token: 0x040025D7 RID: 9687
	[SerializeField]
	protected bool skipToEndOnStop;

	// Token: 0x040025D8 RID: 9688
	protected bool isRunning;

	// Token: 0x040025D9 RID: 9689
	protected bool isPaused;

	// Token: 0x040025DA RID: 9690
	protected global::dfEasingFunctions.EasingFunction easingFunction;

	// Token: 0x040025DB RID: 9691
	protected global::dfObservableProperty boundProperty;

	// Token: 0x040025DC RID: 9692
	protected bool wasAutoStarted;
}
