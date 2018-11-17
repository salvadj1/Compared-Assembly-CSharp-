using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006E7 RID: 1767
[AddComponentMenu("Daikon Forge/User Interface/Panel Addon/Flow Layout")]
[RequireComponent(typeof(dfPanel))]
[ExecuteInEditMode]
public class dfPanelFlowLayout : MonoBehaviour
{
	// Token: 0x17000C5F RID: 3167
	// (get) Token: 0x06003F29 RID: 16169 RVA: 0x000F00E0 File Offset: 0x000EE2E0
	// (set) Token: 0x06003F2A RID: 16170 RVA: 0x000F00E8 File Offset: 0x000EE2E8
	public dfControlOrientation Direction
	{
		get
		{
			return this.flowDirection;
		}
		set
		{
			if (value != this.flowDirection)
			{
				this.flowDirection = value;
				this.performLayout();
			}
		}
	}

	// Token: 0x17000C60 RID: 3168
	// (get) Token: 0x06003F2B RID: 16171 RVA: 0x000F0104 File Offset: 0x000EE304
	// (set) Token: 0x06003F2C RID: 16172 RVA: 0x000F010C File Offset: 0x000EE30C
	public Vector2 ItemSpacing
	{
		get
		{
			return this.itemSpacing;
		}
		set
		{
			value = Vector2.Max(value, Vector2.zero);
			if (!object.Equals(value, this.itemSpacing))
			{
				this.itemSpacing = value;
				this.performLayout();
			}
		}
	}

	// Token: 0x17000C61 RID: 3169
	// (get) Token: 0x06003F2D RID: 16173 RVA: 0x000F0144 File Offset: 0x000EE344
	// (set) Token: 0x06003F2E RID: 16174 RVA: 0x000F0164 File Offset: 0x000EE364
	public RectOffset BorderPadding
	{
		get
		{
			if (this.borderPadding == null)
			{
				this.borderPadding = new RectOffset();
			}
			return this.borderPadding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.borderPadding))
			{
				this.borderPadding = value;
				this.performLayout();
			}
		}
	}

	// Token: 0x17000C62 RID: 3170
	// (get) Token: 0x06003F2F RID: 16175 RVA: 0x000F0198 File Offset: 0x000EE398
	// (set) Token: 0x06003F30 RID: 16176 RVA: 0x000F01A0 File Offset: 0x000EE3A0
	public bool HideClippedControls
	{
		get
		{
			return this.hideClippedControls;
		}
		set
		{
			if (value != this.hideClippedControls)
			{
				this.hideClippedControls = value;
				this.performLayout();
			}
		}
	}

	// Token: 0x06003F31 RID: 16177 RVA: 0x000F01BC File Offset: 0x000EE3BC
	public void OnEnable()
	{
		this.panel = base.GetComponent<dfPanel>();
		this.panel.SizeChanged += this.OnSizeChanged;
	}

	// Token: 0x06003F32 RID: 16178 RVA: 0x000F01E4 File Offset: 0x000EE3E4
	public void OnControlAdded(dfControl container, dfControl child)
	{
		child.ZOrderChanged += this.child_ZOrderChanged;
		child.SizeChanged += this.child_SizeChanged;
		this.performLayout();
	}

	// Token: 0x06003F33 RID: 16179 RVA: 0x000F021C File Offset: 0x000EE41C
	public void OnControlRemoved(dfControl container, dfControl child)
	{
		child.ZOrderChanged -= this.child_ZOrderChanged;
		child.SizeChanged -= this.child_SizeChanged;
		this.performLayout();
	}

	// Token: 0x06003F34 RID: 16180 RVA: 0x000F0254 File Offset: 0x000EE454
	public void OnSizeChanged(dfControl control, Vector2 value)
	{
		this.performLayout();
	}

	// Token: 0x06003F35 RID: 16181 RVA: 0x000F025C File Offset: 0x000EE45C
	private void child_SizeChanged(dfControl control, Vector2 value)
	{
		this.performLayout();
	}

	// Token: 0x06003F36 RID: 16182 RVA: 0x000F0264 File Offset: 0x000EE464
	private void child_ZOrderChanged(dfControl control, int value)
	{
		this.performLayout();
	}

	// Token: 0x06003F37 RID: 16183 RVA: 0x000F026C File Offset: 0x000EE46C
	private void performLayout()
	{
		if (this.panel == null)
		{
			this.panel = base.GetComponent<dfPanel>();
		}
		Vector3 relativePosition;
		relativePosition..ctor((float)this.borderPadding.left, (float)this.borderPadding.top);
		bool flag = true;
		float num = this.panel.Width - (float)this.borderPadding.right;
		float num2 = this.panel.Height - (float)this.borderPadding.bottom;
		int num3 = 0;
		IList<dfControl> controls = this.panel.Controls;
		int i = 0;
		while (i < controls.Count)
		{
			if (!flag)
			{
				if (this.flowDirection == dfControlOrientation.Horizontal)
				{
					relativePosition.x += this.itemSpacing.x;
				}
				else
				{
					relativePosition.y += this.itemSpacing.y;
				}
			}
			dfControl dfControl = controls[i];
			if (this.flowDirection == dfControlOrientation.Horizontal)
			{
				if (!flag && relativePosition.x + dfControl.Width >= num)
				{
					relativePosition.x = (float)this.borderPadding.left;
					relativePosition.y += (float)num3;
					num3 = 0;
				}
			}
			else if (!flag && relativePosition.y + dfControl.Height >= num2)
			{
				relativePosition.y = (float)this.borderPadding.top;
				relativePosition.x += (float)num3;
				num3 = 0;
			}
			dfControl.RelativePosition = relativePosition;
			if (this.flowDirection == dfControlOrientation.Horizontal)
			{
				relativePosition.x += dfControl.Width;
				num3 = Mathf.Max(Mathf.CeilToInt(dfControl.Height + this.itemSpacing.y), num3);
			}
			else
			{
				relativePosition.y += dfControl.Height;
				num3 = Mathf.Max(Mathf.CeilToInt(dfControl.Width + this.itemSpacing.x), num3);
			}
			dfControl.IsVisible = this.canShowControlUnclipped(dfControl);
			i++;
			flag = false;
		}
	}

	// Token: 0x06003F38 RID: 16184 RVA: 0x000F0498 File Offset: 0x000EE698
	private bool canShowControlUnclipped(dfControl control)
	{
		if (!this.hideClippedControls)
		{
			return true;
		}
		Vector3 relativePosition = control.RelativePosition;
		return relativePosition.x + control.Width < this.panel.Width - (float)this.borderPadding.right && relativePosition.y + control.Height < this.panel.Height - (float)this.borderPadding.bottom;
	}

	// Token: 0x040021C1 RID: 8641
	[SerializeField]
	protected RectOffset borderPadding = new RectOffset();

	// Token: 0x040021C2 RID: 8642
	[SerializeField]
	protected Vector2 itemSpacing = default(Vector2);

	// Token: 0x040021C3 RID: 8643
	[SerializeField]
	protected dfControlOrientation flowDirection;

	// Token: 0x040021C4 RID: 8644
	[SerializeField]
	protected bool hideClippedControls;

	// Token: 0x040021C5 RID: 8645
	private dfPanel panel;
}
