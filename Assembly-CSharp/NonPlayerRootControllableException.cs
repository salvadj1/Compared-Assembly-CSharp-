using System;
using System.Runtime.Serialization;

// Token: 0x0200014B RID: 331
[Serializable]
public class NonPlayerRootControllableException : global::InstantiateControllableException
{
	// Token: 0x0600096D RID: 2413 RVA: 0x00028688 File Offset: 0x00026888
	public NonPlayerRootControllableException()
	{
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x00028690 File Offset: 0x00026890
	public NonPlayerRootControllableException(string message) : base(message)
	{
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0002869C File Offset: 0x0002689C
	public NonPlayerRootControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x000286A8 File Offset: 0x000268A8
	protected NonPlayerRootControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
