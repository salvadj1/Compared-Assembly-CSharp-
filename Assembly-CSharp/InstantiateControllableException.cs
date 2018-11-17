using System;
using System.Runtime.Serialization;

// Token: 0x0200014A RID: 330
[Serializable]
public abstract class InstantiateControllableException : ArgumentException
{
	// Token: 0x06000969 RID: 2409 RVA: 0x0002865C File Offset: 0x0002685C
	public InstantiateControllableException()
	{
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00028664 File Offset: 0x00026864
	public InstantiateControllableException(string message) : base(message)
	{
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x00028670 File Offset: 0x00026870
	public InstantiateControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0002867C File Offset: 0x0002687C
	protected InstantiateControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
