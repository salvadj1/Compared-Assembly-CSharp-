using System;

// Token: 0x02000005 RID: 5
[AttributeUsage(AttributeTargets.Field)]
public sealed class PostAuthFetchAttribute : PostAuthAttribute
{
	// Token: 0x06000005 RID: 5 RVA: 0x00002150 File Offset: 0x00000350
	public PostAuthFetchAttribute(AuthTarg target, string nameMask) : base(target, (AuthOptions)0, nameMask)
	{
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000215C File Offset: 0x0000035C
	public PostAuthFetchAttribute(AuthTarg target) : this(target, null)
	{
	}
}
