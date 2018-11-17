using System;

// Token: 0x02000175 RID: 373
public abstract class IDLocalCharacterIdleControl : global::IDLocalCharacter
{
	// Token: 0x17000316 RID: 790
	// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0002C398 File Offset: 0x0002A598
	public new global::IDLocalCharacterIdleControl idleControl
	{
		get
		{
			return this;
		}
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x0002C39C File Offset: 0x0002A59C
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

	// Token: 0x17000317 RID: 791
	// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0002C458 File Offset: 0x0002A658
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

	// Token: 0x06000B44 RID: 2884
	protected abstract void OnIdleEnter();

	// Token: 0x06000B45 RID: 2885
	protected abstract void OnIdleExit();

	// Token: 0x04000797 RID: 1943
	[NonSerialized]
	internal bool _idle = true;

	// Token: 0x04000798 RID: 1944
	[NonSerialized]
	internal bool _setIdle;

	// Token: 0x04000799 RID: 1945
	[NonSerialized]
	internal bool _changingIdle;
}
