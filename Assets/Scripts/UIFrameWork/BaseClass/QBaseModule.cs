using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QUIFrameWork
{
	public class QBaseModule : MonoBehaviour
	{
		/// <summary>
		/// Enum registe mode.
		/// </summary>
		public enum EnumRegisteMode
		{
			NotRegister,
			AutoRegister,
			AlreadyRegister
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="QUIFrameWork.QBaseModule"/> class.
		/// </summary>
		public QBaseModule()
		{
			
		}

		/// <summary>
		/// Release this instance.
		/// </summary>
		public void Release()
		{
			if (registerMode == EnumRegisteMode.AlreadyRegister) 
			{
				//unregister

				registerMode = EnumRegisteMode.AutoRegister;
			}

			OnRelease ();
		}

		/// <summary>
		/// Raises the release event.
		/// </summary>
		public virtual void OnRelease()
		{
			
		}

		/// <summary>
		/// Load this instance.
		/// </summary>
		public void Load()
		{
			if (registerMode == EnumRegisteMode.AutoRegister) 
			{
				//register

				registerMode = EnumRegisteMode.AlreadyRegister;
			}

			OnLoad ();
		}

		/// <summary>
		/// Raises the load event.
		/// </summary>
		public virtual void OnLoad()
		{
			
		}

		/// <summary>
		/// The register mode.
		/// </summary>
		private EnumRegisteMode registerMode = EnumRegisteMode.NotRegister;
		public bool AutoRegister
		{
			get
			{ 
				return registerMode == EnumRegisteMode.NotRegister ? false : true;	
			}
			set
			{
				if (registerMode == EnumRegisteMode.NotRegister || registerMode == EnumRegisteMode.AutoRegister)
					registerMode = value ? EnumRegisteMode.AutoRegister : EnumRegisteMode.NotRegister;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has register.
		/// </summary>
		/// <value><c>true</c> if this instance has register; otherwise, <c>false</c>.</value>
		public bool HasRegister
		{
			get
			{
				return registerMode == EnumRegisteMode.AlreadyRegister;
			}
		}
	}
}
