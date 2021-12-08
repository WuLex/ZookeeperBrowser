using System.Collections.Generic;

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
namespace org.apache.zookeeper.inspector.gui.nodeviewer
{

	using ZooInspectorNodeManager = org.apache.zookeeper.inspector.manager.ZooInspectorNodeManager;

	/// 
	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public abstract class ZooInspectorNodeViewer : JPanel, Transferable
	{
		/// <summary>
		/// The <seealso cref="DataFlavor"/> used for DnD in the node viewer configuration
		/// dialog
		/// </summary>
		public static readonly DataFlavor nodeViewerDataFlavor = new DataFlavor(typeof(ZooInspectorNodeViewer), "nodeviewer");

		/// <param name="zooInspectorManager"> </param>
		public abstract ZooInspectorNodeManager ZooInspectorManager {set;}

		/// <summary>
		/// Called whenever the selected nodes in the tree view changes.
		/// </summary>
		/// <param name="selectedNodes">
		///            - the nodes currently selected in the tree view
		///  </param>
		public abstract void nodeSelectionChanged(IList<string> selectedNodes);

		/// <returns> the title of the node viewer. this will be shown on the tab for
		///         this node viewer. </returns>
		public abstract string Title {get;}

		/*
		 * (non-Javadoc)
		 * 
		 * @see
		 * java.awt.datatransfer.Transferable#getTransferData(java.awt.datatransfer
		 * .DataFlavor)
		 */
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public Object getTransferData(java.awt.datatransfer.DataFlavor flavor) throws UnsupportedFlavorException, java.io.IOException
		public virtual object getTransferData(DataFlavor flavor)
		{
			if (flavor.Equals(nodeViewerDataFlavor))
			{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getCanonicalName method:
				return this.GetType().FullName;
			}
			else
			{
				return null;
			}
		}

		/*
		 * (non-Javadoc)
		 * 
		 * @see java.awt.datatransfer.Transferable#getTransferDataFlavors()
		 */
		public virtual DataFlavor[] TransferDataFlavors
		{
			get
			{
				return new DataFlavor[] {nodeViewerDataFlavor};
			}
		}

		/*
		 * (non-Javadoc)
		 * 
		 * @seejava.awt.datatransfer.Transferable#isDataFlavorSupported(java.awt.
		 * datatransfer.DataFlavor)
		 */
		public virtual bool isDataFlavorSupported(DataFlavor flavor)
		{
			return flavor.Equals(nodeViewerDataFlavor);
		}

		/*
		 * (non-Javadoc)
		 * 
		 * @see java.lang.Object#hashCode()
		 */
		public override int GetHashCode()
		{
			const int prime = 31;
			int result = 1;
			result = prime * result + ((string.ReferenceEquals(Title, null)) ? 0 : Title.GetHashCode());
			return result;
		}

		/*
		 * (non-Javadoc)
		 * 
		 * @see java.lang.Object#equals(java.lang.Object)
		 */
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (this.GetType() != obj.GetType())
			{
				return false;
			}
			ZooInspectorNodeViewer other = (ZooInspectorNodeViewer) obj;
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getCanonicalName method:
			if (!string.ReferenceEquals(this.GetType().FullName, other.GetType().FullName))
			{
				return false;
			}
			if (string.ReferenceEquals(Title, null))
			{
				if (!string.ReferenceEquals(other.Title, null))
				{
					return false;
				}
			}
			else if (!Title.Equals(other.Title))
			{
				return false;
			}
			return true;
		}
	}

}