using System;
using UnityEngine;

// Token: 0x02000855 RID: 2133
[AddComponentMenu("NGUI/Interaction/Forward Events")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x060049A8 RID: 18856 RVA: 0x0011A5BC File Offset: 0x001187BC
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, 1);
		}
	}

	// Token: 0x060049A9 RID: 18857 RVA: 0x0011A5F4 File Offset: 0x001187F4
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, 1);
		}
	}

	// Token: 0x060049AA RID: 18858 RVA: 0x0011A62C File Offset: 0x0011882C
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", 1);
		}
	}

	// Token: 0x060049AB RID: 18859 RVA: 0x0011A65C File Offset: 0x0011885C
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", 1);
		}
	}

	// Token: 0x060049AC RID: 18860 RVA: 0x0011A68C File Offset: 0x0011888C
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, 1);
		}
	}

	// Token: 0x060049AD RID: 18861 RVA: 0x0011A6C4 File Offset: 0x001188C4
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, 1);
		}
	}

	// Token: 0x060049AE RID: 18862 RVA: 0x0011A6FC File Offset: 0x001188FC
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, 1);
		}
	}

	// Token: 0x060049AF RID: 18863 RVA: 0x0011A738 File Offset: 0x00118938
	private void OnInput(string text)
	{
		if (this.onInput && this.target != null)
		{
			this.target.SendMessage("OnInput", text, 1);
		}
	}

	// Token: 0x060049B0 RID: 18864 RVA: 0x0011A774 File Offset: 0x00118974
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", 1);
		}
	}

	// Token: 0x040027CF RID: 10191
	public GameObject target;

	// Token: 0x040027D0 RID: 10192
	public bool onHover;

	// Token: 0x040027D1 RID: 10193
	public bool onPress;

	// Token: 0x040027D2 RID: 10194
	public bool onClick;

	// Token: 0x040027D3 RID: 10195
	public bool onDoubleClick;

	// Token: 0x040027D4 RID: 10196
	public bool onSelect;

	// Token: 0x040027D5 RID: 10197
	public bool onDrag;

	// Token: 0x040027D6 RID: 10198
	public bool onDrop;

	// Token: 0x040027D7 RID: 10199
	public bool onInput;

	// Token: 0x040027D8 RID: 10200
	public bool onSubmit;
}
