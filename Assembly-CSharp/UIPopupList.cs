using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200085B RID: 2139
[AddComponentMenu("NGUI/Interaction/Popup List")]
[ExecuteInEditMode]
public class UIPopupList : MonoBehaviour
{
	// Token: 0x17000E0A RID: 3594
	// (get) Token: 0x060049BE RID: 18878 RVA: 0x0011AE00 File Offset: 0x00119000
	public bool isOpen
	{
		get
		{
			return this.mChild != null;
		}
	}

	// Token: 0x17000E0B RID: 3595
	// (get) Token: 0x060049BF RID: 18879 RVA: 0x0011AE10 File Offset: 0x00119010
	// (set) Token: 0x060049C0 RID: 18880 RVA: 0x0011AE18 File Offset: 0x00119018
	public string selection
	{
		get
		{
			return this.mSelectedItem;
		}
		set
		{
			if (this.mSelectedItem != value)
			{
				this.mSelectedItem = value;
				if (this.textLabel != null)
				{
					this.textLabel.text = ((!this.isLocalized || !(global::Localization.instance != null)) ? value : global::Localization.instance.Get(value));
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName) && Application.isPlaying)
				{
					this.eventReceiver.SendMessage(this.functionName, this.mSelectedItem, 1);
				}
			}
		}
	}

	// Token: 0x17000E0C RID: 3596
	// (get) Token: 0x060049C1 RID: 18881 RVA: 0x0011AEC8 File Offset: 0x001190C8
	// (set) Token: 0x060049C2 RID: 18882 RVA: 0x0011AEF4 File Offset: 0x001190F4
	private bool handleEvents
	{
		get
		{
			global::UIButtonKeys component = base.GetComponent<global::UIButtonKeys>();
			return component == null || !component.enabled;
		}
		set
		{
			global::UIButtonKeys component = base.GetComponent<global::UIButtonKeys>();
			if (component != null)
			{
				component.enabled = !value;
			}
		}
	}

	// Token: 0x060049C3 RID: 18883 RVA: 0x0011AF20 File Offset: 0x00119120
	private void Start()
	{
		if (string.IsNullOrEmpty(this.mSelectedItem))
		{
			if (this.items.Count > 0)
			{
				this.selection = this.items[0];
			}
		}
		else
		{
			string selection = this.mSelectedItem;
			this.mSelectedItem = null;
			this.selection = selection;
		}
	}

	// Token: 0x060049C4 RID: 18884 RVA: 0x0011AF7C File Offset: 0x0011917C
	private void OnLocalize(global::Localization loc)
	{
		if (this.isLocalized && this.textLabel != null)
		{
			this.textLabel.text = loc.Get(this.mSelectedItem);
		}
	}

	// Token: 0x060049C5 RID: 18885 RVA: 0x0011AFB4 File Offset: 0x001191B4
	private void Highlight(global::UILabel lbl, bool instant)
	{
		if (this.mHighlight != null)
		{
			global::TweenPosition component = lbl.GetComponent<global::TweenPosition>();
			if (component != null && component.enabled)
			{
				return;
			}
			this.mHighlightedLabel = lbl;
			global::UIAtlas.Sprite sprite = this.mHighlight.sprite;
			float num = sprite.inner.xMin - sprite.outer.xMin;
			float num2 = sprite.inner.yMin - sprite.outer.yMin;
			Vector3 vector = lbl.cachedTransform.localPosition + new Vector3(-num, num2, 0f);
			if (instant || !this.isAnimated)
			{
				this.mHighlight.cachedTransform.localPosition = vector;
			}
			else
			{
				global::TweenPosition.Begin(this.mHighlight.gameObject, 0.1f, vector).method = global::UITweener.Method.EaseOut;
			}
		}
	}

	// Token: 0x060049C6 RID: 18886 RVA: 0x0011B098 File Offset: 0x00119298
	private void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			global::UILabel component = go.GetComponent<global::UILabel>();
			this.Highlight(component, false);
		}
	}

	// Token: 0x060049C7 RID: 18887 RVA: 0x0011B0BC File Offset: 0x001192BC
	private void Select(global::UILabel lbl, bool instant)
	{
		this.Highlight(lbl, instant);
		global::UIEventListener component = lbl.gameObject.GetComponent<global::UIEventListener>();
		this.selection = (component.parameter as string);
		global::UIButtonSound[] components = base.GetComponents<global::UIButtonSound>();
		int i = 0;
		int num = components.Length;
		while (i < num)
		{
			global::UIButtonSound uibuttonSound = components[i];
			if (uibuttonSound.trigger == global::UIButtonSound.Trigger.OnClick)
			{
				global::NGUITools.PlaySound(uibuttonSound.audioClip, uibuttonSound.volume, 1f);
			}
			i++;
		}
	}

	// Token: 0x060049C8 RID: 18888 RVA: 0x0011B138 File Offset: 0x00119338
	private void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.Select(go.GetComponent<global::UILabel>(), true);
		}
	}

	// Token: 0x060049C9 RID: 18889 RVA: 0x0011B150 File Offset: 0x00119350
	private void OnKey(KeyCode key)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.handleEvents)
		{
			int num = this.mLabelList.IndexOf(this.mHighlightedLabel);
			if (key == 273)
			{
				if (num > 0)
				{
					this.Select(this.mLabelList[num - 1], false);
				}
			}
			else if (key == 274)
			{
				if (num + 1 < this.mLabelList.Count)
				{
					this.Select(this.mLabelList[num + 1], false);
				}
			}
			else if (key == 27)
			{
				this.OnSelect(false);
			}
		}
	}

	// Token: 0x060049CA RID: 18890 RVA: 0x0011B20C File Offset: 0x0011940C
	private void OnSelect(bool isSelected)
	{
		if (!isSelected && this.mChild != null)
		{
			this.mLabelList.Clear();
			this.handleEvents = false;
			if (this.isAnimated)
			{
				global::UIWidget[] componentsInChildren = this.mChild.GetComponentsInChildren<global::UIWidget>();
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					global::UIWidget uiwidget = componentsInChildren[i];
					Color color = uiwidget.color;
					color.a = 0f;
					global::TweenColor.Begin(uiwidget.gameObject, 0.15f, color).method = global::UITweener.Method.EaseOut;
					i++;
				}
				global::NGUITools.SetAllowClickChildren(this.mChild, false);
				global::UpdateManager.AddDestroy(this.mChild, 0.15f);
			}
			else
			{
				Object.Destroy(this.mChild);
			}
			this.mBackground = null;
			this.mHighlight = null;
			this.mChild = null;
		}
	}

	// Token: 0x060049CB RID: 18891 RVA: 0x0011B2E0 File Offset: 0x001194E0
	private void AnimateColor(global::UIWidget widget)
	{
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		global::TweenColor.Begin(widget.gameObject, 0.15f, color).method = global::UITweener.Method.EaseOut;
	}

	// Token: 0x060049CC RID: 18892 RVA: 0x0011B330 File Offset: 0x00119530
	private void AnimatePosition(global::UIWidget widget, bool placeAbove, float bottom)
	{
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 localPosition2 = (!placeAbove) ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, bottom, localPosition.z);
		widget.cachedTransform.localPosition = localPosition2;
		GameObject gameObject = widget.gameObject;
		global::TweenPosition.Begin(gameObject, 0.15f, localPosition).method = global::UITweener.Method.EaseOut;
	}

	// Token: 0x060049CD RID: 18893 RVA: 0x0011B3A8 File Offset: 0x001195A8
	private void AnimateScale(global::UIWidget widget, bool placeAbove, float bottom)
	{
		GameObject gameObject = widget.gameObject;
		Transform cachedTransform = widget.cachedTransform;
		float num = (float)this.font.size * this.textScale + this.mBgBorder * 2f;
		Vector3 localScale = cachedTransform.localScale;
		cachedTransform.localScale = new Vector3(localScale.x, num, localScale.z);
		global::TweenScale.Begin(gameObject, 0.15f, localScale).method = global::UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.localPosition;
			cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - localScale.y + num, localPosition.z);
			global::TweenPosition.Begin(gameObject, 0.15f, localPosition).method = global::UITweener.Method.EaseOut;
		}
	}

	// Token: 0x060049CE RID: 18894 RVA: 0x0011B464 File Offset: 0x00119664
	private void Animate(global::UIWidget widget, bool placeAbove, float bottom)
	{
		this.AnimateColor(widget);
		this.AnimatePosition(widget, placeAbove, bottom);
	}

	// Token: 0x060049CF RID: 18895 RVA: 0x0011B478 File Offset: 0x00119678
	private void OnClick()
	{
		if (this.mChild == null && this.atlas != null && this.font != null && this.items.Count > 1)
		{
			this.mLabelList.Clear();
			this.handleEvents = true;
			if (this.mPanel == null)
			{
				this.mPanel = global::UIPanel.Find(base.transform, true);
			}
			Transform transform = base.transform;
			global::AABBox aabbox = global::NGUIMath.CalculateRelativeWidgetBounds(transform.parent, transform);
			this.mChild = new GameObject("Drop-down List");
			this.mChild.layer = base.gameObject.layer;
			Transform transform2 = this.mChild.transform;
			transform2.parent = transform.parent;
			transform2.localPosition = aabbox.min;
			transform2.localRotation = Quaternion.identity;
			transform2.localScale = Vector3.one;
			this.mBackground = global::NGUITools.AddSprite(this.mChild, this.atlas, this.backgroundSprite);
			this.mBackground.pivot = global::UIWidget.Pivot.TopLeft;
			this.mBackground.depth = global::NGUITools.CalculateNextDepth(this.mPanel.gameObject);
			this.mBackground.color = this.backgroundColor;
			Vector4 border = this.mBackground.border;
			this.mBgBorder = border.y;
			this.mBackground.cachedTransform.localPosition = new Vector3(0f, border.y, 0f);
			this.mHighlight = global::NGUITools.AddSprite(this.mChild, this.atlas, this.highlightSprite);
			this.mHighlight.pivot = global::UIWidget.Pivot.TopLeft;
			this.mHighlight.color = this.highlightColor;
			global::UIAtlas.Sprite sprite = this.mHighlight.sprite;
			float num = sprite.inner.yMin - sprite.outer.yMin;
			float num2 = (float)this.font.size * this.textScale;
			float num3 = 0f;
			float num4 = -this.padding.y;
			List<global::UILabel> list = new List<global::UILabel>();
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				string text = this.items[i];
				global::UILabel uilabel = global::NGUITools.AddWidget<global::UILabel>(this.mChild);
				uilabel.pivot = global::UIWidget.Pivot.TopLeft;
				uilabel.font = this.font;
				uilabel.text = ((!this.isLocalized || !(global::Localization.instance != null)) ? text : global::Localization.instance.Get(text));
				uilabel.color = this.textColor;
				uilabel.cachedTransform.localPosition = new Vector3(border.x, num4, 0f);
				uilabel.MakePixelPerfect();
				if (this.textScale != 1f)
				{
					Vector3 localScale = uilabel.cachedTransform.localScale;
					uilabel.cachedTransform.localScale = localScale * this.textScale;
				}
				list.Add(uilabel);
				num4 -= num2;
				num4 -= this.padding.y;
				num3 = Mathf.Max(num3, uilabel.relativeSize.x * num2);
				global::UIEventListener uieventListener = global::UIEventListener.Get(uilabel.gameObject);
				uieventListener.onHover = new global::UIEventListener.BoolDelegate(this.OnItemHover);
				uieventListener.onPress = new global::UIEventListener.BoolDelegate(this.OnItemPress);
				uieventListener.parameter = text;
				if (this.mSelectedItem == text)
				{
					this.Highlight(uilabel, true);
				}
				this.mLabelList.Add(uilabel);
				i++;
			}
			num3 = Mathf.Max(num3, aabbox.size.x - border.x * 2f);
			Vector3 center;
			center..ctor(num3 * 0.5f / num2, -0.5f, 0f);
			Vector3 size;
			size..ctor(num3 / num2, (num2 + this.padding.y) / num2, 1f);
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				global::UILabel uilabel2 = list[j];
				global::UIHotSpot uihotSpot = global::NGUITools.AddWidgetHotSpot(uilabel2.gameObject);
				center.z = uihotSpot.center.z;
				uihotSpot.center = center;
				uihotSpot.size = size;
				j++;
			}
			num3 += border.x * 2f;
			num4 -= border.y;
			this.mBackground.cachedTransform.localScale = new Vector3(num3, -num4 + border.y, 1f);
			this.mHighlight.cachedTransform.localScale = new Vector3(num3 - border.x * 2f + (sprite.inner.xMin - sprite.outer.xMin) * 2f, num2 + num * 2f, 1f);
			bool flag = this.position == global::UIPopupList.Position.Above;
			if (this.position == global::UIPopupList.Position.Auto)
			{
				global::UICamera uicamera = global::UICamera.FindCameraForLayer(base.gameObject.layer);
				if (uicamera != null)
				{
					flag = (uicamera.cachedCamera.WorldToViewportPoint(transform.position).y < 0.5f);
				}
			}
			if (this.isAnimated)
			{
				float bottom = num4 + num2;
				this.Animate(this.mHighlight, flag, bottom);
				int k = 0;
				int count3 = list.Count;
				while (k < count3)
				{
					this.Animate(list[k], flag, bottom);
					k++;
				}
				this.AnimateColor(this.mBackground);
				this.AnimateScale(this.mBackground, flag, bottom);
			}
			if (flag)
			{
				transform2.localPosition = new Vector3(aabbox.min.x, aabbox.max.y - num4 - border.y, aabbox.min.z);
			}
		}
		else
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x040027F1 RID: 10225
	private const float animSpeed = 0.15f;

	// Token: 0x040027F2 RID: 10226
	public global::UIAtlas atlas;

	// Token: 0x040027F3 RID: 10227
	public global::UIFont font;

	// Token: 0x040027F4 RID: 10228
	public global::UILabel textLabel;

	// Token: 0x040027F5 RID: 10229
	public string backgroundSprite;

	// Token: 0x040027F6 RID: 10230
	public string highlightSprite;

	// Token: 0x040027F7 RID: 10231
	public global::UIPopupList.Position position;

	// Token: 0x040027F8 RID: 10232
	public List<string> items = new List<string>();

	// Token: 0x040027F9 RID: 10233
	public Vector2 padding = new Vector3(4f, 4f);

	// Token: 0x040027FA RID: 10234
	public float textScale = 1f;

	// Token: 0x040027FB RID: 10235
	public Color textColor = Color.white;

	// Token: 0x040027FC RID: 10236
	public Color backgroundColor = Color.white;

	// Token: 0x040027FD RID: 10237
	public Color highlightColor = new Color(0.596078455f, 1f, 0.2f, 1f);

	// Token: 0x040027FE RID: 10238
	public bool isAnimated = true;

	// Token: 0x040027FF RID: 10239
	public bool isLocalized;

	// Token: 0x04002800 RID: 10240
	public GameObject eventReceiver;

	// Token: 0x04002801 RID: 10241
	public string functionName = "OnSelectionChange";

	// Token: 0x04002802 RID: 10242
	[HideInInspector]
	[SerializeField]
	private string mSelectedItem;

	// Token: 0x04002803 RID: 10243
	private global::UIPanel mPanel;

	// Token: 0x04002804 RID: 10244
	private GameObject mChild;

	// Token: 0x04002805 RID: 10245
	private global::UISprite mBackground;

	// Token: 0x04002806 RID: 10246
	private global::UISprite mHighlight;

	// Token: 0x04002807 RID: 10247
	private global::UILabel mHighlightedLabel;

	// Token: 0x04002808 RID: 10248
	private List<global::UILabel> mLabelList = new List<global::UILabel>();

	// Token: 0x04002809 RID: 10249
	private float mBgBorder;

	// Token: 0x0200085C RID: 2140
	public enum Position
	{
		// Token: 0x0400280B RID: 10251
		Auto,
		// Token: 0x0400280C RID: 10252
		Above,
		// Token: 0x0400280D RID: 10253
		Below
	}
}
