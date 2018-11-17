using System;
using System.Runtime.Serialization;

// Token: 0x02000128 RID: 296
[Serializable]
public class NonAIRootControllableException : InstantiateControllableException
{
	// Token: 0x0600087B RID: 2171 RVA: 0x00025440 File Offset: 0x00023640
	public NonAIRootControllableException()
	{
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00025448 File Offset: 0x00023648
	public NonAIRootControllableException(string message) : base(message)
	{
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x00025454 File Offset: 0x00023654
	public NonAIRootControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x00025460 File Offset: 0x00023660
	protected NonAIRootControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
