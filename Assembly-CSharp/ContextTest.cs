using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;

// Token: 0x02000482 RID: 1154
public class ContextTest : MonoBehaviour, IContextRequestable, IContextRequestableMenu, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x06002917 RID: 10519 RVA: 0x000A16B8 File Offset: 0x0009F8B8
	public ContextExecution ContextQuery(Controllable controllable, ulong timestamp)
	{
		return ContextExecution.Quick | ContextExecution.Menu;
	}

	// Token: 0x06002918 RID: 10520 RVA: 0x000A16BC File Offset: 0x0009F8BC
	public IEnumerable<ContextActionPrototype> ContextQueryMenu(Controllable controllable, ulong timestamp)
	{
		yield return new ContextTest.ContextCallback(0, "Option1", new ContextTest.CallbackFunction(this.Option1));
		yield return new ContextTest.ContextCallback(1, "Option2", new ContextTest.CallbackFunction(this.Option2));
		yield break;
	}

	// Token: 0x06002919 RID: 10521 RVA: 0x000A16E0 File Offset: 0x0009F8E0
	public ContextResponse ContextRespondMenu(Controllable controllable, ContextActionPrototype action, ulong timestamp)
	{
		ContextTest.ContextCallback contextCallback = (ContextTest.ContextCallback)action;
		return contextCallback.func(controllable);
	}

	// Token: 0x0600291A RID: 10522 RVA: 0x000A1700 File Offset: 0x0009F900
	private ContextResponse Option1(Controllable control)
	{
		Debug.Log("Wee option 1");
		return ContextResponse.DoneBreak;
	}

	// Token: 0x0600291B RID: 10523 RVA: 0x000A1710 File Offset: 0x0009F910
	private ContextResponse Option2(Controllable control)
	{
		Debug.Log("Wee option 2");
		return ContextResponse.DoneBreak;
	}

	// Token: 0x02000483 RID: 1155
	private class ContextCallback : ContextActionPrototype
	{
		// Token: 0x0600291C RID: 10524 RVA: 0x000A1720 File Offset: 0x0009F920
		public ContextCallback(int name, string text, ContextTest.CallbackFunction function)
		{
			this.name = name;
			this.text = text;
			this.func = function;
		}

		// Token: 0x0400152B RID: 5419
		public ContextTest.CallbackFunction func;
	}

	// Token: 0x020008DA RID: 2266
	// (Invoke) Token: 0x06004D40 RID: 19776
	private delegate ContextResponse CallbackFunction(Controllable controllable);
}
