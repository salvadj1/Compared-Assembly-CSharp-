using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;

// Token: 0x0200053A RID: 1338
public class ContextTest : MonoBehaviour, global::IContextRequestable, global::IContextRequestableMenu, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06002CB5 RID: 11445 RVA: 0x000A7880 File Offset: 0x000A5A80
	public global::ContextExecution ContextQuery(global::Controllable controllable, ulong timestamp)
	{
		return global::ContextExecution.Quick | global::ContextExecution.Menu;
	}

	// Token: 0x06002CB6 RID: 11446 RVA: 0x000A7884 File Offset: 0x000A5A84
	public IEnumerable<global::ContextActionPrototype> ContextQueryMenu(global::Controllable controllable, ulong timestamp)
	{
		yield return new global::ContextTest.ContextCallback(0, "Option1", new global::ContextTest.CallbackFunction(this.Option1));
		yield return new global::ContextTest.ContextCallback(1, "Option2", new global::ContextTest.CallbackFunction(this.Option2));
		yield break;
	}

	// Token: 0x06002CB7 RID: 11447 RVA: 0x000A78A8 File Offset: 0x000A5AA8
	public global::ContextResponse ContextRespondMenu(global::Controllable controllable, global::ContextActionPrototype action, ulong timestamp)
	{
		global::ContextTest.ContextCallback contextCallback = (global::ContextTest.ContextCallback)action;
		return contextCallback.func(controllable);
	}

	// Token: 0x06002CB8 RID: 11448 RVA: 0x000A78C8 File Offset: 0x000A5AC8
	private global::ContextResponse Option1(global::Controllable control)
	{
		Debug.Log("Wee option 1");
		return global::ContextResponse.DoneBreak;
	}

	// Token: 0x06002CB9 RID: 11449 RVA: 0x000A78D8 File Offset: 0x000A5AD8
	private global::ContextResponse Option2(global::Controllable control)
	{
		Debug.Log("Wee option 2");
		return global::ContextResponse.DoneBreak;
	}

	// Token: 0x0200053B RID: 1339
	private class ContextCallback : global::ContextActionPrototype
	{
		// Token: 0x06002CBA RID: 11450 RVA: 0x000A78E8 File Offset: 0x000A5AE8
		public ContextCallback(int name, string text, global::ContextTest.CallbackFunction function)
		{
			this.name = name;
			this.text = text;
			this.func = function;
		}

		// Token: 0x040016B7 RID: 5815
		public global::ContextTest.CallbackFunction func;
	}

	// Token: 0x0200053C RID: 1340
	// (Invoke) Token: 0x06002CBC RID: 11452
	private delegate global::ContextResponse CallbackFunction(global::Controllable controllable);
}
