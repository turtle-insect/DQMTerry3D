﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQMTerry3D
{
	internal class SaveData
	{
		private static SaveData mThis;
		private String mFileName = null;
		private Byte[] mBuffer = null;
		private readonly System.Text.Encoding mEncode = System.Text.Encoding.UTF8;
		public uint Adventure { private get; set; } = 0;

		private SaveData()
		{ }

		public static SaveData Instance()
		{
			if (mThis == null) mThis = new SaveData();
			return mThis;
		}

		public bool Open(String filename, bool force)
		{
			if (System.IO.File.Exists(filename) == false) return false;

			mBuffer = System.IO.File.ReadAllBytes(filename);
			if (force == false)
			{
				uint sum = CalcCheckSum();
				if (sum != ReadNumber(0xB4, 4))
				{
					mBuffer = null;
					return false;
				}
			}
			mFileName = filename;
			Backup();
			return true;
		}

		public bool Save()
		{
			if (mFileName == null || mBuffer == null) return false;

			uint sum = CalcCheckSum();
			WriteNumber(0xB4, 4, sum);
			System.IO.File.WriteAllBytes(mFileName, mBuffer);
			return true;
		}

		public bool SaveAs(String filename)
		{
			if (mFileName == null || mBuffer == null) return false;
			mFileName = filename;
			return Save();
		}

		public void Import(String filename)
		{
			if (mFileName == null) return;

			mBuffer = System.IO.File.ReadAllBytes(filename);
		}

		public void Export(String filename)
		{
			System.IO.File.WriteAllBytes(filename, mBuffer);
		}

		public uint ReadNumber(uint address, uint size)
		{
			if (mBuffer == null) return 0;
			address = CalcAddress(address);
			if (address + size > mBuffer.Length) return 0;
			uint result = 0;
			for (int i = 0; i < size; i++)
			{
				result += (uint)(mBuffer[address + i]) << (i * 8);
			}
			return result;
		}

		public Byte[] ReadValue(uint address, uint size)
		{
			Byte[] result = new Byte[size];
			if (mBuffer == null) return result;
			address = CalcAddress(address);
			if (address + size > mBuffer.Length) return result;
			for (int i = 0; i < size; i++)
			{
				result[i] = mBuffer[address + i];
			}
			return result;
		}

		public Byte ReadByte(uint address, bool isLow)
		{
			Byte result = 0;
			if (mBuffer == null) return result;
			address = CalcAddress(address);
			if (address > mBuffer.Length) return result;
			result = mBuffer[address];
			if (isLow == false)
			{
				result = (Byte)(result >> 4);
			}
			result &= 0x0F;

			return result;
		}

		// 0 to 7.
		public bool ReadBit(uint address, uint bit)
		{
			if (bit < 0) return false;
			if (bit > 7) return false;
			if (mBuffer == null) return false;
			address = CalcAddress(address);
			if (address > mBuffer.Length) return false;
			Byte mask = (Byte)(1 << (int)bit);
			Byte result = (Byte)(mBuffer[address] & mask);
			return result != 0;
		}

		public String ReadText(uint address, uint size)
		{
			if (mBuffer == null) return "";
			address = CalcAddress(address);
			if (address + size > mBuffer.Length) return "";

			Byte[] tmp = new Byte[size];
			for (uint i = 0; i < size; i++)
			{
				if (mBuffer[address + i] == 0) break;
				tmp[i] = mBuffer[address + i];
			}
			return mEncode.GetString(tmp).Trim('\0');
		}

		public void WriteNumber(uint address, uint size, uint value)
		{
			if (mBuffer == null) return;
			address = CalcAddress(address);
			if (address + size > mBuffer.Length) return;
			for (uint i = 0; i < size; i++)
			{
				mBuffer[address + i] = (Byte)(value & 0xFF);
				value >>= 8;
			}
		}

		public void WriteByte(uint address, bool isLow, Byte value)
		{
			if (mBuffer == null) return;
			address = CalcAddress(address);
			if (address > mBuffer.Length) return;
			Byte tmp = mBuffer[address];
			if (isLow == false)
			{
				tmp &= 0x0F;
				value = (Byte)(value << 4);
			}
			else
			{
				tmp &= 0xF0;
			}
			mBuffer[address] = (Byte)(tmp | value);
		}

		// 0 to 7.
		public void WriteBit(uint address, uint bit, bool value)
		{
			if (bit < 0) return;
			if (bit > 7) return;
			if (mBuffer == null) return;
			address = CalcAddress(address);
			if (address > mBuffer.Length) return;
			Byte mask = (Byte)(1 << (int)bit);
			if (value) mBuffer[address] = (Byte)(mBuffer[address] | mask);
			else mBuffer[address] = (Byte)(mBuffer[address] & ~mask);
		}

		public void WriteText(uint address, uint size, String value)
		{
			if (mBuffer == null) return;
			address = CalcAddress(address);
			if (address + size > mBuffer.Length) return;
			Byte[] tmp = mEncode.GetBytes(value);
			Array.Resize(ref tmp, (int)size);
			for (uint i = 0; i < size; i++)
			{
				mBuffer[address + i] = tmp[i];
			}
		}

		public void WriteValue(uint address, Byte[] buffer)
		{
			if (mBuffer == null) return;
			address = CalcAddress(address);
			if (address + buffer.Length > mBuffer.Length) return;

			for (uint i = 0; i < buffer.Length; i++)
			{
				mBuffer[address + i] = buffer[i];
			}
		}

		public void Fill(uint address, uint size, Byte number)
		{
			if (mBuffer == null) return;
			address = CalcAddress(address);
			if (address + size > mBuffer.Length) return;
			for (uint i = 0; i < size; i++)
			{
				mBuffer[address + i] = number;
			}
		}

		public void Copy(uint from, uint to, uint size)
		{
			if (mBuffer == null) return;
			from = CalcAddress(from);
			to = CalcAddress(to);
			if (from + size > mBuffer.Length) return;
			if (to + size > mBuffer.Length) return;
			for (uint i = 0; i < size; i++)
			{
				mBuffer[to + i] = mBuffer[from + i];
			}
		}

		public void Swap(uint from, uint to, uint size)
		{
			if (mBuffer == null) return;
			from = CalcAddress(from);
			to = CalcAddress(to);
			if (from + size > mBuffer.Length) return;
			if (to + size > mBuffer.Length) return;
			for (uint i = 0; i < size; i++)
			{
				Byte tmp = mBuffer[to + i];
				mBuffer[to + i] = mBuffer[from + i];
				mBuffer[from + i] = tmp;
			}
		}

		public List<uint> FindAddress(String name, uint index)
		{
			List<uint> result = new List<uint>();
			if (mBuffer == null) return result;

			for (; index < mBuffer.Length; index++)
			{
				if (mBuffer[index] != name[0]) continue;

				int len = 1;
				for (; len < name.Length; len++)
				{
					if (mBuffer[index + len] != name[len]) break;
				}
				if (len >= name.Length) result.Add(index);
				index += (uint)len;
			}
			return result;
		}

		private uint CalcCheckSum()
		{
			uint sum = 0;
			for (uint address = 0xB8; address < mBuffer.Length; address += 4)
			{
				sum += ReadNumber(address, 4);
			}

			return sum;
		}

		private uint CalcAddress(uint address)
		{
			return address + Adventure;
		}

		private void Backup()
		{
			DateTime now = DateTime.Now;
			String path = "backup";
			if (!System.IO.Directory.Exists(path))
			{
				System.IO.Directory.CreateDirectory(path);
			}
			path = System.IO.Path.Combine(path, $"{now:yyyy-MM-dd HH-mm-ss} {System.IO.Path.GetFileName(mFileName)}");
			System.IO.File.Copy(mFileName, path, true);
		}
	}
}
