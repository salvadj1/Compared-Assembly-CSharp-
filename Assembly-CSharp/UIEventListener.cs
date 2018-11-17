using System;
using UnityEngine;

// Token: 0x0200087F RID: 2175
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x06004ACA RID: 19146 RVA: 0x00121980 File Offset: 0x0011FB80
	private void OnSubmit()
	{
		if (this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x06004ACB RID: 19147 RVA: 0x001219A0 File Offset: 0x0011FBA0
	private void OnClick()
	{
		if (this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x06004ACC RID: 19148 RVA: 0x001219C0 File Offset: 0x0011FBC0
	private void OnDoubleClick()
	{
		if (this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x06004ACD RID: 19149 RVA: 0x001219E0 File Offset: 0x0011FBE0
	private void OnHover(bool isOver)
	{
		if (this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x06004ACE RID: 19150 RVA: 0x00121A00 File Offset: 0x0011FC00
	private void OnPress(bool isPressed)
	{
		if (this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x06004ACF RID: 19151 RVA: 0x00121A20 File Offset: 0x0011FC20
	private void OnSelect(bool selected)
	{
		if (this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x06004AD0 RID: 19152 RVA: 0x00121A40 File Offset: 0x0011FC40
	private void OnScroll(float delta)
	{
		if (this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x06004AD1 RID: 19153 RVA: 0x00121A60 File Offset: 0x0011FC60
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x06004AD2 RID: 19154 RVA: 0x00121A80 File Offset: 0x0011FC80
	private void OnDrop(GameObject go)
	{
		if (this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x06004AD3 RID: 19155 RVA: 0x00121AA0 File Offset: 0x0011FCA0
	private void OnInput(string text)
	{
		if (this.onInput != null)
		{
			this.onInput(base.gameObject, text);
		}
	}

	// Token: 0x06004AD4 RID: 19156 RVA: 0x00121AC0 File Offset: 0x0011FCC0
	public static global::UIEventListener Get(GameObject go)
	{
		global::UIEventListener uieventListener = go.GetComponent<global::UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<global::UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x06004AD5 RID: 19157 RVA: 0x00121AE8 File Offset: 0x0011FCE8
	[Obsolete("Please use UIEventListener.Get instead of UIEventListener.Add")]
	public static global::UIEventListener Add(GameObject go)
	{
		return global::UIEventListener.Get(go);
	}

	// Token: 0x040028CC RID: 10444
	public object parameter;

	// Token: 0x040028CD RID: 10445
	public global::UIEventListener.VoidDelegate onSubmit;

	// Token: 0x040028CE RID: 10446
	public global::UIEventListener.VoidDelegate onClick;

	// Token: 0x040028CF RID: 10447
	public global::UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x040028D0 RID: 10448
	public global::UIEventListener.BoolDelegate onHover;

	// Token: 0x040028D1 RID: 10449
	public global::UIEventListener.BoolDelegate onPress;

	// Token: 0x040028D2 RID: 10450
	public global::UIEventListener.BoolDelegate onSelect;

	// Token: 0x040028D3 RID: 10451
	public global::UIEventListener.FloatDelegate onScroll;

	// Token: 0x040028D4 RID: 10452
	public global::UIEventListener.VectorDelegate onDrag;

	// Token: 0x040028D5 RID: 10453
	public global::UIEventListener.ObjectDelegate onDrop;

	// Token: 0x040028D6 RID: 10454
	public global::UIEventListener.StringDelegate onInput;

	// Token: 0x02000880 RID: 2176
	// (Invoke) Token: 0x06004AD7 RID: 19159
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x02000881 RID: 2177
	// (Invoke) Token: 0x06004ADB RID: 19163
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x02000882 RID: 2178
	// (Invoke) Token: 0x06004ADF RID: 19167
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x02000883 RID: 2179
	// (Invoke) Token: 0x06004AE3 RID: 19171
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x02000884 RID: 2180
	// (Invoke) Token: 0x06004AE7 RID: 19175
	public delegate void StringDelegate(GameObject go, string text);

	// Token: 0x02000885 RID: 2181
	// (Invoke) Token: 0x06004AEB RID: 19179
	public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);
}
