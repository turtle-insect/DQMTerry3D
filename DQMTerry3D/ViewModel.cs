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
		private ObservableCollection<Item> AllItems = new ObservableCollection<Item>();
		public ObservableCollection<Monster> Monsters { get; private set; } = new ObservableCollection<Monster>();
		public ObservableCollection<Egg> Eggs { get; private set; } = new ObservableCollection<Egg>();

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
				if (monster.Type == 0) continue;

				Monsters.Add(monster);
			}

			for (uint index = 0; index < 2; index++)
			{
				Egg egg = new Egg(0x1E620 + index * 8);
				if (egg.Type == 0) continue;

				Eggs.Add(egg);
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
	}
}
