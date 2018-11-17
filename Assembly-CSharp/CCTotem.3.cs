using System;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public abstract class CCTotem<TTotemObject, CCTotemScript> : global::CCTotem<TTotemObject> where TTotemObject : global::CCTotem.TotemicObject<CCTotemScript, TTotemObject>, new() where CCTotemScript : global::CCTotem<TTotemObject, CCTotemScript>, new()
{
	// Token: 0x06001963 RID: 6499 RVA: 0x0006181C File Offset: 0x0005FA1C
	internal CCTotem()
	{
	}

	// Token: 0x06001964 RID: 6500 RVA: 0x00061824 File Offset: 0x0005FA24
	internal void AssignTotemicObject(TTotemObject totemObject)
	{
		if (!object.ReferenceEquals(this.totemicObject, null))
		{
			if (object.ReferenceEquals(this.totemicObject, totemObject))
			{
				return;
			}
			this.ClearTotemicObject();
		}
		this.totemicObject = totemObject;
		if (!object.ReferenceEquals(this.totemicObject, null))
		{
			if (this.destroyed)
			{
				this.totemicObject = (TTotemObject)((object)null);
				throw new InvalidOperationException("Cannot assign non-null script during destruction");
			}
			this.totemicObject.AssignedToScript((CCTotemScript)((object)this));
		}
	}

	// Token: 0x06001965 RID: 6501 RVA: 0x000618C0 File Offset: 0x0005FAC0
	protected void ClearTotemicObject()
	{
		TTotemObject totemicObject = this.totemicObject;
		try
		{
			this.totemicObject.OnScriptDestroy((CCTotemScript)((object)this));
		}
		catch (Exception ex)
		{
			Debug.LogException(ex, this);
		}
		finally
		{
			if (object.ReferenceEquals(totemicObject, this.totemicObject))
			{
				this.totemicObject = (TTotemObject)((object)null);
			}
		}
	}

	// Token: 0x06001966 RID: 6502 RVA: 0x0006195C File Offset: 0x0005FB5C
	protected void OnDestroy()
	{
		if (!this.destroyed)
		{
			this.destroyed = true;
			if (!object.ReferenceEquals(this.totemicObject, null))
			{
				this.ClearTotemicObject();
			}
		}
	}

	// Token: 0x04000DE8 RID: 3560
	[NonSerialized]
	private bool destroyed;
}
