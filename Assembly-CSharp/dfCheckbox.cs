using System;
using UnityEngine;

// Token: 0x02000768 RID: 1896
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Checkbox")]
[RequireComponent(typeof(BoxCollider))]
[Serializable]
public class dfCheckbox : global::dfControl
{
	// Token: 0x14000022 RID: 34
	// (add) Token: 0x06003EE5 RID: 16101 RVA: 0x000E5458 File Offset: 0x000E3658
	// (remove) Token: 0x06003EE6 RID: 16102 RVA: 0x000E5474 File Offset: 0x000E3674
	public event global::PropertyChangedEventHandler<bool> CheckChanged;

	// Token: 0x17000BF0 RID: 3056
	// (get) Token: 0x06003EE7 RID: 16103 RVA: 0x000E5490 File Offset: 0x000E3690
	// (set) Token: 0x06003EE8 RID: 16104 RVA: 0x000E5498 File Offset: 0x000E3698
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

	// Token: 0x17000BF1 RID: 3057
	// (get) Token: 0x06003EE9 RID: 16105 RVA: 0x000E54B4 File Offset: 0x000E36B4
	// (set) Token: 0x06003EEA RID: 16106 RVA: 0x000E54BC File Offset: 0x000E36BC
	public global::dfControl CheckIcon
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

	// Token: 0x17000BF2 RID: 3058
	// (get) Token: 0x06003EEB RID: 16107 RVA: 0x000E54DC File Offset: 0x000E36DC
	// (set) Token: 0x06003EEC RID: 16108 RVA: 0x000E54E4 File Offset: 0x000E36E4
	public global::dfLabel Label
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

	// Token: 0x17000BF3 RID: 3059
	// (get) Token: 0x06003EED RID: 16109 RVA: 0x000E5504 File Offset: 0x000E3704
	// (set) Token: 0x06003EEE RID: 16110 RVA: 0x000E550C File Offset: 0x000E370C
	public global::dfControl GroupContainer
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

	// Token: 0x17000BF4 RID: 3060
	// (get) Token: 0x06003EEF RID: 16111 RVA: 0x000E552C File Offset: 0x000E372C
	// (set) Token: 0x06003EF0 RID: 16112 RVA: 0x000E555C File Offset: 0x000E375C
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

	// Token: 0x17000BF5 RID: 3061
	// (get) Token: 0x06003EF1 RID: 16113 RVA: 0x000E557C File Offset: 0x000E377C
	public override bool CanFocus
	{
		get
		{
			return base.IsEnabled && base.IsVisible;
		}
	}

	// Token: 0x06003EF2 RID: 16114 RVA: 0x000E5594 File Offset: 0x000E3794
	public override void Start()
	{
		base.Start();
		if (this.checkIcon != null)
		{
			this.checkIcon.BringToFront();
			this.checkIcon.IsVisible = this.IsChecked;
		}
	}

	// Token: 0x06003EF3 RID: 16115 RVA: 0x000E55D4 File Offset: 0x000E37D4
	protected internal override void OnKeyPress(global::dfKeyEventArgs args)
	{
		if (args.KeyCode == 32)
		{
			this.OnClick(new global::dfMouseEventArgs(this, global::dfMouseButtons.Left, 1, default(Ray), Vector2.zero, 0f));
			return;
		}
		base.OnKeyPress(args);
	}

	// Token: 0x06003EF4 RID: 16116 RVA: 0x000E5618 File Offset: 0x000E3818
	protected internal override void OnClick(global::dfMouseEventArgs args)
	{
		if (this.group == null)
		{
			this.IsChecked = !this.IsChecked;
		}
		else
		{
			foreach (global::dfCheckbox dfCheckbox in base.transform.parent.GetComponentsInChildren<global::dfCheckbox>())
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

	// Token: 0x06003EF5 RID: 16117 RVA: 0x000E56BC File Offset: 0x000E38BC
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

	// Token: 0x0400211D RID: 8477
	[SerializeField]
	protected bool isChecked;

	// Token: 0x0400211E RID: 8478
	[SerializeField]
	protected global::dfControl checkIcon;

	// Token: 0x0400211F RID: 8479
	[SerializeField]
	protected global::dfLabel label;

	// Token: 0x04002120 RID: 8480
	[SerializeField]
	protected global::dfControl group;
}
