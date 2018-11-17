using System;
using System.Runtime.Serialization;

// Token: 0x0200014E RID: 334
[Serializable]
public class NonControllableException : global::InstantiateControllableException
{
	// Token: 0x06000979 RID: 2425 RVA: 0x0002870C File Offset: 0x0002690C
	public NonControllableException()
	{
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00028714 File Offset: 0x00026914
	public NonControllableException(string message) : base(message)
	{
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00028720 File Offset: 0x00026920
	public NonControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0002872C File Offset: 0x0002692C
	protected NonControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
