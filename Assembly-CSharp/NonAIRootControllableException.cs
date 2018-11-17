using System;
using System.Runtime.Serialization;

// Token: 0x0200014C RID: 332
[Serializable]
public class NonAIRootControllableException : global::InstantiateControllableException
{
	// Token: 0x06000971 RID: 2417 RVA: 0x000286B4 File Offset: 0x000268B4
	public NonAIRootControllableException()
	{
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x000286BC File Offset: 0x000268BC
	public NonAIRootControllableException(string message) : base(message)
	{
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x000286C8 File Offset: 0x000268C8
	public NonAIRootControllableException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x000286D4 File Offset: 0x000268D4
	protected NonAIRootControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
