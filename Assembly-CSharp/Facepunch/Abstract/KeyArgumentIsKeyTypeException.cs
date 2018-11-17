using System;
using System.Runtime.Serialization;

namespace Facepunch.Abstract
{
	// Token: 0x02000208 RID: 520
	[Serializable]
	internal class KeyArgumentIsKeyTypeException : ArgumentOutOfRangeException
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x0003724C File Offset: 0x0003544C
		public KeyArgumentIsKeyTypeException()
		{
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00037254 File Offset: 0x00035454
		public KeyArgumentIsKeyTypeException(string parameterName) : base(parameterName)
		{
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00037260 File Offset: 0x00035460
		public KeyArgumentIsKeyTypeException(string parameterName, string message) : base(parameterName, message)
		{
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003726C File Offset: 0x0003546C
		public KeyArgumentIsKeyTypeException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00037278 File Offset: 0x00035478
		protected KeyArgumentIsKeyTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
