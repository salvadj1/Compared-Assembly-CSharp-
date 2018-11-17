using System;
using Facepunch;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x0200009B RID: 155
public sealed class ContextProbe : IDLocalCharacterAddon
{
	// Token: 0x0600033C RID: 828 RVA: 0x0001018C File Offset: 0x0000E38C
	public ContextProbe() : this(IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | IDLocalCharacterAddon.AddonFlags.FireOnWillRemoveAddon)
	{
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00010198 File Offset: 0x0000E398
	private ContextProbe(IDLocalCharacterAddon.AddonFlags addonFlags) : base(addonFlags)
	{
	}

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x0600033E RID: 830 RVA: 0x000101A4 File Offset: 0x0000E3A4
	public bool isHighlighting
	{
		get
		{
			return this.hasHighlight;
		}
	}

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x0600033F RID: 831 RVA: 0x000101AC File Offset: 0x0000E3AC
	public static bool aProbeIsHighlighting
	{
		get
		{
			return ContextProbe.singleton && ContextProbe.singleton.hasHighlight;
		}
	}

	// Token: 0x06000340 RID: 832 RVA: 0x000101CC File Offset: 0x0000E3CC
	protected override void OnAddonAwake()
	{
		ContextProbe.singleton = this;
		CharacterContextProbeTrait trait = base.GetTrait<CharacterContextProbeTrait>();
		this.raycastLength = trait.rayLength;
	}

	// Token: 0x06000341 RID: 833 RVA: 0x000101F4 File Offset: 0x0000E3F4
	protected override void OnWillRemoveAddon()
	{
		if (ContextProbe.singleton == this)
		{
			ContextProbe.singleton = null;
		}
		this.hasHighlight = false;
		this.lastUseHighlight = null;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x00010228 File Offset: 0x0000E428
	private void Update()
	{
		if (base.dead)
		{
			return;
		}
		bool press;
		if (LockCursorManager.IsLocked())
		{
			press = Context.ButtonDown;
			bool buttonUp = Context.ButtonUp;
		}
		else
		{
			press = false;
			bool flag = Context.WorkingInMenu && Context.ButtonUp;
		}
		this.hasHighlight = this.ClientCheckUse(base.eyesRay, press);
		if (Context.ButtonUp)
		{
			Context.EndQuery();
		}
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00010294 File Offset: 0x0000E494
	private bool ClientCheckUse(Ray ray, bool press)
	{
		RaycastHit raycastHit;
		MonoBehaviour monoBehaviour;
		if (Physics.Raycast(ray, ref raycastHit, this.raycastLength, -201523205))
		{
			Transform transform = raycastHit.transform;
			Transform parent = transform.parent;
			NetEntityID netEntityID;
			MonoBehaviour component;
			NetEntityID.Kind kind;
			while ((int)(kind = NetEntityID.Of(transform, out netEntityID, out component)) == 0 && parent)
			{
				transform = parent;
				parent = transform.parent;
			}
			Contextual contextual;
			if ((int)kind == 0)
			{
				monoBehaviour = null;
			}
			else if (Contextual.ContextOf(component, out contextual))
			{
				monoBehaviour = contextual.implementor;
				if (press)
				{
					Context.BeginQuery(contextual);
				}
			}
			else
			{
				monoBehaviour = null;
			}
		}
		else
		{
			monoBehaviour = null;
		}
		if (monoBehaviour != this.lastUseHighlight)
		{
			this.lastUseHighlight = monoBehaviour;
			if (monoBehaviour)
			{
				IContextRequestableText contextRequestableText = monoBehaviour as IContextRequestableText;
				if (contextRequestableText != null)
				{
					RPOS.UseHoverTextSet(base.controllable, contextRequestableText);
				}
				else
				{
					RPOS.UseHoverTextSet(monoBehaviour.name);
				}
			}
			else
			{
				RPOS.UseHoverTextClear();
			}
		}
		return monoBehaviour;
	}

	// Token: 0x040002B7 RID: 695
	private const IDLocalCharacterAddon.AddonFlags ContextProbeAddonFlags = IDLocalCharacterAddon.AddonFlags.FireOnAddonAwake | IDLocalCharacterAddon.AddonFlags.FireOnWillRemoveAddon;

	// Token: 0x040002B8 RID: 696
	[NonSerialized]
	private float raycastLength;

	// Token: 0x040002B9 RID: 697
	[NonSerialized]
	private MonoBehaviour lastUseHighlight;

	// Token: 0x040002BA RID: 698
	[NonSerialized]
	private bool hasHighlight;

	// Token: 0x040002BB RID: 699
	private static ContextProbe singleton;
}
