using System;

namespace Facepunch.Build
{
	// Token: 0x020000FE RID: 254
	public interface ConnectionHandler : IDisposable
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005E3 RID: 1507
		string address { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005E4 RID: 1508
		int? port { get; }
	}
}
