using System;

// Token: 0x02000006 RID: 6
[AttributeUsage(AttributeTargets.Field)]
public sealed class PostAuthFetchParentAttribute : PostAuthAttribute
{
	// Token: 0x06000007 RID: 7 RVA: 0x00002168 File Offset: 0x00000368
	private PostAuthFetchParentAttribute(AuthTarg target, bool includeThisGameObject, string nameMask) : base(target, (!includeThisGameObject) ? AuthOptions.SearchUp : (AuthOptions.SearchUp | AuthOptions.SearchInclusive), nameMask)
	{
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002180 File Offset: 0x00000380
	public PostAuthFetchParentAttribute(AuthTarg target, string nameMask) : this(target, false, nameMask)
	{
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000218C File Offset: 0x0000038C
	public PostAuthFetchParentAttribute(AuthTarg target, bool includeThisGameObject) : this(target, includeThisGameObject, null)
	{
	}

	// Token: 0x04000012 RID: 18
	private const AuthOptions kFixedOptions = AuthOptions.SearchUp;
}
