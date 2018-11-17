using System;
using System.Collections;
using System.Text;
using UnityEngine;

// Token: 0x0200081C RID: 2076
[Serializable]
public abstract class dfTweenComponent<T> : global::dfTweenComponentBase
{
	// Token: 0x14000065 RID: 101
	// (add) Token: 0x060047EB RID: 18411 RVA: 0x0010FE90 File Offset: 0x0010E090
	// (remove) Token: 0x060047EC RID: 18412 RVA: 0x0010FEAC File Offset: 0x0010E0AC
	public event global::TweenNotification TweenStarted;

	// Token: 0x14000066 RID: 102
	// (add) Token: 0x060047ED RID: 18413 RVA: 0x0010FEC8 File Offset: 0x0010E0C8
	// (remove) Token: 0x060047EE RID: 18414 RVA: 0x0010FEE4 File Offset: 0x0010E0E4
	public event global::TweenNotification TweenStopped;

	// Token: 0x14000067 RID: 103
	// (add) Token: 0x060047EF RID: 18415 RVA: 0x0010FF00 File Offset: 0x0010E100
	// (remove) Token: 0x060047F0 RID: 18416 RVA: 0x0010FF1C File Offset: 0x0010E11C
	public event global::TweenNotification TweenPaused;

	// Token: 0x14000068 RID: 104
	// (add) Token: 0x060047F1 RID: 18417 RVA: 0x0010FF38 File Offset: 0x0010E138
	// (remove) Token: 0x060047F2 RID: 18418 RVA: 0x0010FF54 File Offset: 0x0010E154
	public event global::TweenNotification TweenResumed;

	// Token: 0x14000069 RID: 105
	// (add) Token: 0x060047F3 RID: 18419 RVA: 0x0010FF70 File Offset: 0x0010E170
	// (remove) Token: 0x060047F4 RID: 18420 RVA: 0x0010FF8C File Offset: 0x0010E18C
	public event global::TweenNotification TweenReset;

	// Token: 0x1400006A RID: 106
	// (add) Token: 0x060047F5 RID: 18421 RVA: 0x0010FFA8 File Offset: 0x0010E1A8
	// (remove) Token: 0x060047F6 RID: 18422 RVA: 0x0010FFC4 File Offset: 0x0010E1C4
	public event global::TweenNotification TweenCompleted;

	// Token: 0x17000DC5 RID: 3525
	// (get) Token: 0x060047F7 RID: 18423 RVA: 0x0010FFE0 File Offset: 0x0010E1E0
	// (set) Token: 0x060047F8 RID: 18424 RVA: 0x0010FFE8 File Offset: 0x0010E1E8
	public T StartValue
	{
		get
		{
			return this.startValue;
		}
		set
		{
			this.startValue = value;
			if (this.isRunning)
			{
				this.Stop();
				this.Play();
			}
		}
	}

	// Token: 0x17000DC6 RID: 3526
	// (get) Token: 0x060047F9 RID: 18425 RVA: 0x00110008 File Offset: 0x0010E208
	// (set) Token: 0x060047FA RID: 18426 RVA: 0x00110010 File Offset: 0x0010E210
	public T EndValue
	{
		get
		{
			return this.endValue;
		}
		set
		{
			this.endValue = value;
			if (this.isRunning)
			{
				this.Stop();
				this.Play();
			}
		}
	}

