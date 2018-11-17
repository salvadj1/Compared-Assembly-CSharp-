using System;
using System.Collections;
using System.Text;
using UnityEngine;

// Token: 0x0200073E RID: 1854
[Serializable]
public abstract class dfTweenComponent<T> : dfTweenComponentBase
{
	// Token: 0x14000065 RID: 101
	// (add) Token: 0x0600439F RID: 17311 RVA: 0x00106B80 File Offset: 0x00104D80
	// (remove) Token: 0x060043A0 RID: 17312 RVA: 0x00106B9C File Offset: 0x00104D9C
	public event TweenNotification TweenStarted;

	// Token: 0x14000066 RID: 102
	// (add) Token: 0x060043A1 RID: 17313 RVA: 0x00106BB8 File Offset: 0x00104DB8
	// (remove) Token: 0x060043A2 RID: 17314 RVA: 0x00106BD4 File Offset: 0x00104DD4
	public event TweenNotification TweenStopped;

	// Token: 0x14000067 RID: 103
	// (add) Token: 0x060043A3 RID: 17315 RVA: 0x00106BF0 File Offset: 0x00104DF0
	// (remove) Token: 0x060043A4 RID: 17316 RVA: 0x00106C0C File Offset: 0x00104E0C
	public event TweenNotification TweenPaused;

	// Token: 0x14000068 RID: 104
	// (add) Token: 0x060043A5 RID: 17317 RVA: 0x00106C28 File Offset: 0x00104E28
	// (remove) Token: 0x060043A6 RID: 17318 RVA: 0x00106C44 File Offset: 0x00104E44
	public event TweenNotification TweenResumed;

	// Token: 0x14000069 RID: 105
	// (add) Token: 0x060043A7 RID: 17319 RVA: 0x00106C60 File Offset: 0x00104E60
	// (remove) Token: 0x060043A8 RID: 17320 RVA: 0x00106C7C File Offset: 0x00104E7C
	public event TweenNotification TweenReset;

	// Token: 0x1400006A RID: 106
	// (add) Token: 0x060043A9 RID: 17321 RVA: 0x00106C98 File Offset: 0x00104E98
	// (remove) Token: 0x060043AA RID: 17322 RVA: 0x00106CB4 File Offset: 0x00104EB4
	public event TweenNotification TweenCompleted;

	// Token: 0x17000D3B RID: 3387
	// (get) Token: 0x060043AB RID: 17323 RVA: 0x00106CD0 File Offset: 0x00104ED0
	// (set) Token: 0x060043AC RID: 17324 RVA: 0x00106CD8 File Offset: 0x00104ED8
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

	// Token: 0x17000D3C RID: 3388
	// (get) Token: 0x060043AD RID: 17325 RVA: 0x00106CF8 File Offset: 0x00104EF8
	// (set) Token: 0x060043AE RID: 17326 RVA: 0x00106D00 File Offset: 0x00104F00
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

	// Token: 0x060043AF RID: 17327 RVA: 0x00106D20 File Offset: 0x00104F20
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
		dfObservableProperty property = this.target.GetProperty();
		base.StartCoroutine(this.Execute(property));
	}

	// Token: 0x060043B0 RID: 17328 RVA: 0x00106DE8 File Offset: 0x00104FE8
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

	// Token: 0x060043B1 RID: 17329 RVA: 0x00106E44 File Offset: 0x00105044
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

	// Token: 0x060043B2 RID: 17330 RVA: 0x00106E94 File Offset: 0x00105094
	public void Pause()
	{
		base.IsPaused = true;
	}

	// Token: 0x060043B3 RID: 17331 RVA: 0x00106EA0 File Offset: 0x001050A0
	public void Resume()
	{
		base.IsPaused = false;
	}

	// Token: 0x060043B4 RID: 17332 RVA: 0x00106EAC File Offset: 0x001050AC
	protected internal IEnumerator Execute(dfObservableProperty property)
	{
		this.isRunning = true;
		this.easingFunction = dfEasingFunctions.GetFunction(this.easingType);
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
					if (this.loopType == dfTweenLoopType.Once)
					{
						break;
					}
					if (this.loopType == dfTweenLoopType.Loop)
					{
						startTime = Time.realtimeSinceStartup;
					}
					else
					{
						if (this.loopType != dfTweenLoopType.PingPong)
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

	// Token: 0x060043B5 RID: 17333
	public abstract T evaluate(T startValue, T endValue, float time);

	// Token: 0x060043B6 RID: 17334
	public abstract T offset(T value, T offset);

	// Token: 0x060043B7 RID: 17335 RVA: 0x00106ED8 File Offset: 0x001050D8
	public override string ToString()
	{
		if (base.Target != null && base.Target.IsValid)
		{
			string name = this.target.Component.name;
			return string.Format("{0} ({1}.{2})", this.TweenName, name, this.target.MemberName);
		}
		return this.TweenName;
	}

	// Token: 0x060043B8 RID: 17336 RVA: 0x00106F34 File Offset: 0x00105134
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

	// Token: 0x060043B9 RID: 17337 RVA: 0x00106FA0 File Offset: 0x001051A0
	protected internal static float Lerp(float startValue, float endValue, float time)
	{
		return startValue + (endValue - startValue) * time;
	}

	// Token: 0x060043BA RID: 17338 RVA: 0x00106FAC File Offset: 0x001051AC
	protected internal override void onPaused()
	{
		base.SendMessage("TweenPaused", this, 1);
		if (this.TweenPaused != null)
		{
			this.TweenPaused();
		}
	}

	// Token: 0x060043BB RID: 17339 RVA: 0x00106FD4 File Offset: 0x001051D4
	protected internal override void onResumed()
	{
		base.SendMessage("TweenResumed", this, 1);
		if (this.TweenResumed != null)
		{
			this.TweenResumed();
		}
	}

	// Token: 0x060043BC RID: 17340 RVA: 0x00106FFC File Offset: 0x001051FC
	protected internal override void onStarted()
	{
		base.SendMessage("TweenStarted", this, 1);
		if (this.TweenStarted != null)
		{
			this.TweenStarted();
		}
	}

	// Token: 0x060043BD RID: 17341 RVA: 0x00107024 File Offset: 0x00105224
	protected internal override void onStopped()
	{
		base.SendMessage("TweenStopped", this, 1);
		if (this.TweenStopped != null)
		{
			this.TweenStopped();
		}
	}

	// Token: 0x060043BE RID: 17342 RVA: 0x0010704C File Offset: 0x0010524C
	protected internal override void onReset()
	{
		base.SendMessage("TweenReset", this, 1);
		if (this.TweenReset != null)
		{
			this.TweenReset();
		}
	}

	// Token: 0x060043BF RID: 17343 RVA: 0x00107074 File Offset: 0x00105274
	protected internal override void onCompleted()
	{
		base.SendMessage("TweenCompleted", this, 1);
		if (this.TweenCompleted != null)
		{
			this.TweenCompleted();
		}
	}

	// Token: 0x04002396 RID: 9110
	[SerializeField]
	protected T startValue;

	// Token: 0x04002397 RID: 9111
	[SerializeField]
	protected T endValue;

	// Token: 0x04002398 RID: 9112
	private T actualStartValue;

	// Token: 0x04002399 RID: 9113
	private T actualEndValue;
}
