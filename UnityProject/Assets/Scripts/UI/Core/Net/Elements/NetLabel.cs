using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.Core.NetUI
{
	///Text label, not modifiable by clients directly
	[RequireComponent(typeof(TMP_Text))]
	[Serializable]
	public class NetLabel : NetUIStringElement
	{
		/// <summary>
		/// Invoked when the value synced between client / server is updated.
		/// </summary>
		[NonSerialized]
		public StringEvent OnSyncedValueChanged = new StringEvent();

		public override ElementMode InteractionMode => ElementMode.ServerWrite;

		public override string Value {
			get => ElementTmp != null ? ElementTmp.text : Element.text;
			set {
				externalChange = true;
				if (ElementTmp != null)
				{
					ElementTmp.text = value;
				}
				else if (Element != null)
				{
					Element.text = value;
				}

				externalChange = false;
				OnSyncedValueChanged?.Invoke(value);
			}
		}

		public Text Element => element ??= GetComponent<Text>();
		public TMP_Text ElementTmp => elementTmp ??= GetComponent<TMP_Text>();

		private Text element;
		private TMP_Text elementTmp;
	}
}
