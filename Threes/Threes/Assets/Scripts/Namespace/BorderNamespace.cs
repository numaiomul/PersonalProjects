namespace BorderTools {
	public struct DirectionConstants {
	//0 - noBorder, 1-left, 2-right, 4-up, 8-down
		public const byte LEFT = 1;
		public const byte RIGHT = 2;
		public const byte UP = 4;
		public const byte DOWN = 8;
	}
	public class Border {
		public byte value {get;private set;}
		
		public Border() {
			value = 0;
		}
		
		/// <summary>
		/// Add borded to the param position
		/// </summary>
		/// <param name="_border">Position of border.</param>
		public void Add(byte _border) {
			if (!Has(_border)) value += _border;
		}
		
		/// <summary>
		/// Determines whether this instance has a border at the specified position
		/// </summary>
		/// <returns><c>true</c> if this instance has border; otherwise, <c>false</c>.</returns>
		/// <param name="_border">Position of border.</param>
		public bool Has(byte _border) {
			if ((_border & value) == _border) {
				return true;
			}
			return false;
		}
		/// <summary>
		/// Remove the specified border.
		/// </summary>
		/// <param name="_border">Position of border.</param>
		public void Remove(byte _border) {
			if (Has(_border)) value -= _border;
		}
	}
}
