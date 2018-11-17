using System;
using System.Runtime.Serialization;

// Token: 0x02000127 RID: 295
[Serializable]
public class NonPlayerRootControllableException : InstantiateControllableException
{
	// Token: 0x06000877 RID: 2167 RVA: 0x00025414 File Offset: 0x00023614
	public NonPlayerRootControllableException()
	{
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0002541C File Offset: 0x0002361C
	public NonPlayerRootControllableException(string message) : base(message)
	{
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x00025428 File Offset: 0x00023628
	public NonPlayerRootControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x00025434 File Offset: 0x00023634
	protected NonPlayerRootControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
