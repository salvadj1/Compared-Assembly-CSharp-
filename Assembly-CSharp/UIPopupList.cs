using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000778 RID: 1912
[AddComponentMenu("NGUI/Interaction/Popup List")]
[ExecuteInEditMode]
public class UIPopupList : MonoBehaviour
{
	// Token: 0x17000D7A RID: 3450
	// (get) Token: 0x06004559 RID: 17753 RVA: 0x00111480 File Offset: 0x0010F680
	public bool isOpen
	{
		get
		{
			return this.mChild != null;
		}
	}

	// Token: 0x17000D7B RID: 3451
	// (get) Token: 0x0600455A RID: 17754 RVA: 0x00111490 File Offset: 0x0010F690
	// (set) Token: 0x0600455B RID: 17755 RVA: 0x00111498 File Offset: 0x0010F698
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
					this.textLabel.text = ((!this.isLocalized || !(Localization.instance != null)) ? value : Localization.instance.Get(value));
				}
				if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName) && Application.isPlaying)
				{
					this.eventReceiver.SendMessage(this.functionName, this.mSelectedItem, 1);
				}
			}
		}
	}

	// Token: 0x17000D7C RID: 3452
	// (get) Token: 0x0600455C RID: 17756 RVA: 0x00111548 File Offset: 0x0010F748
	// (set) Token: 0x0600455D RID: 17757 RVA: 0x00111574 File Offset: 0x0010F774
	private bool handleEvents
	{
		get
		{
			UIButtonKeys component = base.GetComponent<UIButtonKeys>();
			return component == null || !component.enabled;
		}
		set
		{
			UIButtonKeys component = base.GetComponent<UIButtonKeys>();
			if (component != null)
			{
				component.enabled = !value;
			}
		}
	}

	// Token: 0x0600455E RID: 17758 RVA: 0x001115A0 File Offset: 0x0010F7A0
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

	// Token: 0x0600455F RID: 17759 RVA: 0x001115FC File Offset: 0x0010F7FC
	private void OnLocalize(Localization loc)
	{
		if (this.isLocalized && this.textLabel != null)
		{
			this.textLabel.text = loc.Get(this.mSelectedItem);
		}
	}

	// Token: 0x06004560 RID: 17760 RVA: 0x00111634 File Offset: 0x0010F834
	private void Highlight(UILabel lbl, bool instant)
	{
		if (this.mHighlight != null)
		{
			TweenPosition component = lbl.GetComponent<TweenPosition>();
			if (component != null && component.enabled)
			{
				return;
			}
			this.mHighlightedLabel = lbl;
			UIAtlas.Sprite sprite = this.mHighlight.sprite;
			float num = sprite.inner.xMin - sprite.outer.xMin;
			float num2 = sprite.inner.yMin - sprite.outer.yMin;
			Vector3 vector = lbl.cachedTransform.localPosition + new Vector3(-num, num2, 0f);
			if (instant || !this.isAnimated)
			{
				this.mHighlight.cachedTransform.localPosition = vector;
			}
			else
			{
				TweenPosition.Begin(this.mHighlight.gameObject, 0.1f, vector).method = UITweener.Method.EaseOut;
			}
		}
	}

	// Token: 0x06004561 RID: 17761 RVA: 0x00111718 File Offset: 0x0010F918
	private void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			this.Highlight(component, false);
		}
	}

	// Token: 0x06004562 RID: 17762 RVA: 0x0011173C File Offset: 0x0010F93C
	private void Select(UILabel lbl, bool instant)
	{
		this.Highlight(lbl, instant);
		UIEventListener component = lbl.gameObject.GetComponent<UIEventListener>();
		this.selection = (component.parameter as string);
		UIButtonSound[] components = base.GetComponents<UIButtonSound>();
		int i = 0;
		int num = components.Length;
		while (i < num)
		{
			UIButtonSound uibuttonSound = components[i];
			if (uibuttonSound.trigger == UIButtonSound.Trigger.OnClick)
			{
				NGUITools.PlaySound(uibuttonSound.audioClip, uibuttonSound.volume, 1f);
			}
			i++;
		}
	}

	// Token: 0x06004563 RID: 17763 RVA: 0x001117B8 File Offset: 0x0010F9B8
	private void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed)
		{
			this.Select(go.GetComponent<UILabel>(), true);
		}
	}

	// Token: 0x06004564 RID: 17764 RVA: 0x001117D0 File Offset: 0x0010F9D0
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

	// Token: 0x06004565 RID: 17765 RVA: 0x0011188C File Offset: 0x0010FA8C
	private void OnSelect(bool isSelected)
	{
		if (!isSelected && this.mChild != null)
		{
			this.mLabelList.Clear();
			this.handleEvents = false;
			if (this.isAnimated)
			{
				UIWidget[] componentsInChildren = this.mChild.GetComponentsInChildren<UIWidget>();
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					UIWidget uiwidget = componentsInChildren[i];
					Color color = uiwidget.color;
					color.a = 0f;
					TweenColor.Begin(uiwidget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
					i++;
				}
				NGUITools.SetAllowClickChildren(this.mChild, false);
				UpdateManager.AddDestroy(this.mChild, 0.15f);
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

	// Token: 0x06004566 RID: 17766 RVA: 0x00111960 File Offset: 0x0010FB60
	private void AnimateColor(UIWidget widget)
	{
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
	}

	// Token: 0x06004567 RID: 17767 RVA: 0x001119B0 File Offset: 0x0010FBB0
	private void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 localPosition2 = (!placeAbove) ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, bottom, localPosition.z);
		widget.cachedTransform.localPosition = localPosition2;
		GameObject gameObject = widget.gameObject;
		TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	// Token: 0x06004568 RID: 17768 RVA: 0x00111A28 File Offset: 0x0010FC28
	private void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
	{
		GameObject gameObject = widget.gameObject;
		Transform cachedTransform = widget.cachedTransform;
		float num = (float)this.font.size * this.textScale + this.mBgBorder * 2f;
		Vector3 localScale = cachedTransform.localScale;
		cachedTransform.localScale = new Vector3(localScale.x, num, localScale.z);
		TweenScale.Begin(gameObject, 0.15f, localScale).method = UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.localPosition;
			cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - localScale.y + num, localPosition.z);
			TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
		}
	}

	// Token: 0x06004569 RID: 17769 RVA: 0x00111AE4 File Offset: 0x0010FCE4
	private void Animate(UIWidget widget, bool placeAbove, float bottom)
	{
		this.AnimateColor(widget);
		this.AnimatePosition(widget, placeAbove, bottom);
	}

	// Token: 0x0600456A RID: 17770 RVA: 0x00111AF8 File Offset: 0x0010FCF8
	private void OnClick()
	{
		if (this.mChild == null && this.atlas != null && this.font != null && this.items.Count > 1)
		{
			this.mLabelList.Clear();
			this.handleEvents = true;
			if (this.mPanel == null)
			{
				this.mPanel = UIPanel.Find(base.transform, true);
			}
			Transform transform = base.transform;
			AABBox aabbox = NGUIMath.CalculateRelativeWidgetBounds(transform.parent, transform);
			this.mChild = new GameObject("Drop-down List");
			this.mChild.layer = base.gameObject.layer;
			Transform transform2 = this.mChild.transform;
			transform2.parent = transform.parent;
			transform2.localPosition = aabbox.min;
			transform2.localRotation = Quaternion.identity;
			transform2.localScale = Vector3.one;
			this.mBackground = NGUITools.AddSprite(this.mChild, this.atlas, this.backgroundSprite);
			this.mBackground.pivot = UIWidget.Pivot.TopLeft;
			this.mBackground.depth = NGUITools.CalculateNextDepth(this.mPanel.gameObject);
			this.mBackground.color = this.backgroundColor;
			Vector4 border = this.mBackground.border;
			this.mBgBorder = border.y;
			this.mBackground.cachedTransform.localPosition = new Vector3(0f, border.y, 0f);
			this.mHighlight = NGUITools.AddSprite(this.mChild, this.atlas, this.highlightSprite);
			this.mHighlight.pivot = UIWidget.Pivot.TopLeft;
			this.mHighlight.color = this.highlightColor;
			UIAtlas.Sprite sprite = this.mHighlight.sprite;
			float num = sprite.inner.yMin - sprite.outer.yMin;
			float num2 = (float)this.font.size * this.textScale;
			float num3 = 0f;
			float num4 = -this.padding.y;
			List<UILabel> list = new List<UILabel>();
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				string text = this.items[i];
				UILabel uilabel = NGUITools.AddWidget<UILabel>(this.mChild);
				uilabel.pivot = UIWidget.Pivot.TopLeft;
				uilabel.font = this.font;
				uilabel.text = ((!this.isLocalized || !(Localization.instance != null)) ? text : Localization.instance.Get(text));
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
				UIEventListener uieventListener = UIEventListener.Get(uilabel.gameObject);
				uieventListener.onHover = new UIEventListener.BoolDelegate(this.OnItemHover);
				uieventListener.onPress = new UIEventListener.BoolDelegate(this.OnItemPress);
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
				UILabel uilabel2 = list[j];
				UIHotSpot uihotSpot = NGUITools.AddWidgetHotSpot(uilabel2.gameObject);
				center.z = uihotSpot.center.z;
				uihotSpot.center = center;
				uihotSpot.size = size;
				j++;
			}
			num3 += border.x * 2f;
			num4 -= border.y;
			this.mBackground.cachedTransform.localScale = new Vector3(num3, -num4 + border.y, 1f);
			this.mHighlight.cachedTransform.localScale = new Vector3(num3 - border.x * 2f + (sprite.inner.xMin - sprite.outer.xMin) * 2f, num2 + num * 2f, 1f);
			bool flag = this.position == UIPopupList.Position.Above;
			if (this.position == UIPopupList.Position.Auto)
			{
				UICamera uicamera = UICamera.FindCameraForLayer(base.gameObject.layer);
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

	// Token: 0x040025BA RID: 9658
	private const float animSpeed = 0.15f;

	// Token: 0x040025BB RID: 9659
	public UIAtlas atlas;

	// Token: 0x040025BC RID: 9660
	public UIFont font;

	// Token: 0x040025BD RID: 9661
	public UILabel textLabel;

	// Token: 0x040025BE RID: 9662
	public string backgroundSprite;

	// Token: 0x040025BF RID: 9663
	public string highlightSprite;

	// Token: 0x040025C0 RID: 9664
	public UIPopupList.Position position;

	// Token: 0x040025C1 RID: 9665
	public List<string> items = new List<string>();

	// Token: 0x040025C2 RID: 9666
	public Vector2 padding = new Vector3(4f, 4f);

	// Token: 0x040025C3 RID: 9667
	public float textScale = 1f;

	// Token: 0x040025C4 RID: 9668
	public Color textColor = Color.white;

	// Token: 0x040025C5 RID: 9669
	public Color backgroundColor = Color.white;

	// Token: 0x040025C6 RID: 9670
	public Color highlightColor = new Color(0.596078455f, 1f, 0.2f, 1f);

	// Token: 0x040025C7 RID: 9671
	public bool isAnimated = true;

	// Token: 0x040025C8 RID: 9672
	public bool isLocalized;

	// Token: 0x040025C9 RID: 9673
	public GameObject eventReceiver;

	// Token: 0x040025CA RID: 9674
	public string functionName = "OnSelectionChange";

	// Token: 0x040025CB RID: 9675
	[HideInInspector]
	[SerializeField]
	private string mSelectedItem;

	// Token: 0x040025CC RID: 9676
	private UIPanel mPanel;

	// Token: 0x040025CD RID: 9677
	private GameObject mChild;

	// Token: 0x040025CE RID: 9678
	private UISprite mBackground;

	// Token: 0x040025CF RID: 9679
	private UISprite mHighlight;

	// Token: 0x040025D0 RID: 9680
	private UILabel mHighlightedLabel;

	// Token: 0x040025D1 RID: 9681
	private List<UILabel> mLabelList = new List<UILabel>();

	// Token: 0x040025D2 RID: 9682
	private float mBgBorder;

	// Token: 0x02000779 RID: 1913
	public enum Position
	{
		// Token: 0x040025D4 RID: 9684
		Auto,
		// Token: 0x040025D5 RID: 9685
		Above,
		// Token: 0x040025D6 RID: 9686
		Below
	}
}
