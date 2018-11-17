using System;
using System.Runtime.Serialization;

// Token: 0x0200012A RID: 298
[Serializable]
public class NonControllableException : InstantiateControllableException
{
	// Token: 0x06000883 RID: 2179 RVA: 0x00025498 File Offset: 0x00023698
	public NonControllableException()
	{
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x000254A0 File Offset: 0x000236A0
	public NonControllableException(string message) : base(message)
	{
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x000254AC File Offset: 0x000236AC
	public NonControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x000254B8 File Offset: 0x000236B8
	protected NonControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
