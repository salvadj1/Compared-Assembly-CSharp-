using System;
using System.Runtime.Serialization;

// Token: 0x0200012B RID: 299
[Serializable]
public class NonVesselControllableException : InstantiateControllableException
{
	// Token: 0x06000887 RID: 2183 RVA: 0x000254C4 File Offset: 0x000236C4
	public NonVesselControllableException()
	{
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x000254CC File Offset: 0x000236CC
	public NonVesselControllableException(string message) : base(message)
	{
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x000254D8 File Offset: 0x000236D8
	public NonVesselControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x000254E4 File Offset: 0x000236E4
	protected NonVesselControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
