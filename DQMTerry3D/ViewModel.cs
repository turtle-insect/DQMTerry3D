using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQMTerry3D
{
	internal class ViewModel
	{
		public String ItemFilter { get; set; } = "";
		public ObservableCollection<Item> Items { get; private set; } = new ObservableCollection<Item>();
		private List<Item> AllItems = new List<Item>();
		public ObservableCollection<Monster> Monsters { get; private set; } = new ObservableCollection<Monster>();
		public ObservableCollection<Egg> Eggs { get; private set; } = new ObservableCollection<Egg>();
		public ObservableCollection<Memory> Memorys { get; private set; } = new ObservableCollection<Memory>();

		public ViewModel()
		{
			foreach (var info in Info.Instance().Item)
			{
				AllItems.Add(new Item(260U + info.Value, info.Value));
			}
			FilterItem();

			for (uint index = 0; index < 500; index++)
			{
				Monster monster = new Monster(0x328 + index * 244);
				if (monster.Type.Value == 0) continue;

				Monsters.Add(monster);
			}

			for (uint index = 0; index < 2; index++)
			{
				Egg egg = new Egg(0x1E620 + index * 8);
				if (egg.Type == 0) continue;

				Eggs.Add(egg);
			}

			for (uint index = 0; index < 2; index++)
			{
				Memory memory = new Memory(0x30422 + index * 1336);
				Memorys.Add(memory);
			}
		}

		public void FilterItem()
		{
			Items.Clear();
			foreach (var item in AllItems)
			{
				String name = Info.Instance().Search(Info.Instance().Item, item.ID)?.Name;
				if (name?.IndexOf(ItemFilter) != -1) Items.Add(item);
			}
		}

		public uint Money
		{
			get { return SaveData.Instance().ReadNumber(0xDC, 4); }
			set { Util.WriteNumber(0xDC, 4, value, 0, 999999); }
		}

		public uint Bank
		{
			get { return SaveData.Instance().ReadNumber(0xE0, 4); }
			set { Util.WriteNumber(0xE0, 4, value, 0, 99999999); }
		}

		public uint MiniMedalHave
		{
			get { return SaveData.Instance().ReadNumber(0x1E580, 1); }
			set { Util.WriteNumber(0x1E580, 1, value, 0, 0xFF); }
		}

		public uint MiniMedalPass
		{
			get { return SaveData.Instance().ReadNumber(0x1E581, 2); }
			set { Util.WriteNumber(0x1E581, 2, value, 0, 150); }
		}

		public uint BattleWin
		{
			get { return SaveData.Instance().ReadNumber(0x318, 2); }
			set { SaveData.Instance().WriteNumber(0x318, 2, value); }
		}

		public uint Scout
		{
			get { return SaveData.Instance().ReadNumber(0x31A, 2); }
			set { SaveData.Instance().WriteNumber(0x31A, 2, value); }
		}

		public uint Mix
		{
			get { return SaveData.Instance().ReadNumber(0x31C, 2); }
			set { SaveData.Instance().WriteNumber(0x31C, 2, value); }
		}

		public uint Tournament
		{
			get { return SaveData.Instance().ReadNumber(0x1E564, 2); }
			set { SaveData.Instance().WriteNumber(0x1E564, 2, value); }
		}

		public String Name
		{
			get { return Util.ReadName(0xC0); }
		}
	}
}
