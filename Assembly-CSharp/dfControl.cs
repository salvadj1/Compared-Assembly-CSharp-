using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

// Token: 0x020006A6 RID: 1702
[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
[Serializable]
public abstract class dfControl : MonoBehaviour, IComparable<dfControl>
{
	// Token: 0x14000023 RID: 35
	// (add) Token: 0x06003B05 RID: 15109 RVA: 0x000DD818 File Offset: 0x000DBA18
	// (remove) Token: 0x06003B06 RID: 15110 RVA: 0x000DD834 File Offset: 0x000DBA34
	[HideInInspector]
	public event ChildControlEventHandler ControlAdded;

	// Token: 0x14000024 RID: 36
	// (add) Token: 0x06003B07 RID: 15111 RVA: 0x000DD850 File Offset: 0x000DBA50
	// (remove) Token: 0x06003B08 RID: 15112 RVA: 0x000DD86C File Offset: 0x000DBA6C
	[HideInInspector]
	public event ChildControlEventHandler ControlRemoved;

	// Token: 0x14000025 RID: 37
	// (add) Token: 0x06003B09 RID: 15113 RVA: 0x000DD888 File Offset: 0x000DBA88
	// (remove) Token: 0x06003B0A RID: 15114 RVA: 0x000DD8A4 File Offset: 0x000DBAA4
	public event FocusEventHandler GotFocus;

	// Token: 0x14000026 RID: 38
	// (add) Token: 0x06003B0B RID: 15115 RVA: 0x000DD8C0 File Offset: 0x000DBAC0
	// (remove) Token: 0x06003B0C RID: 15116 RVA: 0x000DD8DC File Offset: 0x000DBADC
	public event FocusEventHandler EnterFocus;

	// Token: 0x14000027 RID: 39
	// (add) Token: 0x06003B0D RID: 15117 RVA: 0x000DD8F8 File Offset: 0x000DBAF8
	// (remove) Token: 0x06003B0E RID: 15118 RVA: 0x000DD914 File Offset: 0x000DBB14
	public event FocusEventHandler LostFocus;

	// Token: 0x14000028 RID: 40
	// (add) Token: 0x06003B0F RID: 15119 RVA: 0x000DD930 File Offset: 0x000DBB30
	// (remove) Token: 0x06003B10 RID: 15120 RVA: 0x000DD94C File Offset: 0x000DBB4C
	public event FocusEventHandler LeaveFocus;

	// Token: 0x14000029 RID: 41
	// (add) Token: 0x06003B11 RID: 15121 RVA: 0x000DD968 File Offset: 0x000DBB68
	// (remove) Token: 0x06003B12 RID: 15122 RVA: 0x000DD984 File Offset: 0x000DBB84
	public event PropertyChangedEventHandler<int> TabIndexChanged;

	// Token: 0x1400002A RID: 42
	// (add) Token: 0x06003B13 RID: 15123 RVA: 0x000DD9A0 File Offset: 0x000DBBA0
	// (remove) Token: 0x06003B14 RID: 15124 RVA: 0x000DD9BC File Offset: 0x000DBBBC
	public event PropertyChangedEventHandler<Vector2> PositionChanged;

	// Token: 0x1400002B RID: 43
	// (add) Token: 0x06003B15 RID: 15125 RVA: 0x000DD9D8 File Offset: 0x000DBBD8
	// (remove) Token: 0x06003B16 RID: 15126 RVA: 0x000DD9F4 File Offset: 0x000DBBF4
	public event PropertyChangedEventHandler<Vector2> SizeChanged;

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x06003B17 RID: 15127 RVA: 0x000DDA10 File Offset: 0x000DBC10
	// (remove) Token: 0x06003B18 RID: 15128 RVA: 0x000DDA2C File Offset: 0x000DBC2C
	[HideInInspector]
	public event PropertyChangedEventHandler<Color32> ColorChanged;

	// Token: 0x1400002D RID: 45
	// (add) Token: 0x06003B19 RID: 15129 RVA: 0x000DDA48 File Offset: 0x000DBC48
	// (remove) Token: 0x06003B1A RID: 15130 RVA: 0x000DDA64 File Offset: 0x000DBC64
	public event PropertyChangedEventHandler<bool> IsVisibleChanged;

	// Token: 0x1400002E RID: 46
	// (add) Token: 0x06003B1B RID: 15131 RVA: 0x000DDA80 File Offset: 0x000DBC80
	// (remove) Token: 0x06003B1C RID: 15132 RVA: 0x000DDA9C File Offset: 0x000DBC9C
	public event PropertyChangedEventHandler<bool> IsEnabledChanged;

	// Token: 0x1400002F RID: 47
	// (add) Token: 0x06003B1D RID: 15133 RVA: 0x000DDAB8 File Offset: 0x000DBCB8
	// (remove) Token: 0x06003B1E RID: 15134 RVA: 0x000DDAD4 File Offset: 0x000DBCD4
	[HideInInspector]
	public event PropertyChangedEventHandler<float> OpacityChanged;

	// Token: 0x14000030 RID: 48
	// (add) Token: 0x06003B1F RID: 15135 RVA: 0x000DDAF0 File Offset: 0x000DBCF0
	// (remove) Token: 0x06003B20 RID: 15136 RVA: 0x000DDB0C File Offset: 0x000DBD0C
	[HideInInspector]
	public event PropertyChangedEventHandler<dfAnchorStyle> AnchorChanged;

	// Token: 0x14000031 RID: 49
	// (add) Token: 0x06003B21 RID: 15137 RVA: 0x000DDB28 File Offset: 0x000DBD28
	// (remove) Token: 0x06003B22 RID: 15138 RVA: 0x000DDB44 File Offset: 0x000DBD44
	[HideInInspector]
	public event PropertyChangedEventHandler<dfPivotPoint> PivotChanged;

	// Token: 0x14000032 RID: 50
	// (add) Token: 0x06003B23 RID: 15139 RVA: 0x000DDB60 File Offset: 0x000DBD60
	// (remove) Token: 0x06003B24 RID: 15140 RVA: 0x000DDB7C File Offset: 0x000DBD7C
	[HideInInspector]
	public event PropertyChangedEventHandler<int> ZOrderChanged;

	// Token: 0x14000033 RID: 51
	// (add) Token: 0x06003B25 RID: 15141 RVA: 0x000DDB98 File Offset: 0x000DBD98
	// (remove) Token: 0x06003B26 RID: 15142 RVA: 0x000DDBB4 File Offset: 0x000DBDB4
	public event DragEventHandler DragStart;

	// Token: 0x14000034 RID: 52
	// (add) Token: 0x06003B27 RID: 15143 RVA: 0x000DDBD0 File Offset: 0x000DBDD0
	// (remove) Token: 0x06003B28 RID: 15144 RVA: 0x000DDBEC File Offset: 0x000DBDEC
	public event DragEventHandler DragEnd;

	// Token: 0x14000035 RID: 53
	// (add) Token: 0x06003B29 RID: 15145 RVA: 0x000DDC08 File Offset: 0x000DBE08
	// (remove) Token: 0x06003B2A RID: 15146 RVA: 0x000DDC24 File Offset: 0x000DBE24
	public event DragEventHandler DragDrop;

	// Token: 0x14000036 RID: 54
	// (add) Token: 0x06003B2B RID: 15147 RVA: 0x000DDC40 File Offset: 0x000DBE40
	// (remove) Token: 0x06003B2C RID: 15148 RVA: 0x000DDC5C File Offset: 0x000DBE5C
	public event DragEventHandler DragEnter;

	// Token: 0x14000037 RID: 55
	// (add) Token: 0x06003B2D RID: 15149 RVA: 0x000DDC78 File Offset: 0x000DBE78
	// (remove) Token: 0x06003B2E RID: 15150 RVA: 0x000DDC94 File Offset: 0x000DBE94
	public event DragEventHandler DragLeave;

	// Token: 0x14000038 RID: 56
	// (add) Token: 0x06003B2F RID: 15151 RVA: 0x000DDCB0 File Offset: 0x000DBEB0
	// (remove) Token: 0x06003B30 RID: 15152 RVA: 0x000DDCCC File Offset: 0x000DBECC
	public event DragEventHandler DragOver;

	// Token: 0x14000039 RID: 57
	// (add) Token: 0x06003B31 RID: 15153 RVA: 0x000DDCE8 File Offset: 0x000DBEE8
	// (remove) Token: 0x06003B32 RID: 15154 RVA: 0x000DDD04 File Offset: 0x000DBF04
	public event KeyPressHandler KeyPress;

	// Token: 0x1400003A RID: 58
	// (add) Token: 0x06003B33 RID: 15155 RVA: 0x000DDD20 File Offset: 0x000DBF20
	// (remove) Token: 0x06003B34 RID: 15156 RVA: 0x000DDD3C File Offset: 0x000DBF3C
	public event KeyPressHandler KeyDown;

	// Token: 0x1400003B RID: 59
	// (add) Token: 0x06003B35 RID: 15157 RVA: 0x000DDD58 File Offset: 0x000DBF58
	// (remove) Token: 0x06003B36 RID: 15158 RVA: 0x000DDD74 File Offset: 0x000DBF74
	public event KeyPressHandler KeyUp;

	// Token: 0x1400003C RID: 60
	// (add) Token: 0x06003B37 RID: 15159 RVA: 0x000DDD90 File Offset: 0x000DBF90
	// (remove) Token: 0x06003B38 RID: 15160 RVA: 0x000DDDAC File Offset: 0x000DBFAC
	public event ControlMultiTouchEventHandler MultiTouch;

	// Token: 0x1400003D RID: 61
	// (add) Token: 0x06003B39 RID: 15161 RVA: 0x000DDDC8 File Offset: 0x000DBFC8
	// (remove) Token: 0x06003B3A RID: 15162 RVA: 0x000DDDE4 File Offset: 0x000DBFE4
	public event MouseEventHandler MouseEnter;

	// Token: 0x1400003E RID: 62
	// (add) Token: 0x06003B3B RID: 15163 RVA: 0x000DDE00 File Offset: 0x000DC000
	// (remove) Token: 0x06003B3C RID: 15164 RVA: 0x000DDE1C File Offset: 0x000DC01C
	public event MouseEventHandler MouseMove;

	// Token: 0x1400003F RID: 63
	// (add) Token: 0x06003B3D RID: 15165 RVA: 0x000DDE38 File Offset: 0x000DC038
	// (remove) Token: 0x06003B3E RID: 15166 RVA: 0x000DDE54 File Offset: 0x000DC054
	public event MouseEventHandler MouseHover;

	// Token: 0x14000040 RID: 64
	// (add) Token: 0x06003B3F RID: 15167 RVA: 0x000DDE70 File Offset: 0x000DC070
	// (remove) Token: 0x06003B40 RID: 15168 RVA: 0x000DDE8C File Offset: 0x000DC08C
	public event MouseEventHandler MouseLeave;

	// Token: 0x14000041 RID: 65
	// (add) Token: 0x06003B41 RID: 15169 RVA: 0x000DDEA8 File Offset: 0x000DC0A8
	// (remove) Token: 0x06003B42 RID: 15170 RVA: 0x000DDEC4 File Offset: 0x000DC0C4
	public event MouseEventHandler MouseDown;

	// Token: 0x14000042 RID: 66
	// (add) Token: 0x06003B43 RID: 15171 RVA: 0x000DDEE0 File Offset: 0x000DC0E0
	// (remove) Token: 0x06003B44 RID: 15172 RVA: 0x000DDEFC File Offset: 0x000DC0FC
	public event MouseEventHandler MouseUp;

	// Token: 0x14000043 RID: 67
	// (add) Token: 0x06003B45 RID: 15173 RVA: 0x000DDF18 File Offset: 0x000DC118
	// (remove) Token: 0x06003B46 RID: 15174 RVA: 0x000DDF34 File Offset: 0x000DC134
	public event MouseEventHandler MouseWheel;

	// Token: 0x14000044 RID: 68
	// (add) Token: 0x06003B47 RID: 15175 RVA: 0x000DDF50 File Offset: 0x000DC150
	// (remove) Token: 0x06003B48 RID: 15176 RVA: 0x000DDF6C File Offset: 0x000DC16C
	public event MouseEventHandler Click;

	// Token: 0x14000045 RID: 69
	// (add) Token: 0x06003B49 RID: 15177 RVA: 0x000DDF88 File Offset: 0x000DC188
	// (remove) Token: 0x06003B4A RID: 15178 RVA: 0x000DDFA4 File Offset: 0x000DC1A4
	public event MouseEventHandler DoubleClick;

	// Token: 0x17000B72 RID: 2930
	// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000DDFC0 File Offset: 0x000DC1C0
	public dfGUIManager GUIManager
	{
		get
		{
			return this.GetManager();
		}
	}

	// Token: 0x17000B73 RID: 2931
	// (get) Token: 0x06003B4C RID: 15180 RVA: 0x000DDFC8 File Offset: 0x000DC1C8
	// (set) Token: 0x06003B4D RID: 15181 RVA: 0x000DE03C File Offset: 0x000DC23C
	public bool IsEnabled
	{
		get
		{
			return base.enabled && (!(base.gameObject != null) || base.gameObject.activeSelf) && ((!(this.parent != null)) ? this.isEnabled : (this.isEnabled && this.parent.IsEnabled));
		}
		set
		{
			if (value != this.isEnabled)
			{
				this.isEnabled = value;
				this.OnIsEnabledChanged();
			}
		}
	}

	// Token: 0x17000B74 RID: 2932
	// (get) Token: 0x06003B4E RID: 15182 RVA: 0x000DE058 File Offset: 0x000DC258
	// (set) Token: 0x06003B4F RID: 15183 RVA: 0x000DE090 File Offset: 0x000DC290
	[SerializeField]
	public bool IsVisible
	{
		get
		{
			return (!(this.parent == null)) ? (this.isVisible && this.parent.IsVisible) : this.isVisible;
		}
		set
		{
			if (value != this.isVisible)
			{
				if (Application.isPlaying && !this.IsInteractive)
				{
					base.collider.enabled = false;
				}
				else
				{
					base.collider.enabled = value;
				}
				this.isVisible = value;
				this.OnIsVisibleChanged();
			}
		}
	}

	// Token: 0x17000B75 RID: 2933
	// (get) Token: 0x06003B50 RID: 15184 RVA: 0x000DE0E8 File Offset: 0x000DC2E8
	// (set) Token: 0x06003B51 RID: 15185 RVA: 0x000DE0F0 File Offset: 0x000DC2F0
	public virtual bool IsInteractive
	{
		get
		{
			return this.isInteractive;
		}
		set
		{
			if (this.HasFocus && !value)
			{
				dfGUIManager.SetFocus(null);
			}
			this.isInteractive = value;
		}
	}

	// Token: 0x17000B76 RID: 2934
	// (get) Token: 0x06003B52 RID: 15186 RVA: 0x000DE110 File Offset: 0x000DC310
	// (set) Token: 0x06003B53 RID: 15187 RVA: 0x000DE118 File Offset: 0x000DC318
	[SerializeField]
	public string Tooltip
	{
		get
		{
			return this.tooltip;
		}
		set
		{
			if (value != this.tooltip)
			{
				this.tooltip = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B77 RID: 2935
	// (get) Token: 0x06003B54 RID: 15188 RVA: 0x000DE138 File Offset: 0x000DC338
	// (set) Token: 0x06003B55 RID: 15189 RVA: 0x000DE14C File Offset: 0x000DC34C
	[SerializeField]
	public dfAnchorStyle Anchor
	{
		get
		{
			this.ensureLayoutExists();
			return this.layout.AnchorStyle;
		}
		set
		{
			this.ensureLayoutExists();
			if (value != this.layout.AnchorStyle)
			{
				this.layout.AnchorStyle = value;
				this.Invalidate();
				this.OnAnchorChanged();
			}
		}
	}

	// Token: 0x17000B78 RID: 2936
	// (get) Token: 0x06003B56 RID: 15190 RVA: 0x000DE188 File Offset: 0x000DC388
	// (set) Token: 0x06003B57 RID: 15191 RVA: 0x000DE19C File Offset: 0x000DC39C
	public float Opacity
	{
		get
		{
			return (float)this.color.a / 255f;
		}
		set
		{
			value = Mathf.Max(0f, Mathf.Min(1f, value));
			float num = (float)this.color.a / 255f;
			if (value != num)
			{
				this.color.a = (byte)(value * 255f);
				this.OnOpacityChanged();
			}
		}
	}

	// Token: 0x17000B79 RID: 2937
	// (get) Token: 0x06003B58 RID: 15192 RVA: 0x000DE1F4 File Offset: 0x000DC3F4
	// (set) Token: 0x06003B59 RID: 15193 RVA: 0x000DE1FC File Offset: 0x000DC3FC
	public Color32 Color
	{
		get
		{
			return this.color;
		}
		set
		{
			if (!this.color.Equals(value))
			{
				this.color = value;
				this.OnColorChanged();
			}
		}
	}

	// Token: 0x17000B7A RID: 2938
	// (get) Token: 0x06003B5A RID: 15194 RVA: 0x000DE234 File Offset: 0x000DC434
	// (set) Token: 0x06003B5B RID: 15195 RVA: 0x000DE23C File Offset: 0x000DC43C
	public Color32 DisabledColor
	{
		get
		{
			return this.disabledColor;
		}
		set
		{
			if (!value.Equals(this.disabledColor))
			{
				this.disabledColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B7B RID: 2939
	// (get) Token: 0x06003B5C RID: 15196 RVA: 0x000DE274 File Offset: 0x000DC474
	// (set) Token: 0x06003B5D RID: 15197 RVA: 0x000DE27C File Offset: 0x000DC47C
	public dfPivotPoint Pivot
	{
		get
		{
			return this.pivot;
		}
		set
		{
			if (value != this.pivot)
			{
				Vector3 position = this.Position;
				this.pivot = value;
				Vector3 vector = this.Position - position;
				this.SuspendLayout();
				this.Position = position;
				for (int i = 0; i < this.controls.Count; i++)
				{
					this.controls[i].Position += vector;
				}
				this.ResumeLayout();
				this.OnPivotChanged();
			}
		}
	}

	// Token: 0x17000B7C RID: 2940
	// (get) Token: 0x06003B5E RID: 15198 RVA: 0x000DE304 File Offset: 0x000DC504
	// (set) Token: 0x06003B5F RID: 15199 RVA: 0x000DE30C File Offset: 0x000DC50C
	public Vector3 RelativePosition
	{
		get
		{
			return this.getRelativePosition();
		}
		set
		{
			this.setRelativePosition(value);
		}
	}

	// Token: 0x17000B7D RID: 2941
	// (get) Token: 0x06003B60 RID: 15200 RVA: 0x000DE318 File Offset: 0x000DC518
	// (set) Token: 0x06003B61 RID: 15201 RVA: 0x000DE354 File Offset: 0x000DC554
	public Vector3 Position
	{
		get
		{
			Vector3 vector = base.transform.localPosition / this.PixelsToUnits();
			return vector + this.pivot.TransformToUpperLeft(this.Size);
		}
		set
		{
			this.setPositionInternal(value);
		}
	}

	// Token: 0x17000B7E RID: 2942
	// (get) Token: 0x06003B62 RID: 15202 RVA: 0x000DE360 File Offset: 0x000DC560
	// (set) Token: 0x06003B63 RID: 15203 RVA: 0x000DE368 File Offset: 0x000DC568
	public Vector2 Size
	{
		get
		{
			return this.size;
		}
		set
		{
			value = Vector2.Max(this.CalculateMinimumSize(), value);
			value.x = ((this.maxSize.x <= 0f) ? value.x : Mathf.Min(value.x, this.maxSize.x));
			value.y = ((this.maxSize.y <= 0f) ? value.y : Mathf.Min(value.y, this.maxSize.y));
			if ((value - this.size).sqrMagnitude <= 1.401298E-45f)
			{
				return;
			}
			this.size = value;
			this.OnSizeChanged();
		}
	}

	// Token: 0x17000B7F RID: 2943
	// (get) Token: 0x06003B64 RID: 15204 RVA: 0x000DE430 File Offset: 0x000DC630
	// (set) Token: 0x06003B65 RID: 15205 RVA: 0x000DE440 File Offset: 0x000DC640
	public float Width
	{
		get
		{
			return this.size.x;
		}
		set
		{
			this.Size = new Vector2(value, this.size.y);
		}
	}

	// Token: 0x17000B80 RID: 2944
	// (get) Token: 0x06003B66 RID: 15206 RVA: 0x000DE45C File Offset: 0x000DC65C
	// (set) Token: 0x06003B67 RID: 15207 RVA: 0x000DE46C File Offset: 0x000DC66C
	public float Height
	{
		get
		{
			return this.size.y;
		}
		set
		{
			this.Size = new Vector2(this.size.x, value);
		}
	}

	// Token: 0x17000B81 RID: 2945
	// (get) Token: 0x06003B68 RID: 15208 RVA: 0x000DE488 File Offset: 0x000DC688
	// (set) Token: 0x06003B69 RID: 15209 RVA: 0x000DE490 File Offset: 0x000DC690
	public Vector2 MinimumSize
	{
		get
		{
			return this.minSize;
		}
		set
		{
			value = Vector2.Max(Vector2.zero, value.RoundToInt());
			if (value != this.minSize)
			{
				this.minSize = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B82 RID: 2946
	// (get) Token: 0x06003B6A RID: 15210 RVA: 0x000DE4D0 File Offset: 0x000DC6D0
	// (set) Token: 0x06003B6B RID: 15211 RVA: 0x000DE4D8 File Offset: 0x000DC6D8
	public Vector2 MaximumSize
	{
		get
		{
			return this.maxSize;
		}
		set
		{
			value = Vector2.Max(Vector2.zero, value.RoundToInt());
			if (value != this.maxSize)
			{
				this.maxSize = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B83 RID: 2947
	// (get) Token: 0x06003B6C RID: 15212 RVA: 0x000DE518 File Offset: 0x000DC718
	// (set) Token: 0x06003B6D RID: 15213 RVA: 0x000DE520 File Offset: 0x000DC720
	[HideInInspector]
	public int ZOrder
	{
		get
		{
			return this.zindex;
		}
		set
		{
			if (value != this.zindex)
			{
				this.zindex = Mathf.Max(-1, value);
				this.Invalidate();
				if (this.parent != null)
				{
					this.parent.SetControlIndex(this, value);
				}
				this.OnZOrderChanged();
			}
		}
	}

	// Token: 0x17000B84 RID: 2948
	// (get) Token: 0x06003B6E RID: 15214 RVA: 0x000DE570 File Offset: 0x000DC770
	// (set) Token: 0x06003B6F RID: 15215 RVA: 0x000DE578 File Offset: 0x000DC778
	[HideInInspector]
	public int TabIndex
	{
		get
		{
			return this.tabIndex;
		}
		set
		{
			if (value != this.tabIndex)
			{
				this.tabIndex = Mathf.Max(-1, value);
				this.OnTabIndexChanged();
			}
		}
	}

	// Token: 0x17000B85 RID: 2949
	// (get) Token: 0x06003B70 RID: 15216 RVA: 0x000DE59C File Offset: 0x000DC79C
	public IList<dfControl> Controls
	{
		get
		{
			return this.controls;
		}
	}

	// Token: 0x17000B86 RID: 2950
	// (get) Token: 0x06003B71 RID: 15217 RVA: 0x000DE5A4 File Offset: 0x000DC7A4
	public dfControl Parent
	{
		get
		{
			return this.parent;
		}
	}

	// Token: 0x17000B87 RID: 2951
	// (get) Token: 0x06003B72 RID: 15218 RVA: 0x000DE5AC File Offset: 0x000DC7AC
	// (set) Token: 0x06003B73 RID: 15219 RVA: 0x000DE5B4 File Offset: 0x000DC7B4
	public bool ClipChildren
	{
		get
		{
			return this.clipChildren;
		}
		set
		{
			if (value != this.clipChildren)
			{
				this.clipChildren = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000B88 RID: 2952
	// (get) Token: 0x06003B74 RID: 15220 RVA: 0x000DE5D0 File Offset: 0x000DC7D0
	protected bool IsLayoutSuspended
	{
		get
		{
			return this.performingLayout || (this.layout != null && this.layout.IsLayoutSuspended);
		}
	}

	// Token: 0x17000B89 RID: 2953
	// (get) Token: 0x06003B75 RID: 15221 RVA: 0x000DE5FC File Offset: 0x000DC7FC
	protected bool IsPerformingLayout
	{
		get
		{
			return this.performingLayout || (this.layout != null && this.layout.IsPerformingLayout);
		}
	}

	// Token: 0x17000B8A RID: 2954
	// (get) Token: 0x06003B76 RID: 15222 RVA: 0x000DE62C File Offset: 0x000DC82C
	// (set) Token: 0x06003B77 RID: 15223 RVA: 0x000DE634 File Offset: 0x000DC834
	public object Tag
	{
		get
		{
			return this.tag;
		}
		set
		{
			this.tag = value;
		}
	}

	// Token: 0x17000B8B RID: 2955
	// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000DE640 File Offset: 0x000DC840
	internal uint Version
	{
		get
		{
			return this.version;
		}
	}

	// Token: 0x17000B8C RID: 2956
	// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000DE648 File Offset: 0x000DC848
	// (set) Token: 0x06003B7A RID: 15226 RVA: 0x000DE650 File Offset: 0x000DC850
	public bool IsLocalized
	{
		get
		{
			return this.isLocalized;
		}
		set
		{
			this.isLocalized = value;
			if (value)
			{
				this.Localize();
			}
		}
	}

	// Token: 0x17000B8D RID: 2957
	// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000DE668 File Offset: 0x000DC868
	// (set) Token: 0x06003B7C RID: 15228 RVA: 0x000DE670 File Offset: 0x000DC870
	public Vector2 HotZoneScale
	{
		get
		{
			return this.hotZoneScale;
		}
		set
		{
			this.hotZoneScale = Vector2.Max(value, Vector2.zero);
			this.Invalidate();
		}
	}

	// Token: 0x17000B8E RID: 2958
	// (get) Token: 0x06003B7D RID: 15229 RVA: 0x000DE68C File Offset: 0x000DC88C
	// (set) Token: 0x06003B7E RID: 15230 RVA: 0x000DE6A4 File Offset: 0x000DC8A4
	public virtual bool CanFocus
	{
		get
		{
			return this.canFocus && this.IsInteractive;
		}
		set
		{
			this.canFocus = value;
		}
	}

	// Token: 0x17000B8F RID: 2959
	// (get) Token: 0x06003B7F RID: 15231 RVA: 0x000DE6B0 File Offset: 0x000DC8B0
	public virtual bool ContainsFocus
	{
		get
		{
			return dfGUIManager.ContainsFocus(this);
		}
	}

	// Token: 0x17000B90 RID: 2960
	// (get) Token: 0x06003B80 RID: 15232 RVA: 0x000DE6B8 File Offset: 0x000DC8B8
	public virtual bool HasFocus
	{
		get
		{
			return dfGUIManager.HasFocus(this);
		}
	}

	// Token: 0x17000B91 RID: 2961
	// (get) Token: 0x06003B81 RID: 15233 RVA: 0x000DE6C0 File Offset: 0x000DC8C0
	public bool ContainsMouse
	{
		get
		{
			return this.isMouseHovering;
		}
	}

	// Token: 0x06003B82 RID: 15234 RVA: 0x000DE6C8 File Offset: 0x000DC8C8
	internal void setRenderOrder(ref int order)
	{
		this.renderOrder = ++order;
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].setRenderOrder(ref order);
		}
	}

	// Token: 0x17000B92 RID: 2962
	// (get) Token: 0x06003B83 RID: 15235 RVA: 0x000DE714 File Offset: 0x000DC914
	[HideInInspector]
	public int RenderOrder
	{
		get
		{
			return this.renderOrder;
		}
	}

	// Token: 0x06003B84 RID: 15236 RVA: 0x000DE71C File Offset: 0x000DC91C
	internal virtual void OnDragStart(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragStart", new object[]
			{
				args
			});
			if (!args.Used && this.DragStart != null)
			{
				this.DragStart(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragStart(args);
		}
	}

	// Token: 0x06003B85 RID: 15237 RVA: 0x000DE78C File Offset: 0x000DC98C
	internal virtual void OnDragEnd(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragEnd", new object[]
			{
				args
			});
			if (!args.Used && this.DragEnd != null)
			{
				this.DragEnd(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragEnd(args);
		}
	}

	// Token: 0x06003B86 RID: 15238 RVA: 0x000DE7FC File Offset: 0x000DC9FC
	internal virtual void OnDragDrop(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragDrop", new object[]
			{
				args
			});
			if (!args.Used && this.DragDrop != null)
			{
				this.DragDrop(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragDrop(args);
		}
	}

	// Token: 0x06003B87 RID: 15239 RVA: 0x000DE86C File Offset: 0x000DCA6C
	internal virtual void OnDragEnter(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragEnter", new object[]
			{
				args
			});
			if (!args.Used && this.DragEnter != null)
			{
				this.DragEnter(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragEnter(args);
		}
	}

	// Token: 0x06003B88 RID: 15240 RVA: 0x000DE8DC File Offset: 0x000DCADC
	internal virtual void OnDragLeave(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragLeave", new object[]
			{
				args
			});
			if (!args.Used && this.DragLeave != null)
			{
				this.DragLeave(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragLeave(args);
		}
	}

	// Token: 0x06003B89 RID: 15241 RVA: 0x000DE94C File Offset: 0x000DCB4C
	internal virtual void OnDragOver(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragOver", new object[]
			{
				args
			});
			if (!args.Used && this.DragOver != null)
			{
				this.DragOver(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragOver(args);
		}
	}

	// Token: 0x06003B8A RID: 15242 RVA: 0x000DE9BC File Offset: 0x000DCBBC
	protected internal virtual void OnMultiTouch(dfTouchEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnMultiTouch", new object[]
			{
				args
			});
			if (this.MultiTouch != null)
			{
				this.MultiTouch(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMultiTouch(args);
		}
	}

	// Token: 0x06003B8B RID: 15243 RVA: 0x000DEA20 File Offset: 0x000DCC20
	protected internal virtual void OnMouseEnter(dfMouseEventArgs args)
	{
		this.isMouseHovering = true;
		if (!args.Used)
		{
			this.Signal("OnMouseEnter", new object[]
			{
				args
			});
			if (this.MouseEnter != null)
			{
				this.MouseEnter(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseEnter(args);
		}
	}

	// Token: 0x06003B8C RID: 15244 RVA: 0x000DEA8C File Offset: 0x000DCC8C
	protected internal virtual void OnMouseLeave(dfMouseEventArgs args)
	{
		this.isMouseHovering = false;
		if (!args.Used)
		{
			this.Signal("OnMouseLeave", new object[]
			{
				args
			});
			if (this.MouseLeave != null)
			{
				this.MouseLeave(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseLeave(args);
		}
	}

	// Token: 0x06003B8D RID: 15245 RVA: 0x000DEAF8 File Offset: 0x000DCCF8
	protected internal virtual void OnMouseMove(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnMouseMove", new object[]
			{
				args
			});
			if (this.MouseMove != null)
			{
				this.MouseMove(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseMove(args);
		}
	}

	// Token: 0x06003B8E RID: 15246 RVA: 0x000DEB5C File Offset: 0x000DCD5C
	protected internal virtual void OnMouseHover(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnMouseHover", new object[]
			{
				args
			});
			if (this.MouseHover != null)
			{
				this.MouseHover(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseHover(args);
		}
	}

	// Token: 0x06003B8F RID: 15247 RVA: 0x000DEBC0 File Offset: 0x000DCDC0
	protected internal virtual void OnMouseWheel(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnMouseWheel", new object[]
			{
				args
			});
			if (this.MouseWheel != null)
			{
				this.MouseWheel(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseWheel(args);
		}
	}

	// Token: 0x06003B90 RID: 15248 RVA: 0x000DEC24 File Offset: 0x000DCE24
	protected internal virtual void OnMouseDown(dfMouseEventArgs args)
	{
		bool flag = this.Opacity > 0.01f && this.IsVisible && this.IsEnabled && this.CanFocus && !this.ContainsFocus;
		if (flag)
		{
			this.Focus();
		}
		if (!args.Used)
		{
			this.Signal("OnMouseDown", new object[]
			{
				args
			});
			if (this.MouseDown != null)
			{
				this.MouseDown(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseDown(args);
		}
	}

	// Token: 0x06003B91 RID: 15249 RVA: 0x000DECD4 File Offset: 0x000DCED4
	protected internal virtual void OnMouseUp(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnMouseUp", new object[]
			{
				args
			});
			if (this.MouseUp != null)
			{
				this.MouseUp(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseUp(args);
		}
	}

	// Token: 0x06003B92 RID: 15250 RVA: 0x000DED38 File Offset: 0x000DCF38
	protected internal virtual void OnClick(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnClick", new object[]
			{
				args
			});
			if (this.Click != null)
			{
				this.Click(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnClick(args);
		}
	}

	// Token: 0x06003B93 RID: 15251 RVA: 0x000DED9C File Offset: 0x000DCF9C
	protected internal virtual void OnDoubleClick(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDoubleClick", new object[]
			{
				args
			});
			if (this.DoubleClick != null)
			{
				this.DoubleClick(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDoubleClick(args);
		}
	}

	// Token: 0x06003B94 RID: 15252 RVA: 0x000DEE00 File Offset: 0x000DD000
	protected internal virtual void OnKeyPress(dfKeyEventArgs args)
	{
		if (this.IsInteractive && !args.Used)
		{
			this.Signal("OnKeyPress", new object[]
			{
				args
			});
			if (this.KeyPress != null)
			{
				this.KeyPress(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnKeyPress(args);
		}
	}

	// Token: 0x06003B95 RID: 15253 RVA: 0x000DEE70 File Offset: 0x000DD070
	protected internal virtual void OnKeyDown(dfKeyEventArgs args)
	{
		if (this.IsInteractive && !args.Used)
		{
			if (args.KeyCode == 9)
			{
				this.OnTabKeyPressed(args);
			}
			if (!args.Used)
			{
				this.Signal("OnKeyDown", new object[]
				{
					args
				});
				if (this.KeyDown != null)
				{
					this.KeyDown(this, args);
				}
			}
		}
		if (this.parent != null)
		{
			this.parent.OnKeyDown(args);
		}
	}

	// Token: 0x06003B96 RID: 15254 RVA: 0x000DEF00 File Offset: 0x000DD100
	protected virtual void OnTabKeyPressed(dfKeyEventArgs args)
	{
		List<dfControl> list = (from c in this.GetManager().GetComponentsInChildren<dfControl>()
		where c != this && c.TabIndex >= 0 && c.IsInteractive && c.CanFocus && c.IsVisible
		select c).ToList<dfControl>();
		if (list.Count == 0)
		{
			return;
		}
		list.Sort(delegate(dfControl lhs, dfControl rhs)
		{
			if (lhs.TabIndex == rhs.TabIndex)
			{
				return lhs.RenderOrder.CompareTo(rhs.RenderOrder);
			}
			return lhs.TabIndex.CompareTo(rhs.TabIndex);
		});
		if (!args.Shift)
		{
			for (int i = 0; i < list.Count; i++)
			{
				dfControl dfControl = list[i];
				if (dfControl.TabIndex >= this.TabIndex)
				{
					list[i].Focus();
					args.Use();
					return;
				}
			}
			list[0].Focus();
			args.Use();
			return;
		}
		for (int j = list.Count - 1; j >= 0; j--)
		{
			dfControl dfControl2 = list[j];
			if (dfControl2.TabIndex <= this.TabIndex)
			{
				list[j].Focus();
				args.Use();
				return;
			}
		}
		list[list.Count - 1].Focus();
		args.Use();
	}

	// Token: 0x06003B97 RID: 15255 RVA: 0x000DF020 File Offset: 0x000DD220
	protected internal virtual void OnKeyUp(dfKeyEventArgs args)
	{
		if (this.IsInteractive)
		{
			this.Signal("OnKeyUp", new object[]
			{
				args
			});
			if (this.KeyUp != null)
			{
				this.KeyUp(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnKeyUp(args);
		}
	}

	// Token: 0x06003B98 RID: 15256 RVA: 0x000DF084 File Offset: 0x000DD284
	protected internal virtual void OnEnterFocus(dfFocusEventArgs args)
	{
		this.Signal("OnEnterFocus", new object[]
		{
			args
		});
		if (this.EnterFocus != null)
		{
			this.EnterFocus(this, args);
		}
	}

	// Token: 0x06003B99 RID: 15257 RVA: 0x000DF0C0 File Offset: 0x000DD2C0
	protected internal virtual void OnLeaveFocus(dfFocusEventArgs args)
	{
		this.Signal("OnLeaveFocus", new object[]
		{
			args
		});
		if (this.LeaveFocus != null)
		{
			this.LeaveFocus(this, args);
		}
	}

	// Token: 0x06003B9A RID: 15258 RVA: 0x000DF0FC File Offset: 0x000DD2FC
	protected internal virtual void OnGotFocus(dfFocusEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnGotFocus", new object[]
			{
				args
			});
			if (this.GotFocus != null)
			{
				this.GotFocus(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnGotFocus(args);
		}
	}

	// Token: 0x06003B9B RID: 15259 RVA: 0x000DF160 File Offset: 0x000DD360
	protected internal virtual void OnLostFocus(dfFocusEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnLostFocus", new object[]
			{
				args
			});
			if (this.LostFocus != null)
			{
				this.LostFocus(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnLostFocus(args);
		}
	}

	// Token: 0x06003B9C RID: 15260 RVA: 0x000DF1C4 File Offset: 0x000DD3C4
	[HideInInspector]
	protected internal void RaiseEvent(string eventName, params object[] args)
	{
		FieldInfo fieldInfo = (from f in base.GetType().GetAllFields()
		where f.Name == eventName
		select f).FirstOrDefault<FieldInfo>();
		if (fieldInfo != null)
		{
			object value = fieldInfo.GetValue(this);
			if (value != null)
			{
				((Delegate)value).DynamicInvoke(args);
			}
		}
	}

	// Token: 0x06003B9D RID: 15261 RVA: 0x000DF224 File Offset: 0x000DD424
	protected internal bool Signal(string eventName, params object[] args)
	{
		return this.Signal(base.gameObject, eventName, args);
	}

	// Token: 0x06003B9E RID: 15262 RVA: 0x000DF234 File Offset: 0x000DD434
	protected internal bool SignalHierarchy(string eventName, params object[] args)
	{
		bool flag = false;
		Transform transform = base.transform;
		while (!flag && transform != null)
		{
			flag = this.Signal(transform.gameObject, eventName, args);
			transform = transform.parent;
		}
		return flag;
	}

	// Token: 0x06003B9F RID: 15263 RVA: 0x000DF278 File Offset: 0x000DD478
	[HideInInspector]
	protected internal bool Signal(GameObject target, string eventName, params object[] args)
	{
		Component[] components = target.GetComponents(typeof(MonoBehaviour));
		if (components == null || (target == base.gameObject && components.Length == 1))
		{
			return false;
		}
		if (args.Length == 0 || !object.ReferenceEquals(args[0], this))
		{
			object[] array = new object[args.Length + 1];
			Array.Copy(args, 0, array, 1, args.Length);
			array[0] = this;
			args = array;
		}
		Type[] array2 = new Type[args.Length];
		for (int i = 0; i < array2.Length; i++)
		{
			if (args[i] == null)
			{
				array2[i] = typeof(object);
			}
			else
			{
				array2[i] = args[i].GetType();
			}
		}
		bool result = false;
		foreach (Component component in components)
		{
			if (!(component == null) && component.GetType() != null)
			{
				if (!(component is MonoBehaviour) || ((MonoBehaviour)component).enabled)
				{
					if (!(component == this))
					{
						MethodInfo method = component.GetType().GetMethod(eventName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, array2, null);
						if (method != null)
						{
							IEnumerator enumerator = method.Invoke(component, args) as IEnumerator;
							if (enumerator != null)
							{
								((MonoBehaviour)component).StartCoroutine(enumerator);
							}
							result = true;
						}
						else if (args.Length != 0)
						{
							MethodInfo method2 = component.GetType().GetMethod(eventName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
							if (method2 != null)
							{
								IEnumerator enumerator = method2.Invoke(component, null) as IEnumerator;
								if (enumerator != null)
								{
									((MonoBehaviour)component).StartCoroutine(enumerator);
								}
								result = true;
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06003BA0 RID: 15264 RVA: 0x000DF448 File Offset: 0x000DD648
	internal bool GetIsVisibleRaw()
	{
		return this.isVisible;
	}

	// Token: 0x06003BA1 RID: 15265 RVA: 0x000DF450 File Offset: 0x000DD650
	public void Localize()
	{
		if (!this.IsLocalized)
		{
			return;
		}
		if (this.languageManager == null)
		{
			this.languageManager = this.GetManager().GetComponent<dfLanguageManager>();
			if (this.languageManager == null)
			{
				return;
			}
		}
		this.OnLocalize();
	}

	// Token: 0x06003BA2 RID: 15266 RVA: 0x000DF4A4 File Offset: 0x000DD6A4
	public void DoClick()
	{
		Camera camera = this.GetCamera();
		Vector3 vector = camera.WorldToScreenPoint(this.GetCenter());
		Ray ray = camera.ScreenPointToRay(vector);
		this.OnClick(new dfMouseEventArgs(this, dfMouseButtons.Left, 1, ray, vector, 0f));
	}

	// Token: 0x06003BA3 RID: 15267 RVA: 0x000DF4E8 File Offset: 0x000DD6E8
	[HideInInspector]
	protected internal void RemoveEventHandlers(string EventName)
	{
		FieldInfo fieldInfo = (from f in base.GetType().GetAllFields()
		where typeof(Delegate).IsAssignableFrom(f.FieldType) && f.Name == EventName
		select f).FirstOrDefault<FieldInfo>();
		if (fieldInfo != null)
		{
			fieldInfo.SetValue(this, null);
		}
	}

	// Token: 0x06003BA4 RID: 15268 RVA: 0x000DF534 File Offset: 0x000DD734
	[HideInInspector]
	internal void RemoveAllEventHandlers()
	{
		FieldInfo[] array = (from f in base.GetType().GetAllFields()
		where typeof(Delegate).IsAssignableFrom(f.FieldType)
		select f).ToArray<FieldInfo>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetValue(this, null);
		}
	}

	// Token: 0x06003BA5 RID: 15269 RVA: 0x000DF594 File Offset: 0x000DD794
	public void Show()
	{
		this.IsVisible = true;
	}

	// Token: 0x06003BA6 RID: 15270 RVA: 0x000DF5A0 File Offset: 0x000DD7A0
	public void Hide()
	{
		this.IsVisible = false;
	}

	// Token: 0x06003BA7 RID: 15271 RVA: 0x000DF5AC File Offset: 0x000DD7AC
	public void Enable()
	{
		this.IsEnabled = true;
	}

	// Token: 0x06003BA8 RID: 15272 RVA: 0x000DF5B8 File Offset: 0x000DD7B8
	public void Disable()
	{
		this.IsEnabled = false;
	}

	// Token: 0x06003BA9 RID: 15273 RVA: 0x000DF5C4 File Offset: 0x000DD7C4
	public bool GetHitPosition(Ray ray, out Vector2 position)
	{
		position = Vector2.one * float.MinValue;
		Plane plane;
		plane..ctor(base.transform.TransformDirection(Vector3.back), base.transform.position);
		float num = 0f;
		if (!plane.Raycast(ray, ref num))
		{
			return false;
		}
		Vector3 vector = ray.origin + ray.direction * num;
		Plane[] array = (!this.ClipChildren) ? null : this.GetClippingPlanes();
		if (array != null && array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].GetSide(vector))
				{
					return false;
				}
			}
		}
		Vector3[] corners = this.GetCorners();
		Vector3 vector2 = corners[0];
		Vector3 vector3 = corners[1];
		Vector3 vector4 = corners[2];
		Vector3 vector5 = dfControl.closestPointOnLine(vector2, vector3, vector, true);
		float num2 = (vector5 - vector2).magnitude / (vector3 - vector2).magnitude;
		float num3 = this.size.x * num2;
		vector5 = dfControl.closestPointOnLine(vector2, vector4, vector, true);
		num2 = (vector5 - vector2).magnitude / (vector4 - vector2).magnitude;
		float num4 = this.size.y * num2;
		position..ctor(num3, num4);
		return true;
	}

	// Token: 0x06003BAA RID: 15274 RVA: 0x000DF758 File Offset: 0x000DD958
	public T Find<T>(string Name) where T : dfControl
	{
		if (base.name == Name && this is T)
		{
			return (T)((object)this);
		}
		this.updateControlHierarchy(true);
		for (int i = 0; i < this.controls.Count; i++)
		{
			T t = this.controls[i] as T;
			if (t != null && t.name == Name)
			{
				return t;
			}
		}
		for (int j = 0; j < this.controls.Count; j++)
		{
			T t2 = this.controls[j].Find<T>(Name);
			if (t2 != null)
			{
				return t2;
			}
		}
		return (T)((object)null);
	}

	// Token: 0x06003BAB RID: 15275 RVA: 0x000DF838 File Offset: 0x000DDA38
	public dfControl Find(string Name)
	{
		if (base.name == Name)
		{
			return this;
		}
		this.updateControlHierarchy(true);
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			if (dfControl.name == Name)
			{
				return dfControl;
			}
		}
		for (int j = 0; j < this.controls.Count; j++)
		{
			dfControl dfControl2 = this.controls[j].Find(Name);
			if (dfControl2 != null)
			{
				return dfControl2;
			}
		}
		return null;
	}

	// Token: 0x06003BAC RID: 15276 RVA: 0x000DF8DC File Offset: 0x000DDADC
	public void Focus()
	{
		if (!this.CanFocus || this.HasFocus || !this.IsEnabled || !this.IsVisible)
		{
			return;
		}
		dfGUIManager.SetFocus(this);
		this.Invalidate();
	}

	// Token: 0x06003BAD RID: 15277 RVA: 0x000DF924 File Offset: 0x000DDB24
	public void Unfocus()
	{
		if (this.ContainsFocus)
		{
			dfGUIManager.SetFocus(null);
		}
	}

	// Token: 0x06003BAE RID: 15278 RVA: 0x000DF938 File Offset: 0x000DDB38
	public dfControl GetRootContainer()
	{
		dfControl dfControl = this;
		while (dfControl.Parent != null)
		{
			dfControl = dfControl.Parent;
		}
		return dfControl;
	}

	// Token: 0x06003BAF RID: 15279 RVA: 0x000DF968 File Offset: 0x000DDB68
	public virtual void BringToFront()
	{
		if (this.parent == null)
		{
			this.GetManager().BringToFront(this);
		}
		else
		{
			this.parent.SetControlIndex(this, this.parent.controls.Count - 1);
		}
		this.Invalidate();
	}

	// Token: 0x06003BB0 RID: 15280 RVA: 0x000DF9BC File Offset: 0x000DDBBC
	public virtual void SendToBack()
	{
		if (this.parent == null)
		{
			this.GetManager().SendToBack(this);
		}
		else
		{
			this.parent.SetControlIndex(this, 0);
		}
		this.Invalidate();
	}

	// Token: 0x06003BB1 RID: 15281 RVA: 0x000DFA00 File Offset: 0x000DDC00
	internal dfRenderData Render()
	{
		if (this.rendering)
		{
			return this.renderData;
		}
		dfRenderData result;
		try
		{
			this.rendering = true;
			bool flag = this.isVisible;
			bool flag2 = base.enabled && base.gameObject.activeSelf;
			if (!flag || !flag2)
			{
				result = null;
			}
			else
			{
				if (this.renderData == null)
				{
					this.renderData = dfRenderData.Obtain();
					this.isControlInvalidated = true;
				}
				if (this.isControlInvalidated)
				{
					this.renderData.Clear();
					this.OnRebuildRenderData();
					this.updateCollider();
				}
				this.renderData.Transform = base.transform.localToWorldMatrix;
				result = this.renderData;
			}
		}
		finally
		{
			this.rendering = false;
			this.isControlInvalidated = false;
		}
		return result;
	}

	// Token: 0x06003BB2 RID: 15282 RVA: 0x000DFAEC File Offset: 0x000DDCEC
	public virtual void Invalidate()
	{
		this.updateVersion();
		this.isControlInvalidated = true;
		dfGUIManager dfGUIManager = this.GetManager();
		if (dfGUIManager != null)
		{
			dfGUIManager.Invalidate();
		}
	}

	// Token: 0x06003BB3 RID: 15283 RVA: 0x000DFB20 File Offset: 0x000DDD20
	[HideInInspector]
	public virtual void ResetLayout(bool recursive = false, bool force = false)
	{
		bool flag = this.IsPerformingLayout || this.IsLayoutSuspended;
		if (!force && flag)
		{
			return;
		}
		this.ensureLayoutExists();
		this.layout.Attach(this);
		this.layout.Reset(force);
		if (recursive)
		{
			for (int i = 0; i < this.Controls.Count; i++)
			{
				this.controls[i].ResetLayout(false, false);
			}
		}
	}

	// Token: 0x06003BB4 RID: 15284 RVA: 0x000DFBA4 File Offset: 0x000DDDA4
	[HideInInspector]
	public virtual void PerformLayout()
	{
		if (this.isDisposing || this.performingLayout)
		{
			return;
		}
		try
		{
			this.performingLayout = true;
			this.ensureLayoutExists();
			this.layout.PerformLayout();
			this.Invalidate();
		}
		finally
		{
			this.performingLayout = false;
		}
	}

	// Token: 0x06003BB5 RID: 15285 RVA: 0x000DFC10 File Offset: 0x000DDE10
	[HideInInspector]
	public virtual void SuspendLayout()
	{
		this.ensureLayoutExists();
		this.layout.SuspendLayout();
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].SuspendLayout();
		}
	}

	// Token: 0x06003BB6 RID: 15286 RVA: 0x000DFC5C File Offset: 0x000DDE5C
	[HideInInspector]
	public virtual void ResumeLayout()
	{
		this.ensureLayoutExists();
		this.layout.ResumeLayout();
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].ResumeLayout();
		}
	}

	// Token: 0x06003BB7 RID: 15287 RVA: 0x000DFCA8 File Offset: 0x000DDEA8
	public virtual Vector2 CalculateMinimumSize()
	{
		return this.MinimumSize;
	}

	// Token: 0x06003BB8 RID: 15288 RVA: 0x000DFCB0 File Offset: 0x000DDEB0
	[HideInInspector]
	public void MakePixelPerfect(bool recursive = true)
	{
		this.size = this.size.RoundToInt();
		float num = this.PixelsToUnits();
		base.transform.position = (base.transform.position / num).RoundToInt() * num;
		this.cachedPosition = base.transform.localPosition;
		int num2 = 0;
		while (num2 < this.controls.Count && recursive)
		{
			this.controls[num2].MakePixelPerfect(true);
			num2++;
		}
		this.Invalidate();
	}

	// Token: 0x06003BB9 RID: 15289 RVA: 0x000DFD48 File Offset: 0x000DDF48
	public Bounds GetBounds()
	{
		Vector3[] corners = this.GetCorners();
		Vector3 vector = corners[0] + (corners[3] - corners[0]) * 0.5f;
		Vector3 vector2 = vector;
		Vector3 vector3 = vector;
		for (int i = 0; i < corners.Length; i++)
		{
			vector2 = Vector3.Min(vector2, corners[i]);
			vector3 = Vector3.Max(vector3, corners[i]);
		}
		return new Bounds(vector, vector3 - vector2);
	}

	// Token: 0x06003BBA RID: 15290 RVA: 0x000DFDE8 File Offset: 0x000DDFE8
	public Vector3 GetCenter()
	{
		return base.transform.position + this.Pivot.TransformToCenter(this.Size) * this.PixelsToUnits();
	}

	// Token: 0x06003BBB RID: 15291 RVA: 0x000DFE24 File Offset: 0x000DE024
	public Vector3[] GetCorners()
	{
		float num = this.PixelsToUnits();
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		Vector3 vector = this.pivot.TransformToUpperLeft(this.size);
		Vector3 vector2 = vector + new Vector3(this.size.x, 0f);
		Vector3 vector3 = vector + new Vector3(0f, -this.size.y);
		Vector3 vector4 = vector2 + new Vector3(0f, -this.size.y);
		this.cachedCorners[0] = localToWorldMatrix.MultiplyPoint(vector * num);
		this.cachedCorners[1] = localToWorldMatrix.MultiplyPoint(vector2 * num);
		this.cachedCorners[2] = localToWorldMatrix.MultiplyPoint(vector3 * num);
		this.cachedCorners[3] = localToWorldMatrix.MultiplyPoint(vector4 * num);
		return this.cachedCorners;
	}

	// Token: 0x06003BBC RID: 15292 RVA: 0x000DFF34 File Offset: 0x000DE134
	public Camera GetCamera()
	{
		dfGUIManager dfGUIManager = this.GetManager();
		if (dfGUIManager == null)
		{
			Debug.LogError("The Manager hosting this control could not be determined");
			return null;
		}
		return dfGUIManager.RenderCamera;
	}

	// Token: 0x06003BBD RID: 15293 RVA: 0x000DFF68 File Offset: 0x000DE168
	public Rect GetScreenRect()
	{
		Camera camera = this.GetCamera();
		Vector3[] corners = this.GetCorners();
		Vector3 vector = camera.WorldToScreenPoint(corners[0]);
		Vector3 vector2 = camera.WorldToScreenPoint(corners[3]);
		return new Rect(vector.x, (float)Screen.height - vector.y, vector2.x - vector.x, vector.y - vector2.y);
	}

	// Token: 0x06003BBE RID: 15294 RVA: 0x000DFFE4 File Offset: 0x000DE1E4
	public dfGUIManager GetManager()
	{
		if (this.manager != null || !base.gameObject.activeInHierarchy)
		{
			return this.manager;
		}
		if (this.parent != null && this.parent.manager != null)
		{
			return this.manager = this.parent.manager;
		}
		GameObject gameObject = base.gameObject;
		while (gameObject != null)
		{
			dfGUIManager component = gameObject.GetComponent<dfGUIManager>();
			if (component != null)
			{
				return this.manager = component;
			}
			if (gameObject.transform.parent == null)
			{
				break;
			}
			gameObject = gameObject.transform.parent.gameObject;
		}
		dfGUIManager dfGUIManager = Object.FindObjectsOfType(typeof(dfGUIManager)).FirstOrDefault<Object>() as dfGUIManager;
		if (dfGUIManager != null)
		{
			return this.manager = dfGUIManager;
		}
		return null;
	}

	// Token: 0x06003BBF RID: 15295 RVA: 0x000E00EC File Offset: 0x000DE2EC
	protected internal float PixelsToUnits()
	{
		if (this.cachedPixelSize > 1.401298E-45f)
		{
			return this.cachedPixelSize;
		}
		dfGUIManager dfGUIManager = this.GetManager();
		if (dfGUIManager == null)
		{
			return 0.0026f;
		}
		return this.cachedPixelSize = dfGUIManager.PixelsToUnits();
	}

	// Token: 0x06003BC0 RID: 15296 RVA: 0x000E0138 File Offset: 0x000DE338
	protected internal virtual Plane[] GetClippingPlanes()
	{
		Vector3[] corners = this.GetCorners();
		Vector3 vector = base.transform.TransformDirection(Vector3.right);
		Vector3 vector2 = base.transform.TransformDirection(Vector3.left);
		Vector3 vector3 = base.transform.TransformDirection(Vector3.up);
		Vector3 vector4 = base.transform.TransformDirection(Vector3.down);
		this.cachedClippingPlanes[0] = new Plane(vector, corners[0]);
		this.cachedClippingPlanes[1] = new Plane(vector2, corners[1]);
		this.cachedClippingPlanes[2] = new Plane(vector3, corners[2]);
		this.cachedClippingPlanes[3] = new Plane(vector4, corners[0]);
		return this.cachedClippingPlanes;
	}

	// Token: 0x06003BC1 RID: 15297 RVA: 0x000E0224 File Offset: 0x000DE424
	public bool Contains(dfControl child)
	{
		return child != null && child.transform.IsChildOf(base.transform);
	}

	// Token: 0x06003BC2 RID: 15298 RVA: 0x000E0254 File Offset: 0x000DE454
	[HideInInspector]
	protected internal virtual void OnLocalize()
	{
	}

	// Token: 0x06003BC3 RID: 15299 RVA: 0x000E0258 File Offset: 0x000DE458
	[HideInInspector]
	protected internal string getLocalizedValue(string key)
	{
		if (!this.IsLocalized || !Application.isPlaying)
		{
			return key;
		}
		if (this.languageManager == null)
		{
			if (this.languageManagerChecked)
			{
				return key;
			}
			this.languageManagerChecked = true;
			this.languageManager = this.GetManager().GetComponent<dfLanguageManager>();
			if (this.languageManager == null)
			{
				return key;
			}
		}
		return this.languageManager.GetValue(key);
	}

	// Token: 0x06003BC4 RID: 15300 RVA: 0x000E02D4 File Offset: 0x000DE4D4
	[HideInInspector]
	protected internal virtual void updateCollider()
	{
		if (Application.isPlaying && !this.isInteractive)
		{
			return;
		}
		BoxCollider boxCollider = base.collider as BoxCollider;
		if (boxCollider == null)
		{
			boxCollider = base.gameObject.AddComponent<BoxCollider>();
		}
		float num = this.PixelsToUnits();
		Vector2 vector = this.size * num;
		Vector3 center = this.pivot.TransformToCenter(vector);
		boxCollider.size = new Vector3(vector.x * this.hotZoneScale.x, vector.y * this.hotZoneScale.y, 0.001f);
		boxCollider.center = center;
		if (Application.isPlaying && !this.IsInteractive)
		{
			boxCollider.enabled = false;
		}
		else
		{
			boxCollider.enabled = (base.enabled && this.IsVisible);
		}
	}

	// Token: 0x06003BC5 RID: 15301 RVA: 0x000E03B4 File Offset: 0x000DE5B4
	[HideInInspector]
	protected virtual void OnRebuildRenderData()
	{
	}

	// Token: 0x06003BC6 RID: 15302 RVA: 0x000E03B8 File Offset: 0x000DE5B8
	[HideInInspector]
	protected internal virtual void OnControlAdded(dfControl child)
	{
		this.Invalidate();
		if (this.ControlAdded != null)
		{
			this.ControlAdded(this, child);
		}
		this.Signal("OnControlAdded", new object[]
		{
			this,
			child
		});
	}

	// Token: 0x06003BC7 RID: 15303 RVA: 0x000E0400 File Offset: 0x000DE600
	[HideInInspector]
	protected internal virtual void OnControlRemoved(dfControl child)
	{
		this.Invalidate();
		if (this.ControlRemoved != null)
		{
			this.ControlRemoved(this, child);
		}
		this.Signal("OnControlRemoved", new object[]
		{
			this,
			child
		});
	}

	// Token: 0x06003BC8 RID: 15304 RVA: 0x000E0448 File Offset: 0x000DE648
	[HideInInspector]
	protected internal virtual void OnPositionChanged()
	{
		base.transform.hasChanged = false;
		if (this.renderData != null)
		{
			this.updateVersion();
			this.GetManager().Invalidate();
		}
		else
		{
			this.Invalidate();
		}
		this.ResetLayout(false, false);
		if (this.PositionChanged != null)
		{
			this.PositionChanged(this, this.Position);
		}
	}

	// Token: 0x06003BC9 RID: 15305 RVA: 0x000E04B4 File Offset: 0x000DE6B4
	[HideInInspector]
	protected internal virtual void OnSizeChanged()
	{
		this.updateCollider();
		this.Invalidate();
		this.ResetLayout(false, false);
		if (this.Anchor.IsAnyFlagSet(dfAnchorStyle.CenterHorizontal | dfAnchorStyle.CenterVertical))
		{
			this.PerformLayout();
		}
		if (this.SizeChanged != null)
		{
			this.SizeChanged(this, this.Size);
		}
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].PerformLayout();
		}
	}

	// Token: 0x06003BCA RID: 15306 RVA: 0x000E053C File Offset: 0x000DE73C
	[HideInInspector]
	protected internal virtual void OnPivotChanged()
	{
		this.Invalidate();
		if (this.Anchor.IsAnyFlagSet(dfAnchorStyle.CenterHorizontal | dfAnchorStyle.CenterVertical))
		{
			this.ResetLayout(false, false);
			this.PerformLayout();
		}
		if (this.PivotChanged != null)
		{
			this.PivotChanged(this, this.pivot);
		}
	}

	// Token: 0x06003BCB RID: 15307 RVA: 0x000E0590 File Offset: 0x000DE790
	[HideInInspector]
	protected internal virtual void OnAnchorChanged()
	{
		dfAnchorStyle anchorStyle = this.layout.AnchorStyle;
		this.Invalidate();
		this.ResetLayout(false, false);
		if (anchorStyle.IsAnyFlagSet(dfAnchorStyle.CenterHorizontal | dfAnchorStyle.CenterVertical))
		{
			this.PerformLayout();
		}
		if (this.AnchorChanged != null)
		{
			this.AnchorChanged(this, anchorStyle);
		}
	}

	// Token: 0x06003BCC RID: 15308 RVA: 0x000E05E8 File Offset: 0x000DE7E8
	[HideInInspector]
	protected internal virtual void OnOpacityChanged()
	{
		this.Invalidate();
		if (this.OpacityChanged != null)
		{
			this.OpacityChanged(this, this.Opacity);
		}
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].OnOpacityChanged();
		}
	}

	// Token: 0x06003BCD RID: 15309 RVA: 0x000E0648 File Offset: 0x000DE848
	[HideInInspector]
	protected internal virtual void OnColorChanged()
	{
		this.Invalidate();
		if (this.ColorChanged != null)
		{
			this.ColorChanged(this, this.Color);
		}
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].OnColorChanged();
		}
	}

	// Token: 0x06003BCE RID: 15310 RVA: 0x000E06A8 File Offset: 0x000DE8A8
	[HideInInspector]
	protected internal virtual void OnZOrderChanged()
	{
		this.Invalidate();
		if (this.ZOrderChanged != null)
		{
			this.ZOrderChanged(this, this.zindex);
		}
	}

	// Token: 0x06003BCF RID: 15311 RVA: 0x000E06D0 File Offset: 0x000DE8D0
	[HideInInspector]
	protected internal virtual void OnTabIndexChanged()
	{
		this.Invalidate();
		if (this.TabIndexChanged != null)
		{
			this.TabIndexChanged(this, this.tabIndex);
		}
	}

	// Token: 0x06003BD0 RID: 15312 RVA: 0x000E06F8 File Offset: 0x000DE8F8
	[HideInInspector]
	protected internal virtual void OnIsVisibleChanged()
	{
		if (this.HasFocus && !this.IsVisible)
		{
			dfGUIManager.SetFocus(null);
		}
		this.Invalidate();
		this.Signal("OnIsVisibleChanged", new object[]
		{
			this,
			this.IsVisible
		});
		if (this.IsVisibleChanged != null)
		{
			this.IsVisibleChanged(this, this.isVisible);
		}
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].OnIsVisibleChanged();
		}
	}

	// Token: 0x06003BD1 RID: 15313 RVA: 0x000E0798 File Offset: 0x000DE998
	[HideInInspector]
	protected internal virtual void OnIsEnabledChanged()
	{
		if (dfGUIManager.ContainsFocus(this) && !this.IsEnabled)
		{
			dfGUIManager.SetFocus(null);
		}
		this.Invalidate();
		this.Signal("OnIsEnabledChanged", new object[]
		{
			this,
			this.IsEnabled
		});
		if (this.IsEnabledChanged != null)
		{
			this.IsEnabledChanged(this, this.isEnabled);
		}
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].OnIsEnabledChanged();
		}
	}

	// Token: 0x06003BD2 RID: 15314 RVA: 0x000E0838 File Offset: 0x000DEA38
	protected internal float CalculateOpacity()
	{
		if (this.parent == null)
		{
			return this.Opacity;
		}
		return this.Opacity * this.parent.CalculateOpacity();
	}

	// Token: 0x06003BD3 RID: 15315 RVA: 0x000E0870 File Offset: 0x000DEA70
	protected internal Color32 ApplyOpacity(Color32 color)
	{
		float num = this.CalculateOpacity();
		color.a = (byte)(num * 255f);
		return color;
	}

	// Token: 0x06003BD4 RID: 15316 RVA: 0x000E0894 File Offset: 0x000DEA94
	protected internal Vector2 GetHitPosition(dfMouseEventArgs args)
	{
		Vector2 result;
		this.GetHitPosition(args.Ray, out result);
		return result;
	}

	// Token: 0x06003BD5 RID: 15317 RVA: 0x000E08B4 File Offset: 0x000DEAB4
	protected internal Vector3 getScaledDirection(Vector3 direction)
	{
		Vector3 localScale = this.GetManager().transform.localScale;
		direction = base.transform.TransformDirection(direction);
		return Vector3.Scale(direction, localScale);
	}

	// Token: 0x06003BD6 RID: 15318 RVA: 0x000E08E8 File Offset: 0x000DEAE8
	protected internal Vector3 transformOffset(Vector3 offset)
	{
		Vector3 vector = offset.x * this.getScaledDirection(Vector3.right);
		Vector3 vector2 = offset.y * this.getScaledDirection(Vector3.down);
		return (vector + vector2) * this.PixelsToUnits();
	}

	// Token: 0x06003BD7 RID: 15319 RVA: 0x000E0938 File Offset: 0x000DEB38
	protected internal virtual void OnResolutionChanged(Vector2 previousResolution, Vector2 currentResolution)
	{
		this.Invalidate();
		this.updateControlHierarchy(false);
		this.cachedPixelSize = 0f;
		Vector3 vector = base.transform.localPosition / (2f / previousResolution.y);
		Vector3 localPosition = vector * (2f / currentResolution.y);
		base.transform.localPosition = localPosition;
		this.cachedPosition = localPosition;
		this.layout.Attach(this);
		this.updateCollider();
		this.Signal("OnResolutionChanged", new object[]
		{
			this,
			previousResolution,
			currentResolution
		});
	}

	// Token: 0x06003BD8 RID: 15320 RVA: 0x000E09DC File Offset: 0x000DEBDC
	[HideInInspector]
	public virtual void Awake()
	{
		if (base.transform.parent != null)
		{
			dfControl component = base.transform.parent.GetComponent<dfControl>();
			if (component != null)
			{
				this.parent = component;
				component.AddControl(this);
			}
			if (this.controls == null)
			{
				this.updateControlHierarchy(false);
			}
			if (!Application.isPlaying)
			{
				this.PerformLayout();
			}
		}
	}

	// Token: 0x06003BD9 RID: 15321 RVA: 0x000E0A4C File Offset: 0x000DEC4C
	[HideInInspector]
	public virtual void Start()
	{
	}

	// Token: 0x06003BDA RID: 15322 RVA: 0x000E0A50 File Offset: 0x000DEC50
	[HideInInspector]
	public virtual void OnEnable()
	{
		if (Application.isPlaying)
		{
			base.collider.enabled = this.IsInteractive;
		}
		this.initializeControl();
		if (this.controls == null || this.controls.Count == 0)
		{
			this.updateControlHierarchy(false);
		}
		if (Application.isPlaying && this.IsLocalized)
		{
			this.Localize();
		}
		this.OnIsEnabledChanged();
	}

	// Token: 0x06003BDB RID: 15323 RVA: 0x000E0AC4 File Offset: 0x000DECC4
	[HideInInspector]
	public virtual void OnApplicationQuit()
	{
		this.RemoveAllEventHandlers();
	}

	// Token: 0x06003BDC RID: 15324 RVA: 0x000E0ACC File Offset: 0x000DECCC
	[HideInInspector]
	public virtual void OnDisable()
	{
		try
		{
			this.Invalidate();
			if (this.renderData != null)
			{
				this.renderData.Release();
				this.renderData = null;
			}
			if (dfGUIManager.HasFocus(this))
			{
				dfGUIManager.SetFocus(null);
			}
			this.OnIsEnabledChanged();
		}
		catch
		{
		}
	}

	// Token: 0x06003BDD RID: 15325 RVA: 0x000E0B3C File Offset: 0x000DED3C
	[HideInInspector]
	public virtual void OnDestroy()
	{
		this.isDisposing = true;
		if (Application.isPlaying)
		{
			this.RemoveAllEventHandlers();
		}
		if (this.layout != null)
		{
			this.layout.Dispose();
		}
		if (this.parent != null && this.parent.controls != null && !this.parent.isDisposing && this.parent.controls.Remove(this))
		{
			this.parent.cachedChildCount--;
			this.parent.OnControlRemoved(this);
		}
		for (int i = 0; i < this.controls.Count; i++)
		{
			if (this.controls[i].layout != null)
			{
				this.controls[i].layout.Dispose();
				this.controls[i].layout = null;
			}
			this.controls[i].parent = null;
		}
		this.controls.Release();
		if (this.manager != null)
		{
			this.manager.Invalidate();
		}
		if (this.renderData != null)
		{
			this.renderData.Release();
		}
		this.layout = null;
		this.manager = null;
		this.parent = null;
		this.cachedClippingPlanes = null;
		this.cachedCorners = null;
		this.renderData = null;
		this.controls = null;
	}

	// Token: 0x06003BDE RID: 15326 RVA: 0x000E0CB8 File Offset: 0x000DEEB8
	[HideInInspector]
	public virtual void LateUpdate()
	{
		if (this.layout != null && this.layout.HasPendingLayoutRequest)
		{
			this.layout.PerformLayout();
		}
	}

	// Token: 0x06003BDF RID: 15327 RVA: 0x000E0CEC File Offset: 0x000DEEEC
	[HideInInspector]
	public virtual void Update()
	{
		Transform transform = base.transform;
		this.updateControlHierarchy(false);
		if (transform.hasChanged)
		{
			if (Application.isPlaying)
			{
				if (this.cachedScale != transform.localScale)
				{
					this.cachedScale = transform.localScale;
					this.Invalidate();
				}
			}
			if ((this.cachedPosition - transform.localPosition).sqrMagnitude > 1.401298E-45f)
			{
				this.cachedPosition = transform.localPosition;
				this.OnPositionChanged();
			}
			if (this.cachedRotation != transform.localRotation)
			{
				this.cachedRotation = transform.localRotation;
				this.Invalidate();
			}
			transform.hasChanged = false;
		}
	}

	// Token: 0x06003BE0 RID: 15328 RVA: 0x000E0DB0 File Offset: 0x000DEFB0
	protected internal void SetControlIndex(dfControl child, int zindex)
	{
		dfControl dfControl = this.controls.FirstOrDefault((dfControl c) => c.zindex == zindex && c != child);
		if (dfControl != null)
		{
			dfControl.zindex = this.controls.IndexOf(child);
		}
		child.zindex = zindex;
		this.RebuildControlOrder();
	}

	// Token: 0x06003BE1 RID: 15329 RVA: 0x000E0E24 File Offset: 0x000DF024
	public T AddControl<T>() where T : dfControl
	{
		return (T)((object)this.AddControl(typeof(T)));
	}

	// Token: 0x06003BE2 RID: 15330 RVA: 0x000E0E3C File Offset: 0x000DF03C
	public dfControl AddControl(Type ControlType)
	{
		if (!typeof(dfControl).IsAssignableFrom(ControlType))
		{
			throw new InvalidCastException();
		}
		GameObject gameObject = new GameObject(ControlType.Name);
		gameObject.transform.parent = base.transform;
		gameObject.layer = base.gameObject.layer;
		Vector2 vector = this.Size * this.PixelsToUnits() * 0.5f;
		gameObject.transform.localPosition = new Vector3(vector.x, vector.y, 0f);
		dfControl dfControl = gameObject.AddComponent(ControlType) as dfControl;
		dfControl.parent = this;
		dfControl.zindex = -1;
		this.AddControl(dfControl);
		return dfControl;
	}

	// Token: 0x06003BE3 RID: 15331 RVA: 0x000E0EF4 File Offset: 0x000DF0F4
	public void AddControl(dfControl child)
	{
		if (child.transform == null)
		{
			throw new NullReferenceException("The child control does not have a Transform");
		}
		if (!this.controls.Contains(child))
		{
			this.controls.Add(child);
			child.parent = this;
			child.transform.parent = base.transform;
		}
		if (child.zindex == -1)
		{
			child.zindex = this.getMaxZOrder() + 1;
		}
		this.controls.Sort();
		this.OnControlAdded(child);
		child.Invalidate();
		this.Invalidate();
	}

	// Token: 0x06003BE4 RID: 15332 RVA: 0x000E0F8C File Offset: 0x000DF18C
	private int getMaxZOrder()
	{
		int num = -1;
		for (int i = 0; i < this.controls.Count; i++)
		{
			num = Mathf.Max(this.controls[i].zindex, num);
		}
		return num;
	}

	// Token: 0x06003BE5 RID: 15333 RVA: 0x000E0FD0 File Offset: 0x000DF1D0
	public void RemoveControl(dfControl child)
	{
		if (this.isDisposing)
		{
			return;
		}
		if (child.Parent == this)
		{
			child.parent = null;
		}
		if (this.controls.Remove(child))
		{
			this.OnControlRemoved(child);
			child.Invalidate();
			this.Invalidate();
		}
	}

	// Token: 0x06003BE6 RID: 15334 RVA: 0x000E1028 File Offset: 0x000DF228
	[HideInInspector]
	public void RebuildControlOrder()
	{
		bool flag = false;
		this.controls.Sort();
		for (int i = 0; i < this.controls.Count; i++)
		{
			if (this.controls[i].ZOrder != i)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return;
		}
		this.controls.Sort();
		for (int j = 0; j < this.controls.Count; j++)
		{
			this.controls[j].zindex = j;
		}
	}

	// Token: 0x06003BE7 RID: 15335 RVA: 0x000E10BC File Offset: 0x000DF2BC
	internal void updateControlHierarchy(bool force = false)
	{
		int childCount = base.transform.childCount;
		if (!force && childCount == this.cachedChildCount)
		{
			return;
		}
		this.cachedChildCount = childCount;
		dfList<dfControl> childControls = this.getChildControls();
		for (int i = 0; i < childControls.Count; i++)
		{
			dfControl dfControl = childControls[i];
			if (!this.controls.Contains(dfControl))
			{
				dfControl.parent = this;
				if (!Application.isPlaying)
				{
					dfControl.ResetLayout(false, false);
				}
				this.OnControlAdded(dfControl);
				dfControl.updateControlHierarchy(false);
			}
		}
		for (int j = 0; j < this.controls.Count; j++)
		{
			dfControl dfControl2 = this.controls[j];
			if (dfControl2 == null || !childControls.Contains(dfControl2))
			{
				this.OnControlRemoved(dfControl2);
				if (dfControl2 != null && dfControl2.parent == this)
				{
					dfControl2.parent = null;
				}
			}
		}
		this.controls.Release();
		this.controls = childControls;
		this.RebuildControlOrder();
	}

	// Token: 0x06003BE8 RID: 15336 RVA: 0x000E11E4 File Offset: 0x000DF3E4
	private dfList<dfControl> getChildControls()
	{
		int childCount = base.transform.childCount;
		dfList<dfControl> dfList = dfList<dfControl>.Obtain();
		dfList.EnsureCapacity(childCount);
		for (int i = 0; i < childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			if (child.gameObject.activeSelf)
			{
				dfControl component = child.GetComponent<dfControl>();
				if (component != null)
				{
					dfList.Add(component);
				}
			}
		}
		return dfList;
	}

	// Token: 0x06003BE9 RID: 15337 RVA: 0x000E125C File Offset: 0x000DF45C
	private void ensureLayoutExists()
	{
		if (this.layout == null)
		{
			this.layout = new dfControl.AnchorLayout(dfAnchorStyle.Top | dfAnchorStyle.Left, this);
		}
		else
		{
			this.layout.Attach(this);
		}
		int num = 0;
		while (this.Controls != null && num < this.Controls.Count)
		{
			if (this.controls[num] != null)
			{
				this.controls[num].ensureLayoutExists();
			}
			num++;
		}
	}

	// Token: 0x06003BEA RID: 15338 RVA: 0x000E12E4 File Offset: 0x000DF4E4
	protected internal void updateVersion()
	{
		this.version = (dfControl.versionCounter += 1u);
	}

	// Token: 0x06003BEB RID: 15339 RVA: 0x000E12FC File Offset: 0x000DF4FC
	private void setPositionInternal(Vector3 value)
	{
		value += this.pivot.UpperLeftToTransform(this.Size);
		value *= this.PixelsToUnits();
		if ((value - this.cachedPosition).sqrMagnitude <= 1.401298E-45f)
		{
			return;
		}
		Vector3 localPosition = value;
		base.transform.localPosition = localPosition;
		this.cachedPosition = localPosition;
		this.OnPositionChanged();
	}

	// Token: 0x06003BEC RID: 15340 RVA: 0x000E136C File Offset: 0x000DF56C
	private void initializeControl()
	{
		if (this.renderOrder == -1)
		{
			this.renderOrder = this.ZOrder;
		}
		if (base.transform.parent != null)
		{
			dfControl component = base.transform.parent.GetComponent<dfControl>();
			if (component != null)
			{
				component.AddControl(this);
			}
		}
		this.ensureLayoutExists();
		this.Invalidate();
		base.collider.isTrigger = false;
		if (Application.isPlaying && base.rigidbody == null)
		{
			Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
			rigidbody.hideFlags = 15;
			rigidbody.isKinematic = true;
			rigidbody.detectCollisions = false;
		}
		this.updateCollider();
	}

	// Token: 0x06003BED RID: 15341 RVA: 0x000E1428 File Offset: 0x000DF628
	private Vector3 getRelativePosition()
	{
		if (base.transform.parent == null)
		{
			return Vector3.zero;
		}
		if (this.parent != null)
		{
			float num = this.PixelsToUnits();
			Vector3 position = base.transform.parent.position;
			Vector3 position2 = base.transform.position;
			Transform transform = base.transform.parent;
			Vector3 vector = transform.InverseTransformPoint(position / num);
			vector += this.parent.pivot.TransformToUpperLeft(this.parent.size);
			Vector3 vector2 = transform.InverseTransformPoint(position2 / num);
			vector2 += this.pivot.TransformToUpperLeft(this.size);
			Vector3 vector3 = vector2 - vector;
			return vector3.Scale(1f, -1f, 1f);
		}
		dfGUIManager dfGUIManager = this.GetManager();
		if (dfGUIManager == null)
		{
			Debug.LogError("Cannot get position: View not found");
			return Vector3.zero;
		}
		float num2 = this.PixelsToUnits();
		Vector3 vector4 = base.transform.position + this.pivot.TransformToUpperLeft(this.size) * num2;
		Plane[] clippingPlanes = dfGUIManager.GetClippingPlanes();
		float num3 = clippingPlanes[0].GetDistanceToPoint(vector4) / num2;
		float num4 = clippingPlanes[3].GetDistanceToPoint(vector4) / num2;
		return new Vector3(num3, num4).RoundToInt();
	}

	// Token: 0x06003BEE RID: 15342 RVA: 0x000E15AC File Offset: 0x000DF7AC
	private void setRelativePosition(Vector3 value)
	{
		if (base.transform.parent == null)
		{
			Debug.LogError("Cannot set relative position without a parent Transform.");
			return;
		}
		if ((value - this.getRelativePosition()).sqrMagnitude <= 1.401298E-45f)
		{
			return;
		}
		if (this.parent != null)
		{
			Vector3 vector = value.Scale(1f, -1f, 1f) + this.pivot.UpperLeftToTransform(this.size) - this.parent.pivot.UpperLeftToTransform(this.parent.size);
			vector *= this.PixelsToUnits();
			if ((vector - base.transform.localPosition).sqrMagnitude >= 1.401298E-45f)
			{
				base.transform.localPosition = vector;
				this.cachedPosition = vector;
				this.OnPositionChanged();
			}
			return;
		}
		dfGUIManager dfGUIManager = this.GetManager();
		if (dfGUIManager == null)
		{
			Debug.LogError("Cannot get position: View not found");
			return;
		}
		Vector3[] corners = dfGUIManager.GetCorners();
		Vector3 vector2 = corners[0];
		float num = this.PixelsToUnits();
		value = value.Scale(1f, -1f, 1f) * num;
		Vector3 vector3 = this.pivot.UpperLeftToTransform(this.Size) * num;
		Vector3 vector4 = vector2 + dfGUIManager.transform.TransformDirection(value) + vector3;
		if ((vector4 - this.cachedPosition).sqrMagnitude > 1.401298E-45f)
		{
			base.transform.position = vector4;
			this.cachedPosition = base.transform.localPosition;
			this.OnPositionChanged();
		}
	}

	// Token: 0x06003BEF RID: 15343 RVA: 0x000E1774 File Offset: 0x000DF974
	private static float distanceFromLine(Vector3 start, Vector3 end, Vector3 test)
	{
		Vector3 vector = start - end;
		Vector3 vector2 = test - end;
		float num = Vector3.Dot(vector2, vector);
		if (num <= 0f)
		{
			return Vector3.Distance(test, end);
		}
		float num2 = Vector3.Dot(vector, vector);
		if (num2 <= num)
		{
			return Vector3.Distance(test, start);
		}
		float num3 = num / num2;
		Vector3 vector3 = end + num3 * vector;
		return Vector3.Distance(test, vector3);
	}

	// Token: 0x06003BF0 RID: 15344 RVA: 0x000E17E0 File Offset: 0x000DF9E0
	private static Vector3 closestPointOnLine(Vector3 start, Vector3 end, Vector3 test, bool clamp)
	{
		Vector3 vector = test - start;
		Vector3 vector2 = (end - start).normalized;
		float magnitude = (end - start).magnitude;
		float num = Vector3.Dot(vector2, vector);
		if (clamp)
		{
			if (num < 0f)
			{
				return start;
			}
			if (num > magnitude)
			{
				return end;
			}
		}
		vector2 *= num;
		return start + vector2;
	}

	// Token: 0x06003BF1 RID: 15345 RVA: 0x000E184C File Offset: 0x000DFA4C
	public int CompareTo(dfControl other)
	{
		if (this.ZOrder >= 0)
		{
			return this.ZOrder.CompareTo(other.ZOrder);
		}
		if (other.ZOrder < 0)
		{
			return 0;
		}
		return 1;
	}

	// Token: 0x04001F2E RID: 7982
	private const float MINIMUM_OPACITY = 0.0125f;

	// Token: 0x04001F2F RID: 7983
	private static uint versionCounter;

	// Token: 0x04001F30 RID: 7984
	[SerializeField]
	protected bool isEnabled = true;

	// Token: 0x04001F31 RID: 7985
	[SerializeField]
	protected bool isVisible = true;

	// Token: 0x04001F32 RID: 7986
	[SerializeField]
	protected bool isInteractive = true;

	// Token: 0x04001F33 RID: 7987
	[SerializeField]
	protected string tooltip;

	// Token: 0x04001F34 RID: 7988
	[SerializeField]
	protected dfPivotPoint pivot;

	// Token: 0x04001F35 RID: 7989
	[SerializeField]
	protected int zindex = -1;

	// Token: 0x04001F36 RID: 7990
	[SerializeField]
	protected Color32 color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x04001F37 RID: 7991
	[SerializeField]
	protected Color32 disabledColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x04001F38 RID: 7992
	[SerializeField]
	protected Vector2 size = Vector2.zero;

	// Token: 0x04001F39 RID: 7993
	[SerializeField]
	protected Vector2 minSize = Vector2.zero;

	// Token: 0x04001F3A RID: 7994
	[SerializeField]
	protected Vector2 maxSize = Vector2.zero;

	// Token: 0x04001F3B RID: 7995
	[SerializeField]
	protected bool clipChildren;

	// Token: 0x04001F3C RID: 7996
	[SerializeField]
	protected int tabIndex = -1;

	// Token: 0x04001F3D RID: 7997
	[SerializeField]
	protected bool canFocus;

	// Token: 0x04001F3E RID: 7998
	[SerializeField]
	protected dfControl.AnchorLayout layout;

	// Token: 0x04001F3F RID: 7999
	[SerializeField]
	protected int renderOrder = -1;

	// Token: 0x04001F40 RID: 8000
	[SerializeField]
	protected bool isLocalized;

	// Token: 0x04001F41 RID: 8001
	[SerializeField]
	protected Vector2 hotZoneScale = Vector2.one;

	// Token: 0x04001F42 RID: 8002
	protected bool isControlInvalidated = true;

	// Token: 0x04001F43 RID: 8003
	protected dfControl parent;

	// Token: 0x04001F44 RID: 8004
	protected dfList<dfControl> controls = dfList<dfControl>.Obtain();

	// Token: 0x04001F45 RID: 8005
	protected dfGUIManager manager;

	// Token: 0x04001F46 RID: 8006
	protected dfLanguageManager languageManager;

	// Token: 0x04001F47 RID: 8007
	protected bool languageManagerChecked;

	// Token: 0x04001F48 RID: 8008
	protected int cachedChildCount;

	// Token: 0x04001F49 RID: 8009
	protected Vector3 cachedPosition = Vector3.one * float.MinValue;

	// Token: 0x04001F4A RID: 8010
	protected Quaternion cachedRotation = Quaternion.identity;

	// Token: 0x04001F4B RID: 8011
	protected Vector3 cachedScale = Vector3.one;

	// Token: 0x04001F4C RID: 8012
	protected float cachedPixelSize;

	// Token: 0x04001F4D RID: 8013
	protected dfRenderData renderData;

	// Token: 0x04001F4E RID: 8014
	protected bool isMouseHovering;

	// Token: 0x04001F4F RID: 8015
	private object tag;

	// Token: 0x04001F50 RID: 8016
	protected bool isDisposing;

	// Token: 0x04001F51 RID: 8017
	private bool performingLayout;

	// Token: 0x04001F52 RID: 8018
	private Vector3[] cachedCorners = new Vector3[4];

	// Token: 0x04001F53 RID: 8019
	private Plane[] cachedClippingPlanes = new Plane[4];

	// Token: 0x04001F54 RID: 8020
	private uint version;

	// Token: 0x04001F55 RID: 8021
	private bool rendering;

	// Token: 0x020006A7 RID: 1703
	[Serializable]
	protected class AnchorLayout
	{
		// Token: 0x06003BF5 RID: 15349 RVA: 0x000E1930 File Offset: 0x000DFB30
		internal AnchorLayout(dfAnchorStyle anchorStyle)
		{
			this.anchorStyle = anchorStyle;
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x000E1940 File Offset: 0x000DFB40
		internal AnchorLayout(dfAnchorStyle anchorStyle, dfControl owner) : this(anchorStyle)
		{
			this.Attach(owner);
			this.Reset(false);
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000E1958 File Offset: 0x000DFB58
		// (set) Token: 0x06003BF8 RID: 15352 RVA: 0x000E1960 File Offset: 0x000DFB60
		internal dfAnchorStyle AnchorStyle
		{
			get
			{
				return this.anchorStyle;
			}
			set
			{
				if (value != this.anchorStyle)
				{
					this.anchorStyle = value;
					this.Reset(false);
				}
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06003BF9 RID: 15353 RVA: 0x000E197C File Offset: 0x000DFB7C
		internal bool IsPerformingLayout
		{
			get
			{
				return this.performingLayout;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x000E1984 File Offset: 0x000DFB84
		internal bool IsLayoutSuspended
		{
			get
			{
				return this.suspendLayoutCounter > 0;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x000E1990 File Offset: 0x000DFB90
		internal bool HasPendingLayoutRequest
		{
			get
			{
				return this.pendingLayoutRequest;
			}
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x000E1998 File Offset: 0x000DFB98
		internal void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.owner = null;
			}
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x000E19B4 File Offset: 0x000DFBB4
		internal void SuspendLayout()
		{
			this.suspendLayoutCounter++;
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000E19C4 File Offset: 0x000DFBC4
		internal void ResumeLayout()
		{
			bool flag = this.suspendLayoutCounter > 0;
			this.suspendLayoutCounter = Mathf.Max(0, this.suspendLayoutCounter - 1);
			if (flag && this.suspendLayoutCounter == 0 && this.pendingLayoutRequest)
			{
				this.PerformLayout();
			}
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x000E1A14 File Offset: 0x000DFC14
		internal void PerformLayout()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.suspendLayoutCounter > 0)
			{
				this.pendingLayoutRequest = true;
			}
			else
			{
				this.performLayoutInternal();
			}
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x000E1A4C File Offset: 0x000DFC4C
		internal void Attach(dfControl ownerControl)
		{
			this.owner = ownerControl;
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x000E1A58 File Offset: 0x000DFC58
		internal void Reset(bool force = false)
		{
			if (this.owner == null || this.owner.transform.parent == null)
			{
				return;
			}
			bool flag = (!force && (this.IsPerformingLayout || this.IsLayoutSuspended)) || this.owner == null || !this.owner.gameObject.activeSelf;
			if (flag)
			{
				return;
			}
			if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Proportional))
			{
				this.resetLayoutProportional();
			}
			else
			{
				this.resetLayoutAbsolute();
			}
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x000E1B0C File Offset: 0x000DFD0C
		private void resetLayoutProportional()
		{
			Vector3 relativePosition = this.owner.RelativePosition;
			Vector2 size = this.owner.Size;
			Vector2 parentSize = this.getParentSize();
			float x = relativePosition.x;
			float y = relativePosition.y;
			float num = x + size.x;
			float num2 = y + size.y;
			if (this.margins == null)
			{
				this.margins = new dfAnchorMargins();
			}
			this.margins.left = x / parentSize.x;
			this.margins.right = num / parentSize.x;
			this.margins.top = y / parentSize.y;
			this.margins.bottom = num2 / parentSize.y;
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x000E1BCC File Offset: 0x000DFDCC
		private void resetLayoutAbsolute()
		{
			Vector3 relativePosition = this.owner.RelativePosition;
			Vector2 size = this.owner.Size;
			Vector2 parentSize = this.getParentSize();
			float x = relativePosition.x;
			float y = relativePosition.y;
			float right = parentSize.x - size.x - x;
			float bottom = parentSize.y - size.y - y;
			if (this.margins == null)
			{
				this.margins = new dfAnchorMargins();
			}
			this.margins.left = x;
			this.margins.right = right;
			this.margins.top = y;
			this.margins.bottom = bottom;
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x000E1C7C File Offset: 0x000DFE7C
		protected void performLayoutInternal()
		{
			bool flag = this.margins == null || this.IsPerformingLayout || this.IsLayoutSuspended || this.owner == null || !this.owner.gameObject.activeSelf;
			if (flag)
			{
				return;
			}
			try
			{
				this.performingLayout = true;
				this.pendingLayoutRequest = false;
				Vector2 parentSize = this.getParentSize();
				Vector2 size = this.owner.Size;
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Proportional))
				{
					this.performLayoutProportional(parentSize, size);
				}
				else
				{
					this.performLayoutAbsolute(parentSize, size);
				}
			}
			finally
			{
				this.performingLayout = false;
			}
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x000E1D50 File Offset: 0x000DFF50
		private string getPath(dfControl owner)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			while (owner != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Insert(0, '/');
				}
				stringBuilder.Insert(0, owner.name);
				owner = owner.Parent;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x000E1DAC File Offset: 0x000DFFAC
		private void performLayoutProportional(Vector2 parentSize, Vector2 controlSize)
		{
			float x = this.margins.left * parentSize.x;
			float num = this.margins.right * parentSize.x;
			float y = this.margins.top * parentSize.y;
			float num2 = this.margins.bottom * parentSize.y;
			Vector3 relativePosition = this.owner.RelativePosition;
			Vector2 size = controlSize;
			if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Left))
			{
				relativePosition.x = x;
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Right))
				{
					size.x = (this.margins.right - this.margins.left) * parentSize.x;
				}
			}
			else if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Right))
			{
				relativePosition.x = num - controlSize.x;
			}
			else if (this.anchorStyle.IsFlagSet(dfAnchorStyle.CenterHorizontal))
			{
				relativePosition.x = (parentSize.x - controlSize.x) * 0.5f;
			}
			if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Top))
			{
				relativePosition.y = y;
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Bottom))
				{
					size.y = (this.margins.bottom - this.margins.top) * parentSize.y;
				}
			}
			else if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Bottom))
			{
				relativePosition.y = num2 - controlSize.y;
			}
			else if (this.anchorStyle.IsFlagSet(dfAnchorStyle.CenterVertical))
			{
				relativePosition.y = (parentSize.y - controlSize.y) * 0.5f;
			}
			this.owner.Size = size;
			this.owner.RelativePosition = relativePosition;
			if (this.owner.GetManager().PixelPerfectMode)
			{
				this.owner.MakePixelPerfect(false);
			}
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x000E1FAC File Offset: 0x000E01AC
		private void performLayoutAbsolute(Vector2 parentSize, Vector2 controlSize)
		{
			float num = this.margins.left;
			float num2 = this.margins.top;
			float num3 = num + controlSize.x;
			float num4 = num2 + controlSize.y;
			if (this.anchorStyle.IsFlagSet(dfAnchorStyle.CenterHorizontal))
			{
				num = (float)Mathf.RoundToInt((parentSize.x - controlSize.x) * 0.5f);
				num3 = (float)Mathf.RoundToInt(num + controlSize.x);
			}
			else
			{
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Left))
				{
					num = this.margins.left;
					num3 = num + controlSize.x;
				}
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Right))
				{
					num3 = parentSize.x - this.margins.right;
					if (!this.anchorStyle.IsFlagSet(dfAnchorStyle.Left))
					{
						num = num3 - controlSize.x;
					}
				}
			}
			if (this.anchorStyle.IsFlagSet(dfAnchorStyle.CenterVertical))
			{
				num2 = (float)Mathf.RoundToInt((parentSize.y - controlSize.y) * 0.5f);
				num4 = (float)Mathf.RoundToInt(num2 + controlSize.y);
			}
			else
			{
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Top))
				{
					num2 = this.margins.top;
					num4 = num2 + controlSize.y;
				}
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Bottom))
				{
					num4 = parentSize.y - this.margins.bottom;
					if (!this.anchorStyle.IsFlagSet(dfAnchorStyle.Top))
					{
						num2 = num4 - controlSize.y;
					}
				}
			}
			Vector2 size;
			size..ctor(Mathf.Max(0f, num3 - num), Mathf.Max(0f, num4 - num2));
			this.owner.Size = size;
			this.owner.RelativePosition = new Vector3(num, num2);
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x000E217C File Offset: 0x000E037C
		private Vector2 getParentSize()
		{
			dfControl component = this.owner.transform.parent.GetComponent<dfControl>();
			if (component != null)
			{
				return component.Size;
			}
			dfGUIManager manager = this.owner.GetManager();
			return manager.GetScreenSize();
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x000E21C8 File Offset: 0x000E03C8
		public override string ToString()
		{
			if (this.owner == null)
			{
				return "NO OWNER FOR ANCHOR";
			}
			dfControl parent = this.owner.parent;
			return string.Format("{0}.{1} - {2}", (!(parent != null)) ? "SCREEN" : parent.name, this.owner.name, this.margins);
		}

		// Token: 0x04001F7B RID: 8059
		[SerializeField]
		protected dfAnchorStyle anchorStyle;

		// Token: 0x04001F7C RID: 8060
		[SerializeField]
		protected dfAnchorMargins margins;

		// Token: 0x04001F7D RID: 8061
		[SerializeField]
		protected dfControl owner;

		// Token: 0x04001F7E RID: 8062
		private int suspendLayoutCounter;

		// Token: 0x04001F7F RID: 8063
		private bool performingLayout;

		// Token: 0x04001F80 RID: 8064
		private bool disposed;

		// Token: 0x04001F81 RID: 8065
		private bool pendingLayoutRequest;
	}
}
