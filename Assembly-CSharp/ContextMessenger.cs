using System;
using System.Collections.Generic;
using Facepunch;

// Token: 0x02000484 RID: 1156
public class ContextMessenger : MonoBehaviour, IContextRequestable, IContextRequestableMenu, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x0600291E RID: 10526 RVA: 0x000A1748 File Offset: 0x0009F948
	public ContextExecution ContextQuery(Controllable controllable, ulong timestamp)
	{
		return (this.messageOptions != null && this.messageOptions.Length != 0) ? ContextExecution.Menu : ContextExecution.NotAvailable;
	}

	// Token: 0x0600291F RID: 10527 RVA: 0x000A176C File Offset: 0x0009F96C
	public IEnumerable<ContextActionPrototype> ContextQueryMenu(Controllable controllable, ulong timestamp)
	{
		int name = 0;
		foreach (string message in this.messageOptions)
		{
			int name2;
			name = (name2 = name) + 1;
			yield return new ContextMessenger.MessageAction(name2, message, message);
		}
		yield break;
	}

	// Token: 0x06002920 RID: 10528 RVA: 0x000A1790 File Offset: 0x0009F990
	public ContextResponse ContextRespondMenu(Controllable controllable, ContextActionPrototype action, ulong timestamp)
	{
		base.SendMessage(((ContextMessenger.MessageAction)action).message, controllable);
		return ContextResponse.DoneBreak;
	}

	// Token: 0x0400152C RID: 5420
	public string[] messageOptions;

	// Token: 0x02000485 RID: 1157
	private class MessageAction : ContextActionPrototype
	{
		// Token: 0x06002921 RID: 10529 RVA: 0x000A17A8 File Offset: 0x0009F9A8
		public MessageAction(int name, string text, string message)
		{
			this.name = name;
			this.text = text;
			this.message = message;
		}

		// Token: 0x0400152D RID: 5421
		public string message;
	}
}
