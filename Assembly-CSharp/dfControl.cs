using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

// Token: 0x0200076C RID: 1900
[RequireComponent(typeof(BoxCollider))]
[ExecuteInEditMode]
[Serializable]
public abstract class dfControl : MonoBehaviour, IComparable<global::dfControl>
{
	// Token: 0x14000023 RID: 35
	// (add) Token: 0x06003F03 RID: 16131 RVA: 0x000E62A8 File Offset: 0x000E44A8
	// (remove) Token: 0x06003F04 RID: 16132 RVA: 0x000E62C4 File Offset: 0x000E44C4
	[HideInInspector]
	public event global::ChildControlEventHandler ControlAdded;

	// Token: 0x14000024 RID: 36
	// (add) Token: 0x06003F05 RID: 16133 RVA: 0x000E62E0 File Offset: 0x000E44E0
	// (remove) Token: 0x06003F06 RID: 16134 RVA: 0x000E62FC File Offset: 0x000E44FC
	[HideInInspector]
	public event global::ChildControlEventHandler ControlRemoved;

	// Token: 0x14000025 RID: 37
	// (add) Token: 0x06003F07 RID: 16135 RVA: 0x000E6318 File Offset: 0x000E4518
	// (remove) Token: 0x06003F08 RID: 16136 RVA: 0x000E6334 File Offset: 0x000E4534
	public event global::FocusEventHandler GotFocus;

	// Token: 0x14000026 RID: 38
	// (add) Token: 0x06003F09 RID: 16137 RVA: 0x000E6350 File Offset: 0x000E4550
	// (remove) Token: 0x06003F0A RID: 16138 RVA: 0x000E636C File Offset: 0x000E456C
	public event global::FocusEventHandler EnterFocus;

	// Token: 0x14000027 RID: 39
	// (add) Token: 0x06003F0B RID: 16139 RVA: 0x000E6388 File Offset: 0x000E4588
	// (remove) Token: 0x06003F0C RID: 16140 RVA: 0x000E63A4 File Offset: 0x000E45A4
	public event global::FocusEventHandler LostFocus;

	// Token: 0x14000028 RID: 40
	// (add) Token: 0x06003F0D RID: 16141 RVA: 0x000E63C0 File Offset: 0x000E45C0
	// (remove) Token: 0x06003F0E RID: 16142 RVA: 0x000E63DC File Offset: 0x000E45DC
	public event global::FocusEventHandler LeaveFocus;

	// Token: 0x14000029 RID: 41
	// (add) Token: 0x06003F0F RID: 16143 RVA: 0x000E63F8 File Offset: 0x000E45F8
	// (remove) Token: 0x06003F10 RID: 16144 RVA: 0x000E6414 File Offset: 0x000E4614
	public event global::PropertyChangedEventHandler<int> TabIndexChanged;

	// Token: 0x1400002A RID: 42
	// (add) Token: 0x06003F11 RID: 16145 RVA: 0x000E6430 File Offset: 0x000E4630
	// (remove) Token: 0x06003F12 RID: 16146 RVA: 0x000E644C File Offset: 0x000E464C
	public event global::PropertyChangedEventHandler<Vector2> PositionChanged;

	// Token: 0x1400002B RID: 43
	// (add) Token: 0x06003F13 RID: 16147 RVA: 0x000E6468 File Offset: 0x000E4668
	// (remove) Token: 0x06003F14 RID: 16148 RVA: 0x000E6484 File Offset: 0x000E4684
	public event global::PropertyChangedEventHandler<Vector2> SizeChanged;

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x06003F15 RID: 16149 RVA: 0x000E64A0 File Offset: 0x000E46A0
	// (remove) Token: 0x06003F16 RID: 16150 RVA: 0x000E64BC File Offset: 0x000E46BC
	[HideInInspector]
	public event global::PropertyChangedEventHandler<Color32> ColorChanged;

	// Token: 0x1400002D RID: 45
	// (add) Token: 0x06003F17 RID: 16151 RVA: 0x000E64D8 File Offset: 0x000E46D8
	// (remove) Token: 0x06003F18 RID: 16152 RVA: 0x000E64F4 File Offset: 0x000E46F4
	public event global::PropertyChangedEventHandler<bool> IsVisibleChanged;

	// Token: 0x1400002E RID: 46
	// (add) Token: 0x06003F19 RID: 16153 RVA: 0x000E6510 File Offset: 0x000E4710
	// (remove) Token: 0x06003F1A RID: 16154 RVA: 0x000E652C File Offset: 0x000E472C
	public event global::PropertyChangedEventHandler<bool> IsEnabledChanged;

	// Token: 0x1400002F RID: 47
	// (add) Token: 0x06003F1B RID: 16155 RVA: 0x000E6548 File Offset: 0x000E4748
	// (remove) Token: 0x06003F1C RID: 16156 RVA: 0x000E6564 File Offset: 0x000E4764
	[HideInInspector]
	public event global::PropertyChangedEventHandler<float> OpacityChanged;

	// Token: 0x14000030 RID: 48
	// (add) Token: 0x06003F1D RID: 16157 RVA: 0x000E6580 File Offset: 0x000E4780
	// (remove) Token: 0x06003F1E RID: 16158 RVA: 0x000E659C File Offset: 0x000E479C
	[HideInInspector]
	public event global::PropertyChangedEventHandler<global::dfAnchorStyle> AnchorChanged;

	// Token: 0x14000031 RID: 49
	// (add) Token: 0x06003F1F RID: 16159 RVA: 0x000E65B8 File Offset: 0x000E47B8
	// (remove) Token: 0x06003F20 RID: 16160 RVA: 0x000E65D4 File Offset: 0x000E47D4
	[HideInInspector]
	public event global::PropertyChangedEventHandler<global::dfPivotPoint> PivotChanged;

	// Token: 0x14000032 RID: 50
	// (add) Token: 0x06003F21 RID: 16161 RVA: 0x000E65F0 File Offset: 0x000E47F0
	// (remove) Token: 0x06003F22 RID: 16162 RVA: 0x000E660C File Offset: 0x000E480C
	[HideInInspector]
	public event global::PropertyChangedEventHandler<int> ZOrderChanged;

	// Token: 0x14000033 RID: 51
	// (add) Token: 0x06003F23 RID: 16163 RVA: 0x000E6628 File Offset: 0x000E4828
	// (remove) Token: 0x06003F24 RID: 16164 RVA: 0x000E6644 File Offset: 0x000E4844
	public event global::DragEventHandler DragStart;

	// Token: 0x14000034 RID: 52
	// (add) Token: 0x06003F25 RID: 16165 RVA: 0x000E6660 File Offset: 0x000E4860
	// (remove) Token: 0x06003F26 RID: 16166 RVA: 0x000E667C File Offset: 0x000E487C
	public event global::DragEventHandler DragEnd;

	// Token: 0x14000035 RID: 53
	// (add) Token: 0x06003F27 RID: 16167 RVA: 0x000E6698 File Offset: 0x000E4898
	// (remove) Token: 0x06003F28 RID: 16168 RVA: 0x000E66B4 File Offset: 0x000E48B4
	public event global::DragEventHandler DragDrop;

	// Token: 0x14000036 RID: 54
	// (add) Token: 0x06003F29 RID: 16169 RVA: 0x000E66D0 File Offset: 0x000E48D0
	// (remove) Token: 0x06003F2A RID: 16170 RVA: 0x000E66EC File Offset: 0x000E48EC
	public event global::DragEventHandler DragEnter;

	// Token: 0x14000037 RID: 55
	// (add) Token: 0x06003F2B RID: 16171 RVA: 0x000E6708 File Offset: 0x000E4908
	// (remove) Token: 0x06003F2C RID: 16172 RVA: 0x000E6724 File Offset: 0x000E4924
	public event global::DragEventHandler DragLeave;

