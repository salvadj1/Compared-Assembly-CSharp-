using System;

// Token: 0x0200000A RID: 10
[AttributeUsage(AttributeTargets.Class)]
public sealed class AuthorSuiteCreationAttribute : Attribute
{
	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000014 RID: 20 RVA: 0x00002198 File Offset: 0x00000398
	// (set) Token: 0x06000015 RID: 21 RVA: 0x000021A0 File Offset: 0x000003A0
	public string Title { get; set; }

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000016 RID: 22 RVA: 0x000021AC File Offset: 0x000003AC
	// (set) Token: 0x06000017 RID: 23 RVA: 0x000021B4 File Offset: 0x000003B4
	public string Description { get; set; }

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000018 RID: 24 RVA: 0x000021C0 File Offset: 0x000003C0
	// (set) Token: 0x06000019 RID: 25 RVA: 0x000021C8 File Offset: 0x000003C8
	public string Scripter { get; set; }

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600001A RID: 26 RVA: 0x000021D4 File Offset: 0x000003D4
	// (set) Token: 0x0600001B RID: 27 RVA: 0x000021DC File Offset: 0x000003DC
	public Type OutputType { get; set; }

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600001C RID: 28 RVA: 0x000021E8 File Offset: 0x000003E8
	// (set) Token: 0x0600001D RID: 29 RVA: 0x000021F0 File Offset: 0x000003F0
	public bool Ready { get; set; }
}
