using System;

namespace Facepunch.Load
{
	// Token: 0x0200026B RID: 619
	public abstract class Stream : IDisposable
	{
		// Token: 0x060016A4 RID: 5796
		public abstract void Dispose();

		// Token: 0x0200026C RID: 620
		protected static class Property
		{
			// Token: 0x04000B7D RID: 2941
			public const string Path = "filename";

			// Token: 0x04000B7E RID: 2942
			public const string TypeOfAssets = "type";

			// Token: 0x04000B7F RID: 2943
			public const string ContentType = "content";

			// Token: 0x04000B80 RID: 2944
			public const string ByteLength = "size";

			// Token: 0x04000B81 RID: 2945
			public const string RelativeOrAbsoluteURL = "url";
		}
	}
}
