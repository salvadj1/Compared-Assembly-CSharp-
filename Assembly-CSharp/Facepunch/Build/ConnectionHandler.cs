using System;

namespace Facepunch.Build
{
	// Token: 0x020000EA RID: 234
	public interface ConnectionHandler : IDisposable
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000565 RID: 1381
		string address { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000566 RID: 1382
		int? port { get; }
	}
}
