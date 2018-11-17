using System;
using System.Runtime.Serialization;

// Token: 0x02000151 RID: 337
[Serializable]
public class NonPlayerVesselControllableException : global::InstantiateControllableException
{
	// Token: 0x06000985 RID: 2437 RVA: 0x00028790 File Offset: 0x00026990
	public NonPlayerVesselControllableException()
	{
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00028798 File Offset: 0x00026998
	public NonPlayerVesselControllableException(string message) : base(message)
	{
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x000287A4 File Offset: 0x000269A4
	public NonPlayerVesselControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x000287B0 File Offset: 0x000269B0
	protected NonPlayerVesselControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
