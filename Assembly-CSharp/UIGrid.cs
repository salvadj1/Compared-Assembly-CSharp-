using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000773 RID: 1907
[AddComponentMenu("NGUI/Interaction/Grid")]
[ExecuteInEditMode]
public class UIGrid : MonoBehaviour
{
	// Token: 0x0600454D RID: 17741 RVA: 0x00110E4C File Offset: 0x0010F04C
	private void Start()
	{
		this.mStarted = true;
		this.Reposition();
	}

	// Token: 0x0600454E RID: 17742 RVA: 0x00110E5C File Offset: 0x0010F05C
	private void Update()
	{
		if (this.repositionNow)
		{
			this.repositionNow = false;
			this.Reposition();
		}
	}

	// Token: 0x0600454F RID: 17743 RVA: 0x00110E78 File Offset: 0x0010F078
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x06004550 RID: 17744 RVA: 0x00110E8C File Offset: 0x0010F08C
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
			list.Sort(new Comparison<Transform>(UIGrid.SortByName));
			int j = 0;
			int count = list.Count;
			while (j < count)
			{
				Transform transform2 = list[j];
				if (transform2.gameObject.activeInHierarchy || !this.hideInactive)
				{
					float z = transform2.localPosition.z;
					transform2.localPosition = ((this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z));
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
					child2.localPosition = ((this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z2) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z2));
					if (++num >= this.maxPerLine && this.maxPerLine > 0)
					{
						num = 0;
						num2++;
					}
				}
			}
		}
		UIDraggablePanel uidraggablePanel = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		if (uidraggablePanel != null)
		{
			uidraggablePanel.UpdateScrollbars(true);
		}
	}

	// Token: 0x040025A2 RID: 9634
	public UIGrid.Arrangement arrangement;

	// Token: 0x040025A3 RID: 9635
	public int maxPerLine;

	// Token: 0x040025A4 RID: 9636
	public float cellWidth = 200f;

	// Token: 0x040025A5 RID: 9637
	public float cellHeight = 200f;

	// Token: 0x040025A6 RID: 9638
	public bool repositionNow;

	// Token: 0x040025A7 RID: 9639
	public bool sorted;

	// Token: 0x040025A8 RID: 9640
	public bool hideInactive = true;

	// Token: 0x040025A9 RID: 9641
	private bool mStarted;

	// Token: 0x02000774 RID: 1908
	public enum Arrangement
	{
		// Token: 0x040025AB RID: 9643
		Horizontal,
		// Token: 0x040025AC RID: 9644
		Vertical
	}
}
