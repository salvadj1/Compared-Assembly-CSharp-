using System;
using UnityEngine;

// Token: 0x0200029C RID: 668
public abstract class CCTotem<TTotemObject, CCTotemScript> : CCTotem<TTotemObject> where TTotemObject : CCTotem.TotemicObject<CCTotemScript, TTotemObject>, new() where CCTotemScript : CCTotem<TTotemObject, CCTotemScript>, new()
{
	// Token: 0x060017D3 RID: 6099 RVA: 0x0005CEA8 File Offset: 0x0005B0A8
	internal CCTotem()
	{
	}

	// Token: 0x060017D4 RID: 6100 RVA: 0x0005CEB0 File Offset: 0x0005B0B0
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

	// Token: 0x060017D5 RID: 6101 RVA: 0x0005CF4C File Offset: 0x0005B14C
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

	// Token: 0x060017D6 RID: 6102 RVA: 0x0005CFE8 File Offset: 0x0005B1E8
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

	// Token: 0x04000CAD RID: 3245
	[NonSerialized]
	private bool destroyed;
}
