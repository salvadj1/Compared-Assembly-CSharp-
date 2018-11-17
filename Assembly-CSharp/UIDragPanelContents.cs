using System;
using UnityEngine;

// Token: 0x0200084F RID: 2127
[AddComponentMenu("NGUI/Interaction/Drag Panel Contents")]
[ExecuteInEditMode]
public class UIDragPanelContents : MonoBehaviour
{
	// Token: 0x06004973 RID: 18803 RVA: 0x00118CD0 File Offset: 0x00116ED0
	private void Awake()
	{
		if (this.panel != null)
		{
			if (this.draggablePanel == null)
			{
				this.draggablePanel = this.panel.GetComponent<global::UIDraggablePanel>();
				if (this.draggablePanel == null)
				{
					this.draggablePanel = this.panel.gameObject.AddComponent<global::UIDraggablePanel>();
				}
			}
			this.panel = null;
		}
	}

	// Token: 0x06004974 RID: 18804 RVA: 0x00118D40 File Offset: 0x00116F40
	private void Start()
	{
		if (this.draggablePanel == null)
		{
			this.draggablePanel = global::NGUITools.FindInParents<global::UIDraggablePanel>(base.gameObject);
		}
	}

	// Token: 0x06004975 RID: 18805 RVA: 0x00118D70 File Offset: 0x00116F70
	private void OnPress(bool pressed)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggablePanel != null)
		{
			this.draggablePanel.Press(pressed);
		}
	}

	// Token: 0x06004976 RID: 18806 RVA: 0x00118DB8 File Offset: 0x00116FB8
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggablePanel != null)
		{
			this.draggablePanel.Drag(delta);
		}
	}

	// Token: 0x06004977 RID: 18807 RVA: 0x00118E00 File Offset: 0x00117000
	private void OnScroll(float delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggablePanel != null)
		{
			this.draggablePanel.Scroll(delta);
		}
	}

	// Token: 0x0400279A RID: 10138
	public global::UIDraggablePanel draggablePanel;

	// Token: 0x0400279B RID: 10139
	[HideInInspector]
	[SerializeField]
	private global::UIPanel panel;
}