	// Token: 0x14000038 RID: 56
	// (add) Token: 0x06003F2D RID: 16173 RVA: 0x000E6740 File Offset: 0x000E4940
	// (remove) Token: 0x06003F2E RID: 16174 RVA: 0x000E675C File Offset: 0x000E495C
	public event global::DragEventHandler DragOver;

	// Token: 0x14000039 RID: 57
	// (add) Token: 0x06003F2F RID: 16175 RVA: 0x000E6778 File Offset: 0x000E4978
	// (remove) Token: 0x06003F30 RID: 16176 RVA: 0x000E6794 File Offset: 0x000E4994
	public event global::KeyPressHandler KeyPress;

	// Token: 0x1400003A RID: 58
	// (add) Token: 0x06003F31 RID: 16177 RVA: 0x000E67B0 File Offset: 0x000E49B0
	// (remove) Token: 0x06003F32 RID: 16178 RVA: 0x000E67CC File Offset: 0x000E49CC
	public event global::KeyPressHandler KeyDown;

	// Token: 0x1400003B RID: 59
	// (add) Token: 0x06003F33 RID: 16179 RVA: 0x000E67E8 File Offset: 0x000E49E8
	// (remove) Token: 0x06003F34 RID: 16180 RVA: 0x000E6804 File Offset: 0x000E4A04
	public event global::KeyPressHandler KeyUp;

	// Token: 0x1400003C RID: 60
	// (add) Token: 0x06003F35 RID: 16181 RVA: 0x000E6820 File Offset: 0x000E4A20
	// (remove) Token: 0x06003F36 RID: 16182 RVA: 0x000E683C File Offset: 0x000E4A3C
	public event global::ControlMultiTouchEventHandler MultiTouch;

	// Token: 0x1400003D RID: 61
	// (add) Token: 0x06003F37 RID: 16183 RVA: 0x000E6858 File Offset: 0x000E4A58
	// (remove) Token: 0x06003F38 RID: 16184 RVA: 0x000E6874 File Offset: 0x000E4A74
	public event global::MouseEventHandler MouseEnter;

	// Token: 0x1400003E RID: 62
	// (add) Token: 0x06003F39 RID: 16185 RVA: 0x000E6890 File Offset: 0x000E4A90
	// (remove) Token: 0x06003F3A RID: 16186 RVA: 0x000E68AC File Offset: 0x000E4AAC
	public event global::MouseEventHandler MouseMove;

	// Token: 0x1400003F RID: 63
	// (add) Token: 0x06003F3B RID: 16187 RVA: 0x000E68C8 File Offset: 0x000E4AC8
	// (remove) Token: 0x06003F3C RID: 16188 RVA: 0x000E68E4 File Offset: 0x000E4AE4
	public event global::MouseEventHandler MouseHover;

	// Token: 0x14000040 RID: 64
	// (add) Token: 0x06003F3D RID: 16189 RVA: 0x000E6900 File Offset: 0x000E4B00
	// (remove) Token: 0x06003F3E RID: 16190 RVA: 0x000E691C File Offset: 0x000E4B1C
	public event global::MouseEventHandler MouseLeave;

	// Token: 0x14000041 RID: 65
	// (add) Token: 0x06003F3F RID: 16191 RVA: 0x000E6938 File Offset: 0x000E4B38
	// (remove) Token: 0x06003F40 RID: 16192 RVA: 0x000E6954 File Offset: 0x000E4B54
	public event global::MouseEventHandler MouseDown;

	// Token: 0x14000042 RID: 66
	// (add) Token: 0x06003F41 RID: 16193 RVA: 0x000E6970 File Offset: 0x000E4B70
	// (remove) Token: 0x06003F42 RID: 16194 RVA: 0x000E698C File Offset: 0x000E4B8C
	public event global::MouseEventHandler MouseUp;

	// Token: 0x14000043 RID: 67
	// (add) Token: 0x06003F43 RID: 16195 RVA: 0x000E69A8 File Offset: 0x000E4BA8
	// (remove) Token: 0x06003F44 RID: 16196 RVA: 0x000E69C4 File Offset: 0x000E4BC4
	public event global::MouseEventHandler MouseWheel;

	// Token: 0x14000044 RID: 68
	// (add) Token: 0x06003F45 RID: 16197 RVA: 0x000E69E0 File Offset: 0x000E4BE0
	// (remove) Token: 0x06003F46 RID: 16198 RVA: 0x000E69FC File Offset: 0x000E4BFC
	public event global::MouseEventHandler Click;

	// Token: 0x14000045 RID: 69
	// (add) Token: 0x06003F47 RID: 16199 RVA: 0x000E6A18 File Offset: 0x000E4C18
	// (remove) Token: 0x06003F48 RID: 16200 RVA: 0x000E6A34 File Offset: 0x000E4C34
	public event global::MouseEventHandler DoubleClick;

	// Token: 0x17000BF6 RID: 3062
	// (get) Token: 0x06003F49 RID: 16201 RVA: 0x000E6A50 File Offset: 0x000E4C50
	public global::dfGUIManager GUIManager
	{
		get
		{
			return this.GetManager();
		}
	}

	// Token: 0x17000BF7 RID: 3063
	// (get) Token: 0x06003F4A RID: 16202 RVA: 0x000E6A58 File Offset: 0x000E4C58
	// (set) Token: 0x06003F4B RID: 16203 RVA: 0x000E6ACC File Offset: 0x000E4CCC
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

	// Token: 0x17000BF8 RID: 3064
	// (get) Token: 0x06003F4C RID: 16204 RVA: 0x000E6AE8 File Offset: 0x000E4CE8
	// (set) Token: 0x06003F4D RID: 16205 RVA: 0x000E6B20 File Offset: 0x000E4D20
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

