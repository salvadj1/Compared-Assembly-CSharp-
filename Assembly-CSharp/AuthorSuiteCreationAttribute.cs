using System;

// Token: 0x0200000A RID: 10
[AttributeUsage(AttributeTargets.Class)]
public sealed class AuthorSuiteCreationAttribute : Attribute
{
	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000014 RID: 20 RVA: 0x00002234 File Offset: 0x00000434
	// (set) Token: 0x06000015 RID: 21 RVA: 0x0000223C File Offset: 0x0000043C
	public string Title { get; set; }

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000016 RID: 22 RVA: 0x00002248 File Offset: 0x00000448
	// (set) Token: 0x06000017 RID: 23 RVA: 0x00002250 File Offset: 0x00000450
	public string Description { get; set; }

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000018 RID: 24 RVA: 0x0000225C File Offset: 0x0000045C
	// (set) Token: 0x06000019 RID: 25 RVA: 0x00002264 File Offset: 0x00000464
	public string Scripter { get; set; }

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600001A RID: 26 RVA: 0x00002270 File Offset: 0x00000470
	// (set) Token: 0x0600001B RID: 27 RVA: 0x00002278 File Offset: 0x00000478
	public Type OutputType { get; set; }

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600001C RID: 28 RVA: 0x00002284 File Offset: 0x00000484
	// (set) Token: 0x0600001D RID: 29 RVA: 0x0000228C File Offset: 0x0000048C
	public bool Ready { get; set; }
}
