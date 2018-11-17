using System;
using System.Runtime.Serialization;

// Token: 0x02000125 RID: 293
[Serializable]
public class ControllableCallstackException : InvalidOperationException
{
	// Token: 0x0600086F RID: 2159 RVA: 0x000253BC File Offset: 0x000235BC
	public ControllableCallstackException()
	{
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x000253C4 File Offset: 0x000235C4
	public ControllableCallstackException(string message) : base(message)
	{
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x000253D0 File Offset: 0x000235D0
	public ControllableCallstackException(string message, Exception inner) : base(message, inner)
	{
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x000253DC File Offset: 0x000235DC
	protected ControllableCallstackException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}
}
