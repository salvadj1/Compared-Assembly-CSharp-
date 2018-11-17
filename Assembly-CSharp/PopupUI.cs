using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003F4 RID: 1012
public class PopupUI : MonoBehaviour
{
	// Token: 0x0600254F RID: 9551 RVA: 0x0008F47C File Offset: 0x0008D67C
	private void Start()
	{
		PopupUI.singleton = this;
		this.panelLocal = base.GetComponent<dfPanel>();
	}

	// Token: 0x06002550 RID: 9552 RVA: 0x0008F490 File Offset: 0x0008D690
	public IEnumerator DoTests()
	{
		this.CreateNotice(10f, "", "You've woken up from 24 days of unconsciousness.");
		yield return new WaitForSeconds(1f);
		this.CreateNotice(3f, "", "ONE");
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(1f);
		this.CreateNotice(3f, "", "You TWO.");
		yield return new WaitForSeconds(1f);
		this.CreateNotice(3f, "", "TGHREEEE wank.");
		yield return new WaitForSeconds(1f);
		this.CreateInventory("10 x Wood");
		this.CreateNotice(3f, "", "FOUR wank.");
		yield return new WaitForSeconds(1f);
		this.CreateNotice(3f, "", "FIVE wank.");
		yield return new WaitForSeconds(1f);
		this.CreateInventory("1 x Rock");
		yield return new WaitForSeconds(0.2f);
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(1.2f);
		this.CreateInventory("7 x Rock");
		yield return new WaitForSeconds(1.2f);
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(1.3f);
		this.CreateInventory("1 x Rock");
		yield return new WaitForSeconds(0.1f);
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(0.7f);
		this.CreateInventory("7 x Rock");
		yield return new WaitForSeconds(0.3f);
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(2.4f);
		this.CreateInventory("1 x Rock");
		yield return new WaitForSeconds(0.5f);
		this.CreateInventory("10 x Wood");
		yield return new WaitForSeconds(0.3f);
		this.CreateInventory("7 x Rock");
		yield return new WaitForSeconds(0.3f);
		this.CreateNotice(3f, "", "Big sweaty testicles");
		yield return new WaitForSeconds(1f);
		this.CreateNotice(3f, "", "Dry testicles");
		yield break;
	}

	// Token: 0x06002551 RID: 9553 RVA: 0x0008F4AC File Offset: 0x0008D6AC
	public void CreateNotice(float fSeconds, string strIcon, string strText)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(this.prefabNotice);
		this.panelLocal.AddControl(gameObject.GetComponent<dfPanel>());
		PopupNotice component = gameObject.GetComponent<PopupNotice>();
		component.Setup(fSeconds, strIcon, strText);
	}

	// Token: 0x06002552 RID: 9554 RVA: 0x0008F4EC File Offset: 0x0008D6EC
	public void CreateInventory(string strText)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(this.prefabInventory);
		this.panelLocal.AddControl(gameObject.GetComponent<dfPanel>());
		PopupInventory component = gameObject.GetComponent<PopupInventory>();
		component.Setup(1.5f, strText);
	}

	// Token: 0x04001216 RID: 4630
	public static PopupUI singleton;

	// Token: 0x04001217 RID: 4631
	public Object prefabNotice;

	// Token: 0x04001218 RID: 4632
	public Object prefabInventory;

	// Token: 0x04001219 RID: 4633
	protected dfPanel panelLocal;
}
