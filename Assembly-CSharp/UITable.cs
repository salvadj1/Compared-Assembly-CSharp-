using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000780 RID: 1920
[AddComponentMenu("NGUI/Interaction/Table")]
[ExecuteInEditMode]
public class UITable : MonoBehaviour
{
	// Token: 0x0600459C RID: 17820 RVA: 0x00113588 File Offset: 0x00111788
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x0600459D RID: 17821 RVA: 0x0011359C File Offset: 0x0011179C
	private void RepositionVariableSize(List<Transform> children)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = (this.columns <= 0) ? 1 : (children.Count / this.columns + 1);
		int num4 = (this.columns <= 0) ? children.Count : this.columns;
		AABBox[,] array = new AABBox[num3, num4];
		AABBox[] array2 = new AABBox[num4];
		AABBox[] array3 = new AABBox[num3];
		int num5 = 0;
		int num6 = 0;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Transform transform = children[i];
			AABBox aabbox = NGUIMath.CalculateRelativeWidgetBounds(transform);
			Vector3 localScale = transform.localScale;
			aabbox.SetMinMax(Vector3.Scale(aabbox.min, localScale), Vector3.Scale(aabbox.max, localScale));
			array[num6, num5] = aabbox;
			array2[num5].Encapsulate(aabbox);
			array3[num6].Encapsulate(aabbox);
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
			}
			i++;
		}
		num5 = 0;
		num6 = 0;
		int j = 0;
		int count2 = children.Count;
		while (j < count2)
		{
			Transform transform2 = children[j];
			AABBox aabbox2 = array[num6, num5];
			AABBox aabbox3 = array2[num5];
			AABBox aabbox4 = array3[num6];
			Vector3 localPosition = transform2.localPosition;
			Vector3 min = aabbox2.min;
			Vector3 max = aabbox2.max;
			Vector3 vector = aabbox2.size * 0.5f;
			Vector3 center = aabbox2.center;
			Vector3 min2 = aabbox4.min;
			Vector3 max2 = aabbox4.max;
			Vector3 min3 = aabbox3.min;
			localPosition.x = num + vector.x - center.x;
			localPosition.x += min.x - min3.x + this.padding.x;
			if (this.direction == UITable.Direction.Down)
			{
				localPosition.y = -num2 - vector.y - center.y;
				localPosition.y += (max.y - min.y - max2.y + min2.y) * 0.5f - this.padding.y;
			}
			else
			{
				localPosition.y = num2 + vector.y - center.y;
				localPosition.y += (max.y - min.y - max2.y + min2.y) * 0.5f - this.padding.y;
			}
			num += min3.x - min3.x + this.padding.x * 2f;
			transform2.localPosition = localPosition;
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
				num = 0f;
				num2 += vector.y * 2f + this.padding.y * 2f;
			}
			j++;
		}
	}

	// Token: 0x0600459E RID: 17822 RVA: 0x001138F0 File Offset: 0x00111AF0
	public void Reposition()
	{
		if (this.mStarted)
		{
			Transform transform = base.transform;
			List<Transform> list = new List<Transform>();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (child && (!this.hideInactive || child.gameObject.activeInHierarchy))
				{
					list.Add(child);
				}
			}
			if (this.sorted)
			{
				list.Sort(new Comparison<Transform>(UITable.SortByName));
			}
			if (list.Count > 0)
			{
				this.RepositionVariableSize(list);
			}
			if (this.mPanel != null && this.mDrag == null)
			{
				this.mPanel.ConstrainTargetToBounds(transform, true);
			}
			if (this.mDrag != null)
			{
				this.mDrag.UpdateScrollbars(true);
			}
		}
		else
		{
			this.repositionNow = true;
		}
	}

	// Token: 0x0600459F RID: 17823 RVA: 0x001139EC File Offset: 0x00111BEC
	private void Start()
	{
		this.mStarted = true;
		if (this.keepWithinPanel)
		{
			this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
			this.mDrag = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		}
		this.Reposition();
	}

	// Token: 0x060045A0 RID: 17824 RVA: 0x00113A34 File Offset: 0x00111C34
	private void LateUpdate()
	{
		if (this.repositionNow)
		{
			this.repositionNow = false;
			this.Reposition();
			if (this.onReposition != null)
			{
				this.onReposition();
			}
		}
	}

	// Token: 0x040025FC RID: 9724
	public int columns;

	// Token: 0x040025FD RID: 9725
	public UITable.Direction direction;

	// Token: 0x040025FE RID: 9726
	public Vector2 padding = Vector2.zero;

	// Token: 0x040025FF RID: 9727
	public bool sorted;

	// Token: 0x04002600 RID: 9728
	public bool hideInactive = true;

	// Token: 0x04002601 RID: 9729
	public bool repositionNow;

	// Token: 0x04002602 RID: 9730
	public bool keepWithinPanel;

	// Token: 0x04002603 RID: 9731
	public UITable.OnReposition onReposition;

	// Token: 0x04002604 RID: 9732
	private UIPanel mPanel;

	// Token: 0x04002605 RID: 9733
	private UIDraggablePanel mDrag;

	// Token: 0x04002606 RID: 9734
	private bool mStarted;

	// Token: 0x02000781 RID: 1921
	public enum Direction
	{
		// Token: 0x04002608 RID: 9736
		Down,
		// Token: 0x04002609 RID: 9737
		Up
	}

	// Token: 0x020008E6 RID: 2278
	// (Invoke) Token: 0x06004D70 RID: 19824
	public delegate void OnReposition();
}
