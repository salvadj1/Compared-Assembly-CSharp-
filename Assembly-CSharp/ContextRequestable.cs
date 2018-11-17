using System;
using UnityEngine;

// Token: 0x0200051F RID: 1311
public static class ContextRequestable
{
	// Token: 0x06002C6B RID: 11371 RVA: 0x000A63D0 File Offset: 0x000A45D0
	public static bool UseableForwardFromContext(global::IContextRequestable requestable, global::Controllable controllable, global::Useable useable)
	{
		MonoBehaviour monoBehaviour = requestable as MonoBehaviour;
		if (!useable)
		{
			useable = monoBehaviour.GetComponent<global::Useable>();
		}
		global::Character idMain = controllable.idMain;
		return idMain && useable && useable.EnterFromContext(idMain).Succeeded();
	}

	// Token: 0x06002C6C RID: 11372 RVA: 0x000A6428 File Offset: 0x000A4628
	private static bool UseableForwardFromContext(global::IContextRequestable requestable, global::Controllable controllable)
	{
		return global::ContextRequestable.UseableForwardFromContext(requestable, controllable, null);
	}

	// Token: 0x06002C6D RID: 11373 RVA: 0x000A6434 File Offset: 0x000A4634
	public static global::ContextResponse UseableForwardFromContextRespond(global::IContextRequestable requestable, global::Controllable controllable, global::Useable useable)
	{
		return (!global::ContextRequestable.UseableForwardFromContext(requestable, controllable, useable)) ? global::ContextResponse.FailBreak : global::ContextResponse.DoneBreak;
	}

	// Token: 0x06002C6E RID: 11374 RVA: 0x000A644C File Offset: 0x000A464C
	public static global::ContextResponse UseableForwardFromContextRespond(global::IContextRequestable requestable, global::Controllable controllable)
	{
		return (!global::ContextRequestable.UseableForwardFromContext(requestable, controllable, null)) ? global::ContextResponse.FailBreak : global::ContextResponse.DoneBreak;
	}

	// Token: 0x0400166B RID: 5739
	public const global::ContextExecution AllExecutionFlags = global::ContextExecution.Quick | global::ContextExecution.Menu;

	// Token: 0x02000520 RID: 1312
	public static class PointUtil
	{
		// Token: 0x06002C6F RID: 11375 RVA: 0x000A6464 File Offset: 0x000A4664
		public static bool SpriteOrOrigin(Component component, out Vector3 worldPoint)
		{
			global::ContextSprite contextSprite;
			if (global::ContextSprite.FindSprite(component, out contextSprite))
			{
				worldPoint = contextSprite.transform.position;
				return true;
			}
			worldPoint = component.transform.position;
			return false;
		}
	}
}
