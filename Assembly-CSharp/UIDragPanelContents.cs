using System;
using UnityEngine;

// Token: 0x0200076D RID: 1901
[AddComponentMenu("NGUI/Interaction/Drag Panel Contents")]
[ExecuteInEditMode]
public class UIDragPanelContents : MonoBehaviour
{
	// Token: 0x06004512 RID: 17682 RVA: 0x0010F350 File Offset: 0x0010D550
	private void Awake()
	{
		if (this.panel != null)
		{
			if (this.draggablePanel == null)
			{
				this.draggablePanel = this.panel.GetComponent<UIDraggablePanel>();
				if (this.draggablePanel == null)
				{
					this.draggablePanel = this.panel.gameObject.AddComponent<UIDraggablePanel>();
				}
			}
			this.panel = null;
		}
	}

	// Token: 0x06004513 RID: 17683 RVA: 0x0010F3C0 File Offset: 0x0010D5C0
	private void Start()
	{
		if (this.draggablePanel == null)
		{
			this.draggablePanel = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		}
	}

	// Token: 0x06004514 RID: 17684 RVA: 0x0010F3F0 File Offset: 0x0010D5F0
	private void OnPress(bool pressed)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggablePanel != null)
		{
			this.draggablePanel.Press(pressed);
		}
	}

	// Token: 0x06004515 RID: 17685 RVA: 0x0010F438 File Offset: 0x0010D638
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggablePanel != null)
		{
			this.draggablePanel.Drag(delta);
		}
	}

	// Token: 0x06004516 RID: 17686 RVA: 0x0010F480 File Offset: 0x0010D680
	private void OnScroll(float delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggablePanel != null)
		{
			this.draggablePanel.Scroll(delta);
		}
	}

	// Token: 0x04002563 RID: 9571
	public UIDraggablePanel draggablePanel;

	// Token: 0x04002564 RID: 9572
	[HideInInspector]
	[SerializeField]
	private UIPanel panel;
}
