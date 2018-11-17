using System;
using System.Collections.Generic;
using Facepunch;

// Token: 0x0200053E RID: 1342
public class ContextMessenger : MonoBehaviour, global::IContextRequestable, global::IContextRequestableMenu, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06002CC8 RID: 11464 RVA: 0x000A7A14 File Offset: 0x000A5C14
	public global::ContextExecution ContextQuery(global::Controllable controllable, ulong timestamp)
	{
		return (this.messageOptions != null && this.messageOptions.Length != 0) ? global::ContextExecution.Menu : global::ContextExecution.NotAvailable;
	}

	// Token: 0x06002CC9 RID: 11465 RVA: 0x000A7A38 File Offset: 0x000A5C38
	public IEnumerable<global::ContextActionPrototype> ContextQueryMenu(global::Controllable controllable, ulong timestamp)
	{
		int name = 0;
		foreach (string message in this.messageOptions)
		{
			int name2;
			name = (name2 = name) + 1;
			yield return new global::ContextMessenger.MessageAction(name2, message, message);
		}
		yield break;
	}

	// Token: 0x06002CCA RID: 11466 RVA: 0x000A7A5C File Offset: 0x000A5C5C
	public global::ContextResponse ContextRespondMenu(global::Controllable controllable, global::ContextActionPrototype action, ulong timestamp)
	{
		base.SendMessage(((global::ContextMessenger.MessageAction)action).message, controllable);
		return global::ContextResponse.DoneBreak;
	}

	// Token: 0x040016BB RID: 5819
	public string[] messageOptions;

	// Token: 0x0200053F RID: 1343
	private class MessageAction : global::ContextActionPrototype
	{
		// Token: 0x06002CCB RID: 11467 RVA: 0x000A7A74 File Offset: 0x000A5C74
		public MessageAction(int name, string text, string message)
		{
			this.name = name;
			this.text = text;
			this.message = message;
		}

		// Token: 0x040016BC RID: 5820
		public string message;
	}
}
