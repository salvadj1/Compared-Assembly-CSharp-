using System;
using UnityEngine;

// Token: 0x02000425 RID: 1061
public class RPOS_Craft_IngredientPlaque : MonoBehaviour
{
	// Token: 0x0600276A RID: 10090 RVA: 0x00099B90 File Offset: 0x00097D90
	public void Bind(BlueprintDataBlock.IngredientEntry ingredient, int needAmount, int haveAmount)
	{
		ItemDataBlock ingredient2 = ingredient.Ingredient;
		Color color;
		if (needAmount <= haveAmount)
		{
			this.checkIcon.enabled = true;
			this.xIcon.enabled = false;
			color = Color.green;
		}
		else
		{
			this.checkIcon.enabled = false;
			this.xIcon.enabled = true;
			color = Color.red;
		}
		UIWidget uiwidget = this.need;
		Color color2 = color;
		this.have.color = color2;
		uiwidget.color = color2;
		this.itemName.text = ingredient2.name;
		this.need.text = needAmount.ToString("N0");
		this.have.text = haveAmount.ToString("N0");
	}

	// Token: 0x04001368 RID: 4968
	[PrefetchChildComponent(NameMask = "ItemName")]
	public UILabel itemName;

	// Token: 0x04001369 RID: 4969
	[PrefetchChildComponent(NameMask = "NeedLabel")]
	public UILabel need;

	// Token: 0x0400136A RID: 4970
	[PrefetchChildComponent(NameMask = "HaveLabel")]
	public UILabel have;

	// Token: 0x0400136B RID: 4971
	[PrefetchChildComponent(NameMask = "checkmark")]
	public UISprite checkIcon;

	// Token: 0x0400136C RID: 4972
	[PrefetchChildComponent(NameMask = "xmark")]
	public UISprite xIcon;
}
