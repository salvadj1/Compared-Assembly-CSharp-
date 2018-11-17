using System;

// Token: 0x02000007 RID: 7
[AttributeUsage(AttributeTargets.Field)]
public sealed class PostAuthFetchChildAttribute : PostAuthAttribute
{
	// Token: 0x0600000A RID: 10 RVA: 0x00002198 File Offset: 0x00000398
	private PostAuthFetchChildAttribute(AuthTarg target, bool includeThisGameObject, string nameMask) : base(target, (!includeThisGameObject) ? AuthOptions.SearchDown : (AuthOptions.SearchDown | AuthOptions.SearchInclusive), nameMask)
	{
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000021B0 File Offset: 0x000003B0
	public PostAuthFetchChildAttribute(AuthTarg target, string nameMask) : this(target, false, nameMask)
	{
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021BC File Offset: 0x000003BC
	public PostAuthFetchChildAttribute(AuthTarg target, bool includeThisGameObject) : this(target, includeThisGameObject, null)
	{
	}

	// Token: 0x04000013 RID: 19
	private const AuthOptions kFixedOptions = AuthOptions.SearchDown;
}
