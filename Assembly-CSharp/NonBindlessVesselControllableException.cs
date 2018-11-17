using System;
using System.Runtime.Serialization;

// Token: 0x0200014D RID: 333
[Serializable]
public class NonBindlessVesselControllableException : global::InstantiateControllableException
{
	// Token: 0x06000975 RID: 2421 RVA: 0x000286E0 File Offset: 0x000268E0
	public NonBindlessVesselControllableException()
	{
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x000286E8 File Offset: 0x000268E8
	public NonBindlessVesselControllableException(string message) : base(message)
	{
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x000286F4 File Offset: 0x000268F4
	public NonBindlessVesselControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00028700 File Offset: 0x00026900
	protected NonBindlessVesselControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
