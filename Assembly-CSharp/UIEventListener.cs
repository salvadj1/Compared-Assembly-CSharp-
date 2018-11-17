using System;
using UnityEngine;

// Token: 0x0200079A RID: 1946
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x0600465D RID: 18013 RVA: 0x00118000 File Offset: 0x00116200
	private void OnSubmit()
	{
		if (this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x0600465E RID: 18014 RVA: 0x00118020 File Offset: 0x00116220
	private void OnClick()
	{
		if (this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x0600465F RID: 18015 RVA: 0x00118040 File Offset: 0x00116240
	private void OnDoubleClick()
	{
		if (this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x06004660 RID: 18016 RVA: 0x00118060 File Offset: 0x00116260
	private void OnHover(bool isOver)
	{
		if (this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x06004661 RID: 18017 RVA: 0x00118080 File Offset: 0x00116280
	private void OnPress(bool isPressed)
	{
		if (this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x06004662 RID: 18018 RVA: 0x001180A0 File Offset: 0x001162A0
	private void OnSelect(bool selected)
	{
		if (this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x06004663 RID: 18019 RVA: 0x001180C0 File Offset: 0x001162C0
	private void OnScroll(float delta)
	{
		if (this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x06004664 RID: 18020 RVA: 0x001180E0 File Offset: 0x001162E0
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x06004665 RID: 18021 RVA: 0x00118100 File Offset: 0x00116300
	private void OnDrop(GameObject go)
	{
		if (this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x06004666 RID: 18022 RVA: 0x00118120 File Offset: 0x00116320
	private void OnInput(string text)
	{
		if (this.onInput != null)
		{
			this.onInput(base.gameObject, text);
		}
	}

	// Token: 0x06004667 RID: 18023 RVA: 0x00118140 File Offset: 0x00116340
	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uieventListener = go.GetComponent<UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x06004668 RID: 18024 RVA: 0x00118168 File Offset: 0x00116368
	[Obsolete("Please use UIEventListener.Get instead of UIEventListener.Add")]
	public static UIEventListener Add(GameObject go)
	{
		return UIEventListener.Get(go);
	}

	// Token: 0x04002695 RID: 9877
	public object parameter;

	// Token: 0x04002696 RID: 9878
	public UIEventListener.VoidDelegate onSubmit;

	// Token: 0x04002697 RID: 9879
	public UIEventListener.VoidDelegate onClick;

	// Token: 0x04002698 RID: 9880
	public UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x04002699 RID: 9881
	public UIEventListener.BoolDelegate onHover;

	// Token: 0x0400269A RID: 9882
	public UIEventListener.BoolDelegate onPress;

	// Token: 0x0400269B RID: 9883
	public UIEventListener.BoolDelegate onSelect;

	// Token: 0x0400269C RID: 9884
	public UIEventListener.FloatDelegate onScroll;

	// Token: 0x0400269D RID: 9885
	public UIEventListener.VectorDelegate onDrag;

	// Token: 0x0400269E RID: 9886
	public UIEventListener.ObjectDelegate onDrop;

	// Token: 0x0400269F RID: 9887
	public UIEventListener.StringDelegate onInput;

	// Token: 0x020008E7 RID: 2279
	// (Invoke) Token: 0x06004D74 RID: 19828
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x020008E8 RID: 2280
	// (Invoke) Token: 0x06004D78 RID: 19832
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x020008E9 RID: 2281
	// (Invoke) Token: 0x06004D7C RID: 19836
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x020008EA RID: 2282
	// (Invoke) Token: 0x06004D80 RID: 19840
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x020008EB RID: 2283
	// (Invoke) Token: 0x06004D84 RID: 19844
	public delegate void StringDelegate(GameObject go, string text);

	// Token: 0x020008EC RID: 2284
	// (Invoke) Token: 0x06004D88 RID: 19848
	public delegate void ObjectDelegate(GameObject go, GameObject draggedObject);
}
