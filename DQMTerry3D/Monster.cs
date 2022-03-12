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
		public Number Type { get; private set; }

		public Number Skill1 { get; private set; }
		public Number Skill2 { get; private set; }
		public Number Skill3 { get; private set; }

		public Number Father { get; private set; }
		public Number Mother { get; private set; }
		public Number GrandFather_F { get; private set; }
		public Number GrandMother_F { get; private set; }
		public Number GrandFather_M { get; private set; }
		public Number GrandMother_M { get; private set; }

		public Monster(uint address)
		{
			mAddress = address;
			Type = new Number(address + 32, 2);
			Skill1 = new Number(address + 222, 4);
			Skill2 = new Number(address + 226, 4);
			Skill3 = new Number(address + 230, 4);
			Father = new Number(address + 148, 2);
			Mother = new Number(address + 150, 2);
			GrandFather_F = new Number(address + 152, 2);
			GrandMother_F = new Number(address + 156, 2);
			GrandFather_M = new Number(address + 154, 2);
			GrandMother_M = new Number(address + 158, 2);
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


		private readonly uint mAddress;
	}
}
