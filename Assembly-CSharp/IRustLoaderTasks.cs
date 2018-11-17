using System;
using System.Collections.Generic;
using Facepunch.Load;

// Token: 0x0200002D RID: 45
public interface IRustLoaderTasks
{
	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060001D6 RID: 470
	bool Active { get; }

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060001D7 RID: 471
	IDownloadTask Overall { get; }

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060001D8 RID: 472
	IEnumerable<IDownloadTask> Groups { get; }

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060001D9 RID: 473
	IDownloadTask ActiveGroup { get; }

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060001DA RID: 474
	IEnumerable<IDownloadTask> ActiveJobs { get; }

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060001DB RID: 475
	IEnumerable<IDownloadTask> Jobs { get; }
}
