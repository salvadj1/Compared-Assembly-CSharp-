using System;
using System.Runtime.Serialization;

// Token: 0x02000150 RID: 336
[Serializable]
public class NonAIVesselControllableException : global::InstantiateControllableException
{
	// Token: 0x06000981 RID: 2433 RVA: 0x00028764 File Offset: 0x00026964
	public NonAIVesselControllableException()
	{
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0002876C File Offset: 0x0002696C
	public NonAIVesselControllableException(string message) : base(message)
	{
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x00028778 File Offset: 0x00026978
	public NonAIVesselControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x00028784 File Offset: 0x00026984
	protected NonAIVesselControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
