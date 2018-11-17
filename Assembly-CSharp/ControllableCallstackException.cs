using System;
using System.Runtime.Serialization;

// Token: 0x02000149 RID: 329
[Serializable]
public class ControllableCallstackException : InvalidOperationException
{
	// Token: 0x06000965 RID: 2405 RVA: 0x00028630 File Offset: 0x00026830
	public ControllableCallstackException()
	{
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x00028638 File Offset: 0x00026838
	public ControllableCallstackException(string message) : base(message)
	{
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00028644 File Offset: 0x00026844
	public ControllableCallstackException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00028650 File Offset: 0x00026850
	protected ControllableCallstackException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
