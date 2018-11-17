using System;

// Token: 0x020001DF RID: 479
[AttributeUsage(AttributeTargets.Delegate)]
public class EventListerInvokeAttribute : Attribute
{
	// Token: 0x06000D87 RID: 3463 RVA: 0x00034CC4 File Offset: 0x00032EC4
	public EventListerInvokeAttribute(Type invokeClass, string invokeMember, Type invokeCall)
	{
		this.InvokeClass = invokeClass;
		this.InvokeMember = invokeMember;
		this.InvokeCall = invokeCall;
	}

	// Token: 0x0400088C RID: 2188
	internal readonly Type InvokeClass;

	// Token: 0x0400088D RID: 2189
	internal readonly string InvokeMember;

	// Token: 0x0400088E RID: 2190
	internal readonly Type InvokeCall;
}
