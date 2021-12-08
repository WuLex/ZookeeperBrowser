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
namespace org.apache.zookeeper.inspector.manager
{

	/// <summary>
	/// @author CGSmithe
	/// 
	/// </summary>
	public interface NodeListener
	{
		/// <param name="nodePath"> </param>
		/// <param name="eventType"> </param>
		/// <param name="eventInfo"> </param>
		void processEvent(string nodePath, string eventType, IDictionary<string, string> eventInfo);
	}

}