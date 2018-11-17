using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007B9 RID: 1977
[RequireComponent(typeof(global::dfPanel))]
[AddComponentMenu("Daikon Forge/User Interface/Panel Addon/Flow Layout")]
[ExecuteInEditMode]
public class dfPanelFlowLayout : MonoBehaviour
{
	// Token: 0x17000CE3 RID: 3299
	// (get) Token: 0x06004345 RID: 17221 RVA: 0x000F8CE4 File Offset: 0x000F6EE4
	// (set) Token: 0x06004346 RID: 17222 RVA: 0x000F8CEC File Offset: 0x000F6EEC
	public global::dfControlOrientation Direction
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

	// Token: 0x17000CE4 RID: 3300
	// (get) Token: 0x06004347 RID: 17223 RVA: 0x000F8D08 File Offset: 0x000F6F08
	// (set) Token: 0x06004348 RID: 17224 RVA: 0x000F8D10 File Offset: 0x000F6F10
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

	// Token: 0x17000CE5 RID: 3301
	// (get) Token: 0x06004349 RID: 17225 RVA: 0x000F8D48 File Offset: 0x000F6F48
	// (set) Token: 0x0600434A RID: 17226 RVA: 0x000F8D68 File Offset: 0x000F6F68
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

	// Token: 0x17000CE6 RID: 3302
	// (get) Token: 0x0600434B RID: 17227 RVA: 0x000F8D9C File Offset: 0x000F6F9C
	// (set) Token: 0x0600434C RID: 17228 RVA: 0x000F8DA4 File Offset: 0x000F6FA4
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

	// Token: 0x0600434D RID: 17229 RVA: 0x000F8DC0 File Offset: 0x000F6FC0
	public void OnEnable()
	{
		this.panel = base.GetComponent<global::dfPanel>();
		this.panel.SizeChanged += this.OnSizeChanged;
	}

	// Token: 0x0600434E RID: 17230 RVA: 0x000F8DE8 File Offset: 0x000F6FE8
	public void OnControlAdded(global::dfControl container, global::dfControl child)
	{
		child.ZOrderChanged += this.child_ZOrderChanged;
		child.SizeChanged += this.child_SizeChanged;
		this.performLayout();
	}

	// Token: 0x0600434F RID: 17231 RVA: 0x000F8E20 File Offset: 0x000F7020
	public void OnControlRemoved(global::dfControl container, global::dfControl child)
	{
		child.ZOrderChanged -= this.child_ZOrderChanged;
		child.SizeChanged -= this.child_SizeChanged;
		this.performLayout();
	}

	// Token: 0x06004350 RID: 17232 RVA: 0x000F8E58 File Offset: 0x000F7058
	public void OnSizeChanged(global::dfControl control, Vector2 value)
	{
		this.performLayout();
	}

	// Token: 0x06004351 RID: 17233 RVA: 0x000F8E60 File Offset: 0x000F7060
	private void child_SizeChanged(global::dfControl control, Vector2 value)
	{
		this.performLayout();
	}

	// Token: 0x06004352 RID: 17234 RVA: 0x000F8E68 File Offset: 0x000F7068
	private void child_ZOrderChanged(global::dfControl control, int value)
	{
		this.performLayout();
	}

	// Token: 0x06004353 RID: 17235 RVA: 0x000F8E70 File Offset: 0x000F7070
	private void performLayout()
	{
		if (this.panel == null)
		{
			this.panel = base.GetComponent<global::dfPanel>();
		}
		Vector3 relativePosition;
		relativePosition..ctor((float)this.borderPadding.left, (float)this.borderPadding.top);
		bool flag = true;
		float num = this.panel.Width - (float)this.borderPadding.right;
		float num2 = this.panel.Height - (float)this.borderPadding.bottom;
		int num3 = 0;
		IList<global::dfControl> controls = this.panel.Controls;
		int i = 0;
		while (i < controls.Count)
		{
			if (!flag)
			{
				if (this.flowDirection == global::dfControlOrientation.Horizontal)
				{
					relativePosition.x += this.itemSpacing.x;
				}
				else
				{
					relativePosition.y += this.itemSpacing.y;
				}
			}
			global::dfControl dfControl = controls[i];
			if (this.flowDirection == global::dfControlOrientation.Horizontal)
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
			if (this.flowDirection == global::dfControlOrientation.Horizontal)
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

	// Token: 0x06004354 RID: 17236 RVA: 0x000F909C File Offset: 0x000F729C
	private bool canShowControlUnclipped(global::dfControl control)
	{
		if (!this.hideClippedControls)
		{
			return true;
		}
		Vector3 relativePosition = control.RelativePosition;
		return relativePosition.x + control.Width < this.panel.Width - (float)this.borderPadding.right && relativePosition.y + control.Height < this.panel.Height - (float)this.borderPadding.bottom;
	}

	// Token: 0x040023CA RID: 9162
	[SerializeField]
	protected RectOffset borderPadding = new RectOffset();

	// Token: 0x040023CB RID: 9163
	[SerializeField]
	protected Vector2 itemSpacing = default(Vector2);

	// Token: 0x040023CC RID: 9164
	[SerializeField]
	protected global::dfControlOrientation flowDirection;

	// Token: 0x040023CD RID: 9165
	[SerializeField]
	protected bool hideClippedControls;

	// Token: 0x040023CE RID: 9166
	private global::dfPanel panel;
}
