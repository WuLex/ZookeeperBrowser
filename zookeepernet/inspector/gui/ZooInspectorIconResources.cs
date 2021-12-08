/*
 * ZooInspector
 * 
 * Copyright 2010 Colin Goodheart-Smithe

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */
namespace org.apache.zookeeper.inspector.gui
{

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public class ZooInspectorIconResources
	{

		/// <returns> file icon </returns>
		public static ImageIcon TreeLeafIcon
		{
			get
			{
				return new ImageIcon("icons/file_obj.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> folder open icon </returns>
		public static ImageIcon TreeOpenIcon
		{
			get
			{
				return new ImageIcon("icons/fldr_obj.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> folder closed icon </returns>
		public static ImageIcon TreeClosedIcon
		{
			get
			{
				return new ImageIcon("icons/fldr_obj.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> connect icon </returns>
		public static ImageIcon ConnectIcon
		{
			get
			{
				return new ImageIcon("icons/launch_run.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> disconnect icon </returns>
		public static ImageIcon DisconnectIcon
		{
			get
			{
				return new ImageIcon("icons/launch_stop.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> save icon </returns>
		public static ImageIcon SaveIcon
		{
			get
			{
				return new ImageIcon("icons/save_edit.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> add icon </returns>
		public static ImageIcon AddNodeIcon
		{
			get
			{
				return new ImageIcon("icons/new_con.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> delete icon </returns>
		public static ImageIcon DeleteNodeIcon
		{
			get
			{
				return new ImageIcon("icons/trash.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> refresh icon </returns>
		public static ImageIcon RefreshIcon
		{
			get
			{
				return new ImageIcon("icons/refresh.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> information icon </returns>
		public static ImageIcon InformationIcon
		{
			get
			{
				return new ImageIcon("icons/info_obj.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> node viewers icon </returns>
		public static ImageIcon ChangeNodeViewersIcon
		{
			get
			{
				return new ImageIcon("icons/edtsrclkup_co.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> up icon </returns>
		public static ImageIcon UpIcon
		{
			get
			{
				return new ImageIcon("icons/search_prev.gif"); //$NON-NLS-1$
			}
		}

		/// <returns> down icon </returns>
		public static ImageIcon DownIcon
		{
			get
			{
				return new ImageIcon("icons/search_next.gif"); //$NON-NLS-1$
			}
		}
	}

}