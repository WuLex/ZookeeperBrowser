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
	/// A utility class for storing a pair of objects
	/// 
	/// @author CGSmithe
	/// </summary>
	/// @param <K> </param>
	/// @param <V> </param>
	public class Pair<K, V>
	{
		private K key;
		private V value;

		/// <param name="key"> </param>
		/// <param name="value"> </param>
		public Pair(K key, V value)
		{
			this.key = key;
			this.value = value;
		}

		/// 
		public Pair()
		{
			// Do Nothing
		}

		/// <returns> key </returns>
		public virtual K Key
		{
			get
			{
				return key;
			}
			set
			{
				this.key = value;
			}
		}


		/// <returns> value </returns>
		public virtual V Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
			}
		}


		public override string ToString()
		{
			return "Pair [" + key + ", " + value + "]";
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
			result = prime * result + ((key == null) ? 0 : key.GetHashCode());
			result = prime * result + ((value == null) ? 0 : value.GetHashCode());
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
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in C#:
//ORIGINAL LINE: Pair<?, ?> other = (Pair<?, ?>) obj;
			Pair<object, object> other = (Pair<object, object>) obj;
			if (key == null)
			{
				if (other.key != null)
				{
					return false;
				}
			}
			else if (!key.Equals(other.key))
			{
				return false;
			}
			if (value == null)
			{
				if (other.value != null)
				{
					return false;
				}
			}
			else if (!value.Equals(other.value))
			{
				return false;
			}
			return true;
		}

	}

}