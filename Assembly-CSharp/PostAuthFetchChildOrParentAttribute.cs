using System;

// Token: 0x02000008 RID: 8
[AttributeUsage(AttributeTargets.Field)]
public sealed class PostAuthFetchChildOrParentAttribute : PostAuthAttribute
{
	// Token: 0x0600000D RID: 13 RVA: 0x000021C8 File Offset: 0x000003C8
	private PostAuthFetchChildOrParentAttribute(AuthTarg target, bool includeThisGameObject, string nameMask) : base(target, (!includeThisGameObject) ? (AuthOptions.SearchDown | AuthOptions.SearchUp) : (AuthOptions.SearchDown | AuthOptions.SearchUp | AuthOptions.SearchInclusive), nameMask)
	{
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000021E0 File Offset: 0x000003E0
	public PostAuthFetchChildOrParentAttribute(AuthTarg target, string nameMask) : this(target, false, nameMask)
	{
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000021EC File Offset: 0x000003EC
	public PostAuthFetchChildOrParentAttribute(AuthTarg target, bool includeThisGameObject) : this(target, includeThisGameObject, null)
	{
	}

	// Token: 0x04000014 RID: 20
	private const AuthOptions kFixedOptions = AuthOptions.SearchDown | AuthOptions.SearchUp;
}
