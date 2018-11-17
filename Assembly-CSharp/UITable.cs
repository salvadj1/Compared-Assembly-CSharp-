using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000864 RID: 2148
[AddComponentMenu("NGUI/Interaction/Table")]
[ExecuteInEditMode]
public class UITable : MonoBehaviour
{
	// Token: 0x06004A05 RID: 18949 RVA: 0x0011CF08 File Offset: 0x0011B108
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x06004A06 RID: 18950 RVA: 0x0011CF1C File Offset: 0x0011B11C
	private void RepositionVariableSize(List<Transform> children)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = (this.columns <= 0) ? 1 : (children.Count / this.columns + 1);
		int num4 = (this.columns <= 0) ? children.Count : this.columns;
		global::AABBox[,] array = new global::AABBox[num3, num4];
		global::AABBox[] array2 = new global::AABBox[num4];
		global::AABBox[] array3 = new global::AABBox[num3];
		int num5 = 0;
		int num6 = 0;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Transform transform = children[i];
			global::AABBox aabbox = global::NGUIMath.CalculateRelativeWidgetBounds(transform);
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
			global::AABBox aabbox2 = array[num6, num5];
			global::AABBox aabbox3 = array2[num5];
			global::AABBox aabbox4 = array3[num6];
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
			if (this.direction == global::UITable.Direction.Down)
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

	// Token: 0x06004A07 RID: 18951 RVA: 0x0011D270 File Offset: 0x0011B470
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
				list.Sort(new Comparison<Transform>(global::UITable.SortByName));
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

	// Token: 0x06004A08 RID: 18952 RVA: 0x0011D36C File Offset: 0x0011B56C
	private void Start()
	{
		this.mStarted = true;
		if (this.keepWithinPanel)
		{
			this.mPanel = global::NGUITools.FindInParents<global::UIPanel>(base.gameObject);
			this.mDrag = global::NGUITools.FindInParents<global::UIDraggablePanel>(base.gameObject);
		}
		this.Reposition();
	}

	// Token: 0x06004A09 RID: 18953 RVA: 0x0011D3B4 File Offset: 0x0011B5B4
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

	// Token: 0x04002833 RID: 10291
	public int columns;

	// Token: 0x04002834 RID: 10292
	public global::UITable.Direction direction;

	// Token: 0x04002835 RID: 10293
	public Vector2 padding = Vector2.zero;

	// Token: 0x04002836 RID: 10294
	public bool sorted;

	// Token: 0x04002837 RID: 10295
	public bool hideInactive = true;

	// Token: 0x04002838 RID: 10296
	public bool repositionNow;

	// Token: 0x04002839 RID: 10297
	public bool keepWithinPanel;

	// Token: 0x0400283A RID: 10298
	public global::UITable.OnReposition onReposition;

	// Token: 0x0400283B RID: 10299
	private global::UIPanel mPanel;

	// Token: 0x0400283C RID: 10300
	private global::UIDraggablePanel mDrag;

	// Token: 0x0400283D RID: 10301
	private bool mStarted;

	// Token: 0x02000865 RID: 2149
	public enum Direction
	{
		// Token: 0x0400283F RID: 10303
		Down,
		// Token: 0x04002840 RID: 10304
		Up
	}

	// Token: 0x02000866 RID: 2150
	// (Invoke) Token: 0x06004A0B RID: 18955
	public delegate void OnReposition();
}
