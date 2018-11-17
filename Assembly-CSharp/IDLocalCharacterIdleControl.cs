using System;

// Token: 0x0200014B RID: 331
public abstract class IDLocalCharacterIdleControl : IDLocalCharacter
{
	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0002861C File Offset: 0x0002681C
	public new IDLocalCharacterIdleControl idleControl
	{
		get
		{
			return this;
		}
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00028620 File Offset: 0x00026820
	internal bool SetIdle(bool value)
	{
		if (!this._setIdle)
		{
			this._setIdle = true;
		}
		else
		{
			if (this._idle == value)
			{
				return false;
			}
			if (this._changingIdle)
			{
				throw new InvalidOperationException("check callstack. idleControl.set was invoked multiple times. avoid it");
			}
		}
		this._changingIdle = true;
		this._idle = value;
		if (value)
		{
			try
			{
				this.OnIdleEnter();
			}
			finally
			{
				this._changingIdle = false;
			}
		}
		else
		{
			try
			{
				this.OnIdleExit();
			}
			finally
			{
				this._changingIdle = false;
			}
		}
		return true;
	}

	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06000A1D RID: 2589 RVA: 0x000286DC File Offset: 0x000268DC
	public new bool? idle
	{
		get
		{
			if (this._setIdle)
			{
				return new bool?(this._idle);
			}
			return null;
		}
	}

	// Token: 0x06000A1E RID: 2590
	protected abstract void OnIdleEnter();

	// Token: 0x06000A1F RID: 2591
	protected abstract void OnIdleExit();

	// Token: 0x04000688 RID: 1672
	[NonSerialized]
	internal bool _idle = true;

	// Token: 0x04000689 RID: 1673
	[NonSerialized]
	internal bool _setIdle;

	// Token: 0x0400068A RID: 1674
	[NonSerialized]
	internal bool _changingIdle;
}
