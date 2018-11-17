using System;
using UnityEngine;

// Token: 0x020006A2 RID: 1698
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Daikon Forge/User Interface/Checkbox")]
[ExecuteInEditMode]
[Serializable]
public class dfCheckbox : dfControl
{
	// Token: 0x14000022 RID: 34
	// (add) Token: 0x06003AE7 RID: 15079 RVA: 0x000DC9C8 File Offset: 0x000DABC8
	// (remove) Token: 0x06003AE8 RID: 15080 RVA: 0x000DC9E4 File Offset: 0x000DABE4
	public event PropertyChangedEventHandler<bool> CheckChanged;

	// Token: 0x17000B6C RID: 2924
	// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x000DCA00 File Offset: 0x000DAC00
	// (set) Token: 0x06003AEA RID: 15082 RVA: 0x000DCA08 File Offset: 0x000DAC08
	public bool IsChecked
	{
		get
		{
			return this.isChecked;
		}
		set
		{
			if (value != this.isChecked)
			{
				this.isChecked = value;
				this.OnCheckChanged();
			}
		}
	}

	// Token: 0x17000B6D RID: 2925
	// (get) Token: 0x06003AEB RID: 15083 RVA: 0x000DCA24 File Offset: 0x000DAC24
	// (set) Token: 0x06003AEC RID: 15084 RVA: 0x000DCA2C File Offset: 0x000DAC2C
	public dfControl CheckIcon
	{
		get
		{
			return this.checkIcon;
		}
		set
		{
			if (value != this.checkIcon)
			{
				this.checkIcon = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B6E RID: 2926
	// (get) Token: 0x06003AED RID: 15085 RVA: 0x000DCA4C File Offset: 0x000DAC4C
	// (set) Token: 0x06003AEE RID: 15086 RVA: 0x000DCA54 File Offset: 0x000DAC54
	public dfLabel Label
	{
		get
		{
			return this.label;
		}
		set
		{
			if (value != this.label)
			{
				this.label = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B6F RID: 2927
	// (get) Token: 0x06003AEF RID: 15087 RVA: 0x000DCA74 File Offset: 0x000DAC74
	// (set) Token: 0x06003AF0 RID: 15088 RVA: 0x000DCA7C File Offset: 0x000DAC7C
	public dfControl GroupContainer
	{
		get
		{
			return this.group;
		}
		set
		{
			if (value != this.group)
			{
				this.group = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B70 RID: 2928
	// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x000DCA9C File Offset: 0x000DAC9C
	// (set) Token: 0x06003AF2 RID: 15090 RVA: 0x000DCACC File Offset: 0x000DACCC
	public string Text
	{
		get
		{
			if (this.label != null)
			{
				return this.label.Text;
			}
			return "[LABEL NOT SET]";
		}
		set
		{
			if (this.label != null)
			{
				this.label.Text = value;
			}
		}
	}

	// Token: 0x17000B71 RID: 2929
	// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x000DCAEC File Offset: 0x000DACEC
	public override bool CanFocus
	{
		get
		{
			return base.IsEnabled && base.IsVisible;
		}
	}

	// Token: 0x06003AF4 RID: 15092 RVA: 0x000DCB04 File Offset: 0x000DAD04
	public override void Start()
	{
		base.Start();
		if (this.checkIcon != null)
		{
			this.checkIcon.BringToFront();
			this.checkIcon.IsVisible = this.IsChecked;
		}
	}

	// Token: 0x06003AF5 RID: 15093 RVA: 0x000DCB44 File Offset: 0x000DAD44
	protected internal override void OnKeyPress(dfKeyEventArgs args)
	{
		if (args.KeyCode == 32)
		{
			this.OnClick(new dfMouseEventArgs(this, dfMouseButtons.Left, 1, default(Ray), Vector2.zero, 0f));
			return;
		}
		base.OnKeyPress(args);
	}

	// Token: 0x06003AF6 RID: 15094 RVA: 0x000DCB88 File Offset: 0x000DAD88
	protected internal override void OnClick(dfMouseEventArgs args)
	{
		if (this.group == null)
		{
			this.IsChecked = !this.IsChecked;
		}
		else
		{
			foreach (dfCheckbox dfCheckbox in base.transform.parent.GetComponentsInChildren<dfCheckbox>())
			{
				if (dfCheckbox != this && dfCheckbox.GroupContainer == this.GroupContainer && dfCheckbox.IsChecked)
				{
					dfCheckbox.IsChecked = false;
				}
			}
			this.IsChecked = true;
		}
		args.Use();
		base.OnClick(args);
	}

	// Token: 0x06003AF7 RID: 15095 RVA: 0x000DCC2C File Offset: 0x000DAE2C
	protected internal void OnCheckChanged()
	{
		base.SignalHierarchy("OnCheckChanged", new object[]
		{
			this.isChecked
		});
		if (this.CheckChanged != null)
		{
			this.CheckChanged(this, this.isChecked);
		}
		if (this.checkIcon != null)
		{
			if (this.IsChecked)
			{
				this.checkIcon.BringToFront();
			}
			this.checkIcon.IsVisible = this.IsChecked;
		}
	}

	// Token: 0x04001F21 RID: 7969
	[SerializeField]
	protected bool isChecked;

	// Token: 0x04001F22 RID: 7970
	[SerializeField]
	protected dfControl checkIcon;

	// Token: 0x04001F23 RID: 7971
	[SerializeField]
	protected dfLabel label;

	// Token: 0x04001F24 RID: 7972
	[SerializeField]
	protected dfControl group;
}
