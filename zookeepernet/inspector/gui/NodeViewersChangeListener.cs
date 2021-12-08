using System.Collections.Generic;

/// 
namespace org.apache.zookeeper.inspector.gui
{

	using ZooInspectorNodeViewer = org.apache.zookeeper.inspector.gui.nodeviewer.ZooInspectorNodeViewer;

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public interface NodeViewersChangeListener
	{
		/// <param name="newViewers"> </param>
		void nodeViewersChanged(IList<ZooInspectorNodeViewer> newViewers);
	}

}