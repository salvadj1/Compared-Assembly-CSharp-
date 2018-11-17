using System;
using UnityEngine;

// Token: 0x0200076A RID: 1898
[AddComponentMenu("NGUI/Interaction/Drag Camera")]
[ExecuteInEditMode]
public class UIDragCamera : IgnoreTimeScale
{
	// Token: 0x06004506 RID: 17670 RVA: 0x0010EA1C File Offset: 0x0010CC1C
	private void Awake()
	{
		if (this.target != null)
		{
			if (this.draggableCamera == null)
			{
				this.draggableCamera = this.target.GetComponent<UIDraggableCamera>();
				if (this.draggableCamera == null)
				{
					this.draggableCamera = this.target.gameObject.AddComponent<UIDraggableCamera>();
				}
			}
			this.target = null;
		}
		else if (this.draggableCamera == null)
		{
			this.draggableCamera = NGUITools.FindInParents<UIDraggableCamera>(base.gameObject);
		}
	}

	// Token: 0x06004507 RID: 17671 RVA: 0x0010EAB4 File Offset: 0x0010CCB4
	private void OnPress(bool isPressed)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggableCamera != null)
		{
			this.draggableCamera.Press(isPressed);
		}
	}

	// Token: 0x06004508 RID: 17672 RVA: 0x0010EAFC File Offset: 0x0010CCFC
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggableCamera != null)
		{
			this.draggableCamera.Drag(delta);
		}
	}

	// Token: 0x06004509 RID: 17673 RVA: 0x0010EB44 File Offset: 0x0010CD44
	private void OnScroll(float delta)
	{
		if (base.enabled && base.gameObject.activeInHierarchy && this.draggableCamera != null)
		{
			this.draggableCamera.Scroll(delta);
		}
	}

	// Token: 0x0400254E RID: 9550
	public UIDraggableCamera draggableCamera;

	// Token: 0x0400254F RID: 9551
	[HideInInspector]
	[SerializeField]
	private Component target;
}
