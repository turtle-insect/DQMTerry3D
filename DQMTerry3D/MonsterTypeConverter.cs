using System.Globalization;
using System.Windows.Data;

namespace DQMTerry3D
{
	internal class MonsterTypeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			uint id = (uint)value;
			return Info.Instance().Search(Info.Instance().Type, id)?.Name;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
