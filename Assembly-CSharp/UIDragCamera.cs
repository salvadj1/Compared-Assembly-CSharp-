using System;
using UnityEngine;

// Token: 0x0200084C RID: 2124
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
[ExecuteInEditMode]
public class UIDragCamera : global::IgnoreTimeScale
{
	// Token: 0x06004967 RID: 18791 RVA: 0x0011839C File Offset: 0x0011659C
	private void Awake()
	{
		if (this.target != null)
		{
			if (this.draggableCamera == null)
			{
				this.draggableCamera = this.target.GetComponent<global::UIDraggableCamera>();
				if (this.draggableCamera == null)
				{
					this.draggableCamera = this.target.gameObject.AddComponent<global::UIDraggableCamera>();
				}
			}
			this.target = null;
		}
		else if (this.draggableCamera == null)
		{
			this.draggableCamera = global::NGUITools.FindInParents<global::UIDraggableCamera>(base.gameObject);
		}
	}

	// Token: 0x06004968 RID: 18792 RVA: 0x00118434 File Offset: 0x00116634
	private void OnPress(bool isPressed)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggableCamera != null)
		{
			this.draggableCamera.Press(isPressed);
		}
	}

	// Token: 0x06004969 RID: 18793 RVA: 0x0011847C File Offset: 0x0011667C
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggableCamera != null)
		{
			this.draggableCamera.Drag(delta);
		}
	}

	// Token: 0x0600496A RID: 18794 RVA: 0x001184C4 File Offset: 0x001166C4
	private void OnScroll(float delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggableCamera != null)
		{
			this.draggableCamera.Scroll(delta);
		}
	}

	// Token: 0x04002785 RID: 10117
	public global::UIDraggableCamera draggableCamera;

	// Token: 0x04002786 RID: 10118
	[HideInInspector]
	[SerializeField]
	private Component target;
}
