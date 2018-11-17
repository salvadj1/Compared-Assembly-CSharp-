using System;
using UnityEngine;

// Token: 0x0200075D RID: 1885
[AddComponentMenu("NGUI/Interaction/Button Keys")]
public class UIButtonKeys : MonoBehaviour
{
	// Token: 0x060044C1 RID: 17601 RVA: 0x0010D470 File Offset: 0x0010B670
	private void Start()
	{
		if (this.startsSelected && (UICamera.selectedObject == null || !UICamera.selectedObject.activeInHierarchy))
		{
			UICamera.selectedObject = base.gameObject;
		}
	}

	// Token: 0x060044C2 RID: 17602 RVA: 0x0010D4B4 File Offset: 0x0010B6B4
	private void OnKey(KeyCode key)
	{
		if (base.enabled && base.gameObject.activeInHierarchy)
		{
			switch (key)
			{
			case 273:
				if (this.selectOnUp != null)
				{
					UICamera.selectedObject = this.selectOnUp.gameObject;
				}
				break;
			case 274:
				if (this.selectOnDown != null)
				{
					UICamera.selectedObject = this.selectOnDown.gameObject;
				}
				break;
			case 275:
				if (this.selectOnRight != null)
				{
					UICamera.selectedObject = this.selectOnRight.gameObject;
				}
				break;
			case 276:
				if (this.selectOnLeft != null)
				{
					UICamera.selectedObject = this.selectOnLeft.gameObject;
				}
				break;
			default:
				if (key == 9)
				{
					if (Input.GetKey(304) || Input.GetKey(303))
					{
						if (this.selectOnLeft != null)
						{
							UICamera.selectedObject = this.selectOnLeft.gameObject;
						}
						else if (this.selectOnUp != null)
						{
							UICamera.selectedObject = this.selectOnUp.gameObject;
						}
						else if (this.selectOnDown != null)
						{
							UICamera.selectedObject = this.selectOnDown.gameObject;
						}
						else if (this.selectOnRight != null)
						{
							UICamera.selectedObject = this.selectOnRight.gameObject;
						}
					}
					else if (this.selectOnRight != null)
					{
						UICamera.selectedObject = this.selectOnRight.gameObject;
					}
					else if (this.selectOnDown != null)
					{
						UICamera.selectedObject = this.selectOnDown.gameObject;
					}
					else if (this.selectOnUp != null)
					{
						UICamera.selectedObject = this.selectOnUp.gameObject;
					}
					else if (this.selectOnRight != null)
					{
						UICamera.selectedObject = this.selectOnRight.gameObject;
					}
				}
				break;
			}
		}
	}

	// Token: 0x060044C3 RID: 17603 RVA: 0x0010D6F0 File Offset: 0x0010B8F0
	private void OnClick()
	{
		if (base.enabled && this.selectOnClick != null)
		{
			UICamera.selectedObject = this.selectOnClick.gameObject;
		}
	}

	// Token: 0x040024F0 RID: 9456
	public bool startsSelected;

	// Token: 0x040024F1 RID: 9457
	public UIButtonKeys selectOnClick;

	// Token: 0x040024F2 RID: 9458
	public UIButtonKeys selectOnUp;

	// Token: 0x040024F3 RID: 9459
	public UIButtonKeys selectOnDown;

	// Token: 0x040024F4 RID: 9460
	public UIButtonKeys selectOnLeft;

	// Token: 0x040024F5 RID: 9461
	public UIButtonKeys selectOnRight;
}
