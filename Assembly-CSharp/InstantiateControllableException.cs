using System;
using System.Runtime.Serialization;

// Token: 0x02000126 RID: 294
[Serializable]
public abstract class InstantiateControllableException : ArgumentException
{
	// Token: 0x06000873 RID: 2163 RVA: 0x000253E8 File Offset: 0x000235E8
	public InstantiateControllableException()
	{
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x000253F0 File Offset: 0x000235F0
	public InstantiateControllableException(string message) : base(message)
	{
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x000253FC File Offset: 0x000235FC
	public InstantiateControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x00025408 File Offset: 0x00023608
	protected InstantiateControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
