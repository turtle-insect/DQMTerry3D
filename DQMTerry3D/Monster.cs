using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQMTerry3D
{
	internal class Monster : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		

		public Monster(uint address)
		{
			mAddress = address;
		}

		public uint Lv
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 28, 1); }
			set
			{
				Util.WriteNumber(mAddress + 28, 1, value, 1, 99);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Lv)));
			}
		}

		public uint Type
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 32, 2); }
			set
			{
				SaveData.Instance().WriteNumber(mAddress + 32, 2, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
			}
		}

		public bool Guest
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 36, 1) == 1; }
			set
			{
				SaveData.Instance().WriteNumber(mAddress + 36, 1, value ? 1U : 0);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Guest)));
			}
		}

		public uint MaxHP
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 40, 2); }
			set
			{
				Util.WriteNumber(mAddress + 40, 2, value, 1, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxHP)));
			}
		}

		public uint MaxMP
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 42, 2); }
			set
			{
				Util.WriteNumber(mAddress + 42, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxMP)));
			}
		}

		public uint HP
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 44, 2); }
			set
			{
				Util.WriteNumber(mAddress + 44, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HP)));
			}
		}

		public uint MP
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 46, 2); }
			set
			{
				Util.WriteNumber(mAddress + 46, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MP)));
			}
		}

		public uint Offense
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 48, 2); }
			set
			{
				Util.WriteNumber(mAddress + 48, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Offense)));
			}
		}

		public uint Defense
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 50, 2); }
			set
			{
				Util.WriteNumber(mAddress + 50, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Defense)));
			}
		}

		public uint Speed
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 52, 2); }
			set
			{
				Util.WriteNumber(mAddress + 52, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Speed)));
			}
		}

		public uint Wise
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 54, 2); }
			set
			{
				Util.WriteNumber(mAddress + 54, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Wise)));
			}
		}

		public uint Exp
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 64, 4); }
			set
			{
				Util.WriteNumber(mAddress + 64, 4, value, 0, 9999999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Exp)));
			}
		}

		public uint SkillPoint
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 72, 2); }
			set
			{
				Util.WriteNumber(mAddress + 72, 2, value, 0, 999);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SkillPoint)));
			}
		}

		public uint Skill1
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 222, 4); }
			set
			{
				SaveData.Instance().WriteNumber(mAddress + 222, 4, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Skill1)));
			}
		}

		public uint Skill2
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 226, 4); }
			set
			{
				SaveData.Instance().WriteNumber(mAddress + 226, 4, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Skill2)));
			}
		}

		public uint Skill3
		{
			get { return SaveData.Instance().ReadNumber(mAddress + 230, 4); }
			set
			{
				SaveData.Instance().WriteNumber(mAddress + 230, 4, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Skill3)));
			}
		}



		private readonly uint mAddress;
	}
}
