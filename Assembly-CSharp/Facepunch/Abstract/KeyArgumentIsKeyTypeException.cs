using System;
using System.Runtime.Serialization;

namespace Facepunch.Abstract
{
	// Token: 0x020001D7 RID: 471
	[Serializable]
	internal class KeyArgumentIsKeyTypeException : ArgumentOutOfRangeException
	{
		// Token: 0x06000D0B RID: 3339 RVA: 0x000331C4 File Offset: 0x000313C4
		public KeyArgumentIsKeyTypeException()
		{
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000331CC File Offset: 0x000313CC
		public KeyArgumentIsKeyTypeException(string parameterName) : base(parameterName)
		{
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000331D8 File Offset: 0x000313D8
		public KeyArgumentIsKeyTypeException(string parameterName, string message) : base(parameterName, message)
		{
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000331E4 File Offset: 0x000313E4
		public KeyArgumentIsKeyTypeException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000331F0 File Offset: 0x000313F0
		protected KeyArgumentIsKeyTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
