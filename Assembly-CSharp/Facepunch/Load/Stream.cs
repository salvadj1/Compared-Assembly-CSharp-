using System;

namespace Facepunch.Load
{
	// Token: 0x0200029F RID: 671
	public abstract class Stream : IDisposable
	{
		// Token: 0x060017FE RID: 6142
		public abstract void Dispose();

		// Token: 0x020002A0 RID: 672
		protected static class Property
		{
			// Token: 0x04000CA3 RID: 3235
			public const string Path = "filename";

			// Token: 0x04000CA4 RID: 3236
			public const string TypeOfAssets = "type";

			// Token: 0x04000CA5 RID: 3237
			public const string ContentType = "content";

			// Token: 0x04000CA6 RID: 3238
			public const string ByteLength = "size";

			// Token: 0x04000CA7 RID: 3239
			public const string RelativeOrAbsoluteURL = "url";
		}
	}
}
