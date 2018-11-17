using System;

// Token: 0x02000009 RID: 9
[AttributeUsage(AttributeTargets.Field)]
public sealed class PostAuthFetchParentOrChildAttribute : PostAuthAttribute
{
	// Token: 0x06000010 RID: 16 RVA: 0x000021F8 File Offset: 0x000003F8
	private PostAuthFetchParentOrChildAttribute(AuthTarg target, bool includeThisGameObject, string nameMask) : base(target, (!includeThisGameObject) ? (AuthOptions.SearchDown | AuthOptions.SearchUp | AuthOptions.SearchReverse) : (AuthOptions.SearchDown | AuthOptions.SearchUp | AuthOptions.SearchInclusive | AuthOptions.SearchReverse), nameMask)
	{
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002214 File Offset: 0x00000414
	public PostAuthFetchParentOrChildAttribute(AuthTarg target, string nameMask) : this(target, false, nameMask)
	{
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002220 File Offset: 0x00000420
	public PostAuthFetchParentOrChildAttribute(AuthTarg target, bool includeThisGameObject) : this(target, includeThisGameObject, null)
	{
	}

	// Token: 0x04000015 RID: 21
	private const AuthOptions kFixedOptions = AuthOptions.SearchDown | AuthOptions.SearchUp | AuthOptions.SearchReverse;
}
