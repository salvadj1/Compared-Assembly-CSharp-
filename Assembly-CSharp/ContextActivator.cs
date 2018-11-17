using System;
using Facepunch;
using UnityEngine;

// Token: 0x02000517 RID: 1303
public sealed class ContextActivator : MonoBehaviour, global::IContextRequestable, global::IContextRequestableQuick, global::IContextRequestableStatus, global::IContextRequestableText, global::IContextRequestablePointText, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06002C64 RID: 11364 RVA: 0x000A6084 File Offset: 0x000A4284
	bool global::IContextRequestablePointText.ContextTextPoint(out Vector3 worldPoint)
	{
		if (this.useTextPoint)
		{
			if (!this.useSpriteTextPoint || !global::ContextRequestable.PointUtil.SpriteOrOrigin(this, out worldPoint))
			{
				if (this.isSwitch)
				{
					global::ActivationToggleState toggleState = this.toggleState;
					if (toggleState != global::ActivationToggleState.On)
					{
						if (toggleState != global::ActivationToggleState.Off)
						{
							worldPoint = this.defaultTextPoint;
						}
						else
						{
							worldPoint = this.offTextPoint;
						}
					}
					else
					{
						worldPoint = this.onTextPoint;
					}
				}
				else
				{
					worldPoint = this.defaultTextPoint;
				}
				worldPoint = base.transform.TransformPoint(worldPoint);
			}
			return true;
		}
		worldPoint = default(Vector3);
		return false;
	}

	// Token: 0x06002C65 RID: 11365 RVA: 0x000A6148 File Offset: 0x000A4348
	string global::IContextRequestableText.ContextText(global::Controllable localControllable)
	{
		if (this.isSwitch)
		{
			global::ActivationToggleState toggleState = this.toggleState;
			if (toggleState == global::ActivationToggleState.On)
			{
				return this.onText;
			}
			if (toggleState == global::ActivationToggleState.Off)
			{
				return this.offText;
			}
		}
		return this.defaultText;
	}

	// Token: 0x06002C66 RID: 11366 RVA: 0x000A6190 File Offset: 0x000A4390
	global::ContextStatusFlags global::IContextRequestableStatus.ContextStatusPoll()
	{
		global::ContextActivator.SpriteQuickMode spriteQuickMode;
		if (this.isSwitch)
		{
			global::ActivationToggleState toggleState = this.toggleState;
			if (toggleState != global::ActivationToggleState.On)
			{
				if (toggleState != global::ActivationToggleState.Off)
				{
					spriteQuickMode = this.defaultSprite;
				}
				else
				{
					spriteQuickMode = this.offSprite;
				}
			}
			else
			{
				spriteQuickMode = this.onSprite;
			}
		}
		else
		{
			spriteQuickMode = this.defaultSprite;
		}
		switch (spriteQuickMode)
		{
		case global::ContextActivator.SpriteQuickMode.Faded:
			return global::ContextStatusFlags.SpriteFlag0;
		case global::ContextActivator.SpriteQuickMode.AlwaysVisible:
			return global::ContextStatusFlags.SpriteFlag0 | global::ContextStatusFlags.SpriteFlag1;
		case global::ContextActivator.SpriteQuickMode.NeverVisisble:
			return global::ContextStatusFlags.SpriteFlag1;
		default:
			return (global::ContextStatusFlags)0;
		}
	}

	// Token: 0x170009B2 RID: 2482
	// (get) Token: 0x06002C67 RID: 11367 RVA: 0x000A6220 File Offset: 0x000A4420
	private global::ActivationToggleState toggleState
	{
		get
		{
			if (!this.mainAction)
			{
				return global::ActivationToggleState.Unspecified;
			}
			return this.mainAction.toggleState;
		}
	}

	// Token: 0x06002C68 RID: 11368 RVA: 0x000A6240 File Offset: 0x000A4440
	private global::ActivationResult ApplyActivatable(global::Activatable activatable, global::Character instigator, ulong timestamp, bool extra)
	{
		global::ActivationResult result;
		if (activatable)
		{
			global::ContextActivator.ActivationMode activationMode = this.activationMode;
			if (activationMode != global::ContextActivator.ActivationMode.TurnOn)
			{
				if (activationMode != global::ContextActivator.ActivationMode.TurnOff)
				{
					result = activatable.Activate(instigator, timestamp);
				}
				else
				{
					result = activatable.Activate(false, instigator, timestamp);
				}
			}
			else
			{
				result = activatable.Activate(true, instigator, timestamp);
			}
		}
		else
		{
			result = global::ActivationResult.Error_Destroyed;
		}
		return result;
	}

	// Token: 0x04001641 RID: 5697
	[SerializeField]
	private global::Activatable mainAction;

	// Token: 0x04001642 RID: 5698
	[SerializeField]
	private global::ContextActivator.ActivationMode activationMode;

	// Token: 0x04001643 RID: 5699
	[SerializeField]
	private global::Activatable[] extraActions;

	// Token: 0x04001644 RID: 5700
	[SerializeField]
	private string defaultText;

	// Token: 0x04001645 RID: 5701
	[SerializeField]
	private string onText;

	// Token: 0x04001646 RID: 5702
	[SerializeField]
	private string offText;

	// Token: 0x04001647 RID: 5703
	[SerializeField]
	private Vector3 defaultTextPoint;

	// Token: 0x04001648 RID: 5704
	[SerializeField]
	private Vector3 onTextPoint;

	// Token: 0x04001649 RID: 5705
	[SerializeField]
	private Vector3 offTextPoint;

	// Token: 0x0400164A RID: 5706
	[SerializeField]
	private bool useTextPoint;

	// Token: 0x0400164B RID: 5707
	[SerializeField]
	private bool useSpriteTextPoint;

	// Token: 0x0400164C RID: 5708
	[SerializeField]
	private global::ContextActivator.SpriteQuickMode defaultSprite;

	// Token: 0x0400164D RID: 5709
	[SerializeField]
	private global::ContextActivator.SpriteQuickMode onSprite;

	// Token: 0x0400164E RID: 5710
	[SerializeField]
	private global::ContextActivator.SpriteQuickMode offSprite;

	// Token: 0x0400164F RID: 5711
	[SerializeField]
	private bool isSwitch;

	// Token: 0x04001650 RID: 5712
	private bool isToggle;

	// Token: 0x02000518 RID: 1304
	private enum SpriteQuickMode
	{
		// Token: 0x04001652 RID: 5714
		Default,
		// Token: 0x04001653 RID: 5715
		Faded,
		// Token: 0x04001654 RID: 5716
		AlwaysVisible,
		// Token: 0x04001655 RID: 5717
		NeverVisisble
	}

	// Token: 0x02000519 RID: 1305
	private enum ActivationMode
	{
		// Token: 0x04001657 RID: 5719
		ActivateOrToggle,
		// Token: 0x04001658 RID: 5720
		TurnOn,
		// Token: 0x04001659 RID: 5721
		TurnOff
	}
}