	// Token: 0x17000BF9 RID: 3065
	// (get) Token: 0x06003F4E RID: 16206 RVA: 0x000E6B78 File Offset: 0x000E4D78
	// (set) Token: 0x06003F4F RID: 16207 RVA: 0x000E6B80 File Offset: 0x000E4D80
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
				global::dfGUIManager.SetFocus(null);
			}
			this.isInteractive = value;
		}
	}

	// Token: 0x17000BFA RID: 3066
	// (get) Token: 0x06003F50 RID: 16208 RVA: 0x000E6BA0 File Offset: 0x000E4DA0
	// (set) Token: 0x06003F51 RID: 16209 RVA: 0x000E6BA8 File Offset: 0x000E4DA8
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

	// Token: 0x17000BFB RID: 3067
	// (get) Token: 0x06003F52 RID: 16210 RVA: 0x000E6BC8 File Offset: 0x000E4DC8
	// (set) Token: 0x06003F53 RID: 16211 RVA: 0x000E6BDC File Offset: 0x000E4DDC
	[SerializeField]
	public global::dfAnchorStyle Anchor
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

	// Token: 0x17000BFC RID: 3068
	// (get) Token: 0x06003F54 RID: 16212 RVA: 0x000E6C18 File Offset: 0x000E4E18
	// (set) Token: 0x06003F55 RID: 16213 RVA: 0x000E6C2C File Offset: 0x000E4E2C
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

	// Token: 0x17000BFD RID: 3069
	// (get) Token: 0x06003F56 RID: 16214 RVA: 0x000E6C84 File Offset: 0x000E4E84
	// (set) Token: 0x06003F57 RID: 16215 RVA: 0x000E6C8C File Offset: 0x000E4E8C
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

	// Token: 0x17000BFE RID: 3070
	// (get) Token: 0x06003F58 RID: 16216 RVA: 0x000E6CC4 File Offset: 0x000E4EC4
	// (set) Token: 0x06003F59 RID: 16217 RVA: 0x000E6CCC File Offset: 0x000E4ECC
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

	// Token: 0x17000BFF RID: 3071
	// (get) Token: 0x06003F5A RID: 16218 RVA: 0x000E6D04 File Offset: 0x000E4F04
	// (set) Token: 0x06003F5B RID: 16219 RVA: 0x000E6D0C File Offset: 0x000E4F0C
	public global::dfPivotPoint Pivot
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

	// Token: 0x17000C00 RID: 3072
	// (get) Token: 0x06003F5C RID: 16220 RVA: 0x000E6D94 File Offset: 0x000E4F94
	// (set) Token: 0x06003F5D RID: 16221 RVA: 0x000E6D9C File Offset: 0x000E4F9C
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

	// Token: 0x17000C01 RID: 3073
	// (get) Token: 0x06003F5E RID: 16222 RVA: 0x000E6DA8 File Offset: 0x000E4FA8
	// (set) Token: 0x06003F5F RID: 16223 RVA: 0x000E6DE4 File Offset: 0x000E4FE4
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

	// Token: 0x17000C02 RID: 3074
	// (get) Token: 0x06003F60 RID: 16224 RVA: 0x000E6DF0 File Offset: 0x000E4FF0
	// (set) Token: 0x06003F61 RID: 16225 RVA: 0x000E6DF8 File Offset: 0x000E4FF8
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

	// Token: 0x17000C03 RID: 3075
	// (get) Token: 0x06003F62 RID: 16226 RVA: 0x000E6EC0 File Offset: 0x000E50C0
	// (set) Token: 0x06003F63 RID: 16227 RVA: 0x000E6ED0 File Offset: 0x000E50D0
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

	// Token: 0x17000C04 RID: 3076
	// (get) Token: 0x06003F64 RID: 16228 RVA: 0x000E6EEC File Offset: 0x000E50EC
	// (set) Token: 0x06003F65 RID: 16229 RVA: 0x000E6EFC File Offset: 0x000E50FC
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

	// Token: 0x17000C05 RID: 3077
	// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000E6F18 File Offset: 0x000E5118
	// (set) Token: 0x06003F67 RID: 16231 RVA: 0x000E6F20 File Offset: 0x000E5120
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

	// Token: 0x17000C06 RID: 3078
	// (get) Token: 0x06003F68 RID: 16232 RVA: 0x000E6F60 File Offset: 0x000E5160
	// (set) Token: 0x06003F69 RID: 16233 RVA: 0x000E6F68 File Offset: 0x000E5168
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

	// Token: 0x17000C07 RID: 3079
	// (get) Token: 0x06003F6A RID: 16234 RVA: 0x000E6FA8 File Offset: 0x000E51A8
	// (set) Token: 0x06003F6B RID: 16235 RVA: 0x000E6FB0 File Offset: 0x000E51B0
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

	// Token: 0x17000C08 RID: 3080
	// (get) Token: 0x06003F6C RID: 16236 RVA: 0x000E7000 File Offset: 0x000E5200
	// (set) Token: 0x06003F6D RID: 16237 RVA: 0x000E7008 File Offset: 0x000E5208
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

	// Token: 0x17000C09 RID: 3081
	// (get) Token: 0x06003F6E RID: 16238 RVA: 0x000E702C File Offset: 0x000E522C
	public IList<global::dfControl> Controls
	{
		get
		{
			return this.controls;
		}
	}

	// Token: 0x17000C0A RID: 3082
	// (get) Token: 0x06003F6F RID: 16239 RVA: 0x000E7034 File Offset: 0x000E5234
	public global::dfControl Parent
	{
		get
		{
			return this.parent;
		}
	}

	// Token: 0x17000C0B RID: 3083
	// (get) Token: 0x06003F70 RID: 16240 RVA: 0x000E703C File Offset: 0x000E523C
	// (set) Token: 0x06003F71 RID: 16241 RVA: 0x000E7044 File Offset: 0x000E5244
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

	// Token: 0x17000C0C RID: 3084
	// (get) Token: 0x06003F72 RID: 16242 RVA: 0x000E7060 File Offset: 0x000E5260
	protected bool IsLayoutSuspended
	{
		get
		{
			return this.performingLayout || (this.layout != null && this.layout.IsLayoutSuspended);
		}
	}

	// Token: 0x17000C0D RID: 3085
	// (get) Token: 0x06003F73 RID: 16243 RVA: 0x000E708C File Offset: 0x000E528C
	protected bool IsPerformingLayout
	{
		get
		{
			return this.performingLayout || (this.layout != null && this.layout.IsPerformingLayout);
		}
	}

	// Token: 0x17000C0E RID: 3086
	// (get) Token: 0x06003F74 RID: 16244 RVA: 0x000E70BC File Offset: 0x000E52BC
	// (set) Token: 0x06003F75 RID: 16245 RVA: 0x000E70C4 File Offset: 0x000E52C4
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

	// Token: 0x17000C0F RID: 3087
	// (get) Token: 0x06003F76 RID: 16246 RVA: 0x000E70D0 File Offset: 0x000E52D0
	internal uint Version
	{
		get
		{
			return this.version;
		}
	}

	// Token: 0x17000C10 RID: 3088
	// (get) Token: 0x06003F77 RID: 16247 RVA: 0x000E70D8 File Offset: 0x000E52D8
	// (set) Token: 0x06003F78 RID: 16248 RVA: 0x000E70E0 File Offset: 0x000E52E0
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

	// Token: 0x17000C11 RID: 3089
	// (get) Token: 0x06003F79 RID: 16249 RVA: 0x000E70F8 File Offset: 0x000E52F8
	// (set) Token: 0x06003F7A RID: 16250 RVA: 0x000E7100 File Offset: 0x000E5300
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

	// Token: 0x17000C12 RID: 3090
	// (get) Token: 0x06003F7B RID: 16251 RVA: 0x000E711C File Offset: 0x000E531C
	// (set) Token: 0x06003F7C RID: 16252 RVA: 0x000E7134 File Offset: 0x000E5334
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

	// Token: 0x17000C13 RID: 3091
	// (get) Token: 0x06003F7D RID: 16253 RVA: 0x000E7140 File Offset: 0x000E5340
	public virtual bool ContainsFocus
	{
		get
		{
			return global::dfGUIManager.ContainsFocus(this);
		}
	}

	// Token: 0x17000C14 RID: 3092
	// (get) Token: 0x06003F7E RID: 16254 RVA: 0x000E7148 File Offset: 0x000E5348
	public virtual bool HasFocus
	{
		get
		{
			return global::dfGUIManager.HasFocus(this);
		}
	}

	// Token: 0x17000C15 RID: 3093
	// (get) Token: 0x06003F7F RID: 16255 RVA: 0x000E7150 File Offset: 0x000E5350
	public bool ContainsMouse
	{
		get
		{
			return this.isMouseHovering;
		}
	}

	// Token: 0x06003F80 RID: 16256 RVA: 0x000E7158 File Offset: 0x000E5358
	internal void setRenderOrder(ref int order)
	{
		this.renderOrder = ++order;
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].setRenderOrder(ref order);
		}
	}

	// Token: 0x17000C16 RID: 3094
	// (get) Token: 0x06003F81 RID: 16257 RVA: 0x000E71A4 File Offset: 0x000E53A4
	[HideInInspector]
	public int RenderOrder
	{
		get
		{
			return this.renderOrder;
		}
	}

	// Token: 0x06003F82 RID: 16258 RVA: 0x000E71AC File Offset: 0x000E53AC
	internal virtual void OnDragStart(global::dfDragEventArgs args)
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

	// Token: 0x06003F83 RID: 16259 RVA: 0x000E721C File Offset: 0x000E541C
	internal virtual void OnDragEnd(global::dfDragEventArgs args)
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

	// Token: 0x06003F84 RID: 16260 RVA: 0x000E728C File Offset: 0x000E548C
	internal virtual void OnDragDrop(global::dfDragEventArgs args)
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

	// Token: 0x06003F85 RID: 16261 RVA: 0x000E72FC File Offset: 0x000E54FC
	internal virtual void OnDragEnter(global::dfDragEventArgs args)
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

	// Token: 0x06003F86 RID: 16262 RVA: 0x000E736C File Offset: 0x000E556C
	internal virtual void OnDragLeave(global::dfDragEventArgs args)
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

	// Token: 0x06003F87 RID: 16263 RVA: 0x000E73DC File Offset: 0x000E55DC
	internal virtual void OnDragOver(global::dfDragEventArgs args)
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

	// Token: 0x06003F88 RID: 16264 RVA: 0x000E744C File Offset: 0x000E564C
	protected internal virtual void OnMultiTouch(global::dfTouchEventArgs args)
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

	// Token: 0x06003F89 RID: 16265 RVA: 0x000E74B0 File Offset: 0x000E56B0
	protected internal virtual void OnMouseEnter(global::dfMouseEventArgs args)
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

	// Token: 0x06003F8A RID: 16266 RVA: 0x000E751C File Offset: 0x000E571C
	protected internal virtual void OnMouseLeave(global::dfMouseEventArgs args)
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

	// Token: 0x06003F8B RID: 16267 RVA: 0x000E7588 File Offset: 0x000E5788
	protected internal virtual void OnMouseMove(global::dfMouseEventArgs args)
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

	// Token: 0x06003F8C RID: 16268 RVA: 0x000E75EC File Offset: 0x000E57EC
	protected internal virtual void OnMouseHover(global::dfMouseEventArgs args)
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

	// Token: 0x06003F8D RID: 16269 RVA: 0x000E7650 File Offset: 0x000E5850
	protected internal virtual void OnMouseWheel(global::dfMouseEventArgs args)
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

	// Token: 0x06003F8E RID: 16270 RVA: 0x000E76B4 File Offset: 0x000E58B4
	protected internal virtual void OnMouseDown(global::dfMouseEventArgs args)
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

	// Token: 0x06003F8F RID: 16271 RVA: 0x000E7764 File Offset: 0x000E5964
	protected internal virtual void OnMouseUp(global::dfMouseEventArgs args)
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

	// Token: 0x06003F90 RID: 16272 RVA: 0x000E77C8 File Offset: 0x000E59C8
	protected internal virtual void OnClick(global::dfMouseEventArgs args)
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

	// Token: 0x06003F91 RID: 16273 RVA: 0x000E782C File Offset: 0x000E5A2C
	protected internal virtual void OnDoubleClick(global::dfMouseEventArgs args)
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

	// Token: 0x06003F92 RID: 16274 RVA: 0x000E7890 File Offset: 0x000E5A90
	protected internal virtual void OnKeyPress(global::dfKeyEventArgs args)
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

	// Token: 0x06003F93 RID: 16275 RVA: 0x000E7900 File Offset: 0x000E5B00
	protected internal virtual void OnKeyDown(global::dfKeyEventArgs args)
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

	// Token: 0x06003F94 RID: 16276 RVA: 0x000E7990 File Offset: 0x000E5B90
	protected virtual void OnTabKeyPressed(global::dfKeyEventArgs args)
	{
		List<global::dfControl> list = (from c in this.GetManager().GetComponentsInChildren<global::dfControl>()
		where c != this && c.TabIndex >= 0 && c.IsInteractive && c.CanFocus && c.IsVisible
		select c).ToList<global::dfControl>();
		if (list.Count == 0)
		{
			return;
		}
		list.Sort(delegate(global::dfControl lhs, global::dfControl rhs)
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
				global::dfControl dfControl = list[i];
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
			global::dfControl dfControl2 = list[j];
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

	// Token: 0x06003F95 RID: 16277 RVA: 0x000E7AB0 File Offset: 0x000E5CB0
	protected internal virtual void OnKeyUp(global::dfKeyEventArgs args)
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

	// Token: 0x06003F96 RID: 16278 RVA: 0x000E7B14 File Offset: 0x000E5D14
	protected internal virtual void OnEnterFocus(global::dfFocusEventArgs args)
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

	// Token: 0x06003F97 RID: 16279 RVA: 0x000E7B50 File Offset: 0x000E5D50
	protected internal virtual void OnLeaveFocus(global::dfFocusEventArgs args)
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

	// Token: 0x06003F98 RID: 16280 RVA: 0x000E7B8C File Offset: 0x000E5D8C
	protected internal virtual void OnGotFocus(global::dfFocusEventArgs args)
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

	// Token: 0x06003F99 RID: 16281 RVA: 0x000E7BF0 File Offset: 0x000E5DF0
	protected internal virtual void OnLostFocus(global::dfFocusEventArgs args)
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

	// Token: 0x06003F9A RID: 16282 RVA: 0x000E7C54 File Offset: 0x000E5E54
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

	// Token: 0x06003F9B RID: 16283 RVA: 0x000E7CB4 File Offset: 0x000E5EB4
	protected internal bool Signal(string eventName, params object[] args)
	{
		return this.Signal(base.gameObject, eventName, args);
	}

	// Token: 0x06003F9C RID: 16284 RVA: 0x000E7CC4 File Offset: 0x000E5EC4
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

	// Token: 0x06003F9D RID: 16285 RVA: 0x000E7D08 File Offset: 0x000E5F08
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

	// Token: 0x06003F9E RID: 16286 RVA: 0x000E7ED8 File Offset: 0x000E60D8
	internal bool GetIsVisibleRaw()
	{
		return this.isVisible;
	}

	// Token: 0x06003F9F RID: 16287 RVA: 0x000E7EE0 File Offset: 0x000E60E0
	public void Localize()
	{
		if (!this.IsLocalized)
		{
			return;
		}
		if (this.languageManager == null)
		{
			this.languageManager = this.GetManager().GetComponent<global::dfLanguageManager>();
			if (this.languageManager == null)
			{
				return;
			}
		}
		this.OnLocalize();
	}

	// Token: 0x06003FA0 RID: 16288 RVA: 0x000E7F34 File Offset: 0x000E6134
	public void DoClick()
	{
		Camera camera = this.GetCamera();
		Vector3 vector = camera.WorldToScreenPoint(this.GetCenter());
		Ray ray = camera.ScreenPointToRay(vector);
		this.OnClick(new global::dfMouseEventArgs(this, global::dfMouseButtons.Left, 1, ray, vector, 0f));
	}

	// Token: 0x06003FA1 RID: 16289 RVA: 0x000E7F78 File Offset: 0x000E6178
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

	// Token: 0x06003FA2 RID: 16290 RVA: 0x000E7FC4 File Offset: 0x000E61C4
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

	// Token: 0x06003FA3 RID: 16291 RVA: 0x000E8024 File Offset: 0x000E6224
	public void Show()
	{
		this.IsVisible = true;
	}

	// Token: 0x06003FA4 RID: 16292 RVA: 0x000E8030 File Offset: 0x000E6230
	public void Hide()
	{
		this.IsVisible = false;
	}

	// Token: 0x06003FA5 RID: 16293 RVA: 0x000E803C File Offset: 0x000E623C
	public void Enable()
	{
		this.IsEnabled = true;
	}

	// Token: 0x06003FA6 RID: 16294 RVA: 0x000E8048 File Offset: 0x000E6248
	public void Disable()
	{
		this.IsEnabled = false;
	}

	// Token: 0x06003FA7 RID: 16295 RVA: 0x000E8054 File Offset: 0x000E6254
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
		Vector3 vector5 = global::dfControl.closestPointOnLine(vector2, vector3, vector, true);
		float num2 = (vector5 - vector2).magnitude / (vector3 - vector2).magnitude;
		float num3 = this.size.x * num2;
		vector5 = global::dfControl.closestPointOnLine(vector2, vector4, vector, true);
		num2 = (vector5 - vector2).magnitude / (vector4 - vector2).magnitude;
		float num4 = this.size.y * num2;
		position..ctor(num3, num4);
		return true;
	}

	// Token: 0x06003FA8 RID: 16296 RVA: 0x000E81E8 File Offset: 0x000E63E8
	public T Find<T>(string Name) where T : global::dfControl
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

	// Token: 0x06003FA9 RID: 16297 RVA: 0x000E82C8 File Offset: 0x000E64C8
	public global::dfControl Find(string Name)
	{
		if (base.name == Name)
		{
			return this;
		}
		this.updateControlHierarchy(true);
		for (int i = 0; i < this.controls.Count; i++)
		{
			global::dfControl dfControl = this.controls[i];
			if (dfControl.name == Name)
			{
				return dfControl;
			}
		}
		for (int j = 0; j < this.controls.Count; j++)
		{
			global::dfControl dfControl2 = this.controls[j].Find(Name);
			if (dfControl2 != null)
			{
				return dfControl2;
			}
		}
		return null;
	}

	// Token: 0x06003FAA RID: 16298 RVA: 0x000E836C File Offset: 0x000E656C
	public void Focus()
	{
		if (!this.CanFocus || this.HasFocus || !this.IsEnabled || !this.IsVisible)
		{
			return;
		}
		global::dfGUIManager.SetFocus(this);
		this.Invalidate();
	}

	// Token: 0x06003FAB RID: 16299 RVA: 0x000E83B4 File Offset: 0x000E65B4
	public void Unfocus()
	{
		if (this.ContainsFocus)
		{
			global::dfGUIManager.SetFocus(null);
		}
	}

	// Token: 0x06003FAC RID: 16300 RVA: 0x000E83C8 File Offset: 0x000E65C8
	public global::dfControl GetRootContainer()
	{
		global::dfControl dfControl = this;
		while (dfControl.Parent != null)
		{
			dfControl = dfControl.Parent;
		}
		return dfControl;
	}

	// Token: 0x06003FAD RID: 16301 RVA: 0x000E83F8 File Offset: 0x000E65F8
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

	// Token: 0x06003FAE RID: 16302 RVA: 0x000E844C File Offset: 0x000E664C
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

	// Token: 0x06003FAF RID: 16303 RVA: 0x000E8490 File Offset: 0x000E6690
	internal global::dfRenderData Render()
	{
		if (this.rendering)
		{
			return this.renderData;
		}
		global::dfRenderData result;
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
					this.renderData = global::dfRenderData.Obtain();
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

	// Token: 0x06003FB0 RID: 16304 RVA: 0x000E857C File Offset: 0x000E677C
	public virtual void Invalidate()
	{
		this.updateVersion();
		this.isControlInvalidated = true;
		global::dfGUIManager dfGUIManager = this.GetManager();
		if (dfGUIManager != null)
		{
			dfGUIManager.Invalidate();
		}
	}

	// Token: 0x06003FB1 RID: 16305 RVA: 0x000E85B0 File Offset: 0x000E67B0
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

	// Token: 0x06003FB2 RID: 16306 RVA: 0x000E8634 File Offset: 0x000E6834
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

	// Token: 0x06003FB3 RID: 16307 RVA: 0x000E86A0 File Offset: 0x000E68A0
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

	// Token: 0x06003FB4 RID: 16308 RVA: 0x000E86EC File Offset: 0x000E68EC
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

	// Token: 0x06003FB5 RID: 16309 RVA: 0x000E8738 File Offset: 0x000E6938
	public virtual Vector2 CalculateMinimumSize()
	{
		return this.MinimumSize;
	}

	// Token: 0x06003FB6 RID: 16310 RVA: 0x000E8740 File Offset: 0x000E6940
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

	// Token: 0x06003FB7 RID: 16311 RVA: 0x000E87D8 File Offset: 0x000E69D8
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

	// Token: 0x06003FB8 RID: 16312 RVA: 0x000E8878 File Offset: 0x000E6A78
	public Vector3 GetCenter()
	{
		return base.transform.position + this.Pivot.TransformToCenter(this.Size) * this.PixelsToUnits();
	}

	// Token: 0x06003FB9 RID: 16313 RVA: 0x000E88B4 File Offset: 0x000E6AB4
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

	// Token: 0x06003FBA RID: 16314 RVA: 0x000E89C4 File Offset: 0x000E6BC4
	public Camera GetCamera()
	{
		global::dfGUIManager dfGUIManager = this.GetManager();
		if (dfGUIManager == null)
		{
			Debug.LogError("The Manager hosting this control could not be determined");
			return null;
		}
		return dfGUIManager.RenderCamera;
	}

	// Token: 0x06003FBB RID: 16315 RVA: 0x000E89F8 File Offset: 0x000E6BF8
	public Rect GetScreenRect()
	{
		Camera camera = this.GetCamera();
		Vector3[] corners = this.GetCorners();
		Vector3 vector = camera.WorldToScreenPoint(corners[0]);
		Vector3 vector2 = camera.WorldToScreenPoint(corners[3]);
		return new Rect(vector.x, (float)Screen.height - vector.y, vector2.x - vector.x, vector.y - vector2.y);
	}

	// Token: 0x06003FBC RID: 16316 RVA: 0x000E8A74 File Offset: 0x000E6C74
	public global::dfGUIManager GetManager()
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
			global::dfGUIManager component = gameObject.GetComponent<global::dfGUIManager>();
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
		global::dfGUIManager dfGUIManager = Object.FindObjectsOfType(typeof(global::dfGUIManager)).FirstOrDefault<Object>() as global::dfGUIManager;
		if (dfGUIManager != null)
		{
			return this.manager = dfGUIManager;
		}
		return null;
	}

	// Token: 0x06003FBD RID: 16317 RVA: 0x000E8B7C File Offset: 0x000E6D7C
	protected internal float PixelsToUnits()
	{
		if (this.cachedPixelSize > 1.401298E-45f)
		{
			return this.cachedPixelSize;
		}
		global::dfGUIManager dfGUIManager = this.GetManager();
		if (dfGUIManager == null)
		{
			return 0.0026f;
		}
		return this.cachedPixelSize = dfGUIManager.PixelsToUnits();
	}

	// Token: 0x06003FBE RID: 16318 RVA: 0x000E8BC8 File Offset: 0x000E6DC8
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

	// Token: 0x06003FBF RID: 16319 RVA: 0x000E8CB4 File Offset: 0x000E6EB4
	public bool Contains(global::dfControl child)
	{
		return child != null && child.transform.IsChildOf(base.transform);
	}

	// Token: 0x06003FC0 RID: 16320 RVA: 0x000E8CE4 File Offset: 0x000E6EE4
	[HideInInspector]
	protected internal virtual void OnLocalize()
	{
	}

	// Token: 0x06003FC1 RID: 16321 RVA: 0x000E8CE8 File Offset: 0x000E6EE8
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
			this.languageManager = this.GetManager().GetComponent<global::dfLanguageManager>();
			if (this.languageManager == null)
			{
				return key;
			}
		}
		return this.languageManager.GetValue(key);
	}

	// Token: 0x06003FC2 RID: 16322 RVA: 0x000E8D64 File Offset: 0x000E6F64
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

	// Token: 0x06003FC3 RID: 16323 RVA: 0x000E8E44 File Offset: 0x000E7044
	[HideInInspector]
	protected virtual void OnRebuildRenderData()
	{
	}

	// Token: 0x06003FC4 RID: 16324 RVA: 0x000E8E48 File Offset: 0x000E7048
	[HideInInspector]
	protected internal virtual void OnControlAdded(global::dfControl child)
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

	// Token: 0x06003FC5 RID: 16325 RVA: 0x000E8E90 File Offset: 0x000E7090
	[HideInInspector]
	protected internal virtual void OnControlRemoved(global::dfControl child)
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

	// Token: 0x06003FC6 RID: 16326 RVA: 0x000E8ED8 File Offset: 0x000E70D8
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

	// Token: 0x06003FC7 RID: 16327 RVA: 0x000E8F44 File Offset: 0x000E7144
	[HideInInspector]
	protected internal virtual void OnSizeChanged()
	{
		this.updateCollider();
		this.Invalidate();
		this.ResetLayout(false, false);
		if (this.Anchor.IsAnyFlagSet(global::dfAnchorStyle.CenterHorizontal | global::dfAnchorStyle.CenterVertical))
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

	// Token: 0x06003FC8 RID: 16328 RVA: 0x000E8FCC File Offset: 0x000E71CC
	[HideInInspector]
	protected internal virtual void OnPivotChanged()
	{
		this.Invalidate();
		if (this.Anchor.IsAnyFlagSet(global::dfAnchorStyle.CenterHorizontal | global::dfAnchorStyle.CenterVertical))
		{
			this.ResetLayout(false, false);
			this.PerformLayout();
		}
		if (this.PivotChanged != null)
		{
			this.PivotChanged(this, this.pivot);
		}
	}

	// Token: 0x06003FC9 RID: 16329 RVA: 0x000E9020 File Offset: 0x000E7220
	[HideInInspector]
	protected internal virtual void OnAnchorChanged()
	{
		global::dfAnchorStyle anchorStyle = this.layout.AnchorStyle;
		this.Invalidate();
		this.ResetLayout(false, false);
		if (anchorStyle.IsAnyFlagSet(global::dfAnchorStyle.CenterHorizontal | global::dfAnchorStyle.CenterVertical))
		{
			this.PerformLayout();
		}
		if (this.AnchorChanged != null)
		{
			this.AnchorChanged(this, anchorStyle);
		}
	}

	// Token: 0x06003FCA RID: 16330 RVA: 0x000E9078 File Offset: 0x000E7278
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

	// Token: 0x06003FCB RID: 16331 RVA: 0x000E90D8 File Offset: 0x000E72D8
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

	// Token: 0x06003FCC RID: 16332 RVA: 0x000E9138 File Offset: 0x000E7338
	[HideInInspector]
	protected internal virtual void OnZOrderChanged()
	{
		this.Invalidate();
		if (this.ZOrderChanged != null)
		{
			this.ZOrderChanged(this, this.zindex);
		}
	}

	// Token: 0x06003FCD RID: 16333 RVA: 0x000E9160 File Offset: 0x000E7360
	[HideInInspector]
	protected internal virtual void OnTabIndexChanged()
	{
		this.Invalidate();
		if (this.TabIndexChanged != null)
		{
			this.TabIndexChanged(this, this.tabIndex);
		}
	}

	// Token: 0x06003FCE RID: 16334 RVA: 0x000E9188 File Offset: 0x000E7388
	[HideInInspector]
	protected internal virtual void OnIsVisibleChanged()
	{
		if (this.HasFocus && !this.IsVisible)
		{
			global::dfGUIManager.SetFocus(null);
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

	// Token: 0x06003FCF RID: 16335 RVA: 0x000E9228 File Offset: 0x000E7428
	[HideInInspector]
	protected internal virtual void OnIsEnabledChanged()
	{
		if (global::dfGUIManager.ContainsFocus(this) && !this.IsEnabled)
		{
			global::dfGUIManager.SetFocus(null);
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

	// Token: 0x06003FD0 RID: 16336 RVA: 0x000E92C8 File Offset: 0x000E74C8
	protected internal float CalculateOpacity()
	{
		if (this.parent == null)
		{
			return this.Opacity;
		}
		return this.Opacity * this.parent.CalculateOpacity();
	}

	// Token: 0x06003FD1 RID: 16337 RVA: 0x000E9300 File Offset: 0x000E7500
	protected internal Color32 ApplyOpacity(Color32 color)
	{
		float num = this.CalculateOpacity();
		color.a = (byte)(num * 255f);
		return color;
	}

	// Token: 0x06003FD2 RID: 16338 RVA: 0x000E9324 File Offset: 0x000E7524
	protected internal Vector2 GetHitPosition(global::dfMouseEventArgs args)
	{
		Vector2 result;
		this.GetHitPosition(args.Ray, out result);
		return result;
	}

	// Token: 0x06003FD3 RID: 16339 RVA: 0x000E9344 File Offset: 0x000E7544
	protected internal Vector3 getScaledDirection(Vector3 direction)
	{
		Vector3 localScale = this.GetManager().transform.localScale;
		direction = base.transform.TransformDirection(direction);
		return Vector3.Scale(direction, localScale);
	}

	// Token: 0x06003FD4 RID: 16340 RVA: 0x000E9378 File Offset: 0x000E7578
	protected internal Vector3 transformOffset(Vector3 offset)
	{
		Vector3 vector = offset.x * this.getScaledDirection(Vector3.right);
		Vector3 vector2 = offset.y * this.getScaledDirection(Vector3.down);
		return (vector + vector2) * this.PixelsToUnits();
	}

	// Token: 0x06003FD5 RID: 16341 RVA: 0x000E93C8 File Offset: 0x000E75C8
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

	// Token: 0x06003FD6 RID: 16342 RVA: 0x000E946C File Offset: 0x000E766C
	[HideInInspector]
	public virtual void Awake()
	{
		if (base.transform.parent != null)
		{
			global::dfControl component = base.transform.parent.GetComponent<global::dfControl>();
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

	// Token: 0x06003FD7 RID: 16343 RVA: 0x000E94DC File Offset: 0x000E76DC
	[HideInInspector]
	public virtual void Start()
	{
	}

	// Token: 0x06003FD8 RID: 16344 RVA: 0x000E94E0 File Offset: 0x000E76E0
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

	// Token: 0x06003FD9 RID: 16345 RVA: 0x000E9554 File Offset: 0x000E7754
	[HideInInspector]
	public virtual void OnApplicationQuit()
	{
		this.RemoveAllEventHandlers();
	}

	// Token: 0x06003FDA RID: 16346 RVA: 0x000E955C File Offset: 0x000E775C
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
			if (global::dfGUIManager.HasFocus(this))
			{
				global::dfGUIManager.SetFocus(null);
			}
			this.OnIsEnabledChanged();
		}
		catch
		{
		}
	}

	// Token: 0x06003FDB RID: 16347 RVA: 0x000E95CC File Offset: 0x000E77CC
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

	// Token: 0x06003FDC RID: 16348 RVA: 0x000E9748 File Offset: 0x000E7948
	[HideInInspector]
	public virtual void LateUpdate()
	{
		if (this.layout != null && this.layout.HasPendingLayoutRequest)
		{
			this.layout.PerformLayout();
		}
	}

	// Token: 0x06003FDD RID: 16349 RVA: 0x000E977C File Offset: 0x000E797C
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

	// Token: 0x06003FDE RID: 16350 RVA: 0x000E9840 File Offset: 0x000E7A40
	protected internal void SetControlIndex(global::dfControl child, int zindex)
	{
		global::dfControl dfControl = this.controls.FirstOrDefault((global::dfControl c) => c.zindex == zindex && c != child);
		if (dfControl != null)
		{
			dfControl.zindex = this.controls.IndexOf(child);
		}
		child.zindex = zindex;
		this.RebuildControlOrder();
	}

	// Token: 0x06003FDF RID: 16351 RVA: 0x000E98B4 File Offset: 0x000E7AB4
	public T AddControl<T>() where T : global::dfControl
	{
		return (T)((object)this.AddControl(typeof(T)));
	}

	// Token: 0x06003FE0 RID: 16352 RVA: 0x000E98CC File Offset: 0x000E7ACC
	public global::dfControl AddControl(Type ControlType)
	{
		if (!typeof(global::dfControl).IsAssignableFrom(ControlType))
		{
			throw new InvalidCastException();
		}
		GameObject gameObject = new GameObject(ControlType.Name);
		gameObject.transform.parent = base.transform;
		gameObject.layer = base.gameObject.layer;
		Vector2 vector = this.Size * this.PixelsToUnits() * 0.5f;
		gameObject.transform.localPosition = new Vector3(vector.x, vector.y, 0f);
		global::dfControl dfControl = gameObject.AddComponent(ControlType) as global::dfControl;
		dfControl.parent = this;
		dfControl.zindex = -1;
		this.AddControl(dfControl);
		return dfControl;
	}

	// Token: 0x06003FE1 RID: 16353 RVA: 0x000E9984 File Offset: 0x000E7B84
	public void AddControl(global::dfControl child)
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

	// Token: 0x06003FE2 RID: 16354 RVA: 0x000E9A1C File Offset: 0x000E7C1C
	private int getMaxZOrder()
	{
		int num = -1;
		for (int i = 0; i < this.controls.Count; i++)
		{
			num = Mathf.Max(this.controls[i].zindex, num);
		}
		return num;
	}

	// Token: 0x06003FE3 RID: 16355 RVA: 0x000E9A60 File Offset: 0x000E7C60
	public void RemoveControl(global::dfControl child)
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

	// Token: 0x06003FE4 RID: 16356 RVA: 0x000E9AB8 File Offset: 0x000E7CB8
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

	// Token: 0x06003FE5 RID: 16357 RVA: 0x000E9B4C File Offset: 0x000E7D4C
	internal void updateControlHierarchy(bool force = false)
	{
		int childCount = base.transform.childCount;
		if (!force && childCount == this.cachedChildCount)
		{
			return;
		}
		this.cachedChildCount = childCount;
		global::dfList<global::dfControl> childControls = this.getChildControls();
		for (int i = 0; i < childControls.Count; i++)
		{
			global::dfControl dfControl = childControls[i];
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
			global::dfControl dfControl2 = this.controls[j];
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

	// Token: 0x06003FE6 RID: 16358 RVA: 0x000E9C74 File Offset: 0x000E7E74
	private global::dfList<global::dfControl> getChildControls()
	{
		int childCount = base.transform.childCount;
		global::dfList<global::dfControl> dfList = global::dfList<global::dfControl>.Obtain();
		dfList.EnsureCapacity(childCount);
		for (int i = 0; i < childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			if (child.gameObject.activeSelf)
			{
				global::dfControl component = child.GetComponent<global::dfControl>();
				if (component != null)
				{
					dfList.Add(component);
				}
			}
		}
		return dfList;
	}

	// Token: 0x06003FE7 RID: 16359 RVA: 0x000E9CEC File Offset: 0x000E7EEC
	private void ensureLayoutExists()
	{
		if (this.layout == null)
		{
			this.layout = new global::dfControl.AnchorLayout(global::dfAnchorStyle.Top | global::dfAnchorStyle.Left, this);
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

	// Token: 0x06003FE8 RID: 16360 RVA: 0x000E9D74 File Offset: 0x000E7F74
	protected internal void updateVersion()
	{
		this.version = (global::dfControl.versionCounter += 1u);
	}

	// Token: 0x06003FE9 RID: 16361 RVA: 0x000E9D8C File Offset: 0x000E7F8C
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

	// Token: 0x06003FEA RID: 16362 RVA: 0x000E9DFC File Offset: 0x000E7FFC
	private void initializeControl()
	{
		if (this.renderOrder == -1)
		{
			this.renderOrder = this.ZOrder;
		}
		if (base.transform.parent != null)
		{
			global::dfControl component = base.transform.parent.GetComponent<global::dfControl>();
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

	// Token: 0x06003FEB RID: 16363 RVA: 0x000E9EB8 File Offset: 0x000E80B8
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
		global::dfGUIManager dfGUIManager = this.GetManager();
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

	// Token: 0x06003FEC RID: 16364 RVA: 0x000EA03C File Offset: 0x000E823C
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
		global::dfGUIManager dfGUIManager = this.GetManager();
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

	// Token: 0x06003FED RID: 16365 RVA: 0x000EA204 File Offset: 0x000E8404
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

	// Token: 0x06003FEE RID: 16366 RVA: 0x000EA270 File Offset: 0x000E8470
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

	// Token: 0x06003FEF RID: 16367 RVA: 0x000EA2DC File Offset: 0x000E84DC
	public int CompareTo(global::dfControl other)
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

	// Token: 0x0400212A RID: 8490
	private const float MINIMUM_OPACITY = 0.0125f;

	// Token: 0x0400212B RID: 8491
	private static uint versionCounter;

	// Token: 0x0400212C RID: 8492
	[SerializeField]
	protected bool isEnabled = true;

	// Token: 0x0400212D RID: 8493
	[SerializeField]
	protected bool isVisible = true;

	// Token: 0x0400212E RID: 8494
	[SerializeField]
	protected bool isInteractive = true;

	// Token: 0x0400212F RID: 8495
	[SerializeField]
	protected string tooltip;

	// Token: 0x04002130 RID: 8496
	[SerializeField]
	protected global::dfPivotPoint pivot;

	// Token: 0x04002131 RID: 8497
	[SerializeField]
	protected int zindex = -1;

	// Token: 0x04002132 RID: 8498
	[SerializeField]
	protected Color32 color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x04002133 RID: 8499
	[SerializeField]
	protected Color32 disabledColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x04002134 RID: 8500
	[SerializeField]
	protected Vector2 size = Vector2.zero;

	// Token: 0x04002135 RID: 8501
	[SerializeField]
	protected Vector2 minSize = Vector2.zero;

	// Token: 0x04002136 RID: 8502
	[SerializeField]
	protected Vector2 maxSize = Vector2.zero;

	// Token: 0x04002137 RID: 8503
	[SerializeField]
	protected bool clipChildren;

	// Token: 0x04002138 RID: 8504
	[SerializeField]
	protected int tabIndex = -1;

	// Token: 0x04002139 RID: 8505
	[SerializeField]
	protected bool canFocus;

	// Token: 0x0400213A RID: 8506
	[SerializeField]
	protected global::dfControl.AnchorLayout layout;

	// Token: 0x0400213B RID: 8507
	[SerializeField]
	protected int renderOrder = -1;

	// Token: 0x0400213C RID: 8508
	[SerializeField]
	protected bool isLocalized;

	// Token: 0x0400213D RID: 8509
	[SerializeField]
	protected Vector2 hotZoneScale = Vector2.one;

	// Token: 0x0400213E RID: 8510
	protected bool isControlInvalidated = true;

	// Token: 0x0400213F RID: 8511
	protected global::dfControl parent;

	// Token: 0x04002140 RID: 8512
	protected global::dfList<global::dfControl> controls = global::dfList<global::dfControl>.Obtain();

	// Token: 0x04002141 RID: 8513
	protected global::dfGUIManager manager;

	// Token: 0x04002142 RID: 8514
	protected global::dfLanguageManager languageManager;

	// Token: 0x04002143 RID: 8515
	protected bool languageManagerChecked;

	// Token: 0x04002144 RID: 8516
	protected int cachedChildCount;

	// Token: 0x04002145 RID: 8517
	protected Vector3 cachedPosition = Vector3.one * float.MinValue;

	// Token: 0x04002146 RID: 8518
	protected Quaternion cachedRotation = Quaternion.identity;

	// Token: 0x04002147 RID: 8519
	protected Vector3 cachedScale = Vector3.one;

	// Token: 0x04002148 RID: 8520
	protected float cachedPixelSize;

	// Token: 0x04002149 RID: 8521
	protected global::dfRenderData renderData;

	// Token: 0x0400214A RID: 8522
	protected bool isMouseHovering;

	// Token: 0x0400214B RID: 8523
	private object tag;

	// Token: 0x0400214C RID: 8524
	protected bool isDisposing;

	// Token: 0x0400214D RID: 8525
	private bool performingLayout;

	// Token: 0x0400214E RID: 8526
	private Vector3[] cachedCorners = new Vector3[4];

	// Token: 0x0400214F RID: 8527
	private Plane[] cachedClippingPlanes = new Plane[4];

	// Token: 0x04002150 RID: 8528
	private uint version;

	// Token: 0x04002151 RID: 8529
	private bool rendering;

	// Token: 0x0200076D RID: 1901
	[Serializable]
	protected class AnchorLayout
	{
		// Token: 0x06003FF3 RID: 16371 RVA: 0x000EA3C0 File Offset: 0x000E85C0
		internal AnchorLayout(global::dfAnchorStyle anchorStyle)
		{
			this.anchorStyle = anchorStyle;
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000EA3D0 File Offset: 0x000E85D0
		internal AnchorLayout(global::dfAnchorStyle anchorStyle, global::dfControl owner) : this(anchorStyle)
		{
			this.Attach(owner);
			this.Reset(false);
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06003FF5 RID: 16373 RVA: 0x000EA3E8 File Offset: 0x000E85E8
		// (set) Token: 0x06003FF6 RID: 16374 RVA: 0x000EA3F0 File Offset: 0x000E85F0
		internal global::dfAnchorStyle AnchorStyle
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

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06003FF7 RID: 16375 RVA: 0x000EA40C File Offset: 0x000E860C
		internal bool IsPerformingLayout
		{
			get
			{
				return this.performingLayout;
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x000EA414 File Offset: 0x000E8614
		internal bool IsLayoutSuspended
		{
			get
			{
				return this.suspendLayoutCounter > 0;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06003FF9 RID: 16377 RVA: 0x000EA420 File Offset: 0x000E8620
		internal bool HasPendingLayoutRequest
		{
			get
			{
				return this.pendingLayoutRequest;
			}
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000EA428 File Offset: 0x000E8628
		internal void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.owner = null;
			}
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x000EA444 File Offset: 0x000E8644
		internal void SuspendLayout()
		{
			this.suspendLayoutCounter++;
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x000EA454 File Offset: 0x000E8654
		internal void ResumeLayout()
		{
			bool flag = this.suspendLayoutCounter > 0;
			this.suspendLayoutCounter = Mathf.Max(0, this.suspendLayoutCounter - 1);
			if (flag && this.suspendLayoutCounter == 0 && this.pendingLayoutRequest)
			{
				this.PerformLayout();
			}
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x000EA4A4 File Offset: 0x000E86A4
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

		// Token: 0x06003FFE RID: 16382 RVA: 0x000EA4DC File Offset: 0x000E86DC
		internal void Attach(global::dfControl ownerControl)
		{
			this.owner = ownerControl;
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x000EA4E8 File Offset: 0x000E86E8
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
			if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Proportional))
			{
				this.resetLayoutProportional();
			}
			else
			{
				this.resetLayoutAbsolute();
			}
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x000EA59C File Offset: 0x000E879C
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
				this.margins = new global::dfAnchorMargins();
			}
			this.margins.left = x / parentSize.x;
			this.margins.right = num / parentSize.x;
			this.margins.top = y / parentSize.y;
			this.margins.bottom = num2 / parentSize.y;
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x000EA65C File Offset: 0x000E885C
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
				this.margins = new global::dfAnchorMargins();
			}
			this.margins.left = x;
			this.margins.right = right;
			this.margins.top = y;
			this.margins.bottom = bottom;
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x000EA70C File Offset: 0x000E890C
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
				if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Proportional))
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

		// Token: 0x06004003 RID: 16387 RVA: 0x000EA7E0 File Offset: 0x000E89E0
		private string getPath(global::dfControl owner)
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

		// Token: 0x06004004 RID: 16388 RVA: 0x000EA83C File Offset: 0x000E8A3C
		private void performLayoutProportional(Vector2 parentSize, Vector2 controlSize)
		{
			float x = this.margins.left * parentSize.x;
			float num = this.margins.right * parentSize.x;
			float y = this.margins.top * parentSize.y;
			float num2 = this.margins.bottom * parentSize.y;
			Vector3 relativePosition = this.owner.RelativePosition;
			Vector2 size = controlSize;
			if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Left))
			{
				relativePosition.x = x;
				if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Right))
				{
					size.x = (this.margins.right - this.margins.left) * parentSize.x;
				}
			}
			else if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Right))
			{
				relativePosition.x = num - controlSize.x;
			}
			else if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.CenterHorizontal))
			{
				relativePosition.x = (parentSize.x - controlSize.x) * 0.5f;
			}
			if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Top))
			{
				relativePosition.y = y;
				if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Bottom))
				{
					size.y = (this.margins.bottom - this.margins.top) * parentSize.y;
				}
			}
			else if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Bottom))
			{
				relativePosition.y = num2 - controlSize.y;
			}
			else if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.CenterVertical))
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

		// Token: 0x06004005 RID: 16389 RVA: 0x000EAA3C File Offset: 0x000E8C3C
		private void performLayoutAbsolute(Vector2 parentSize, Vector2 controlSize)
		{
			float num = this.margins.left;
			float num2 = this.margins.top;
			float num3 = num + controlSize.x;
			float num4 = num2 + controlSize.y;
			if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.CenterHorizontal))
			{
				num = (float)Mathf.RoundToInt((parentSize.x - controlSize.x) * 0.5f);
				num3 = (float)Mathf.RoundToInt(num + controlSize.x);
			}
			else
			{
				if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Left))
				{
					num = this.margins.left;
					num3 = num + controlSize.x;
				}
				if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Right))
				{
					num3 = parentSize.x - this.margins.right;
					if (!this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Left))
					{
						num = num3 - controlSize.x;
					}
				}
			}
			if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.CenterVertical))
			{
				num2 = (float)Mathf.RoundToInt((parentSize.y - controlSize.y) * 0.5f);
				num4 = (float)Mathf.RoundToInt(num2 + controlSize.y);
			}
			else
			{
				if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Top))
				{
					num2 = this.margins.top;
					num4 = num2 + controlSize.y;
				}
				if (this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Bottom))
				{
					num4 = parentSize.y - this.margins.bottom;
					if (!this.anchorStyle.IsFlagSet(global::dfAnchorStyle.Top))
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

		// Token: 0x06004006 RID: 16390 RVA: 0x000EAC0C File Offset: 0x000E8E0C
		private Vector2 getParentSize()
		{
			global::dfControl component = this.owner.transform.parent.GetComponent<global::dfControl>();
			if (component != null)
			{
				return component.Size;
			}
			global::dfGUIManager manager = this.owner.GetManager();
			return manager.GetScreenSize();
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x000EAC58 File Offset: 0x000E8E58
		public override string ToString()
		{
			if (this.owner == null)
			{
				return "NO OWNER FOR ANCHOR";
			}
			global::dfControl parent = this.owner.parent;
			return string.Format("{0}.{1} - {2}", (!(parent != null)) ? "SCREEN" : parent.name, this.owner.name, this.margins);
		}

		// Token: 0x04002177 RID: 8567
		[SerializeField]
		protected global::dfAnchorStyle anchorStyle;

		// Token: 0x04002178 RID: 8568
		[SerializeField]
		protected global::dfAnchorMargins margins;

		// Token: 0x04002179 RID: 8569
		[SerializeField]
		protected global::dfControl owner;

		// Token: 0x0400217A RID: 8570
		private int suspendLayoutCounter;

		// Token: 0x0400217B RID: 8571
		private bool performingLayout;

		// Token: 0x0400217C RID: 8572
		private bool disposed;

		// Token: 0x0400217D RID: 8573
		private bool pendingLayoutRequest;
	}
}
