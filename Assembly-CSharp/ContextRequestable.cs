using System;
using UnityEngine;

// Token: 0x02000469 RID: 1129
public static class ContextRequestable
{
	// Token: 0x060028DB RID: 10459 RVA: 0x000A0450 File Offset: 0x0009E650
	public static bool UseableForwardFromContext(IContextRequestable requestable, Controllable controllable, Useable useable)
	{
		MonoBehaviour monoBehaviour = requestable as MonoBehaviour;
		if (!useable)
		{
			useable = monoBehaviour.GetComponent<Useable>();
		}
		Character idMain = controllable.idMain;
		return idMain && useable && useable.EnterFromContext(idMain).Succeeded();
	}

	// Token: 0x060028DC RID: 10460 RVA: 0x000A04A8 File Offset: 0x0009E6A8
	private static bool UseableForwardFromContext(IContextRequestable requestable, Controllable controllable)
	{
		return ContextRequestable.UseableForwardFromContext(requestable, controllable, null);
	}

	// Token: 0x060028DD RID: 10461 RVA: 0x000A04B4 File Offset: 0x0009E6B4
	public static ContextResponse UseableForwardFromContextRespond(IContextRequestable requestable, Controllable controllable, Useable useable)
	{
		return (!ContextRequestable.UseableForwardFromContext(requestable, controllable, useable)) ? ContextResponse.FailBreak : ContextResponse.DoneBreak;
	}

	// Token: 0x060028DE RID: 10462 RVA: 0x000A04CC File Offset: 0x0009E6CC
	public static ContextResponse UseableForwardFromContextRespond(IContextRequestable requestable, Controllable controllable)
	{
		return (!ContextRequestable.UseableForwardFromContext(requestable, controllable, null)) ? ContextResponse.FailBreak : ContextResponse.DoneBreak;
	}

	// Token: 0x040014E8 RID: 5352
	public const ContextExecution AllExecutionFlags = ContextExecution.Quick | ContextExecution.Menu;

	// Token: 0x0200046A RID: 1130
	public static class PointUtil
	{
		// Token: 0x060028DF RID: 10463 RVA: 0x000A04E4 File Offset: 0x0009E6E4
		public static bool SpriteOrOrigin(Component component, out Vector3 worldPoint)
		{
			ContextSprite contextSprite;
			if (ContextSprite.FindSprite(component, out contextSprite))
			{
				worldPoint = contextSprite.transform.position;
				return true;
			}
			worldPoint = component.transform.position;
			return false;
		}
	}
}
