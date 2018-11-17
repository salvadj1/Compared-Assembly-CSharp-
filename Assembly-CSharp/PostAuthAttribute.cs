using System;

// Token: 0x02000004 RID: 4
public abstract class PostAuthAttribute : Attribute
{
	// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
	internal PostAuthAttribute(AuthTarg target, AuthOptions options, string nameMask)
	{
		this._target = target;
		if (!string.IsNullOrEmpty(nameMask))
		{
			this._options = (options | (AuthOptions)4);
			this._nameMask = nameMask;
		}
		else
		{
			this._options = options;
			this._nameMask = string.Empty;
		}
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000002 RID: 2 RVA: 0x00002138 File Offset: 0x00000338
	public AuthTarg target
	{
		get
		{
			return this._target;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000003 RID: 3 RVA: 0x00002140 File Offset: 0x00000340
	public AuthOptions options
	{
		get
		{
			return this._options;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000004 RID: 4 RVA: 0x00002148 File Offset: 0x00000348
	public string nameMask
	{
		get
		{
			return this._nameMask;
		}
	}

	// Token: 0x04000009 RID: 9
	public const AuthOptions kOption_None = (AuthOptions)0;

	// Token: 0x0400000A RID: 10
	public const AuthOptions kOption_Down = AuthOptions.SearchDown;

	// Token: 0x0400000B RID: 11
	public const AuthOptions kOption_Up = AuthOptions.SearchUp;

	// Token: 0x0400000C RID: 12
	public const AuthOptions kOption_NameMask = (AuthOptions)4;

	// Token: 0x0400000D RID: 13
	public const AuthOptions kOption_Include = AuthOptions.SearchInclusive;

	// Token: 0x0400000E RID: 14
	public const AuthOptions kOption_Reverse = AuthOptions.SearchReverse;

	// Token: 0x0400000F RID: 15
	private readonly AuthOptions _options;

	// Token: 0x04000010 RID: 16
	private readonly AuthTarg _target;

	// Token: 0x04000011 RID: 17
	private readonly string _nameMask;
}
