using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000856 RID: 2134
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : MonoBehaviour
{
	// Token: 0x060049B2 RID: 18866 RVA: 0x0011A7CC File Offset: 0x001189CC
	private void Start()
	{
		this.mStarted = true;
		this.Reposition();
	}

	// Token: 0x060049B3 RID: 18867 RVA: 0x0011A7DC File Offset: 0x001189DC
	private void Update()
	{
		if (this.repositionNow)
		{
			this.repositionNow = false;
			this.Reposition();
		}
	}

	// Token: 0x060049B4 RID: 18868 RVA: 0x0011A7F8 File Offset: 0x001189F8
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x060049B5 RID: 18869 RVA: 0x0011A80C File Offset: 0x00118A0C
	public void Reposition()
	{
		if (!this.mStarted)
		{
			this.repositionNow = true;
			return;
		}
		Transform transform = base.transform;
		int num = 0;
		int num2 = 0;
		if (this.sorted)
		{
			List<Transform> list = new List<Transform>();
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (child)
				{
					list.Add(child);
				}
			}
			list.Sort(new Comparison<Transform>(global::UIGrid.SortByName));
			int j = 0;
			int count = list.Count;
			while (j < count)
			{
				Transform transform2 = list[j];
				if (transform2.gameObject.activeInHierarchy || !this.hideInactive)
				{
					float z = transform2.localPosition.z;
					transform2.localPosition = ((this.arrangement != global::UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z));
					if (++num >= this.maxPerLine && this.maxPerLine > 0)
					{
						num = 0;
						num2++;
					}
				}
				j++;
			}
		}
		else
		{
			for (int k = 0; k < transform.childCount; k++)
			{
				Transform child2 = transform.GetChild(k);
				if (child2.gameObject.activeInHierarchy || !this.hideInactive)
				{
					float z2 = child2.localPosition.z;
					child2.localPosition = ((this.arrangement != global::UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z2) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z2));
					if (++num >= this.maxPerLine && this.maxPerLine > 0)
					{
						num = 0;
						num2++;
					}
				}
			}
		}
		global::UIDraggablePanel uidraggablePanel = global::NGUITools.FindInParents<global::UIDraggablePanel>(base.gameObject);
		if (uidraggablePanel != null)
		{
			uidraggablePanel.UpdateScrollbars(true);
		}
	}

	// Token: 0x040027D9 RID: 10201
	public global::UIGrid.Arrangement arrangement;

	// Token: 0x040027DA RID: 10202
	public int maxPerLine;

	// Token: 0x040027DB RID: 10203
	public float cellWidth = 200f;

	// Token: 0x040027DC RID: 10204
	public float cellHeight = 200f;

	// Token: 0x040027DD RID: 10205
	public bool repositionNow;

	// Token: 0x040027DE RID: 10206
	public bool sorted;

	// Token: 0x040027DF RID: 10207
	public bool hideInactive = true;

	// Token: 0x040027E0 RID: 10208
	private bool mStarted;

	// Token: 0x02000857 RID: 2135
	public enum Arrangement
	{
		// Token: 0x040027E2 RID: 10210
		Horizontal,
		// Token: 0x040027E3 RID: 10211
		Vertical
	}
}
