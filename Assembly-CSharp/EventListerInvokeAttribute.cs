using System;

// Token: 0x020001B1 RID: 433
[AttributeUsage(AttributeTargets.Delegate)]
public class EventListerInvokeAttribute : Attribute
{
	// Token: 0x06000C4F RID: 3151 RVA: 0x00030DD8 File Offset: 0x0002EFD8
	public EventListerInvokeAttribute(Type invokeClass, string invokeMember, Type invokeCall)
	{
		this.InvokeClass = invokeClass;
		this.InvokeMember = invokeMember;
		this.InvokeCall = invokeCall;
	}

	// Token: 0x04000778 RID: 1912
	internal readonly Type InvokeClass;

	// Token: 0x04000779 RID: 1913
	internal readonly string InvokeMember;

	// Token: 0x0400077A RID: 1914
	internal readonly Type InvokeCall;
}
