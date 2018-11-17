using System;
using System.Runtime.Serialization;

// Token: 0x02000129 RID: 297
[Serializable]
public class NonBindlessVesselControllableException : InstantiateControllableException
{
	// Token: 0x0600087F RID: 2175 RVA: 0x0002546C File Offset: 0x0002366C
	public NonBindlessVesselControllableException()
	{
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x00025474 File Offset: 0x00023674
	public NonBindlessVesselControllableException(string message) : base(message)
	{
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00025480 File Offset: 0x00023680
	public NonBindlessVesselControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0002548C File Offset: 0x0002368C
	protected NonBindlessVesselControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
