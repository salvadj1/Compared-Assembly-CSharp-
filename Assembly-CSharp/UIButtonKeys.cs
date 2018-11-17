using System;
using UnityEngine;

// Token: 0x0200083F RID: 2111
[AddComponentMenu("NGUI/Interaction/Button Keys")]
public class UIButtonKeys : MonoBehaviour
{
	// Token: 0x06004922 RID: 18722 RVA: 0x00116DF0 File Offset: 0x00114FF0
	private void Start()
	{
		if (this.startsSelected && (global::UICamera.selectedObject == null || !global::UICamera.selectedObject.activeInHierarchy))
		{
			global::UICamera.selectedObject = base.gameObject;
		}
	}

	// Token: 0x06004923 RID: 18723 RVA: 0x00116E34 File Offset: 0x00115034
	private void OnKey(KeyCode key)
	{
		if (base.enabled && base.gameObject.activeInHierarchy)
		{
			switch (key)
			{
			case 273:
				if (this.selectOnUp != null)
				{
					global::UICamera.selectedObject = this.selectOnUp.gameObject;
				}
				break;
			case 274:
				if (this.selectOnDown != null)
				{
					global::UICamera.selectedObject = this.selectOnDown.gameObject;
				}
				break;
			case 275:
				if (this.selectOnRight != null)
				{
					global::UICamera.selectedObject = this.selectOnRight.gameObject;
				}
				break;
			case 276:
				if (this.selectOnLeft != null)
				{
					global::UICamera.selectedObject = this.selectOnLeft.gameObject;
				}
				break;
			default:
				if (key == 9)
				{
					if (Input.GetKey(304) || Input.GetKey(303))
					{
						if (this.selectOnLeft != null)
						{
							global::UICamera.selectedObject = this.selectOnLeft.gameObject;
						}
						else if (this.selectOnUp != null)
						{
							global::UICamera.selectedObject = this.selectOnUp.gameObject;
						}
						else if (this.selectOnDown != null)
						{
							global::UICamera.selectedObject = this.selectOnDown.gameObject;
						}
						else if (this.selectOnRight != null)
						{
							global::UICamera.selectedObject = this.selectOnRight.gameObject;
						}
					}
					else if (this.selectOnRight != null)
					{
						global::UICamera.selectedObject = this.selectOnRight.gameObject;
					}
					else if (this.selectOnDown != null)
					{
						global::UICamera.selectedObject = this.selectOnDown.gameObject;
					}
					else if (this.selectOnUp != null)
					{
						global::UICamera.selectedObject = this.selectOnUp.gameObject;
					}
					else if (this.selectOnRight != null)
					{
						global::UICamera.selectedObject = this.selectOnRight.gameObject;
					}
				}
				break;
			}
		}
	}

	// Token: 0x06004924 RID: 18724 RVA: 0x00117070 File Offset: 0x00115270
	private void OnClick()
	{
		if (base.enabled && this.selectOnClick != null)
		{
			global::UICamera.selectedObject = this.selectOnClick.gameObject;
		}
	}

	// Token: 0x04002727 RID: 10023
	public bool startsSelected;

	// Token: 0x04002728 RID: 10024
	public global::UIButtonKeys selectOnClick;

	// Token: 0x04002729 RID: 10025
	public global::UIButtonKeys selectOnUp;

	// Token: 0x0400272A RID: 10026
	public global::UIButtonKeys selectOnDown;

	// Token: 0x0400272B RID: 10027
	public global::UIButtonKeys selectOnLeft;

	// Token: 0x0400272C RID: 10028
	public global::UIButtonKeys selectOnRight;
}