	// Token: 0x060047FB RID: 18427 RVA: 0x00110030 File Offset: 0x0010E230
	public override void Play()
	{
		if (this.isRunning)
		{
			this.Stop();
		}
		if (!base.enabled || !base.gameObject.activeSelf || !base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (this.target == null)
		{
			throw new NullReferenceException("Tween target is NULL");
		}
		if (!this.target.IsValid)
		{
			throw new InvalidOperationException(string.Concat(new object[]
			{
				"Invalid property binding configuration on ",
				this.getPath(base.gameObject.transform),
				" - ",
				this.target
			}));
		}
		global::dfObservableProperty property = this.target.GetProperty();
		base.StartCoroutine(this.Execute(property));
	}

	// Token: 0x060047FC RID: 18428 RVA: 0x001100F8 File Offset: 0x0010E2F8
	public override void Stop()
	{
		if (!this.isRunning)
		{
			return;
		}
		if (this.skipToEndOnStop)
		{
			this.boundProperty.Value = this.actualEndValue;
		}
		base.StopAllCoroutines();
		this.isRunning = false;
		this.onStopped();
		this.easingFunction = null;
		this.boundProperty = null;
	}

	// Token: 0x060047FD RID: 18429 RVA: 0x00110154 File Offset: 0x0010E354
	public override void Reset()
	{
		if (!this.isRunning)
		{
			return;
		}
		this.boundProperty.Value = this.actualStartValue;
		base.StopAllCoroutines();
		this.isRunning = false;
		this.onReset();
		this.easingFunction = null;
		this.boundProperty = null;
	}

	// Token: 0x060047FE RID: 18430 RVA: 0x001101A4 File Offset: 0x0010E3A4
	public void Pause()
	{
		base.IsPaused = true;
	}

	// Token: 0x060047FF RID: 18431 RVA: 0x001101B0 File Offset: 0x0010E3B0
	public void Resume()
	{
		base.IsPaused = false;
	}

	// Token: 0x06004800 RID: 18432 RVA: 0x001101BC File Offset: 0x0010E3BC
	protected internal IEnumerator Execute(global::dfObservableProperty property)
	{
		this.isRunning = true;
		this.easingFunction = global::dfEasingFunctions.GetFunction(this.easingType);
		this.boundProperty = property;
		this.onStarted();
		float startTime = Time.realtimeSinceStartup;
		float elapsed = 0f;
		float pingPongDirection = 0f;
		this.actualStartValue = this.startValue;
		this.actualEndValue = this.endValue;
		if (this.syncStartWhenRun)
		{
			this.actualStartValue = (T)((object)property.Value);
		}
		else if (this.startValueIsOffset)
		{
			this.actualStartValue = this.offset(this.startValue, (T)((object)property.Value));
		}
		if (this.syncEndWhenRun)
		{
			this.actualEndValue = (T)((object)property.Value);
		}
		else if (this.endValueIsOffset)
		{
			this.actualEndValue = this.offset(this.endValue, (T)((object)property.Value));
		}
		for (;;)
		{
			if (this.isPaused)
			{
				yield return null;
			}
			else
			{
				elapsed = Mathf.Min(Time.realtimeSinceStartup - startTime, this.length);
				float time = this.easingFunction(0f, 1f, Mathf.Abs(pingPongDirection - elapsed / this.length));
				if (this.animCurve != null)
				{
					time = this.animCurve.Evaluate(time);
				}
				property.Value = this.evaluate(this.actualStartValue, this.actualEndValue, time);
				if (elapsed >= this.length)
				{
					if (this.loopType == global::dfTweenLoopType.Once)
					{
						break;
					}
					if (this.loopType == global::dfTweenLoopType.Loop)
					{
						startTime = Time.realtimeSinceStartup;
					}
					else
					{
						if (this.loopType != global::dfTweenLoopType.PingPong)
						{
							goto IL_31A;
						}
						startTime = Time.realtimeSinceStartup;
						if (pingPongDirection == 0f)
						{
							pingPongDirection = 1f;
						}
						else
						{
							pingPongDirection = 0f;
						}
					}
				}
				yield return null;
			}
		}
		this.boundProperty.Value = this.actualEndValue;
		this.isRunning = false;
		this.onCompleted();
		yield break;
		IL_31A:
		throw new NotImplementedException();
	}

	// Token: 0x06004801 RID: 18433
	public abstract T evaluate(T startValue, T endValue, float time);

	// Token: 0x06004802 RID: 18434
	public abstract T offset(T value, T offset);

	// Token: 0x06004803 RID: 18435 RVA: 0x001101E8 File Offset: 0x0010E3E8
	public override string ToString()
	{
		if (base.Target != null && base.Target.IsValid)
		{
			string name = this.target.Component.name;
			return string.Format("{0} ({1}.{2})", this.TweenName, name, this.target.MemberName);
		}
		return this.TweenName;
	}

	// Token: 0x06004804 RID: 18436 RVA: 0x00110244 File Offset: 0x0010E444
	private string getPath(Transform obj)
	{
		StringBuilder stringBuilder = new StringBuilder();
		while (obj != null)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Insert(0, "\\");
				stringBuilder.Insert(0, obj.name);
			}
			else
			{
				stringBuilder.Append(obj.name);
			}
			obj = obj.parent;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06004805 RID: 18437 RVA: 0x001102B0 File Offset: 0x0010E4B0
	protected internal static float Lerp(float startValue, float endValue, float time)
	{
		return startValue + (endValue - startValue) * time;
	}

	// Token: 0x06004806 RID: 18438 RVA: 0x001102BC File Offset: 0x0010E4BC
	protected internal override void onPaused()
	{
		base.SendMessage("TweenPaused", this, 1);
		if (this.TweenPaused != null)
		{
			this.TweenPaused();
		}
	}

	// Token: 0x06004807 RID: 18439 RVA: 0x001102E4 File Offset: 0x0010E4E4
	protected internal override void onResumed()
	{
		base.SendMessage("TweenResumed", this, 1);
		if (this.TweenResumed != null)
		{
			this.TweenResumed();
		}
	}

	// Token: 0x06004808 RID: 18440 RVA: 0x0011030C File Offset: 0x0010E50C
	protected internal override void onStarted()
	{
		base.SendMessage("TweenStarted", this, 1);
		if (this.TweenStarted != null)
		{
			this.TweenStarted();
		}
	}

	// Token: 0x06004809 RID: 18441 RVA: 0x00110334 File Offset: 0x0010E534
	protected internal override void onStopped()
	{
		base.SendMessage("TweenStopped", this, 1);
		if (this.TweenStopped != null)
		{
			this.TweenStopped();
		}
	}

	// Token: 0x0600480A RID: 18442 RVA: 0x0011035C File Offset: 0x0010E55C
	protected internal override void onReset()
	{
		base.SendMessage("TweenReset", this, 1);
		if (this.TweenReset != null)
		{
			this.TweenReset();
		}
	}

	// Token: 0x0600480B RID: 18443 RVA: 0x00110384 File Offset: 0x0010E584
	protected internal override void onCompleted()
	{
		base.SendMessage("TweenCompleted", this, 1);
		if (this.TweenCompleted != null)
		{
			this.TweenCompleted();
		}
	}

	// Token: 0x040025B9 RID: 9657
	[SerializeField]
	protected T startValue;

	// Token: 0x040025BA RID: 9658
	[SerializeField]
	protected T endValue;

	// Token: 0x040025BB RID: 9659
	private T actualStartValue;

	// Token: 0x040025BC RID: 9660
	private T actualEndValue;
}
