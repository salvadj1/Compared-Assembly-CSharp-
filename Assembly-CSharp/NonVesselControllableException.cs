using System;
using System.Runtime.Serialization;

// Token: 0x0200014F RID: 335
[Serializable]
public class NonVesselControllableException : global::InstantiateControllableException
{
	// Token: 0x0600097D RID: 2429 RVA: 0x00028738 File Offset: 0x00026938
	public NonVesselControllableException()
	{
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x00028740 File Offset: 0x00026940
	public NonVesselControllableException(string message) : base(message)
	{
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0002874C File Offset: 0x0002694C
	public NonVesselControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00028758 File Offset: 0x00026958
	protected NonVesselControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
