using System;
using System.Runtime.Serialization;

// Token: 0x0200012C RID: 300
[Serializable]
public class NonAIVesselControllableException : InstantiateControllableException
{
	// Token: 0x0600088B RID: 2187 RVA: 0x000254F0 File Offset: 0x000236F0
	public NonAIVesselControllableException()
	{
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x000254F8 File Offset: 0x000236F8
	public NonAIVesselControllableException(string message) : base(message)
	{
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x00025504 File Offset: 0x00023704
	public NonAIVesselControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00025510 File Offset: 0x00023710
	protected NonAIVesselControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
