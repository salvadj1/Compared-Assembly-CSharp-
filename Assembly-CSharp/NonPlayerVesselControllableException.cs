using System;
using System.Runtime.Serialization;

// Token: 0x0200012D RID: 301
[Serializable]
public class NonPlayerVesselControllableException : InstantiateControllableException
{
	// Token: 0x0600088F RID: 2191 RVA: 0x0002551C File Offset: 0x0002371C
	public NonPlayerVesselControllableException()
	{
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00025524 File Offset: 0x00023724
	public NonPlayerVesselControllableException(string message) : base(message)
	{
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00025530 File Offset: 0x00023730
	public NonPlayerVesselControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0002553C File Offset: 0x0002373C
	protected NonPlayerVesselControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
